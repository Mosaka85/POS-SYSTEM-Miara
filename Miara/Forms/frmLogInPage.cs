using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Miara
{
    public partial class frmLogInPage : Form
    {
        private static readonly string configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config.xml");
        private string SQLservername;
        private string SQLDatabase;
        private string SQLUsername;
        private string SQLPassword;
        private string employeeFirstName;
        private string employeeSurname;
        private int EMID = 0;

        public frmLogInPage()
        {
            InitializeComponent();
            LoadSQLConnectionInfo();
            this.ContextMenuStrip = contextMenuStrip1;
            this.MouseUp += new MouseEventHandler(frmLogInPage_MouseUp);

            // Attach KeyDown event handlers to the textboxes
            txtEmployeeUsername.KeyDown += new KeyEventHandler(Textbox_KeyDown);
            txtEmployeePassword.KeyDown += new KeyEventHandler(Textbox_KeyDown);
        }

        private void Textbox_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the "Enter" key was pressed
            if (e.KeyCode == Keys.Enter)
            {
                // Trigger the login button click event
                button1_Click(sender, e);
            }
        }

        private void LoadSQLConnectionInfo()
        {
            if (!File.Exists(configFile))
            {
                MessageBox.Show("Connection configuration file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (FileStream fileStream = new FileStream(configFile, FileMode.Open))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(LoginInfo));
                    LoginInfo loginInfo = (LoginInfo)serializer.Deserialize(fileStream);

                    if (loginInfo != null)
                    {
                        SQLservername = loginInfo.DataSource;
                        SQLDatabase = loginInfo.SelectedDatabase;
                        SQLUsername = loginInfo.Username;
                        SQLPassword = loginInfo.Password;
                    }
                    else
                    {
                        MessageBox.Show("Failed to load connection information. The configuration file may be corrupted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load connection information: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string employeeUsername = txtEmployeeUsername.Text.Trim();
            string employeePassword = txtEmployeePassword.Text.Trim();

            if (AuthenticateUser(employeeUsername, employeePassword))
            {
                Hide();
                new frmMainForm(employeeFirstName, employeeSurname, EMID).Show();
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool AuthenticateUser(string username, string password)
        {
            string passwordHash = HashPassword(password);

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = SQLservername,
                InitialCatalog = SQLDatabase,
                UserID = SQLUsername,
                Password = SQLPassword,
                PersistSecurityInfo = false
            };

            string query = @"
        SELECT EmployeeID, EmployeeFirstName, EmployeeSurname, Role
        FROM Employees 
        WHERE Username = @Username 
        AND PasswordHash = @PasswordHash 
        AND IsActive = 1";

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@PasswordHash", passwordHash);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int employeeID = Convert.ToInt32(reader["EmployeeID"]);
                            string firstName = reader["EmployeeFirstName"].ToString();
                            string surname = reader["EmployeeSurname"].ToString();
                            string role = reader["Role"].ToString();
                            employeeFirstName = firstName;
                            employeeSurname = surname;

                            MessageBox.Show($"Login successful.\nEmployee: {firstName} {surname}\nRole: {role}",
                                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            LogLoginAttempt(username, true, employeeID, "Login successful");

                            DialogResult posDisclaimerResult = MessageBox.Show(
                                "POS Disclaimer: By using this system, you agree to comply with all company policies and procedures related to the Point of Sale system. Any unauthorized use or misuse of this system may result in disciplinary action.",
                                "POS Disclaimer",
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Warning);

                            if (posDisclaimerResult == DialogResult.Cancel)
                            {
                                LogLoginAttempt(username, false, employeeID, "User canceled POS disclaimer");
                                return false;
                            }

                            DialogResult honestyDisclaimerResult = MessageBox.Show(
                                "Honesty Disclaimer: You are required to be honest and truthful in all transactions and interactions within this system. Any form of dishonesty or fraud will be dealt with severely and may result in termination of employment and legal action.",
                                "Honesty Disclaimer",
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Warning);

                            if (honestyDisclaimerResult == DialogResult.Cancel)
                            {
                                LogLoginAttempt(username, false, employeeID, "User canceled Honesty disclaimer");
                                return false;
                            }

                            return true;
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            LogLoginAttempt(username, false, null, "Invalid username or password");
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LogLoginAttempt(username, false, null, $"Error: {ex.Message}");
                    return false;
                }
            }
        }

        private void LogLoginAttempt(string username, bool isSuccess, int? employeeID, string details)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = SQLservername,
                InitialCatalog = SQLDatabase,
                UserID = SQLUsername,
                Password = SQLPassword,
                PersistSecurityInfo = false
            };

            string query = @"
        INSERT INTO LoginAudit (Username, AttemptTimestamp, IsSuccess, EmployeeID, Details)
        VALUES (@Username, @AttemptTimestamp, @IsSuccess, @EmployeeID, @Details)";

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@AttemptTimestamp", DateTime.Now);
                command.Parameters.AddWithValue("@IsSuccess", isSuccess);
                command.Parameters.AddWithValue("@EmployeeID", (object)employeeID ?? DBNull.Value);
                command.Parameters.AddWithValue("@Details", details);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to log login attempt: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (contextMenuStrip1.Items.Count == 0)
            {
                e.Cancel = true;
            }
        }

        private void configurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmConfigPassword().Show();
        }

        private void frmLogInPage_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && contextMenuStrip1 != null)
            {
                contextMenuStrip1.Show(this, e.Location);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutForm = new AboutBox1();
            aboutForm.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
        }

        private void btnFogortPassword_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmResetPassword newForm = new frmResetPassword();
            newForm.ShowDialog();
        }
    }

    [Serializable]
    public class LoginInfo
    {
        public string DataSource { get; set; }
        public string SelectedDatabase { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}