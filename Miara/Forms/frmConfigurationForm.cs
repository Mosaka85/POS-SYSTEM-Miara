using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Miara
{
    public partial class frmConfigurationForm : Form
    {
        // Use a relative path or dynamically resolve the file location
        private static readonly string configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config.xml");

        public frmConfigurationForm()
        {
            InitializeComponent();
        }

        private void btnTestSQL_Click(object sender, EventArgs e)
        {
            string connectionString = GetConnectionString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    PopulateDatabaseComboBox(connection);
                    MessageBox.Show("Connection successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Connection failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void PopulateDatabaseComboBox(SqlConnection connection)
        {
            DataTable databases = connection.GetSchema("Databases");
            comboDatabase.Items.Clear();

            foreach (DataRow database in databases.Rows)
            {
                comboDatabase.Items.Add(database.Field<string>("database_name"));
            }
        }



        private string GetConnectionString()
        {
            string dataSource = txtSQLDataSource.Text;
            string username = txtSQLusername.Text;
            string password = txtPassword.Text;

            // Use a SqlConnectionStringBuilder for better security and flexibility
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = dataSource,
                UserID = username,
                Password = password
            };

            return builder.ConnectionString;
        }

        private void SaveLoginInfo()
        {
            LoginInfo loginInfo = new LoginInfo
            {
                DataSource = txtSQLDataSource.Text,
                Username = txtSQLusername.Text,
                Password = txtPassword.Text,
                SelectedDatabase = comboDatabase.Text
            };

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(LoginInfo));
                using (FileStream fileStream = new FileStream(configFile, FileMode.Create))
                {
                    serializer.Serialize(fileStream, loginInfo);
                }

                MessageBox.Show("Connection information saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save connection information. Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmConfigurationForm_Load(object sender, EventArgs e)
        {
            LoadLoginInfo();
        }

        private void LoadLoginInfo()
        {
            if (!File.Exists(configFile))
            {
                // Create a default Config.xml file if it doesn't exist
                CreateDefaultConfigFile();
                MessageBox.Show("No saved login information found. A default configuration file has been created.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(LoginInfo));
                using (FileStream fileStream = new FileStream(configFile, FileMode.Open))
                {
                    LoginInfo loginInfo = (LoginInfo)serializer.Deserialize(fileStream);

                    txtSQLDataSource.Text = loginInfo.DataSource;
                    txtSQLusername.Text = loginInfo.Username;
                    txtPassword.Text = loginInfo.Password;
                    comboDatabase.Text = loginInfo.SelectedDatabase;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load connection information. Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateDefaultConfigFile()
        {
            LoginInfo defaultLoginInfo = new LoginInfo
            {
                DataSource = "YourDefaultDataSource",
                Username = "YourDefaultUsername",
                Password = "YourDefaultPassword",
                SelectedDatabase = "YourDefaultDatabase"
            };

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(LoginInfo));
                using (FileStream fileStream = new FileStream(configFile, FileMode.Create))
                {
                    serializer.Serialize(fileStream, defaultLoginInfo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to create default configuration file. Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCloseSQLfrm_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void btnSQLSAVE_Click(object sender, EventArgs e)
        {
            SaveLoginInfo();
        }

        private void frmConfigurationForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}