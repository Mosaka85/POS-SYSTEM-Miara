using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Miara
{
    public partial class frmEmployeeDetails : Form
    {
        private static readonly string configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config.xml");
        private string connectionString;
        private DataView employeeDataView;
        private DataTable employeeDataTable;

        public frmEmployeeDetails()
        {
            InitializeComponent();

        }

        private void LoadSQLConnectionInfo()
        {
            if (File.Exists(configFile))
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(LoginInfo));
                    using (FileStream fileStream = new FileStream(configFile, FileMode.Open))
                    {
                        LoginInfo loginInfo = (LoginInfo)serializer.Deserialize(fileStream);
                        connectionString = $"Data Source={loginInfo.DataSource};Initial Catalog={loginInfo.SelectedDatabase};User ID={loginInfo.Username};Password={loginInfo.Password}";
                    }

                }


                catch (Exception ex)
                {
                    MessageBox.Show("Failed to load connection information: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Connection configuration file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadEmployeeData()
        {
            string query = "SELECT EmployeeID, EmployeeFirstName, EmployeeSurname, Department, Role, Username, HireDate, Email, PHONE , IsActive FROM Employees";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    employeeDataTable = new DataTable();
                    adapter.Fill(employeeDataTable);

                    dataGridEmployeeList.DataSource = employeeDataTable;
                    employeeDataView = new DataView(employeeDataTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading employee data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSaveNewEmployee_Click(object sender, EventArgs e)
        {
            // Validate input fields
            if (string.IsNullOrEmpty(txtEmployeeName.Text) || string.IsNullOrEmpty(txtEmployeeSurname.Text) ||
                string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text) ||
                string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtEmployeePhone.Text))
            {
                MessageBox.Show("Please fill in all required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if username already exists
            if (CheckIfUsernameExists(txtUsername.Text))
            {
                MessageBox.Show("Username already exists. Please choose a different username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if name, surname, and phone number combination already exists
            if (CheckIfEmployeeExists(txtEmployeeName.Text, txtEmployeeSurname.Text, txtEmployeePhone.Text))
            {
                MessageBox.Show("An employee with the same name, surname, and phone number already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if email already exists
            if (CheckIfEmailExists(txtEmail.Text))
            {
                MessageBox.Show("Email already exists. Please use a different email address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // If all checks pass, proceed to insert the new employee
            string query = @"
        INSERT INTO [POS_MOSAKA].[dbo].[Employees] (
            [EmployeeFirstName],
            [EmployeeSurname],
            [Department],
            [Role],
            [Username],
            [PasswordHash],
            [HireDate],
            [Email],
            [IsActive],
            [PHONE]
        )
        VALUES (
            @EmployeeFirstName,
            @EmployeeSurname,
            @Department,
            @Role,
            @Username,
            @PasswordHash,
            @HireDate,
            @Email,
            @IsActive,
            @PHONE
        );";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@EmployeeFirstName", txtEmployeeName.Text);
                        command.Parameters.AddWithValue("@EmployeeSurname", txtEmployeeSurname.Text);
                        command.Parameters.AddWithValue("@Department", comboEmployeeDepartment.SelectedItem?.ToString() ?? string.Empty);
                        command.Parameters.AddWithValue("@Role", comboBoxRoles.SelectedItem?.ToString() ?? string.Empty);
                        command.Parameters.AddWithValue("@Username", txtUsername.Text);
                        command.Parameters.AddWithValue("@PasswordHash", HashPassword(txtPassword.Text));
                        command.Parameters.AddWithValue("@HireDate", dtpHireDate.Value);
                        command.Parameters.AddWithValue("@Email", txtEmail.Text);
                        command.Parameters.AddWithValue("@IsActive", chkIsActive.Checked);
                        command.Parameters.AddWithValue("@PHONE", txtEmployeePhone.Text);

                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Employee registered successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadEmployeeData(); // Refresh the employee list
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error registering employee: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool CheckIfUsernameExists(string username)
        {
            string query = "SELECT COUNT(*) FROM Employees WHERE Username = @Username";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0; // If count > 0, username exists
                }
            }
        }

        private bool CheckIfEmployeeExists(string firstName, string surname, string phone)
        {
            string query = "SELECT COUNT(*) FROM Employees WHERE EmployeeFirstName = @FirstName AND EmployeeSurname = @Surname AND PHONE = @Phone";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@Surname", surname);
                    command.Parameters.AddWithValue("@Phone", phone);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0; // If count > 0, employee exists
                }
            }
        }

        private bool CheckIfEmailExists(string email)
        {
            string query = "SELECT COUNT(*) FROM Employees WHERE Email = @Email";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0; // If count > 0, email exists
                }
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                StringBuilder hashBuilder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    hashBuilder.Append(b.ToString("x2"));
                }
                return hashBuilder.ToString();
            }
        }

        private void btnNewEmployee_Click(object sender, EventArgs e)
        {
            txtEmployeeName.Clear();
            txtEmployeeSurname.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            txtEmail.Clear();
            chkIsActive.Checked = true;
        }

        private void frmEmployeeDetails_Load(object sender, EventArgs e)
        {
            LoadSQLConnectionInfo();
            LoadEmployeeData();
            LoadDepartments();
            comboBoxRoles.SelectedItem = -1;
            dataGridEmployeeList.SelectionChanged += dataGridEmployeeList_SelectionChanged;
            if (dataGridEmployeeList.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridEmployeeList.SelectedRows[0];

                // Repopulate form fields
                txtEmployeeID.Text = selectedRow.Cells["EmployeeID"].Value.ToString();
                txtEmployeeName.Text = selectedRow.Cells["EmployeeFirstName"].Value.ToString();
                txtEmployeeSurname.Text = selectedRow.Cells["EmployeeSurname"].Value.ToString();
                comboEmployeeDepartment.SelectedItem = selectedRow.Cells["Department"].Value.ToString();
                comboBoxRoles.SelectedItem = selectedRow.Cells["Role"].Value.ToString();
                txtUsername.Text = selectedRow.Cells["Username"].Value.ToString();
                txtPassword.Text = ""; // Clear password field for security
                dtpHireDate.Value = Convert.ToDateTime(selectedRow.Cells["HireDate"].Value);
                txtEmail.Text = selectedRow.Cells["Email"].Value.ToString();
                chkIsActive.Checked = Convert.ToBoolean(selectedRow.Cells["IsActive"].Value);
                txtEmployeePhone.Text = selectedRow.Cells["PHONE"].Value.ToString(); ;
            }
        }

        private void LoadDepartments()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT DepartmentID, DepartmentName FROM [EmployeeDepartments] WHERE IsActive = 1";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    // Create a DataTable to hold the data
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    // Bind the DataTable to the ComboBox
                    comboEmployeeDepartment.DataSource = dt;
                    comboEmployeeDepartment.DisplayMember = "DepartmentName"; // Display department names
                    comboEmployeeDepartment.ValueMember = "DepartmentID";     // Store department IDs

                    // Ensure the ComboBox displays the correct text
                    comboEmployeeDepartment.SelectedIndex = -1; // Clear any selection

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading departments: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event handler for department selection change
        private void comboEmployeeDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboEmployeeDepartment.SelectedValue != null)
            {
                string selectedDepartmentID = comboEmployeeDepartment.SelectedValue.ToString();
                LoadRoles(selectedDepartmentID);
            }
        }

        // Method to load roles into the ComboBox based on the selected department
        private void LoadRoles(string departmentID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT RoleName FROM EmployeeRoles WHERE DepartmentID = @DepartmentID AND IsActive = 1";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@DepartmentID", departmentID);
                    SqlDataReader reader = command.ExecuteReader();

                    comboBoxRoles.Items.Clear();
                    while (reader.Read())
                    {
                        comboBoxRoles.Items.Add(reader["RoleName"].ToString());
                    }

                    reader.Close(); // Ensure the reader is closed
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading roles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dataGridEmployeeList_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridEmployeeList.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridEmployeeList.SelectedRows[0];

                if (selectedRow != null)
                {
                    txtEmployeeID.Text = selectedRow.Cells["EmployeeID"].Value?.ToString();
                    txtEmployeeName.Text = selectedRow.Cells["EmployeeFirstName"].Value?.ToString();
                    txtEmployeeSurname.Text = selectedRow.Cells["EmployeeSurname"].Value?.ToString();
                    comboEmployeeDepartment.SelectedItem = selectedRow.Cells["Department"].Value?.ToString();
                    comboBoxRoles.SelectedItem = selectedRow.Cells["Role"].Value?.ToString();
                    txtUsername.Text = selectedRow.Cells["Username"].Value?.ToString();
                    txtPassword.Text = ""; // Clear password field for security
                    dtpHireDate.Value = selectedRow.Cells["HireDate"].Value != null ? Convert.ToDateTime(selectedRow.Cells["HireDate"].Value) : DateTime.Now;
                    txtEmail.Text = selectedRow.Cells["Email"].Value?.ToString();
                    chkIsActive.Checked = selectedRow.Cells["IsActive"].Value != null ? Convert.ToBoolean(selectedRow.Cells["IsActive"].Value) : false;
                    txtEmployeePhone.Text = selectedRow.Cells["PHONE"].Value?.ToString();
                }
            }
        }

        private void btnEditEmployee_Click(object sender, EventArgs e)
        {
            // Validate EmployeeID
            if (string.IsNullOrEmpty(txtEmployeeID.Text))
            {
                MessageBox.Show("Please enter a valid Employee ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate other required fields
            if (string.IsNullOrEmpty(txtEmployeeName.Text) || string.IsNullOrEmpty(txtEmployeeSurname.Text))
            {
                MessageBox.Show("Please fill in all required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Fetch the original employee data from the database
            DataRow originalEmployeeData = GetEmployeeDataFromDatabase(txtEmployeeID.Text);

            if (originalEmployeeData == null)
            {
                MessageBox.Show("No employee found with the specified ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Compare current form values with original values to check for changes
            bool hasChanges = HasEmployeeDataChanged(originalEmployeeData);

            if (!hasChanges)
            {
                MessageBox.Show("No changes detected. Employee data was not updated.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Ask for confirmation before saving
            DialogResult result = MessageBox.Show("Are you sure you want to save these changes?", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                return; // Exit if the user cancels
            }

            // Proceed with the update
            string query = @"
        UPDATE [Employees]
        SET
            [EmployeeFirstName] = @EmployeeFirstName,
            [EmployeeSurname] = @EmployeeSurname,
            [Department] = @Department,
            [Role] = @Role,
            [Username] = @Username,
            [PasswordHash] = @PasswordHash,
            [HireDate] = @HireDate,
            [Email] = @Email,
            [IsActive] = @IsActive
        WHERE
            [EmployeeID] = @EmployeeID;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@EmployeeID", txtEmployeeID.Text);
                        command.Parameters.AddWithValue("@EmployeeFirstName", txtEmployeeName.Text);
                        command.Parameters.AddWithValue("@EmployeeSurname", txtEmployeeSurname.Text);
                        command.Parameters.AddWithValue("@Department", comboEmployeeDepartment.SelectedItem?.ToString() ?? string.Empty);
                        command.Parameters.AddWithValue("@Role", comboBoxRoles.SelectedItem?.ToString() ?? string.Empty);
                        command.Parameters.AddWithValue("@Username", txtUsername.Text);
                        command.Parameters.AddWithValue("@PasswordHash", HashPassword(txtPassword.Text));
                        command.Parameters.AddWithValue("@HireDate", dtpHireDate.Value);
                        command.Parameters.AddWithValue("@Email", txtEmail.Text);
                        command.Parameters.AddWithValue("@IsActive", chkIsActive.Checked);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Employee updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadEmployeeData(); // Refresh the employee list
                        }
                        else
                        {
                            MessageBox.Show("No employee found with the specified ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating employee: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private bool HasEmployeeDataChanged(DataRow originalEmployeeData)
        {
            // Compare each field
            if (originalEmployeeData["EmployeeFirstName"].ToString() != txtEmployeeName.Text ||
                originalEmployeeData["EmployeeSurname"].ToString() != txtEmployeeSurname.Text ||
                originalEmployeeData["Department"].ToString() != comboEmployeeDepartment.SelectedItem?.ToString() ||
                originalEmployeeData["Role"].ToString() != comboBoxRoles.SelectedItem?.ToString() ||
                originalEmployeeData["Username"].ToString() != txtUsername.Text ||
                originalEmployeeData["PasswordHash"].ToString() != HashPassword(txtPassword.Text) ||
                Convert.ToDateTime(originalEmployeeData["HireDate"]) != dtpHireDate.Value ||
                originalEmployeeData["Email"].ToString() != txtEmail.Text ||
                Convert.ToBoolean(originalEmployeeData["IsActive"]) != chkIsActive.Checked)
            {
                return true; // Changes detected
            }

            return false; // No changes detected
        }

        private DataRow GetEmployeeDataFromDatabase(string employeeID)
        {
            string query = "SELECT * FROM Employees WHERE EmployeeID = @EmployeeID";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeID", employeeID);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        return dt.Rows[0];
                    }
                }
            }
            return null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }

}