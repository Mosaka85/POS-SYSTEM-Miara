using Miara.Models;
using Miara.Processor_Class; // For DeviceAuditService
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Miara
{
    public partial class frmMainForm : Form
    {
        private const string ConfigFile = "Config.xml";

        // Connection string loaded from Config.xml
        private readonly string _connectionString;

        // Employee information
        private readonly string _employeeFirstName;
        private readonly string _employeeSurname;
        private readonly int _employeeNumber;
        private readonly string _deviceInternetId;
        private readonly string _sessionID;

        // Audit Service - respects user setting "AllowDatabaseLogs"
        private readonly DeviceAuditService _auditService;

        public frmMainForm(string firstName, string surname, int employeeId, string deviceInternetId,string sessionID)
        {
           
            InitializeComponent();
            _connectionString = LoadSqlConnectionInfo();
            ValidateConstructorParameters(firstName, surname, employeeId, deviceInternetId);
            _employeeFirstName = firstName;
            _employeeSurname = surname;
            _employeeNumber = employeeId;
            _deviceInternetId = deviceInternetId;
            _sessionID = sessionID;
            
            lblName.Text = $"WELCOME {firstName} {surname}";

           
            this.FormClosed += (s, e) => Application.Exit();

          

        
            _auditService = new DeviceAuditService(_connectionString);

            // Load role (async) and display it
            _ = LoadEmployeeRoleAsync(employeeId);
            _ = LoadEmployeePermissionsAsync();
        }

        private async Task LoadEmployeeRoleAsync(int employeeId)
        {
            try
            {
                string role = await new EmployeeRoleService(_connectionString, logTrace)
                    .GetEmployeeRoleAsync(employeeId);

                if (!string.IsNullOrEmpty(role))
                {
                    this.Invoke((MethodInvoker)(() => lblRole.Text = role));
                }
            }
            catch (Exception ex)
            {
                await LogAuditAsync($"Failed to load role for employee {_employeeNumber}", ex.Message);
            }
        }

        private void frmMainForm_Load(object sender, EventArgs e)
        {
            _ = LoadEmployeeImageAsync();
            
        }

        #region Helper Methods

        private void ValidateConstructorParameters(string firstName, string surname,
                                                   int employeeId, string deviceInternetId)
        {
            if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("First name cannot be empty.", nameof(firstName));
            if (string.IsNullOrWhiteSpace(surname)) throw new ArgumentException("Surname cannot be empty.", nameof(surname));
            if (employeeId <= 0) throw new ArgumentException("Employee ID must be positive.", nameof(employeeId));
            if (string.IsNullOrWhiteSpace(deviceInternetId)) throw new ArgumentException("Device ID cannot be empty.", nameof(deviceInternetId));
        }

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
                using (var fs = new FileStream(configPath, FileMode.Open, FileAccess.Read))
                {
                    var serializer = new XmlSerializer(typeof(LoginInfo));
                    var loginInfo = (LoginInfo)serializer.Deserialize(fs);
                    return $"Data Source={loginInfo.DataSource};" +
                           $"Initial Catalog={loginInfo.SelectedDatabase};" +
                           $"User ID={loginInfo.Username};Password={loginInfo.Password}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load connection info: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }

        // Unified, clean audit logging method
        private Task LogAuditAsync(string stepDescription, string errorMessage = null)
        {
            var employee = $"{_employeeFirstName} {_employeeSurname} ({_employeeNumber})";
            // Fire-and-forget: non-blocking, safe, respects settings
            return Task.Run(() =>
                _auditService.LogAuditEntry(_deviceInternetId, employee, stepDescription, errorMessage));
        }

        private async void OpenForm<T>(Func<Form> formFactory) where T : Form
        {
            try
            {
                using (var form = formFactory())
                {
                    await LogAuditAsync($"Opened {typeof(T).Name}");
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                await LogAuditAsync($"Failed to open {typeof(T).Name}", ex.Message);
                MessageBox.Show($"Error opening form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Employee Image

        private async Task LoadEmployeeImageAsync()
        {
            if (string.IsNullOrEmpty(_connectionString)) return;

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    const string query = "SELECT TOP 1 ImageData FROM ImageStore WHERE EmployeeID = @empId AND IsActive = 1";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@empId", _employeeNumber);
                        await conn.OpenAsync();
                        var result = await cmd.ExecuteScalarAsync();

                        if (result is byte[] imgData)
                        {
                            using (var ms = new MemoryStream(imgData))
                            {
                                pictureBox1.Image = Image.FromStream(ms);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await LogAuditAsync("Failed to load employee image", ex.Message);
                MessageBox.Show($"Error loading image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            UploadImageForEmployee(_employeeNumber);
            _ = LoadEmployeeImageAsync();
        }

        private void UploadImageForEmployee(int employeeId)
        {
            var ofd = new OpenFileDialog { Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp" };
            if (ofd.ShowDialog() != DialogResult.OK) return;

            try
            {
                using (Image original = Image.FromFile(ofd.FileName))
                using (Image thumbnail = ImageHelper.CreateThumbnail(original, 300))
                {
                    byte[] imageData = ImageHelper.ImageToByteArray(thumbnail);
                    string fileName = Path.GetFileName(ofd.FileName);

                    var service = new EmployeeImageService(_connectionString);
                    service.UploadImage(employeeId, fileName, imageData);

                    MessageBox.Show("Image uploaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                LogAuditAsync("Image upload failed", ex.Message);
                MessageBox.Show("Error uploading image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private async Task LoadEmployeePermissionsAsync()
        {
            if (string.IsNullOrEmpty(_connectionString))
                return;

            _permissionService = new EmployeePermissionService(_connectionString);

            try
            {
                var permissions =
                    await _permissionService.GetEmployeePermissionsAsync(_employeeNumber);

                btnOpenPOS.Enabled = permissions.Contains(1);
                btnManageProduct.Enabled = permissions.Contains(2);
                manageCategory.Enabled = permissions.Contains(3);
                btnRecords.Enabled = permissions.Contains(4);
                btnSalesHistory.Enabled = permissions.Contains(4);
                btnUserSetting.Enabled = permissions.Contains(5);
                btnsettings.Enabled = permissions.Contains(6);
            }
            catch (Exception ex)
            {
                await LogAuditAsync("Failed to load employee permissions", ex.Message);
                MessageBox.Show(
                    "Failed to load user permissions.",
                    "Permission Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region Permissions

        private EmployeePermissionService _permissionService ;


        #endregion

        #region Button Click Handlers

        private void btnOpenPOS_Click(object sender, EventArgs e) => OpenForm<frmSales>(() => new frmSales(_employeeFirstName, _employeeSurname, _employeeNumber, _deviceInternetId,_sessionID));
        private void btnManageProduct_Click(object sender, EventArgs e) => OpenForm<frmProducts>(() => new frmProducts(_employeeFirstName, _employeeSurname, _employeeNumber, _deviceInternetId, _sessionID));
        private void manageCategory_Click(object sender, EventArgs e) => OpenForm<frmCategory>(() => new frmCategory(_employeeFirstName, _employeeSurname, _employeeNumber, _deviceInternetId));
        private void btnRecords_Click(object sender, EventArgs e) => OpenForm<frmReports>(() => new frmReports(_employeeFirstName, _employeeSurname, _employeeNumber, _deviceInternetId,_sessionID));
        private void btnSalesHistory_Click(object sender, EventArgs e) => OpenForm<frmReports>(() => new frmReports(_employeeFirstName, _employeeSurname, _employeeNumber, _deviceInternetId,_sessionID));
        private void btnUserSetting_Click(object sender, EventArgs e) => OpenForm<FormUserPermissions>(() => new FormUserPermissions(_employeeFirstName, _employeeSurname, _employeeNumber, _deviceInternetId));

        private async void btnLogOut_Click(object sender, EventArgs e)
        {
            await LogAuditAsync("User logged out");
            Application.Restart();
        }


        private async void btnsettings_Click(object sender, EventArgs e)
        {
            await LogAuditAsync("Accessed settings (not implemented)");
            MessageBox.Show("Store settings functionality is not yet implemented.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        // Placeholder for logTrace if used elsewhere
        private void logTrace(string message) => System.Diagnostics.Debug.WriteLine(message);

        // Designer placeholders
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void lblName_Click(object sender, EventArgs e) { }
        private void panel2_Paint(object sender, PaintEventArgs e) { }
        private void btnVendor_Click(object sender, EventArgs e) { }
    }
}