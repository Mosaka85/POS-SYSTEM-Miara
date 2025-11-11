using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Miara
{
    public partial class frmMainForm : Form
    {
        private const string ConfigFile = "Config.xml";
        private readonly string _connectionString;
        private readonly string _employeeFirstName;
        private readonly string _employeeSurname;
        private readonly int _employeeNumber;
        private readonly string _deviceInternetId;

        public frmMainForm(string firstName, string surname, int employeeId, string deviceInternetId)
        {
            ValidateConstructorParameters(firstName, surname, employeeId, deviceInternetId);

            InitializeComponent();
            _employeeFirstName = firstName;
            _employeeSurname = surname;
            _employeeNumber = employeeId;
            _deviceInternetId = deviceInternetId;

            lblName.Text = $"WELCOME {firstName} {surname}";
            this.FormClosed += (sender, e) => Application.Exit();

            _connectionString = LoadSqlConnectionInfo();
        }

        private void frmMainForm_Load(object sender, EventArgs e)
        {
            LoadEmployeeImageAsync().ConfigureAwait(false);
        }

     

        #region Helper Methods
        private void ValidateConstructorParameters(string firstName, string surname, int employeeId, string deviceInternetId)
        {
            if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("First name cannot be empty.", nameof(firstName));
            if (string.IsNullOrWhiteSpace(surname)) throw new ArgumentException("Surname cannot be empty.", nameof(surname));
            if (employeeId <= 0) throw new ArgumentException("Employee ID must be a positive integer.", nameof(employeeId));
            if (string.IsNullOrWhiteSpace(deviceInternetId)) throw new ArgumentException("Device Internet ID cannot be empty.", nameof(deviceInternetId));
        }

        /// <summary>
        /// Opens a form of type T in a dialog, logging the action.
        /// </summary>
        private async void OpenForm<T>(Func<Form> formFactory) where T : Form
        {
            try
            {
                using (var form = formFactory())
                {
                    await LogAuditEntryAsync(_deviceInternetId, $"{_employeeFirstName} {_employeeSurname}",
                        $"Opened {typeof(T).Name}");
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                await LogAuditEntryAsync(_deviceInternetId, $"{_employeeFirstName} {_employeeSurname}",
                    $"Failed to open {typeof(T).Name}", ex.Message);
                MessageBox.Show($"Error opening form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Loads SQL connection string from the configuration file.
        /// </summary>
        private string LoadSqlConnectionInfo()
        {
            string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigFile);
            if (!File.Exists(configPath))
            {
                MessageBox.Show("Connection configuration file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }

            try
            {
                using (var fileStream = new FileStream(configPath, FileMode.Open, FileAccess.Read))
                {
                    var serializer = new XmlSerializer(typeof(LoginInfo));
                    var loginInfo = (LoginInfo)serializer.Deserialize(fileStream);
                    return $"Data Source={loginInfo.DataSource};Initial Catalog={loginInfo.SelectedDatabase};User ID={loginInfo.Username};Password={loginInfo.Password}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load connection information: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }

        /// <summary>
        /// Loads the employee's image from the database asynchronously.
        /// </summary>
        private async Task LoadEmployeeImageAsync()
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                MessageBox.Show("Invalid database connection configuration.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    const string query = @"
                        SELECT TOP 1 ImageData 
                        FROM ImageStore 
                        WHERE EmployeeID = @empId AND IsActive = 1";

                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@empId", _employeeNumber);
                        await conn.OpenAsync();
                        var result = await cmd.ExecuteScalarAsync();

                        if (result != null && result != DBNull.Value)
                        {
                            byte[] imgData = (byte[])result;
                            using (var ms = new MemoryStream(imgData))
                            {
                                pictureBox1.Image = Image.FromStream(ms);
                            }
                        }
                        else
                        {
                            MessageBox.Show("No active image found for this employee.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                await LogAuditEntryAsync(_deviceInternetId, $"{_employeeFirstName} {_employeeSurname}",
                    "Failed to load employee image", ex.Message);
                MessageBox.Show($"Database error loading image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                await LogAuditEntryAsync(_deviceInternetId, $"{_employeeFirstName} {_employeeSurname}",
                    "Failed to load employee image", ex.Message);
                MessageBox.Show($"Error loading image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Logs an audit entry to the database asynchronously.
        /// </summary>
        public async Task LogAuditEntryAsync(string device, string employee, string stepDescription, string errorMessage = null)
        {
            if (string.IsNullOrEmpty(_connectionString)) return;

            const string query = @"
                INSERT INTO DeviceAudit (Device, Employee, AuditDate, StepDescription, ErrorMessage)
                VALUES (@Device, @Employee, @AuditDate, @StepDescription, @ErrorMessage);";

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Device", device ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Employee", employee ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@AuditDate", DateTime.Now);
                    command.Parameters.AddWithValue("@StepDescription", stepDescription ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ErrorMessage", string.IsNullOrEmpty(errorMessage) ? (object)DBNull.Value : errorMessage);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Failed to log audit entry: {ex.Message}", "Logging Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        #region Button Click Handlers
        private void btnOpenPOS_Click(object sender, EventArgs e)
        {
            OpenForm<frmSales>(() => new frmSales(_employeeFirstName, _employeeSurname, _employeeNumber, _deviceInternetId));
        }

        private void btnManageProduct_Click(object sender, EventArgs e)
        {
            OpenForm<frmProducts>(() => new frmProducts(_employeeFirstName, _employeeSurname, _employeeNumber, _deviceInternetId));
        }

       

  

        private void manageCategory_Click(object sender, EventArgs e)
        {
            OpenForm<frmCategory>(() => new frmCategory(_employeeFirstName, _employeeSurname, _employeeNumber, _deviceInternetId));
        }

     

        private void btnRecords_Click(object sender, EventArgs e)
        {
            OpenForm<frmReports>(() => new frmReports(_employeeFirstName, _employeeSurname, _employeeNumber, _deviceInternetId));
        }

        private void btnSalesHistory_Click(object sender, EventArgs e)
        {
            OpenForm<frmReports>(() => new frmReports(_employeeFirstName, _employeeSurname, _employeeNumber, _deviceInternetId));
        }

        private void btnUserSetting_Click(object sender, EventArgs e)
        {
            OpenForm<FormUserPermissions>(() => new FormUserPermissions(_employeeFirstName, _employeeSurname, _employeeNumber, _deviceInternetId));
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void btnsettings_Click(object sender, EventArgs e)
        {
            // TODO: Implement store settings form if needed
            MessageBox.Show("Store settings functionality is not yet implemented.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion
    }


}