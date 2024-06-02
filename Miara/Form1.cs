using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace Miara
{
    public partial class frmLogInPage : Form
    {
        private const string configFile = @"C:\Users\TSHEP\source\repos\Miara\Miara\Config.xml"; // Adjust path as needed
        private string SQLservername;
        private string SQLDatabase;
        private string SQLUsername;
        private string SQLPassword;


        public frmLogInPage()
        {
            InitializeComponent();
            LoadSQLConnectionInfo(); // Load connection info when the form loads
            this.ContextMenuStrip = contextMenuStrip1;
            this.MouseUp += new MouseEventHandler(frmLogInPage_MouseUp);
        }

        
        private void LoadSQLConnectionInfo()
        {
            if (File.Exists(configFile))
            {
                try
                {
                    // Deserialize the XML file to get the connection information
                    XmlSerializer serializer = new XmlSerializer(typeof(LoginInfo));
                    using (FileStream fileStream = new FileStream(configFile, FileMode.Open))
                    {
                        LoginInfo loginInfo = (LoginInfo)serializer.Deserialize(fileStream);
                        SQLservername = loginInfo.DataSource;
                        SQLDatabase = loginInfo.SelectedDatabase;
                        SQLUsername = loginInfo.Username;
                        SQLPassword = loginInfo.Password;
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


        private void button1_Click(object sender, EventArgs e)
        {
            string employeePassword = txtEmployeePassword.Text;
            string employeeUsername = txtEmployeeUsername.Text;

            if (AuthenticateUser(employeeUsername, employeePassword))
                
            {
                Hide();
                new frmMainForm().Show();
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool AuthenticateUser(string username, string password)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = SQLservername;
            builder.InitialCatalog = SQLDatabase;
            builder.UserID = SQLUsername;
            builder.Password = SQLPassword;

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            using (SqlCommand command = new SqlCommand("SELECT Active FROM Employees WHERE LogInName = @Username", connection))
            {
                command.Parameters.AddWithValue("@Username", username);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar(); // Get the Active column value (should be an integer)

                    if (result == null || result == DBNull.Value) // Check for null or DBNull
                    {
                        MessageBox.Show("Invalid username.", "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else
                    {
                        // Convert the result to an integer before casting to a boolean
                        bool isActive = Convert.ToInt32(result) == 1;

                        if (!isActive)
                        {
                            MessageBox.Show("Your account is deactivated. Please contact the administrator.", "Account Deactivated", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                        return isActive; // Return true only if the user is active
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false; // Always return false in case of an error
                }
            }
        }



        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void configurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmConfigPassword().Show();
        }
        private void frmLogInPage_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(this, e.Location);
            }
        }
    }
}



