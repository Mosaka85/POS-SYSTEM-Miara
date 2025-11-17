using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Miara
{
    public partial class FormUserPermissions : Form
    {
        private readonly string _configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config.xml");
        private readonly string _logFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt");
        private string _connectionString;

        private readonly string _employeeFirstName;
        private readonly string _employeeSurname;
        private readonly int _employeeNumber;
        private readonly string _deviceInternetID;

        // LoginInfo class for XML deserialization
        public class LoginInfo
        {
            public string DataSource { get; set; }
            public string SelectedDatabase { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public FormUserPermissions(string firstName, string surname, int employeeId, string deviceId)
        {
            InitializeComponent();
            _employeeFirstName = firstName;
            _employeeSurname = surname;
            _employeeNumber = employeeId;
            _deviceInternetID = deviceId;

            LoadSqlConnectionString();
            LoadUserGroups();
            LoadUserRoles();
            LoadUsers();
            LoadComboSelectGroup();
            LoadGroups();
            LoadRoles();



        }
        private void LoadSqlConnectionString()
        {
            if (!File.Exists(_configFile))
            {
                LogAuditEntry("Connection configuration file not found.", $"File: {_configFile}");
                LogTrace($"Connection configuration file not found: {_configFile}");
                MessageBox.Show("Connection configuration file not found. Please check the configuration.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var serializer = new XmlSerializer(typeof(LoginInfo));
                using (var fileStream = new FileStream(_configFile, FileMode.Open))
                {
                    var loginInfo = (LoginInfo)serializer.Deserialize(fileStream);
                    _connectionString = $"Data Source={loginInfo.DataSource};Initial Catalog={loginInfo.SelectedDatabase};User ID={loginInfo.Username};Password={loginInfo.Password}";
                }
            }
            catch (Exception ex)
            {
                LogAuditEntry("Failed to load connection information", ex.Message);
                LogTrace($"Failed to load connection information: {ex.Message}");
                MessageBox.Show($"Failed to load connection information: {ex.Message}", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadUserGroups()
        {
            LoadDataIntoComboBox("SELECT id, name FROM [user_groups]", comboUserGroup, "name", "id");
        }

        private void LoadUserRoles()
        {
            LoadDataIntoComboBox("SELECT id, name FROM [roles]", comboRole, "name", "id");
        }

        private void LoadUsers(string searchText = "")
        {
            if (string.IsNullOrEmpty(_connectionString)) return;

            string query = @"
        SELECT 
            u.id AS EmployeeID,
            u.first_name AS FirstName,
            u.last_name AS LastName,
            u.username AS Username,
            u.email AS Email,
            ug.name AS UserGroup,
            r.name AS Role,
            u.active AS Active
        FROM [users] u
        LEFT JOIN [user_groups] ug ON u.user_group_id = ug.id
        LEFT JOIN [group_roles] gr ON ug.id = gr.group_id
        LEFT JOIN [roles] r ON gr.role_id = r.id
        WHERE 
            u.username LIKE @searchText
            OR u.email LIKE @searchText
            OR u.first_name LIKE @searchText
            OR u.last_name LIKE @searchText
        ORDER BY u.first_name, u.last_name";

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@searchText", $"%{searchText}%");

                    var dt = new DataTable();
                    new SqlDataAdapter(cmd).Fill(dt);

                    dataGridViewUsers.DataSource = dt;

                    
                    dataGridViewUsers.Columns["EmployeeID"].HeaderText = "Employee ID";
                    // Rename columns for readability
                    dataGridViewUsers.Columns["FirstName"].HeaderText = "First Name";
                    dataGridViewUsers.Columns["LastName"].HeaderText = "Last Name";
                    dataGridViewUsers.Columns["Username"].HeaderText = "Username";
                    dataGridViewUsers.Columns["Email"].HeaderText = "Email";
                    dataGridViewUsers.Columns["UserGroup"].HeaderText = "Group";
                    dataGridViewUsers.Columns["Role"].HeaderText = "Role";
                    dataGridViewUsers.Columns["Active"].HeaderText = "Active";
                }
            }
            catch (Exception ex)
            {
                LogAuditEntry("Failed to load users", ex.Message);
                LogTrace($"Failed to load users: {ex.Message}");
                MessageBox.Show($"Failed to load users: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadDataIntoComboBox(string query, ComboBox comboBox, string displayMember, string valueMember)
        {
            if (string.IsNullOrEmpty(_connectionString)) return;

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand(query, conn))
                {
                    var dt = new DataTable();
                    new SqlDataAdapter(cmd).Fill(dt);

                    comboBox.DataSource = dt;
                    comboBox.DisplayMember = displayMember;  // must be AFTER setting DataSource
                    comboBox.ValueMember = valueMember;
                    comboBox.SelectedIndex = -1; // optional: no default selection
                }
            }
            catch (Exception ex)
            {
                LogTrace($"Failed to load {comboBox.Name} data: {ex.Message}");
                MessageBox.Show($"Failed to load {comboBox.Name} data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            ResetFormFields();
        }

        private void txtSearchUsers_TextChanged(object sender, EventArgs e)
        {
            LoadUsers(txtSearchUsers.Text.Trim());
        }

        private bool UserExists(string username, string email)
        {
            if (string.IsNullOrEmpty(_connectionString)) return false;

            string query = "SELECT COUNT(*) FROM [users] WHERE username = @username OR (@email != '' AND email = @email)";

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    if (string.IsNullOrEmpty(email))
                    {
                        cmd.Parameters.AddWithValue("@email", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@email", email);
                    }
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("A user with this username or email already exists.", "Duplicate User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        LogAuditEntry($"Attempted to add existing user: {username} / {email}");
                        LogTrace($"Attempted to add existing user: {username} / {email}");
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking for existing user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogAuditEntry($"Error checking for existing user: {username} / {email}", ex.Message);
                LogTrace($"Error checking for existing user: {ex.Message}");
            }
            return false;
        }
        private void ResetFormFields()
        {
            txtUsername.Clear();
            txtEmail.Clear();
            txtPassword.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            comboUserGroup.SelectedIndex = -1;
            comboRole.SelectedIndex = -1;
            CheckActiveUser.Checked = true;
            txtUsername.Focus();
        }

        private void LogAuditEntry(string stepDescription, string errorMessage = null)
        {
            if (string.IsNullOrEmpty(_connectionString)) return;

            string query = @"
                INSERT INTO DeviceAudit (Device, Employee, AuditDate, StepDescription, ErrorMessage)
                VALUES (@Device, @Employee, @AuditDate, @StepDescription, @ErrorMessage);";

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Device", _deviceInternetID ?? string.Empty);
                    command.Parameters.AddWithValue("@Employee", $"{_employeeSurname}, {_employeeFirstName}; {_employeeNumber}");
                    command.Parameters.AddWithValue("@AuditDate", DateTime.Now);
                    command.Parameters.AddWithValue("@StepDescription", stepDescription ?? string.Empty);
                    command.Parameters.AddWithValue("@ErrorMessage", string.IsNullOrEmpty(errorMessage) ? DBNull.Value : (object)errorMessage);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to log audit entry: {ex.Message}", "Logging Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LogTrace(string message)
        {
            try
            {
                using (var writer = new StreamWriter(_logFile, true))
                {
                    writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} : {_employeeNumber},{_employeeFirstName},{_employeeSurname}, {_deviceInternetID} , {message}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to log trace: {ex.Message}", "Logging Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
           
        }

        private void btnRefreshUsers_Click(object sender, EventArgs e)
        {
            
        }


        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            // Common field values
            string username = txtUsername.Text.Trim();
            string email = txtEmail.Text.Trim();
            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            object userGroupId = comboUserGroup.SelectedValue ?? DBNull.Value;
            object roleId = comboRole.SelectedValue ?? DBNull.Value;
            bool isActive = CheckActiveUser.Checked;
            bool isEdit = selectedUserId != null;

            // Validation
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Username is required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!isEdit && string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Password is required when creating a new user.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Duplicate check
            if (isEdit)
            {
                if (UserExistsForEdit(username, email, selectedUserId.Value))
                    return;
            }
            else
            {
                if (UserExists(username, email))
                    return;
            }

            string query;

            // If editing
            if (isEdit)
            {
                bool updatePassword = !string.IsNullOrWhiteSpace(txtPassword.Text);

                query = @"
            UPDATE [users]
            SET 
                username = @username,
                email = @email,
                first_name = @first_name,
                last_name = @last_name,
                user_group_id = @user_group_id,
                role_id = @role_id,
                active = @active
            " + (updatePassword ? ", password_hash = @password_hash" : "") + @"
            WHERE id = @id";
            }
            else
            {
                // If adding new user
                query = @"
            INSERT INTO [users] 
            (username, email, password_hash, user_group_id, role_id, created_at, active, first_name, last_name)
            VALUES 
            (@username, @email, @password_hash, @user_group_id, @role_id, @created_at, @active, @first_name, @last_name)";
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Shared parameters
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@email", string.IsNullOrWhiteSpace(email) ? (object)DBNull.Value : email);
                    cmd.Parameters.AddWithValue("@first_name", string.IsNullOrWhiteSpace(firstName) ? (object)DBNull.Value : firstName);
                    cmd.Parameters.AddWithValue("@last_name", string.IsNullOrWhiteSpace(lastName) ? (object)DBNull.Value : lastName);
                    cmd.Parameters.AddWithValue("@user_group_id", userGroupId);
                    cmd.Parameters.AddWithValue("@role_id", roleId);
                    cmd.Parameters.AddWithValue("@active", isActive);

                    if (isEdit)
                    {
                        cmd.Parameters.AddWithValue("@id", selectedUserId);

                        if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                            cmd.Parameters.AddWithValue("@password_hash", HashPassword(txtPassword.Text.Trim()));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@password_hash", HashPassword(txtPassword.Text.Trim()));
                        cmd.Parameters.AddWithValue("@created_at", DateTime.Now);
                    }

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                // Success messages
                if (isEdit)
                {
                    MessageBox.Show("User updated successfully.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LogAuditEntry($"Updated user ID {selectedUserId} ({username})");
                    LogTrace($"Updated user ID {selectedUserId} ({username})");
                }
                else
                {
                    MessageBox.Show("User added successfully.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LogAuditEntry($"Added new user: {username}");
                    LogTrace($"Added new user: {username}");
                }

                txtEmail.Enabled = false;
                txtFirstName.Enabled = false;
                txtLastName.Enabled = false;
                txtUsername.Enabled = false;
                comboUserGroup.Enabled = false;
                comboRole.Enabled = false;
                CheckActiveUser.Enabled = false;
                CheckActiveUser.Enabled = false;
                txtPassword.Enabled = false;
                LoadUsers();
                ResetFormFields();
                selectedUserId = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving user: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                LogAuditEntry("Error saving user", ex.Message);
                LogTrace($"Error saving user: {ex.Message}");
            }
        }

        private void dataGridViewGroups_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void AddUserGroup(string name, string description)
        {
            if (GroupExists(name)) return;

            const string sql = @"
        INSERT INTO [user_groups] (name, description,isActive)
        VALUES (@name, @description,0);
        SELECT SCOPE_IDENTITY();";

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@description",
                        string.IsNullOrWhiteSpace(description) ? (object)DBNull.Value : description);

                    conn.Open();
                    object idObj = cmd.ExecuteScalar();
                    if (idObj != null && int.TryParse(idObj.ToString(), out int newId))
                    {
                        MessageBox.Show($"Group '{name}' added (ID: {newId}).", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LogAuditEntry($"Added user group: {name} (ID: {newId})");
                        LogTrace($"Added user group: {name} (ID: {newId})");

                        // Refresh
                        LoadUserGroups();
                        LoadGroupAssignmentControls(); // for group-role assignment
                        txtGroupName.Clear();
                        txtGroupDesc.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to add group: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogAuditEntry($"Failed to add group: {name}", ex.Message);
                LogTrace($"Failed to add group: {ex.Message}");
            }
        }

        private bool GroupExists(string name)
        {
            if (string.IsNullOrEmpty(_connectionString)) return false;

            const string sql = "SELECT COUNT(*) FROM [user_groups] WHERE name = @name";
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("A group with this name already exists.", "Duplicate",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        LogAuditEntry($"Attempted duplicate group: {name}");
                        LogTrace($"Attempted duplicate group: {name}");
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking group: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogTrace($"GroupExists error: {ex.Message}");
            }
            return false;
        }

        private void AddRole(string name, string description)
        {
            if (RoleExists(name)) return;

            const string sql = @"
        INSERT INTO [roles] (name, description,isActive)
        VALUES (@name, @description,1);
        SELECT SCOPE_IDENTITY();";

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@description",
                        string.IsNullOrWhiteSpace(description) ? (object)DBNull.Value : description);

                    conn.Open();
                    object idObj = cmd.ExecuteScalar();
                    if (idObj != null && int.TryParse(idObj.ToString(), out int newId))
                    {
                        MessageBox.Show($"Role '{name}' added (ID: {newId}).", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LogAuditEntry($"Added role: {name} (ID: {newId})");
                        LogTrace($"Added role: {name} (ID: {newId})");

                        // Refresh
                        LoadUserRoles();
                        LoadGroupAssignmentControls(); // refresh role list
                        txtRoleName.Clear();
                        txtRoleDesc.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to add role: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogAuditEntry($"Failed to add role: {name}", ex.Message);
                LogTrace($"Failed to add role: {ex.Message}");
            }
        }

        private bool RoleExists(string name)
        {
            if (string.IsNullOrEmpty(_connectionString)) return false;

            const string sql = "SELECT COUNT(*) FROM [roles] WHERE name = @name";
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("A role with this name already exists.", "Duplicate",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        LogAuditEntry($"Attempted duplicate role: {name}");
                        LogTrace($"Attempted duplicate role: {name}");
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking role: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogTrace($"RoleExists error: {ex.Message}");
            }
            return false;
        }

        private void LoadGroupAssignmentControls()
        {


            const string roleSql = "SELECT id, name FROM [roles] ORDER BY name";
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand(roleSql, conn))
                {
                    var dt = new DataTable();
                    new SqlDataAdapter(cmd).Fill(dt);
                    clbRoles.DataSource = dt;
                    clbRoles.DisplayMember = "name";
                    clbRoles.ValueMember = "id";
                }
            }
            catch (Exception ex)
            {
                LogTrace($"LoadGroupAssignmentControls error: {ex.Message}");
            }

       
        }

        private void panelUserInput_Paint(object sender, PaintEventArgs e)
        {

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void dataGridUserGroups_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewPermissions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadGroupAssignmentControls();
        }

        private void tabUsers_Click(object sender, EventArgs e)
        {
            LoadGroupAssignmentControls();


        }

        private void toolStripGroups_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            LoadGroups();
        }


        private void LoadGroups()
        {
            string queryActive = "SELECT * FROM user_groups WHERE isActive = 1";
            string queryInactive = "SELECT * FROM user_groups WHERE isActive = 0";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();

                    // Load ACTIVE groups
                    using (SqlDataAdapter daActive = new SqlDataAdapter(queryActive, conn))
                    {
                        DataTable dtActive = new DataTable();
                        daActive.Fill(dtActive);
                        dataGridUserGroupsActive.DataSource = dtActive;
                    }

                    // Load INACTIVE groups
                    using (SqlDataAdapter daInactive = new SqlDataAdapter(queryInactive, conn))
                    {
                        DataTable dtInactive = new DataTable();
                        daInactive.Fill(dtInactive);
                        dataGridInactiveGroups.DataSource = dtInactive;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading user groups: " + ex.Message);
                }
            }
        }


       
        private void btnDeactivate_Click(object sender, EventArgs e)
        {
            if (dataGridUserGroupsActive.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a group to deactivate.");
                return;
            }

            // Get selected ID
            int selectedId = Convert.ToInt32(
                dataGridUserGroupsActive.SelectedRows[0].Cells["id"].Value
            );

            string query = "UPDATE user_groups SET isActive = 0 WHERE id = @id";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", selectedId);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Group deactivated successfully.");

                    // Refresh both grids
                    LoadGroups();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnDeactivategrp_Click(object sender, EventArgs e)
        {
            // Ensure a row is selected in the inactive groups grid
            if (dataGridInactiveGroups.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a group to activate.");
                return;
            }

            // Get selected ID
            int selectedId = Convert.ToInt32(
                dataGridInactiveGroups.SelectedRows[0].Cells["id"].Value
            );

            string query = "UPDATE user_groups SET isActive = 1 WHERE id = @id";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", selectedId);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Group activated successfully.");

                    // Refresh the grids again
                    LoadGroups();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnAddGrp_Click(object sender, EventArgs e)
        {
            string name = txtGroupName.Text.Trim();
            string desc = txtGroupDesc.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Group name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGroupName.Focus();
                return;
            }

            AddUserGroup(name, string.IsNullOrWhiteSpace(desc) ? null : desc);
        }

   
        private void dataGridViewActiveRoles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewInactiveRoles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnDeactiveteRole_Click(object sender, EventArgs e)
        {
            if (dataGridViewActiveRoles.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a role to deactivate.");
                return;
            }

            int selectedId = Convert.ToInt32(
                dataGridViewActiveRoles.SelectedRows[0].Cells["id"].Value
            );

            string query = "UPDATE roles SET isActive = 0 WHERE id = @id";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", selectedId);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Role deactivated successfully.");

                    // refresh
                    LoadRoles();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnActiveteRoles_Click(object sender, EventArgs e)
        {
            if (dataGridViewInactiveRoles.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a role to activate.");
                return;
            }

            int selectedId = Convert.ToInt32(
                dataGridViewInactiveRoles.SelectedRows[0].Cells["id"].Value
            );

            string query = "UPDATE roles SET isActive = 1 WHERE id = @id";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", selectedId);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Role activated successfully.");

                    // refresh
                    LoadRoles();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void LoadRoles()
        {
            string queryActive = "SELECT * FROM roles WHERE isActive = 1";
            string queryInactive = "SELECT * FROM roles WHERE isActive = 0";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();

                    // Load ACTIVE roles
                    using (SqlDataAdapter daActive = new SqlDataAdapter(queryActive, conn))
                    {
                        DataTable dtActive = new DataTable();
                        daActive.Fill(dtActive);
                        dataGridViewActiveRoles.DataSource = dtActive;
                    }

                    // Load INACTIVE roles
                    using (SqlDataAdapter daInactive = new SqlDataAdapter(queryInactive, conn))
                    {
                        DataTable dtInactive = new DataTable();
                        daInactive.Fill(dtInactive);
                        dataGridViewInactiveRoles.DataSource = dtInactive;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading roles: " + ex.Message);
                }
            }
        }

        private void toolStripRoles_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
           LoadRoles();

        }

     


        private void btnSavePermission_Click(object sender, EventArgs e)
        {
          
        
            if (comboRoles.SelectedValue == null || comboRoles.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a role first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int roleId = Convert.ToInt32(comboRoles.SelectedValue);

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    // 1. Delete existing permissions for this role
                    using (var cmdDelete = new SqlCommand("DELETE FROM dbo.RolePermissions WHERE role = @roleId", conn))
                    {
                        cmdDelete.Parameters.AddWithValue("@roleId", roleId);
                        cmdDelete.ExecuteNonQuery();
                    }

                    // 2. Insert checked permissions
                    using (var cmdInsert = new SqlCommand("INSERT INTO dbo.RolePermissions (role, permission_code) VALUES (@roleId, @permId)", conn))
                    {
                        cmdInsert.Parameters.Add("@roleId", SqlDbType.Int).Value = roleId;
                        cmdInsert.Parameters.Add("@permId", SqlDbType.Int);

                        for (int i = 0; i < clbPermissions.Items.Count; i++)
                        {
                            if (clbPermissions.GetItemChecked(i))
                            {
                                var item = (DataRowView)clbPermissions.Items[i];
                                int permId = Convert.ToInt32(item["id"]);
                                cmdInsert.Parameters["@permId"].Value = permId;
                                cmdInsert.ExecuteNonQuery();
                            }
                        }
                    }

                    MessageBox.Show("Permissions saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save permissions: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





        private void Load__Roles()
        {
            if (string.IsNullOrEmpty(_connectionString)) return;

            const string query = "SELECT id, name FROM dbo.roles WHERE isActive = 1 ORDER BY name";

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand(query, conn))
                {
                    var dt = new DataTable();
                    var adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);

                    comboRoles.DataSource = dt;
                    comboRoles.DisplayMember = "name";  // Shown to user
                    comboRoles.ValueMember = "id";      // Actual role ID
                    comboRoles.SelectedIndex = -1;      // No default selection
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load roles: {ex.Message}");
            }
        }

        private void LoadPermissionsForRole(int roleId)
        {
            if (string.IsNullOrEmpty(_connectionString)) return;

            const string queryPermissions = "SELECT id, name FROM dbo.permissions WHERE isActive = 1";
            const string queryRolePerms = "SELECT permission_code FROM dbo.RolePermissions WHERE role = @roleId";

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmdAll = new SqlCommand(queryPermissions, conn))
                using (var cmdAssigned = new SqlCommand(queryRolePerms, conn))
                {
                    cmdAssigned.Parameters.AddWithValue("@roleId", roleId);

                    var dtAll = new DataTable();
                    var dtAssigned = new DataTable();

                    var adapterAll = new SqlDataAdapter(cmdAll);
                    adapterAll.Fill(dtAll);

                    var adapterAssigned = new SqlDataAdapter(cmdAssigned);
                    adapterAssigned.Fill(dtAssigned);

                    clbPermissions.DataSource = dtAll;
                    clbPermissions.DisplayMember = "name";
                    clbPermissions.ValueMember = "id";

                    // Get all assigned permission IDs
                    var assignedIds = dtAssigned.AsEnumerable()
                                                .Select(r => Convert.ToInt32(r["permission_code"]))
                                                .ToList();

                    // Loop through all items and check/uncheck based on assigned permissions
                    for (int i = 0; i < clbPermissions.Items.Count; i++)
                    {
                        var item = (DataRowView)clbPermissions.Items[i];
                        int permId = Convert.ToInt32(item["id"]);
                        clbPermissions.SetItemChecked(i, assignedIds.Contains(permId));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load permissions: {ex.Message}");
            }
        }

        private void FormUserPermissions_Load(object sender, EventArgs e)
        {
            Load__Roles();
        }

        private void comboRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboRoles.SelectedValue != null && comboRoles.SelectedIndex != -1)
            {
                int roleId = Convert.ToInt32(comboRoles.SelectedValue);
                LoadPermissionsForRole(roleId);
            }
        }

        private void btnAddRoleActive_Click(object sender, EventArgs e)
        {
            string name = txtRoleName.Text.Trim();
            string desc = txtRoleDesc.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Role name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRoleName.Focus();
                return;
            }

            AddRole(name, string.IsNullOrWhiteSpace(desc) ? null : desc);
        }

    
        private void btnRefreshPermissions_Click(object sender, EventArgs e)
        {

        }

        private void LoadComboSelectGroup()
        {
            if (string.IsNullOrEmpty(_connectionString)) return;

            const string query = "SELECT id, name FROM [user_groups] WHERE isActive = 1 ORDER BY name";

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand(query, conn))
                {
                    var dt = new DataTable();
                    var adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);

                    comboSelectGroup.DataSource = dt;
                    comboSelectGroup.DisplayMember = "name"; // shown to user
                    comboSelectGroup.ValueMember = "id";     // actual group ID
                    comboSelectGroup.SelectedIndex = -1;     // no default selection
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load groups: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogTrace($"LoadComboSelectGroup error: {ex.Message}");
            }
        }

        private void comboSelectGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboSelectGroup.SelectedValue == null) return;

                // Get selected group ID
                int groupId = Convert.ToInt32(comboSelectGroup.SelectedValue);

                // Load all roles
                const string roleSql = "SELECT id, name FROM roles ORDER BY name";
                DataTable allRoles = new DataTable();
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand(roleSql, conn))
                using (var adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(allRoles);
                }

                // Load roles assigned to the selected group
                const string assignedSql = @"
            SELECT role_id 
            FROM group_roles 
            WHERE group_id = @GroupId";

                HashSet<int> assignedRoleIds = new HashSet<int>();
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand(assignedSql, conn))
                {
                    cmd.Parameters.AddWithValue("@GroupId", groupId);
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            assignedRoleIds.Add(reader.GetInt32(0));
                        }
                    }
                }

                // Bind roles to CheckedListBox (display role names)
                clbRoles.DataSource = allRoles;
                clbRoles.DisplayMember = "name"; // THIS ensures role names are displayed
                clbRoles.ValueMember = "id";

                // Check the roles that are assigned
                for (int i = 0; i < clbRoles.Items.Count; i++)
                {
                    var row = (DataRowView)clbRoles.Items[i];
                    int roleId = Convert.ToInt32(row["id"]);
                    clbRoles.SetItemChecked(i, assignedRoleIds.Contains(roleId));
                }
            }
            catch (Exception ex)
            {
                LogTrace($"comboSelectGroup_SelectedIndexChanged error: {ex.Message}");
                MessageBox.Show("Failed to load group roles. See log for details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAssignRoles_Click(object sender, EventArgs e)
        {
      
            try
            {
                if (comboSelectGroup.SelectedValue == null)
                {
                    MessageBox.Show("Please select a group first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int groupId = Convert.ToInt32(comboSelectGroup.SelectedValue);

                // Collect selected role IDs
                List<int> selectedRoleIds = new List<int>();
                for (int i = 0; i < clbRoles.Items.Count; i++)
                {
                    if (clbRoles.GetItemChecked(i))
                    {
                        var row = (DataRowView)clbRoles.Items[i];
                        selectedRoleIds.Add(Convert.ToInt32(row["id"]));
                    }
                }

                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    // Remove existing roles for this group first
                    using (var deleteCmd = new SqlCommand("DELETE FROM group_roles WHERE group_id = @GroupId", conn))
                    {
                        deleteCmd.Parameters.AddWithValue("@GroupId", groupId);
                        deleteCmd.ExecuteNonQuery();
                    }

                    // Insert new role assignments
                    foreach (int roleId in selectedRoleIds)
                    {
                        using (var insertCmd = new SqlCommand(
                            "INSERT INTO group_roles (group_id, role_id) VALUES (@GroupId, @RoleId)", conn))
                        {
                            insertCmd.Parameters.AddWithValue("@GroupId", groupId);
                            insertCmd.Parameters.AddWithValue("@RoleId", roleId);
                            insertCmd.ExecuteNonQuery();
                        }
                    }
                }

                MessageBox.Show("Roles successfully assigned to group.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LogTrace($"Assigned {selectedRoleIds.Count} roles to group {groupId}.");
            }
            catch (Exception ex)
            {
                LogTrace($"btnAssignRoles_Click error: {ex.Message}");
                MessageBox.Show("Failed to assign roles. See log for details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        

    }

        private void BtnRefreshGrid_Click(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private int? selectedUserId = null;


        private void dataGridViewUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dataGridViewUsers.Rows[e.RowIndex];

            selectedUserId = Convert.ToInt32(row.Cells["EmployeeID"].Value);

            txtUsername.Text = row.Cells["Username"].Value?.ToString();
            txtEmail.Text = row.Cells["Email"].Value?.ToString();
            txtFirstName.Text = row.Cells["FirstName"].Value?.ToString();
            txtLastName.Text = row.Cells["LastName"].Value?.ToString();

            CheckActiveUser.Checked = Convert.ToBoolean(row.Cells["Active"].Value);

            comboUserGroup.Text = row.Cells["UserGroup"].Value?.ToString();
            comboRole.Text = row.Cells["Role"].Value?.ToString();

            txtPassword.Text = ""; // never load password
        }


        private void btnEditUsers_Click(object sender, EventArgs e)
        {
            txtEmail.Enabled = true;
            txtFirstName.Enabled = true;
            txtLastName.Enabled = true;
            txtUsername.Enabled = true;
            comboUserGroup.Enabled = true;
            comboRole.Enabled = true;
            CheckActiveUser.Enabled = true;
            CheckActiveUser.Enabled = true;
        }

        private bool UserExistsForEdit(string username, string email, int userId)
        {
            string query = @"
        SELECT COUNT(*) 
        FROM [users]
        WHERE (username = @username OR email = @email)
        AND id <> @id";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@id", userId);

                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                if (count > 0)
                {
                    MessageBox.Show("Another user with the same username or email already exists.",
                        "Duplicate Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return true;
                }
            }

            return false;
        }

        private void dataGridViewUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dataGridViewUsers.Rows[e.RowIndex];

            selectedUserId = Convert.ToInt32(row.Cells["EmployeeID"].Value);

            lblIDuser.Text = $"ID : {selectedUserId.ToString()}";

            txtUsername.Text = row.Cells["Username"].Value?.ToString();
            txtEmail.Text = row.Cells["Email"].Value?.ToString();
            txtFirstName.Text = row.Cells["FirstName"].Value?.ToString();
            txtLastName.Text = row.Cells["LastName"].Value?.ToString();

            CheckActiveUser.Checked = Convert.ToBoolean(row.Cells["Active"].Value);

            comboUserGroup.Text = row.Cells["UserGroup"].Value?.ToString();
            comboRole.Text = row.Cells["Role"].Value?.ToString();

            txtPassword.Text = "";
        }

        private void txtLastName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnNewUserReg_Click(object sender, EventArgs e)
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtUsername.Text = "";
            txtEmail.Text = "";
            txtPassword.Text = "";
            selectedUserId = null;
            comboRole.SelectedIndex = -1;
            comboUserGroup.SelectedIndex = -1;

            txtEmail.Enabled = true;
            txtFirstName.Enabled = true;
            txtLastName.Enabled = true;
            txtUsername.Enabled = true;
            comboUserGroup.Enabled = true;
            comboRole.Enabled = true;
            CheckActiveUser.Enabled = true;
            CheckActiveUser.Enabled = true;
            txtPassword.Enabled = true;


        }
    }




}