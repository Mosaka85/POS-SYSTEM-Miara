using Miara.Forms;
using Miara.Models;
using Miara.Services;
using Miara.Utilities;
using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using static Miara.Utilities.AppUtilities; 

namespace Miara
{
    
    public partial class frmLogInPage : Form
    {
        private readonly string _configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config.xml");

        private SqlService _sqlService;
        private readonly string _currentDevice;
        private string _sessionID = Guid.Empty.ToString();

        private EmployeeDetails _employee;

        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        public string ActiveUser => _employee != null ? $"User: {_employee.FirstName} {_employee.Surname}, EMID: {_employee.EmployeeID}" : "User: Not Logged In";
        public string NameUser => _employee?.FirstName ?? string.Empty;
        public string SurnameUser => _employee?.Surname ?? string.Empty;
        public int EmployeeID => _employee?.EmployeeID ?? 0;
        public string CurrentDevice => _currentDevice;


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

                // 1. Load Configuration
                string connectionString = await LoadSQLConnectionInfoAsync(_cts.Token);
                _sqlService = new SqlService(connectionString);
                await WriteToLogFileAsync("Connection string initialized.");

                // 2. Load dynamic data in parallel
                Task printerTask = LoadPrinterInfoAsync(_cts.Token);
                Task sessionTask = LoadSessionInfoAsync(_cts.Token);

                await Task.WhenAll(printerTask, sessionTask);

                // 3. Set up UI
                ContextMenuStrip = contextMenuStrip1; 
                MouseUp += frmLogInPage_MouseUp;
                txtEmployeeUsername.KeyDown += Textbox_KeyDown; 
                txtEmployeePassword.KeyDown += Textbox_KeyDown;
                txtEmployeeUsername.Focus();
            }
            catch (OperationCanceledException)
            {
                await WriteToLogFileAsync("Initialization was cancelled.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fatal initialization error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                await WriteToLogFileAsync($"Fatal initialization error: {ex.Message}");
             
                if (_sqlService == null || _sessionID == Guid.Empty.ToString())
                {
                    Application.Exit();
                }
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

        private async Task<string> LoadSQLConnectionInfoAsync(CancellationToken token)
        {
            var configService = new ConfigService(_configFile);

            // 🔹 CASE 1: Config missing → prompt user to create it
            if (!configService.ConfigExists)
            {
                MessageBox.Show(
                    "Configuration file 'Config.xml' not found. Please set up the SQL connection.",
                    "Configuration Missing",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                using (var configForm = new frmConfigurationForm())
                {
                    configForm.ShowDialog();
                }

                if (!configService.ConfigExists)
                    throw new FileNotFoundException("Connection configuration file was not created.");
            }

            // 🔹 CASE 2: Config exists → deserialize it
            LoginInfo loginInfo = await configService.LoadLoginInfoAsync(token);

            // 🔹 Build connection string
            return configService.BuildConnectionString(loginInfo);
        }


        private async Task LoadSessionInfoAsync(CancellationToken token)
        {
            try
            {
                _sessionID = await _sqlService.GetSessionIDAsync(_currentDevice, token);
                lblSessionID.Text = $"Session ID: {_sessionID}";
            }
            catch (Exception ex)
            {
              
                MessageBox.Show($"Failed to create session: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadPrinterInfoAsync(CancellationToken token)
        {
            try
            {
                string printerName = await _sqlService.GetPrinterNameAsync(_currentDevice, token);
                lblPrinter.Text = $"Printer: {printerName}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving printer: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblPrinter.Text = "Printer: Error";
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string username = txtEmployeeUsername.Text.Trim();
            string password = txtEmployeePassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                await WriteToLogFileAsync($"Login attempt started for user: {username} on device {_currentDevice}");

                string passwordHash = AppUtilities.HashPassword(password);
                _employee = await _sqlService.AuthenticateUserAsync(username, passwordHash, _cts.Token);

                if (_employee == null)
                {
                    MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    await _sqlService.LogLoginAttemptAsync(username, false, null, "Invalid credentials", _sessionID, _cts.Token);
                    return;
                }
                if (ShowDisclaimer("POS Disclaimer", "By using this system, you agree to comply with all company policies...") == DialogResult.Cancel)
                {
                    await _sqlService.LogLoginAttemptAsync(username, false, _employee.EmployeeID, "User canceled POS disclaimer", _sessionID, _cts.Token);
                    return;
                }

                if (ShowDisclaimer("Honesty Disclaimer", "You are required to be honest and truthful in all transactions...") == DialogResult.Cancel)
                {
                    await _sqlService.LogLoginAttemptAsync(username, false, _employee.EmployeeID, "User canceled Honesty disclaimer", _sessionID, _cts.Token);
                    return;
                }

                // Login Success flow
                await _sqlService.LogLoginAttemptAsync(username, true, _employee.EmployeeID, "Login successful", _sessionID, _cts.Token);
                await WriteToLogFileAsync($"Login successful for user: {username}, EMID: {_employee.EmployeeID}");

                await _sqlService.UpdateSessionWithEmployeeAsync(_employee.EmployeeID, _sessionID);

                // Hand off to Main Form
                this.Hide();
                new frmMainForm(NameUser, SurnameUser, EmployeeID, CurrentDevice, _sessionID).Show(); // Assumed defined
            }
            catch (Exception ex)
            {
                await WriteToLogFileAsync($"Login process error for '{username}': {ex.Message}");
                MessageBox.Show($"An error occurred during login: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DialogResult ShowDisclaimer(string title, string message)
        {
            return MessageBox.Show($"{message}\n\nAny unauthorized use or misuse may result in disciplinary action.",
                title, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }

        #region UI Event Handlers
        // These remain in the Form as they are purely UI-driven.
        private void btnExit_Click(object sender, EventArgs e) => Application.Exit();
        private void frmLogInPage_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(this, e.Location); // Assumed defined in designer
            }
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) => new AboutBox1().ShowDialog(); // Assumed defined
        private void btnFogortPassword_Click(object sender, EventArgs e) => new frmResetPassword().ShowDialog(); // Assumed defined
        private void sQLConnectionToolStripMenuItem_Click(object sender, EventArgs e) => new frmConfigurationForm().Show(); // Assumed defined
        private void databaseSetupToolStripMenuItem_Click(object sender, EventArgs e) => new TableDefinition().Show(); // Assumed defined
        private void printerSetupToolStripMenuItem_Click(object sender, EventArgs e) => new frmPrinter(_currentDevice).Show(); // Assumed defined
        private void button2_Click_1(object sender, EventArgs e) => Application.Exit();
        private void generalSettingsToolStripMenuItem_Click(object sender, EventArgs e) => new FormApplicationSettings().Show(); // Assumed defined
        #endregion

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _cts.Cancel();
            _cts.Dispose();
            base.OnFormClosing(e);
        }
    }
}