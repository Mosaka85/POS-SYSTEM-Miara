using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
            LoadUsers(); // Load users into DataGridView on form load
        }

        /// <summary>
        /// Loads the SQL connection string from the configuration file.
        /// </summary>
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

        /// <summary>
        /// Loads user groups from the database into the combo box.
        /// </summary>
        private void LoadUserGroups()
        {
            LoadDataIntoComboBox("SELECT id, name FROM [user_groups]", comboUserGroup, "name", "id");
        }

        /// <summary>
        /// Loads user roles from the database into the combo box.
        /// </summary>
        private void LoadUserRoles()
        {
            LoadDataIntoComboBox("SELECT id, name FROM [roles]", comboRole, "name", "id");
        }

        /// <summary>
        /// Loads users from the database into the DataGridView.
        /// </summary>
        private void LoadUsers(string searchText = "")
        {
            if (string.IsNullOrEmpty(_connectionString)) return;

            string query = @"
                SELECT 
        u.id AS EmployeeID,
        u.first_name AS EmployeeFirstName,
        u.last_name AS EmployeeSurname,
        r.name AS Role
    FROM [users] u
    LEFT JOIN[user_groups] ug 
        ON u.user_group_id = ug.id
    LEFT JOIN[group_roles] gr 
        ON ug.id = gr.group_id
    LEFT JOIN[roles] r 
        ON gr.role_id = r.id
    WHERE  u.username LIKE @searchText OR u.email LIKE @searchText OR u.first_name LIKE @searchText OR u.last_name LIKE @searchText";

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@searchText", $"%{searchText}%");
                    var dt = new DataTable();
                    new SqlDataAdapter(cmd).Fill(dt);
                    dataGridViewUsers.DataSource = dt;
                    // Configure DataGridView columns
                    dataGridViewUsers.Columns["id"].Visible = false;
                    dataGridViewUsers.Columns["username"].HeaderText = "Username";
                    dataGridViewUsers.Columns["email"].HeaderText = "Email";
                    dataGridViewUsers.Columns["first_name"].HeaderText = "First Name";
                    dataGridViewUsers.Columns["last_name"].HeaderText = "Last Name";
                    dataGridViewUsers.Columns["active"].HeaderText = "Active";
                    dataGridViewUsers.Columns["group_name"].HeaderText = "Group";
                    dataGridViewUsers.Columns["role_name"].HeaderText = "Role";
                }
            }
            catch (Exception ex)
            {
                LogAuditEntry("Failed to load users", ex.Message);
                LogTrace($"Failed to load users: {ex.Message}");
                MessageBox.Show($"Failed to load users: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Generic method to load data from the database into a ComboBox.
        /// </summary>
        /// <param name="query">The SQL query to execute.</param>
        /// <param name="comboBox">The ComboBox to populate.</param>
        /// <param name="displayMember">The column name to display to the user.</param>
        /// <param name="valueMember">The column name to use for the value.</param>
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
                    comboBox.DisplayMember = displayMember;
                    comboBox.ValueMember = valueMember;
                    comboBox.SelectedIndex = -1; // No default selection
                }
            }
            catch (Exception ex)
            {
                LogAuditEntry($"Failed to load {comboBox.Name} data", ex.Message);
                LogTrace($"Failed to load {comboBox.Name} data: {ex.Message}");
                MessageBox.Show($"Failed to load {comboBox.Name} data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the click event for the 'Add New User' button.
        /// </summary>
       

        /// <summary>
        /// Handles the click event for the 'Cancel' button.
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            ResetFormFields();
        }

        /// <summary>
        /// Handles the TextChanged event for the search box.
        /// </summary>
        private void txtSearchUsers_TextChanged(object sender, EventArgs e)
        {
            LoadUsers(txtSearchUsers.Text.Trim());
        }

        /// <summary>
        /// Checks if a username or email already exists in the database.
        /// </summary>
        /// <param name="username">The username to check.</param>
        /// <param name="email">The email to check.</param>
        /// <returns>True if the user exists, otherwise false.</returns>
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

        /// <summary>
        /// Resets the input fields on the form.
        /// </summary>
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

        /// <summary>
        /// Logs an audit entry to the DeviceAudit table.
        /// </summary>
        /// <param name="stepDescription">Description of the action or step.</param>
        /// <param name="errorMessage">Optional error message if an error occurred.</param>
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

        /// <summary>
        /// Logs a message to a local text file.
        /// </summary>
        /// <param name="message">The message to log.</param>
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

        /// <summary>
        /// Hashes a password using BCrypt.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <returns>The hashed password.</returns>
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
            // Validate required fields
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Username and Password are required fields.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string username = txtUsername.Text.Trim();
            string email = txtEmail.Text.Trim();
            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            object userGroupId = comboUserGroup.SelectedValue ?? DBNull.Value;
            object roleId = comboRole.SelectedValue ?? DBNull.Value;
            bool isActive = CheckActiveUser.Checked;

            // Check if the username or email already exists
            if (UserExists(username, email))
            {
                return; // UserExists should handle the warning
            }

            string query = @"
        INSERT INTO [users] 
        (username, email, password_hash, user_group_id, created_at, active, first_name, last_name)
        VALUES 
        (@username, @email, @password_hash, @user_group_id, @role_id, @created_at, @active, @first_name, @last_name)";

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand(query, conn))
                {
                    // Parameters
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@email", string.IsNullOrWhiteSpace(email) ? (object)DBNull.Value : email);
                    cmd.Parameters.AddWithValue("@password_hash", HashPassword(txtPassword.Text));
                    cmd.Parameters.AddWithValue("@user_group_id", userGroupId);
                    cmd.Parameters.AddWithValue("@role_id", roleId);
                    cmd.Parameters.AddWithValue("@created_at", DateTime.Now);
                    cmd.Parameters.AddWithValue("@active", isActive);
                    cmd.Parameters.AddWithValue("@first_name", string.IsNullOrWhiteSpace(firstName) ? (object)DBNull.Value : firstName);
                    cmd.Parameters.AddWithValue("@last_name", string.IsNullOrWhiteSpace(lastName) ? (object)DBNull.Value : lastName);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("User added successfully.", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LogAuditEntry($"Added new user: {username}");
                        LogTrace($"Added new user: {username}");

                        ResetFormFields();
                        LoadUsers(); // Refresh DataGridView
                    }
                    else
                    {
                        MessageBox.Show("Failed to add user.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);

                        LogAuditEntry($"Failed to add user: {username}");
                        LogTrace($"Failed to add user: {username}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding user: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                LogAuditEntry($"Error adding user: {username}", ex.Message);
                LogTrace($"Error adding user: {ex.Message}");
            }
        }

        private void btnRefreshUsers_Click(object sender, EventArgs e)
        {
            
        }
    }
}