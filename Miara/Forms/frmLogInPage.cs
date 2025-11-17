using Miara.Forms;
using OfficeOpenXml.FormulaParsing.LexicalAnalysis;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Miara
{
    public partial class frmLogInPage : Form
    {

        private const string GetSessionIdQuery = @"
            INSERT INTO UserSessions (Device) 
            OUTPUT INSERTED.SessionID 
            VALUES (@Device);";
        private const string LogLoginAttemptQuery = @"
            INSERT INTO LoginAudit (Username, AttemptTimestamp, IsSuccess, EmployeeID, Details, GUID) 
            VALUES (@Username, @AttemptTimestamp, @IsSuccess, @EmployeeID, @Details, @GUID);";
        private const string GetPrinterNameQuery = "SELECT PrinterName FROM Printers WHERE DEVICE = @Device;";
        private const string LogDeviceAuditQuery = @"
            INSERT INTO DeviceAudit (Device, Employee, AuditDate, StepDescription, ErrorMessage) 
            VALUES (@Device, @Employee, @AuditDate, @StepDescription, @ErrorMessage);";
        private const string AuthenticateUserQuery = @"
    SELECT TOP 1 
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
    WHERE u.username = @Username 
        AND u.password_hash = @PasswordHash 
        AND u.active = 1;";

        private const string UpdateSessionWithEmployeeQuery = @"
    UPDATE UserSessions
    SET EmployeeID = @EmployeeID
    WHERE SessionID = @SessionID;";


        private static readonly string configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config.xml");
        private static readonly string logFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt");



        private static readonly SemaphoreSlim _logSemaphore = new SemaphoreSlim(1, 1);

        private string _connectionString;
        private string _employeeFirstName;
        private string _employeeSurname;
        private string _session;
        private int _emid = 0;
        private readonly string _currentDevice;
        private string _sessionID = Guid.Empty.ToString();


        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        public frmLogInPage()
        {
            InitializeComponent();
            _currentDevice = GetMacAddress();

        }


        private async void frmLogInPage_Load(object sender, EventArgs e)
        {

            try
            {
                lblDevice.Text = $"Device: {_currentDevice}";
                await WriteToLogFileAsync($"Application started on device: {_currentDevice}");

                await LoadSQLConnectionInfoAsync(_cts.Token);
                await WriteToLogFileAsync($"Loaded connection string.");

                Task printerTask = GetPrinterNameAsync(_currentDevice, _cts.Token);
                Task sessionTask = GetSessionIDAsync(_cts.Token);

                await Task.WhenAll(printerTask, sessionTask);

                ContextMenuStrip = contextMenuStrip1;
                MouseUp += frmLogInPage_MouseUp;
                txtEmployeeUsername.KeyDown += Textbox_KeyDown;
                txtEmployeePassword.KeyDown += Textbox_KeyDown;
                txtEmployeeUsername.Focus();
               // txtEmployeeUsername.Text= logFile;
            }
            catch (OperationCanceledException)
            {
                await WriteToLogFileAsync("Initialization was cancelled.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fatal initialization error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                await WriteToLogFileAsync($"Fatal initialization error: {ex.Message}");
                Application.Exit();
            }
        }

        private void Textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                button1_Click(sender, e);
            }
        }


        private async Task LoadSQLConnectionInfoAsync(CancellationToken token)
        {
            if (!File.Exists(configFile))
            {
                // FIX: Show error and open the configuration form if the file is not found.
                MessageBox.Show("Configuration file 'Config.xml' not found. Please set up the SQL connection.", "Configuration Missing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LogAuditEntryAsync(_currentDevice, "System", "Config file not found", "Configuration file missing", token);

                // Show the configuration form as a dialog.
                using (var configForm = new frmConfigurationForm())
                {
                    configForm.ShowDialog();
                }

                // After the user is done with the config form, check again.
                // If they still haven't created the file, throw an exception.
                if (!File.Exists(configFile))
                {
                    throw new FileNotFoundException("Connection configuration file was not created.");
                }
            }

            XmlSerializer serializer = new XmlSerializer(typeof(LoginInfo));
            using (FileStream fileStream = new FileStream(configFile, FileMode.Open))
            {
                LoginInfo loginInfo = (LoginInfo)await Task.Run(() => serializer.Deserialize(fileStream), token);
                _connectionString = $"Data Source={loginInfo.DataSource};Initial Catalog={loginInfo.SelectedDatabase};User ID={loginInfo.Username};Password={loginInfo.Password};TrustServerCertificate=True";
            }
        }

        private async Task GetSessionIDAsync(CancellationToken token)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand(GetSessionIdQuery, connection))
                {
                    command.Parameters.AddWithValue("@Device", _currentDevice);
                    await connection.OpenAsync(token);
                    object result = await command.ExecuteScalarAsync(token);
                    _sessionID = result?.ToString() ?? Guid.Empty.ToString();
                    lblSessionID.Text = $"Session ID: {_sessionID}";
                    _session = _sessionID;
                }
            }
            catch (Exception ex)
            {
                await WriteToLogFileAsync($"Error creating session for device {_currentDevice}: {ex.Message}");
                MessageBox.Show($"Failed to create session: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task GetPrinterNameAsync(string device, CancellationToken token)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync(token);
                    using (var command = new SqlCommand(GetPrinterNameQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Device", device);
                        var printerName = await command.ExecuteScalarAsync(token);
                        lblPrinter.Text = $"Printer: {printerName?.ToString() ?? "Not Configured"}";
                        await LogAuditEntryAsync(device, "System", "Printer configuration retrieved", $"Printer: {printerName?.ToString() ?? "Not Configured"}", token);
                    }
                }
            }
            catch (Exception ex)
            {
                await LogAuditEntryAsync(device, "System", "Error retrieving printer", ex.Message, token);
                MessageBox.Show($"Error retrieving printer: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string employeeUsername = txtEmployeeUsername.Text.Trim();
            string employeePassword = txtEmployeePassword.Text.Trim();
            
            if (string.IsNullOrEmpty(employeeUsername) || string.IsNullOrEmpty(employeePassword))
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                await WriteToLogFileAsync($"Login attempt started for user: {employeeUsername} on device {_currentDevice}");

                if (await AuthenticateUserAsync(employeeUsername, employeePassword, _cts.Token))
                {
                    this.Hide();
                    NameUser = _employeeFirstName;
                    SurnameUser = _employeeSurname;
                    EmployeeID = _emid;
                    ActiveUser = $"User: {_employeeFirstName} {_employeeSurname}, EMID: {_emid}";
                    await WriteToLogFileAsync($"Login successful for user: {employeeUsername} on device {_currentDevice}");
                    await UpdateSessionWithEmployeeAsync();
                    new frmMainForm(_employeeFirstName, _employeeSurname, _emid, _currentDevice).Show();
                }
            }
            catch (Exception ex)
            {
                await WriteToLogFileAsync($"Login process error for '{employeeUsername}': {ex.Message}");
                MessageBox.Show($"An error occurred during login: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<bool> AuthenticateUserAsync(string username, string password, CancellationToken token)
        {
            string passwordHash = HashPassword(password);

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(AuthenticateUserQuery, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@PasswordHash", passwordHash);

                await connection.OpenAsync(token);

                using (SqlDataReader reader = await command.ExecuteReaderAsync(token))
                {
                    if (!await reader.ReadAsync(token))
                    {
                        MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        await LogLoginAttemptAsync(username, false, null, "Invalid credentials", token);
                        return false;
                    }

                    _emid = Convert.ToInt32(reader["EmployeeID"]);
                    _employeeFirstName = reader["EmployeeFirstName"].ToString();
                    _employeeSurname = reader["EmployeeSurname"].ToString();
                    string role = reader["Role"].ToString();

                    await LogLoginAttemptAsync(username, true, _emid, "Login successful", token);

                    var posDisclaimerResult = ShowDisclaimer("POS Disclaimer", "By using this system, you agree to comply with all company policies...");
                    if (posDisclaimerResult == DialogResult.Cancel)
                    {
                        await LogLoginAttemptAsync(username, false, _emid, "User canceled POS disclaimer", token);
                        return false;
                    }

                    var honestyDisclaimerResult = ShowDisclaimer("Honesty Disclaimer", "You are required to be honest and truthful in all transactions...");
                    if (honestyDisclaimerResult == DialogResult.Cancel)
                    {
                        await LogLoginAttemptAsync(username, false, _emid, "User canceled Honesty disclaimer", token);
                        return false;
                    }

                    return true;
                }
            }
        }

        #region Logging and Utilities

        private async Task WriteToLogFileAsync(string message)
        {
            await _logSemaphore.WaitAsync();
            try
            {
                string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}{Environment.NewLine}";
                using (var writer = new StreamWriter(logFile, append: true))
                {
                    await writer.WriteAsync(logMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write to log file: {ex.Message}");
            }
            finally
            {
                _logSemaphore.Release();
            }
        }

        private async Task LogLoginAttemptAsync(string username, bool isSuccess, int? employeeID, string details, CancellationToken token)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand(LogLoginAttemptQuery, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@AttemptTimestamp", DateTime.Now);
                    command.Parameters.AddWithValue("@IsSuccess", isSuccess);
                    command.Parameters.AddWithValue("@EmployeeID", employeeID ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Details", details);
                    command.Parameters.AddWithValue("@GUID", _sessionID);

                    await connection.OpenAsync(token);
                    await command.ExecuteNonQueryAsync(token);
                }
            }
            catch (Exception ex)
            {
                await WriteToLogFileAsync($"Failed to log login attempt for {username}: {ex.Message}");
            }
        }

        public async Task LogAuditEntryAsync(string device, string employee, string stepDescription, string errorMessage, CancellationToken token)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand(LogDeviceAuditQuery, connection))
                {
                    command.Parameters.AddWithValue("@Device", device);
                    command.Parameters.AddWithValue("@Employee", employee);
                    command.Parameters.AddWithValue("@AuditDate", DateTime.Now);
                    command.Parameters.AddWithValue("@StepDescription", stepDescription);
                    command.Parameters.AddWithValue("@ErrorMessage", string.IsNullOrEmpty(errorMessage) ? DBNull.Value : (object)errorMessage);

                    await connection.OpenAsync(token);
                    await command.ExecuteNonQueryAsync(token);
                }
            }
            catch (Exception ex)
            {
                await WriteToLogFileAsync($"Failed to log audit entry for {employee} on {device}: {ex.Message}");
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return string.Concat(hashBytes.Select(b => b.ToString("x2")));
            }
        }


        private static string GetMacAddress()
        {
            return NetworkInterface.GetAllNetworkInterfaces()
                .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .Select(nic => nic.GetPhysicalAddress().ToString())
                .FirstOrDefault() ?? "MAC-NOT-FOUND";
        }


        private DialogResult ShowDisclaimer(string title, string message)
        {
            return MessageBox.Show($"{message}\n\nAny unauthorized use or misuse may result in disciplinary action.",
                title, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {

            _cts.Cancel();
            _cts.Dispose();
            base.OnFormClosing(e);
        }

        #endregion

        #region UI Event Handlers (Unchanged)
        private void btnExit_Click(object sender, EventArgs e) => Application.Exit();
        private void frmLogInPage_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(this, e.Location);
            }
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) => new AboutBox1().ShowDialog();
        private void btnFogortPassword_Click(object sender, EventArgs e) => new frmResetPassword().ShowDialog();
        private void sQLConnectionToolStripMenuItem_Click(object sender, EventArgs e) => new frmConfigurationForm().Show();
        private void databaseSetupToolStripMenuItem_Click(object sender, EventArgs e) => new TableDefinition().Show();
        private void printerSetupToolStripMenuItem_Click(object sender, EventArgs e) => new frmPrinter(_currentDevice).Show();
        private void button2_Click_1(object sender, EventArgs e) => Application.Exit();
        #endregion

        private void lblPrinter_Click(object sender, EventArgs e)
        {

        }
        private async Task UpdateSessionWithEmployeeAsync(int employeeId,string SessionVar, CancellationToken token)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand(UpdateSessionWithEmployeeQuery, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeID", employeeId);
                    command.Parameters.AddWithValue("@SessionID", SessionVar ?? (object)DBNull.Value);

                    await connection.OpenAsync(token);
                    int rows = await command.ExecuteNonQueryAsync(token);

                    if (rows <= 0)
                    {
                        await WriteToLogFileAsync($"Warning: no session row was updated for SessionID={SessionVar} (EmployeeID={employeeId}).");
                    }
                    else
                    {
                        await WriteToLogFileAsync($"Session updated: SessionID={SessionVar} set to EmployeeID={employeeId}.");
                    }
                }
            }
            catch (Exception ex)
            {
                await WriteToLogFileAsync($"Failed to update session {SessionVar} with EmployeeID {employeeId}: {ex.Message}");
                // Do not throw — allow login to proceed even if session update fails.
            }
        }


        private async Task UpdateSessionWithEmployeeAsync()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand(UpdateSessionWithEmployeeQuery, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeID", _emid);
                    command.Parameters.AddWithValue("@SessionID", _session ?? (object)DBNull.Value);

                    await connection.OpenAsync();
                    int rows = await command.ExecuteNonQueryAsync();

                    if (rows <= 0)
                    {
                        await WriteToLogFileAsync($"Warning: no session row was updated for SessionID={_session} (EmployeeID={_emid}).");
                    }
                    else
                    {
                        await WriteToLogFileAsync($"Session updated: SessionID={_session} set to EmployeeID={_emid}.");
                    }
                }
            }
            catch (Exception ex)
            { 
               
                await WriteToLogFileAsync($"Failed to update session {_session} with EmployeeID {_emid}: {ex.Message}");
            
            }
        }




        private void generalSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormApplicationSettings formApplicationSettings = new FormApplicationSettings();
            formApplicationSettings.Show();

        }

        public string ActiveUser { get; private set; }
        public string NameUser { get; private set; }
        public string SurnameUser { get; private set; }
        public int EmployeeID { get; private set; } 
        public string currentDevice { get; private set; } = GetMacAddress();
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