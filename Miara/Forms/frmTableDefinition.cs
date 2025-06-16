using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Miara.Forms
{
    public partial class TableDefinition : Form
    {
        public TableDefinition()
        {
            InitializeComponent();
        }

        private static readonly string configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config.xml");
        private string connectionString;
        private const string XmlPath = @"C:\Users\TSHEP\source\repos\Miara\Miara\TableDefinition.xml";

        private void LoadSQLConnectionInfo()
        {
            if (!File.Exists(configFile))
            {
                MessageBox.Show("Connection configuration file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var serializer = new XmlSerializer(typeof(LoginInfo));
                using (var fileStream = new FileStream(configFile, FileMode.Open))
                {
                    var loginInfo = (LoginInfo)serializer.Deserialize(fileStream);
                    connectionString = $"Data Source={loginInfo.DataSource};Initial Catalog={loginInfo.SelectedDatabase};User ID={loginInfo.Username};Password={loginInfo.Password}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load connection information: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TableDefinition_Load(object sender, EventArgs e)
        {
            LoadSQLConnectionInfo();
            btnCheck.Enabled = !string.IsNullOrEmpty(connectionString);
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (!ValidateDatabaseConnection())
                return;

            dataGridView1.Rows.Clear();

            if (!File.Exists(XmlPath))
            {
                LogToGrid("Unknown", "❌ XML file not found");
                return;
            }

            try
            {
                ProcessTableDefinitions();
                InsertSampleData();
                CreateSendReceiptEmailStoredProcedure();
            }
            catch (Exception ex)
            {
                LogToGrid("System", $"❌ Fatal Error: {ex.Message}");
            }
        }

        private bool ValidateDatabaseConnection()
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                MessageBox.Show("Database connection is not configured.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void ProcessTableDefinitions()
        {
            var doc = XDocument.Load(XmlPath);
            var tables = doc.Descendants("Table");

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                foreach (var table in tables)
                {
                    ProcessTable(conn, table);
                }
            }
        }

        private void ProcessTable(SqlConnection conn, XElement table)
        {
            string tableName = table.Attribute("name")?.Value;
            string schema = table.Attribute("schema")?.Value ?? "dbo";

            if (string.IsNullOrEmpty(tableName))
            {
                LogToGrid("Unknown", "❌ Table name missing in XML");
                return;
            }

            try
            {
                if (!TableExists(conn, schema, tableName))
                {
                    CreateTable(conn, table, schema, tableName);
                    LogToGrid(tableName, "✅ Table created successfully");
                }
                else
                {
                    CheckAndAlterColumns(conn, table, schema, tableName);
                    LogToGrid(tableName, "✅ Table exists and columns checked/updated");
                }
                ApplyConstraints(conn, table, schema, tableName);
            }
            catch (Exception ex)
            {
                LogToGrid(tableName ?? "Unknown", $"❌ Error processing table: {ex.Message}");
            }
        }

        private bool TableExists(SqlConnection conn, string schema, string tableName)
        {
            const string checkTableQuery = @"
                SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES
                WHERE TABLE_SCHEMA = @schema AND TABLE_NAME = @tableName";

            using (var checkCmd = new SqlCommand(checkTableQuery, conn))
            {
                checkCmd.Parameters.AddWithValue("@schema", schema);
                checkCmd.Parameters.AddWithValue("@tableName", tableName);
                return (int)checkCmd.ExecuteScalar() > 0;
            }
        }

        private void CreateTable(SqlConnection conn, XElement table, string schema, string tableName)
        {
            string createQuery = BuildCreateTableSql(table, schema, tableName);
            using (var createCmd = new SqlCommand(createQuery, conn))
            {
                createCmd.ExecuteNonQuery();
            }
        }

        private string BuildCreateTableSql(XElement table, string schema, string tableName)
        {
            var columns = table.Elements("Column")
                .Select(col =>
                {
                    string name = col.Attribute("name")?.Value ?? throw new ArgumentException("Column name is missing");
                    string type = col.Attribute("type")?.Value ?? throw new ArgumentException($"Type is missing for column {name}");

                    string length = col.Attribute("length")?.Value;
                    string precision = col.Attribute("precision")?.Value;
                    string scale = col.Attribute("scale")?.Value;
                    string isNullable = col.Attribute("isNullable")?.Value;
                    string isIdentity = col.Attribute("isIdentity")?.Value;

                    string sqlType = type;

                    if (!string.IsNullOrEmpty(precision) && !string.IsNullOrEmpty(scale))
                    {
                        sqlType += $"({precision},{scale})";
                    }
                    else if (!string.IsNullOrEmpty(length))
                    {
                        sqlType += $"({length})";
                    }

                    string nullStr = (isNullable == "false") ? "NOT NULL" : "NULL";
                    string identityStr = (isIdentity == "true") ? "IDENTITY(1,1)" : "";

                    return $"[{name}] {sqlType} {identityStr} {nullStr}".Trim();
                });

            string createQuery = $"CREATE TABLE [{schema}].[{tableName}] (\n    {string.Join(",\n    ", columns)}";

            // Add Primary Key if exists
            var pk = table.Element("PrimaryKey");
            if (pk != null)
            {
                var pkCols = pk.Elements("Column").Select(c => $"[{c.Attribute("name")?.Value}]");
                if (pkCols.Any())
                {
                    createQuery += $",\n    CONSTRAINT [PK_{tableName}] PRIMARY KEY CLUSTERED ({string.Join(", ", pkCols)})";
                }
            }

            createQuery += "\n);";
            return createQuery;
        }

        private void CheckAndAlterColumns(SqlConnection conn, XElement table, string schema, string tableName)
        {
            // Get existing columns with data types from database
            var existingColumns = GetExistingColumns(conn, schema, tableName);
            bool tableHasData = TableHasData(conn, schema, tableName);

            foreach (var col in table.Elements("Column"))
            {
                string colName = col.Attribute("name")?.Value;
                string type = col.Attribute("type")?.Value;
                string length = col.Attribute("length")?.Value;
                string precisionStr = col.Attribute("precision")?.Value;
                string scaleStr = col.Attribute("scale")?.Value;
                string isNullable = col.Attribute("isNullable")?.Value ?? "true";
                string isIdentity = col.Attribute("isIdentity")?.Value;
                string defaultValue = col.Attribute("defaultValue")?.Value;

                bool colExists = existingColumns.ContainsKey(colName);

                if (!colExists)
                {
                    // Add missing column with special handling for non-empty tables
                    AddMissingColumn(conn, tableName, schema, col, tableHasData);
                }
                else
                {
                    // Check if datatype or nullability differ - alter column if needed
                    CheckAndAlterExistingColumn(conn, tableName, schema, col, existingColumns[colName]);
                }
            }
        }

        private bool TableHasData(SqlConnection conn, string schema, string tableName)
        {
            string checkDataQuery = $"SELECT COUNT(*) FROM [{schema}].[{tableName}]";
            using (SqlCommand cmd = new SqlCommand(checkDataQuery, conn))
            {
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        private void AddMissingColumn(SqlConnection conn, string tableName, string schema, XElement col, bool tableHasData)
        {
            string colName = col.Attribute("name")?.Value;
            string type = col.Attribute("type")?.Value;
            string length = col.Attribute("length")?.Value;
            string precisionStr = col.Attribute("precision")?.Value;
            string scaleStr = col.Attribute("scale")?.Value;
            string isNullable = col.Attribute("isNullable")?.Value ?? "true";
            string isIdentity = col.Attribute("isIdentity")?.Value;
            string defaultValue = col.Attribute("defaultValue")?.Value;

            if (isIdentity == "true")
            {
                LogToGrid(tableName, $"❌ Skipping addition of IDENTITY column [{colName}] (manual intervention needed)");
                return;
            }

            string sqlType = type;
            if (!string.IsNullOrEmpty(precisionStr) && !string.IsNullOrEmpty(scaleStr))
                sqlType += $"({precisionStr},{scaleStr})";
            else if (!string.IsNullOrEmpty(length))
                sqlType += $"({length})";

            // Handle non-nullable columns for tables with data
            if (tableHasData && isNullable == "false" && string.IsNullOrEmpty(defaultValue))
            {
                // Option 1: Make the column nullable
                LogToGrid(tableName, $"✅ Table has data - making column [{colName}] nullable (was NOT NULL)");
                isNullable = "true";

                // Option 2: Alternatively, you could provide a default value
                // defaultValue = GetDefaultValueForType(type);
            }

            string nullStr = (isNullable == "false") ? "NOT NULL" : "NULL";
            string defaultStr = !string.IsNullOrEmpty(defaultValue) ? $"DEFAULT {defaultValue}" : "";

            string addColumnSql = $"ALTER TABLE [{schema}].[{tableName}] ADD [{colName}] {sqlType} {nullStr} {defaultStr};";

            try
            {
                using (SqlCommand addCmd = new SqlCommand(addColumnSql, conn))
                {
                    addCmd.ExecuteNonQuery();
                    LogToGrid(tableName, $"✅ Added column [{colName}]");
                }
            }
            catch (SqlException ex) when (ex.Number == 4901) // ALTER TABLE only allows...
            {
                LogToGrid(tableName, $"❌ Failed to add non-nullable column [{colName}] to non-empty table. Consider:");
                LogToGrid(tableName, $"❌ 1. Making the column nullable (add isNullable='true')");
                LogToGrid(tableName, $"❌ 2. Providing a default value (add defaultValue='...')");
                LogToGrid(tableName, $"❌ 3. Emptying the table first");
            }
        }

        private string GetDefaultValueForType(string type)
        {
            switch (type.ToLower())
            {
                case "int":
                case "bigint":
                case "smallint":
                case "tinyint":
                case "decimal":
                case "numeric":
                case "float":
                case "real":
                    return "0";
                case "bit":
                    return "0";
                case "datetime":
                case "datetime2":
                case "date":
                case "smalldatetime":
                    return "'1900-01-01'";
                case "char":
                case "varchar":
                case "nchar":
                case "nvarchar":
                case "text":
                case "ntext":
                    return "''";
                case "uniqueidentifier":
                    return "'00000000-0000-0000-0000-000000000000'";
                default:
                    return "NULL";
            }
        }

        private Dictionary<string, (string dataType, int? charMaxLength, byte? precision, int? scale, string isNullable)>
            GetExistingColumns(SqlConnection conn, string schema, string tableName)
        {
            const string colQuery = @"
                SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, NUMERIC_PRECISION, NUMERIC_SCALE, IS_NULLABLE
                FROM INFORMATION_SCHEMA.COLUMNS
                WHERE TABLE_SCHEMA = @schema AND TABLE_NAME = @tableName";

            var existingColumns = new Dictionary<string, (string, int?, byte?, int?, string)>(StringComparer.OrdinalIgnoreCase);

            using (var colCmd = new SqlCommand(colQuery, conn))
            {
                colCmd.Parameters.AddWithValue("@schema", schema);
                colCmd.Parameters.AddWithValue("@tableName", tableName);

                using (var reader = colCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string colName = reader.GetString(0);
                        string dataType = reader.GetString(1);
                        int? charMaxLength = reader.IsDBNull(2) ? null : (int?)reader.GetInt32(2);
                        byte? precision = reader.IsDBNull(3) ? null : (byte?)reader.GetByte(3);
                        int? scale = reader.IsDBNull(4) ? null : (int?)reader.GetInt32(4);
                        string isNullable = reader.GetString(5);

                        existingColumns[colName] = (dataType, charMaxLength, precision, scale, isNullable);
                    }
                }
            }
            return existingColumns;
        }

        private void ProcessColumn(SqlConnection conn, string tableName, string schema, XElement col,
            Dictionary<string, (string dataType, int? charMaxLength, byte? precision, int? scale, string isNullable)> existingColumns)
        {
            string colName = col.Attribute("name")?.Value;
            if (string.IsNullOrEmpty(colName))
            {
                LogToGrid(tableName, "Skipping column with missing name");
                return;
            }

            if (!existingColumns.ContainsKey(colName))
            {
                AddMissingColumn(conn, tableName, schema, col);
                return;
            }

            CheckAndAlterExistingColumn(conn, tableName, schema, col, existingColumns[colName]);
        }

        private void AddMissingColumn(SqlConnection conn, string tableName, string schema, XElement col)
        {
            string colName = col.Attribute("name")?.Value;
            string type = col.Attribute("type")?.Value;
            string length = col.Attribute("length")?.Value;
            string precisionStr = col.Attribute("precision")?.Value;
            string scaleStr = col.Attribute("scale")?.Value;
            string isNullable = col.Attribute("isNullable")?.Value ?? "true";
            string isIdentity = col.Attribute("isIdentity")?.Value;

            if (isIdentity == "true")
            {
                LogToGrid(tableName, $"❌ Skipping addition of IDENTITY column [{colName}] (manual intervention needed)");
                return;
            }

            string sqlType = type;
            if (!string.IsNullOrEmpty(precisionStr) && !string.IsNullOrEmpty(scaleStr))
                sqlType += $"({precisionStr},{scaleStr})";
            else if (!string.IsNullOrEmpty(length))
                sqlType += $"({length})";

            string nullStr = (isNullable == "false") ? "NOT NULL" : "NULL";
            string addColumnSql = $"ALTER TABLE [{schema}].[{tableName}] ADD [{colName}] {sqlType} {nullStr};";

            using (var addCmd = new SqlCommand(addColumnSql, conn))
            {
                addCmd.ExecuteNonQuery();
                LogToGrid(tableName, $"✅ Added missing column [{colName}]");
            }
        }

        private void CheckAndAlterExistingColumn(SqlConnection conn, string tableName, string schema, XElement col,
            (string dataType, int? charMaxLength, byte? precision, int? scale, string isNullable) existingCol)
        {
            string colName = col.Attribute("name")?.Value;
            string type = col.Attribute("type")?.Value;
            string length = col.Attribute("length")?.Value;
            string precisionStr = col.Attribute("precision")?.Value;
            string scaleStr = col.Attribute("scale")?.Value;
            string isNullable = col.Attribute("isNullable")?.Value ?? "true";
            string isIdentity = col.Attribute("isIdentity")?.Value;

            string existingTypeWithLength = BuildExistingTypeString(existingCol);
            string desiredType = BuildDesiredTypeString(type, length, precisionStr, scaleStr);

            bool typeMismatch = !string.Equals(existingTypeWithLength, desiredType, StringComparison.OrdinalIgnoreCase);
            bool nullabilityMismatch = (isNullable == "false" && existingCol.isNullable == "YES") ||
                                       (isNullable == "true" && existingCol.isNullable == "NO");

            if (typeMismatch || nullabilityMismatch)
            {
                if (isIdentity == "true")
                {
                    LogToGrid(tableName, $"❌ Skipping alteration of IDENTITY column [{colName}] (manual intervention needed)");
                    return;
                }

                string nullStr = (isNullable == "false") ? "NOT NULL" : "NULL";
                string alterSql = $"ALTER TABLE [{schema}].[{tableName}] ALTER COLUMN [{colName}] {desiredType} {nullStr};";

                using (var alterCmd = new SqlCommand(alterSql, conn))
                {
                    alterCmd.ExecuteNonQuery();
                    LogToGrid(tableName, $"✅ Altered column [{colName}] (type or nullability)");
                }
            }
        }

        private string BuildExistingTypeString((string dataType, int? charMaxLength, byte? precision, int? scale, string isNullable) existingCol)
        {
            string existingTypeWithLength = existingCol.dataType.ToLower();

            if (existingCol.dataType.Equals("varchar", StringComparison.OrdinalIgnoreCase) ||
                existingCol.dataType.Equals("nvarchar", StringComparison.OrdinalIgnoreCase) ||
                existingCol.dataType.Equals("char", StringComparison.OrdinalIgnoreCase) ||
                existingCol.dataType.Equals("nchar", StringComparison.OrdinalIgnoreCase))
            {
                existingTypeWithLength += existingCol.charMaxLength == -1 ? "(max)" : $"({existingCol.charMaxLength})";
            }
            else if (existingCol.dataType.Equals("decimal", StringComparison.OrdinalIgnoreCase) ||
                     existingCol.dataType.Equals("numeric", StringComparison.OrdinalIgnoreCase))
            {
                existingTypeWithLength += $"({existingCol.precision},{existingCol.scale})";
            }

            return existingTypeWithLength;
        }

        private string BuildDesiredTypeString(string type, string length, string precisionStr, string scaleStr)
        {
            string desiredType = type.ToLower();
            if (!string.IsNullOrEmpty(precisionStr) && !string.IsNullOrEmpty(scaleStr))
            {
                desiredType += $"({precisionStr},{scaleStr})";
            }
            else if (!string.IsNullOrEmpty(length))
            {
                desiredType += length == "max" ? "(max)" : $"({length})";
            }
            return desiredType;
        }

        private void ApplyConstraints(SqlConnection conn, XElement table, string schema, string tableName)
        {
            // Find all AlterTable nodes matching the current table
            var alterTables = table.Document.Root.Elements("AlterTable")
                .Where(t =>
                    (t.Attribute("name")?.Value ?? "") == tableName &&
                    (t.Attribute("schema")?.Value ?? "dbo") == schema);

            foreach (var alterTable in alterTables)
            {
                ProcessForeignKeyConstraints(conn, tableName, schema, alterTable);
                ProcessCheckConstraints(conn, tableName, schema, alterTable);
                ProcessDefaultConstraints(conn, tableName, schema, alterTable);
            }
        }
        private void ProcessForeignKeyConstraints(SqlConnection conn, string tableName, string schema, XElement alterTable)
        {
            foreach (var fk in alterTable.Elements("AddForeignKeyConstraint"))
            {
                string fkName = fk.Attribute("name")?.Value ?? $"FK_{tableName}_{fk.Element("Column")?.Attribute("name")?.Value}";
                bool check = (fk.Attribute("check")?.Value ?? "false").Equals("true", StringComparison.OrdinalIgnoreCase);
                string fkColumn = fk.Element("Column")?.Attribute("name")?.Value;
                string refTable = fk.Element("References")?.Attribute("table")?.Value;
                string refSchema = fk.Element("References")?.Attribute("schema")?.Value ?? "dbo";
                string refColumn = fk.Element("References")?.Element("Column")?.Attribute("name")?.Value;

                if (string.IsNullOrEmpty(fkColumn) || string.IsNullOrEmpty(refTable) || string.IsNullOrEmpty(refColumn))
                {
                    LogToGrid(tableName, "❌ Skipping invalid FK constraint definition (missing required elements)");
                    continue;
                }

                string fullTableName = $"[{schema}].[{tableName}]";

                string checkFkQuery = @"
            SELECT COUNT(*) FROM sys.foreign_keys 
            WHERE name = @fkName AND parent_object_id = OBJECT_ID(@fullTableName)";

                using (SqlCommand checkFkCmd = new SqlCommand(checkFkQuery, conn))
                {
                    checkFkCmd.Parameters.AddWithValue("@fkName", fkName);
                    checkFkCmd.Parameters.AddWithValue("@fullTableName", fullTableName);

                    int fkExists = (int)checkFkCmd.ExecuteScalar();

                    if (fkExists == 0)
                    {
                        string createFkSql = $@"
                    ALTER TABLE [{schema}].[{tableName}]
                    WITH {(check ? "CHECK" : "NOCHECK")}
                    ADD CONSTRAINT [{fkName}] FOREIGN KEY ([{fkColumn}])
                    REFERENCES [{refSchema}].[{refTable}] ([{refColumn}])";

                        using (SqlCommand fkCmd = new SqlCommand(createFkSql, conn))
                        {
                            fkCmd.ExecuteNonQuery();
                            LogToGrid(tableName, $"✅ Foreign key constraint '{fkName}' created.");
                        }
                    }
                    else
                    {
                        LogToGrid(tableName, $"✅ Foreign key constraint '{fkName}' already exists.");
                    }
                }
            }
        }


        private void ProcessCheckConstraints(SqlConnection conn, string tableName, string schema, XElement alterTable)
        {
            foreach (var chk in alterTable.Elements("CheckConstraint"))
            {
                string name = chk.Attribute("name")?.Value;
                string condition = chk.Attribute("condition")?.Value;

                if (string.IsNullOrEmpty(name))
                {
                    LogToGrid(tableName, "❌ Skipping check constraint with missing name");
                    continue;
                }

                string fullTableName = $"[{schema}].[{tableName}]";

                string checkConstraintQuery = @"
                    SELECT COUNT(*) FROM sys.check_constraints 
                    WHERE name = @name AND parent_object_id = OBJECT_ID(@fullTableName)";

                using (SqlCommand checkCmd = new SqlCommand(checkConstraintQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@name", name);
                    checkCmd.Parameters.AddWithValue("@fullTableName", fullTableName);

                    int exists = (int)checkCmd.ExecuteScalar();

                    if (exists == 0 && !string.IsNullOrEmpty(condition))
                    {
                        string createCheckSql = $@"
                            ALTER TABLE [{schema}].[{tableName}]
                            ADD CONSTRAINT [{name}] CHECK ({condition})";

                        using (SqlCommand addCmd = new SqlCommand(createCheckSql, conn))
                        {
                            addCmd.ExecuteNonQuery();
                            LogToGrid(tableName, $"Check constraint '{name}' added.");
                        }
                    }
                    else if (exists == 0)
                    {
                        LogToGrid(tableName, $"Check constraint '{name}' referenced but no condition provided.");
                    }
                    else
                    {
                        LogToGrid(tableName, $"✅ Check constraint '{name}' already exists.");
                    }
                }
            }
        }

        private void ProcessDefaultConstraints(SqlConnection conn, string tableName, string schema, XElement alterTable)
        {
            foreach (var def in alterTable.Elements("AddDefaultConstraint"))
            {
                string columnName = def.Element("Column")?.Attribute("name")?.Value;
                string defaultValue = def.Element("DefaultValue")?.Value;

                if (string.IsNullOrEmpty(columnName) || string.IsNullOrEmpty(defaultValue))
                {
                    LogToGrid(tableName, "Skipping invalid default constraint definition (missing column or value)");
                    continue;
                }

                string defaultConstraintName = $"DF_{tableName}_{columnName}";
                string fullTableName = $"[{schema}].[{tableName}]";

                string checkDefaultQuery = @"
                    SELECT COUNT(*) FROM sys.default_constraints
                    WHERE name = @constraintName AND parent_object_id = OBJECT_ID(@tableName)";

                using (SqlCommand checkCmd = new SqlCommand(checkDefaultQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@constraintName", defaultConstraintName);
                    checkCmd.Parameters.AddWithValue("@tableName", fullTableName);

                    int exists = (int)checkCmd.ExecuteScalar();

                    if (exists == 0)
                    {
                        string addDefaultSql = $@"
                            ALTER TABLE [{schema}].[{tableName}]
                            ADD CONSTRAINT [{defaultConstraintName}] DEFAULT {defaultValue} FOR [{columnName}]";

                        using (SqlCommand addCmd = new SqlCommand(addDefaultSql, conn))
                        {
                            addCmd.ExecuteNonQuery();
                            LogToGrid(tableName, $"✅ Default constraint '{defaultConstraintName}' added on column '{columnName}'.");
                        }
                    }
                    else
                    {
                        LogToGrid(tableName, $"✅ Default constraint '{defaultConstraintName}' already exists.");
                    }
                }
            }
        }
       

        private void InsertSampleData()
        {
            try
            {
                InsertEmployee(
                    firstName: "Karabo",
                    surname: "Peteke",
                    department: "IT",  
                    role: "Marketing Specialist",
                    username: "1",
                    passwordHash: "6b86b273ff34fce19d6b804eff5a3f5747ada4eaa22f1d49c01e52ddb7875b4b",
                    hireDate: DateTime.Parse("2025-02-02"),
                    email: "tshepisoofentsendaba@gmail.com",
                    isActive: true,
                    phone: "0785980198",
                    connectionString: connectionString
                );

                var now = DateTime.Now;
                InsertPaymentMethod("Cash", "Physical currency payment", true, now, connectionString);
                InsertPaymentMethod("Credit Card", "Visa, MasterCard, etc.", true, now, connectionString);
                InsertPaymentMethod("Debit Card", "Direct bank payment", true, now, connectionString);
                InsertPaymentMethod("Mobile Payment", "Apple Pay, Google Pay", true, now, connectionString);
                InsertPaymentMethod("Bank Transfer", "EFT or wire transfer", true, now, connectionString);
            }
            catch (Exception ex)
            {
                LogToGrid("Sample Data", $"❌ Error inserting sample data: {ex.Message}");
            }
        }

        public void InsertPaymentMethod(string methodName, string description, bool isActive, DateTime createdAt, string connectionString)
        {
            const string query = @"
                IF NOT EXISTS (
                    SELECT 1 FROM [PaymentMethods]
                    WHERE MethodName = @MethodName
                )
                BEGIN
                    INSERT INTO [PaymentMethods]
                    (
                        MethodName,
                        Description,
                        IsActive,
                        CreatedAt
                    )
                    VALUES
                    (
                        @MethodName,
                        @Description,
                        @IsActive,
                        @CreatedAt
                    )
                END";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MethodName", methodName);
                cmd.Parameters.AddWithValue("@Description", description ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@IsActive", isActive);
                cmd.Parameters.AddWithValue("@CreatedAt", createdAt);

                conn.Open();
                cmd.ExecuteNonQuery();
                LogToGrid(methodName, $"✅ {methodName} payment method created successfully");
            }
        }

        public void InsertEmployee(
            string firstName, string surname, string department, string role,
            string username, string passwordHash, DateTime hireDate, string email,
            bool isActive, string phone, string connectionString)
        {
            const string query = @"
                IF NOT EXISTS (
                    SELECT 1 FROM [Employees]
                    WHERE Username = @Username OR Email = @Email
                )
                BEGIN
                    INSERT INTO [Employees]
                    (
                        EmployeeFirstName,
                        EmployeeSurname,
                        Department,
                        Role,
                        Username,
                        PasswordHash,
                        HireDate,
                        Email,
                        IsActive,
                        PHONE
                    )
                    VALUES
                    (
                        @FirstName,
                        @Surname,
                        @Department,
                        @Role,
                        @Username,
                        @PasswordHash,
                        @HireDate,
                        @Email,
                        @IsActive,
                        @Phone
                    )
                END";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@Surname", surname);
                cmd.Parameters.AddWithValue("@Department", department);
                cmd.Parameters.AddWithValue("@Role", role);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                cmd.Parameters.AddWithValue("@HireDate", hireDate);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@IsActive", isActive);
                cmd.Parameters.AddWithValue("@Phone", phone);

                conn.Open();
                cmd.ExecuteNonQuery();
                LogToGrid("Employees", "✅ Employee record created successfully");
            }
        }

        private void LogToGrid(string tableName, string response)
        {
            if (dataGridView1.InvokeRequired)
            {
                dataGridView1.Invoke((MethodInvoker)delegate {
                    dataGridView1.Rows.Add(tableName, response);
                });
            }
            else
            {
                dataGridView1.Rows.Add(tableName, response);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCheckRequire_Click(object sender, EventArgs e)
        {
            if (!ValidateDatabaseConnection())
                return;
            dataGridView1.Rows.Clear();

            try
            {
                var xml = XElement.Load(XmlPath);
                using (var conn = new SqlConnection(connectionString))
                {
                    CheckSendReceiptEmailStoredProcedure();
                    conn.Open();

                    foreach (var table in xml.Elements("Table"))
                    {
                        string tableName = table.Attribute("name")?.Value;
                        string schema = table.Attribute("schema")?.Value ?? "dbo";
                        if (!string.IsNullOrEmpty(tableName))
                        {
                            CheckTableStructure(conn, table, schema, tableName);
                            
                        }
                    }
                }
                btnCheck.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CheckTableStructure(SqlConnection conn, XElement table, string schema, string tableName)
        {
            string fullTableName = $"[{schema}].[{tableName}]";

            if (!TableExists(conn, schema, tableName))
            {
                LogToGrid(tableName, "❌ Table does not exist.");
                return;
            }

            LogToGrid(tableName, "✅ Table exists.");
            CheckColumns(conn, table, schema, tableName);
            CheckPrimaryKey(conn, table, schema, tableName);
            CheckConstraints(conn, table, schema, tableName);
        }

        private void CheckColumns(SqlConnection conn, XElement table, string schema, string tableName)
        {
            const string columnQuery = @"
                SELECT DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, IS_NULLABLE, COLUMN_NAME
                FROM INFORMATION_SCHEMA.COLUMNS
                WHERE TABLE_SCHEMA = @schema AND TABLE_NAME = @table AND COLUMN_NAME = @col";

            foreach (var col in table.Elements("Column"))
            {
                string colName = col.Attribute("name")?.Value;
                string expectedType = col.Attribute("type")?.Value;
                string expectedLength = col.Attribute("length")?.Value;
                string expectedNullable = col.Attribute("isNullable")?.Value;
                string expectedIdentity = col.Attribute("isIdentity")?.Value;

                if (string.IsNullOrEmpty(colName))
                    continue;

                using (var colCmd = new SqlCommand(columnQuery, conn))
                {
                    colCmd.Parameters.AddWithValue("@schema", schema);
                    colCmd.Parameters.AddWithValue("@table", tableName);
                    colCmd.Parameters.AddWithValue("@col", colName);

                    using (var reader = colCmd.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            LogToGrid(tableName, $"❌ Column missing: {colName}");
                            continue;
                        }

                        string dbType = reader["DATA_TYPE"].ToString();
                        string dbLength = reader["CHARACTER_MAXIMUM_LENGTH"]?.ToString() ?? "";
                        string dbNullable = reader["IS_NULLABLE"].ToString();

                        bool typeMatch = dbType.Equals(expectedType, StringComparison.OrdinalIgnoreCase);
                        bool lengthMatch = string.IsNullOrEmpty(expectedLength) || dbLength == expectedLength;
                        bool nullMatch = (expectedNullable == "false" && dbNullable == "NO") ||
                                         (expectedNullable == "true" && dbNullable == "YES");

                        if (typeMatch && lengthMatch && nullMatch)
                            LogToGrid(tableName, $"✅ Column {colName} OK");
                        else
                            LogToGrid(tableName, $"❌ Column {colName} mismatch (Type: {dbType}, Length: {dbLength}, Nullable: {dbNullable})");
                    }
                }

                if (expectedIdentity == "true")
                {
                    CheckIdentityColumn(conn, schema, tableName, colName);
                }
            }
        }

        private void CheckIdentityColumn(SqlConnection conn, string schema, string tableName, string colName)
        {
            const string identityQuery = @"
                SELECT COLUMN_NAME 
                FROM INFORMATION_SCHEMA.COLUMNS 
                WHERE TABLE_SCHEMA = @schema AND TABLE_NAME = @table 
                AND COLUMN_NAME = @col 
                AND COLUMNPROPERTY(OBJECT_ID(@schema + '.' + @table), COLUMN_NAME, 'IsIdentity') = 1";

            using (var identityCmd = new SqlCommand(identityQuery, conn))
            {
                identityCmd.Parameters.AddWithValue("@schema", schema);
                identityCmd.Parameters.AddWithValue("@table", tableName);
                identityCmd.Parameters.AddWithValue("@col", colName);

                var result = identityCmd.ExecuteScalar();
                if (result == null)
                    LogToGrid(tableName, $"❌ Column {colName} is not Identity");
                else
                    LogToGrid(tableName, $"✅ Column {colName} is Identity");
            }
        }

        private void CheckPrimaryKey(SqlConnection conn, XElement table, string schema, string tableName)
        {
            var pk = table.Element("PrimaryKey");
            if (pk == null)
                return;

            var expectedPkCols = pk.Elements("Column").Select(c => c.Attribute("name")?.Value).ToList();

            const string pkQuery = @"
                SELECT k.COLUMN_NAME 
                FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS t
                JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE k
                  ON t.CONSTRAINT_NAME = k.CONSTRAINT_NAME
                WHERE t.CONSTRAINT_TYPE = 'PRIMARY KEY'
                  AND t.TABLE_SCHEMA = @schema AND t.TABLE_NAME = @table";

            using (var pkCmd = new SqlCommand(pkQuery, conn))
            {
                pkCmd.Parameters.AddWithValue("@schema", schema);
                pkCmd.Parameters.AddWithValue("@table", tableName);

                using (var reader = pkCmd.ExecuteReader())
                {
                    var actualPkCols = new List<string>();
                    while (reader.Read())
                    {
                        actualPkCols.Add(reader["COLUMN_NAME"].ToString());
                    }

                    if (expectedPkCols.All(actualPkCols.Contains) && expectedPkCols.Count == actualPkCols.Count)
                        LogToGrid(tableName, "✅ Primary key columns match.");
                    else
                        LogToGrid(tableName, $"❌ Primary key mismatch. Found: {string.Join(", ", actualPkCols)}");
                }
            }
        }

        private void CheckConstraints(SqlConnection conn, XElement table, string schema, string tableName)
        {
            var alterTables = table.Document?.Root?.Elements("AlterTable")
                .Where(t =>
                    (t.Attribute("name")?.Value ?? "") == tableName &&
                    (t.Attribute("schema")?.Value ?? "dbo") == schema);

            if (alterTables == null)
                return;

            foreach (var alterTable in alterTables)
            {
                CheckForeignKeyConstraints(conn, tableName, schema, alterTable);
                CheckCheckConstraints(conn, tableName, schema, alterTable);
                CheckDefaultConstraints(conn, tableName, schema, alterTable);
            }
        }

        private void CheckForeignKeyConstraints(SqlConnection conn, string tableName, string schema, XElement alterTable)
        {
            foreach (var fk in alterTable.Elements("AddForeignKeyConstraint"))
            {
                string fkName = fk.Attribute("name")?.Value;
                string fkColumn = fk.Element("Column")?.Attribute("name")?.Value;
                string refTable = fk.Element("References")?.Attribute("table")?.Value;
                string refSchema = fk.Element("References")?.Attribute("schema")?.Value ?? "dbo";
                string refColumn = fk.Element("References")?.Element("Column")?.Attribute("name")?.Value;

                if (string.IsNullOrEmpty(fkName) || string.IsNullOrEmpty(fkColumn) ||
                    string.IsNullOrEmpty(refTable) || string.IsNullOrEmpty(refColumn))
                {
                    LogToGrid(tableName, "⚠️ Invalid FK definition (missing required attributes).");
                    continue;
                }

                string fullTableName = $"[{schema}].[{tableName}]";

                const string checkFkQuery = @"
                    SELECT COUNT(*) FROM sys.foreign_keys 
                    WHERE name = @fkName AND parent_object_id = OBJECT_ID(@fullTableName)";

                using (var checkFkCmd = new SqlCommand(checkFkQuery, conn))
                {
                    checkFkCmd.Parameters.AddWithValue("@fkName", fkName);
                    checkFkCmd.Parameters.AddWithValue("@fullTableName", fullTableName);

                    int fkExists = checkFkCmd.ExecuteScalar() as int? ?? 0;
                    if (fkExists > 0)
                        LogToGrid(tableName, $"✅ Foreign key '{fkName}' exists.");
                    else
                        LogToGrid(tableName, $"❌ Missing foreign key '{fkName}'.");
                }
            }
        }

        private void CheckCheckConstraints(SqlConnection conn, string tableName, string schema, XElement alterTable)
        {
            foreach (var chk in alterTable.Elements("CheckConstraint"))
            {
                string name = chk.Attribute("name")?.Value;
                if (string.IsNullOrEmpty(name))
                {
                    LogToGrid(tableName, "⚠️ Invalid check constraint definition (missing name).");
                    continue;
                }

                const string checkConstraintQuery = @"
                    SELECT COUNT(*) FROM sys.check_constraints 
                    WHERE name = @name AND parent_object_id = OBJECT_ID(@fullTableName)";

                using (var cmd = new SqlCommand(checkConstraintQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@fullTableName", $"[{schema}].[{tableName}]");

                    int exists = cmd.ExecuteScalar() as int? ?? 0;
                    if (exists > 0)
                        LogToGrid(tableName, $"✅ Check constraint '{name}' exists.");
                    else
                        LogToGrid(tableName, $"❌ Missing check constraint '{name}'.");
                }
            }
        }

        private void CheckDefaultConstraints(SqlConnection conn, string tableName, string schema, XElement alterTable)
        {
            foreach (var def in alterTable.Elements("AddDefaultConstraint"))
            {
                string columnName = def.Element("Column")?.Attribute("name")?.Value;
                string defaultValue = def.Element("DefaultValue")?.Value;

                if (string.IsNullOrEmpty(columnName) || string.IsNullOrEmpty(defaultValue))
                {
                    LogToGrid(tableName, "⚠️ Invalid default constraint definition (missing column or value).");
                    continue;
                }

                string defaultConstraintName = $"DF_{tableName}_{columnName}";
                string fullTableName = $"[{schema}].[{tableName}]";

                const string checkDefaultQuery = @"
                    SELECT COUNT(*) FROM sys.default_constraints
                    WHERE name = @constraintName AND parent_object_id = OBJECT_ID(@tableName)";

                using (var checkCmd = new SqlCommand(checkDefaultQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@constraintName", defaultConstraintName);
                    checkCmd.Parameters.AddWithValue("@tableName", fullTableName);

                    int exists = checkCmd.ExecuteScalar() as int? ?? 0;
                    if (exists > 0)
                        LogToGrid(tableName, $"✅ Default constraint '{defaultConstraintName}' exists on column '{columnName}'.");
                    else
                        LogToGrid(tableName, $"❌ Missing default constraint '{defaultConstraintName}' on column '{columnName}'.");
                }
            }
        }


        public void CreateSendReceiptEmailStoredProcedure()
        {
            string sql = @"
    IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'SendReceiptEmail')
    BEGIN
        EXEC('
        CREATE PROCEDURE [dbo].[SendReceiptEmail]
            @receiptContent NVARCHAR(MAX),
            @toEmail NVARCHAR(255)
        AS
        BEGIN
            SET NOCOUNT ON;
            
            BEGIN TRY
                EXEC msdb.dbo.sp_send_dbmail
                    @profile_name = ''Miara POS'',  
                    @recipients = @toEmail,
                    @subject = ''Receipt from MIARA TRADING PTY LTD'',
                    @body = @receiptContent,
                    @body_format = ''HTML'';  
                    
                RETURN 0; -- Success
            END TRY
            BEGIN CATCH
                DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
                RAISERROR(@ErrorMessage, 16, 1);
                RETURN -1; -- Error
            END CATCH
        END
        ')
    END";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        LogToGrid("Mail Procedure", $"✅ Mail Procedure   created successfully.");
                    }
                    catch (Exception ex)
                    {
                        LogToGrid("Mail Procedure", $"❌ Error creating Mail Procedure: {ex.Message}");
                        throw;
                    }
                }
            }
        }




        public void CheckSendReceiptEmailStoredProcedure()
        {
            string sql = @"
        SELECT 1 
        FROM sys.objects 
        WHERE type = 'P' AND name = 'SendReceiptEmail'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            LogToGrid("Mail Procedure", "✅ 'SendReceiptEmail' procedure exists.");
                        }
                        else
                        {
                            LogToGrid("Mail Procedure", "❌ 'SendReceiptEmail' procedure does not exist.");
                        }
                    }
                    catch (Exception ex)
                    {
                        LogToGrid("Mail Procedure", $"❌ Error checking Mail Procedure: {ex.Message}");
                        throw;
                    }
                }
            }
        }

    }
}