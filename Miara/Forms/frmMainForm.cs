using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
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

        public frmMainForm(string firstName, string surname, int employeeId, string deviceInternetId)
        {
            ValidateConstructorParameters(firstName, surname, employeeId, deviceInternetId);

            InitializeComponent();

            _employeeFirstName = firstName;
            _employeeSurname = surname;
            _employeeNumber = employeeId;
            _deviceInternetId = deviceInternetId;

            lblName.Text = $"WELCOME {firstName} {surname}";

            // Close the whole application when the main form is closed
            this.FormClosed += (s, e) => Application.Exit();

            _connectionString = LoadSqlConnectionInfo();

            // Load role (async) and display it
            _ = LoadEmployeeRoleAsync(employeeId);
        }

        private async Task LoadEmployeeRoleAsync(int employeeId)
        {
            string role = await GetEmployeeRoleAsync(employeeId);
            if (!string.IsNullOrEmpty(role))
            {
                this.Invoke((MethodInvoker)(() => lblRole.Text = role));
            }
        }

        private void frmMainForm_Load(object sender, EventArgs e)
        {
            _ = LoadEmployeeImageAsync();   // fire-and-forget
            _ = LoadEmployeePermissionsAsync(); // fire-and-forget
        }

        #region Helper Methods

        private void ValidateConstructorParameters(string firstName, string surname,
                                                   int employeeId, string deviceInternetId)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name cannot be empty.", nameof(firstName));
            if (string.IsNullOrWhiteSpace(surname))
                throw new ArgumentException("Surname cannot be empty.", nameof(surname));
            if (employeeId <= 0)
                throw new ArgumentException("Employee ID must be a positive integer.", nameof(employeeId));
            if (string.IsNullOrWhiteSpace(deviceInternetId))
                throw new ArgumentException("Device Internet ID cannot be empty.", nameof(deviceInternetId));
        }

        private async void OpenForm<T>(Func<Form> formFactory) where T : Form
        {
            try
            {
                using (var form = formFactory())
                {
                    await LogAuditEntryAsync(_deviceInternetId,
                                             $"{_employeeFirstName} {_employeeSurname}",
                                             $"Opened {typeof(T).Name}");

                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                await LogAuditEntryAsync(_deviceInternetId,
                                         $"{_employeeFirstName} {_employeeSurname}",
                                         $"Failed to open {typeof(T).Name}", ex.Message);

                MessageBox.Show($"Error opening form: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string LoadSqlConnectionInfo()
        {
            string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigFile);

            if (!File.Exists(configPath))
            {
                MessageBox.Show("Connection configuration file not found.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show($"Failed to load connection information: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
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
                    const string query = @"
                        SELECT TOP 1 ImageData
                        FROM ImageStore
                        WHERE EmployeeID = @empId AND IsActive = 1";

                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@empId", _employeeNumber);
                        await conn.OpenAsync();

                        var result = await cmd.ExecuteScalarAsync();
                        if (result != null && result != DBNull.Value && result is byte[] imgData)
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
                await LogAuditEntryAsync(_deviceInternetId,
                                         $"{_employeeFirstName} {_employeeSurname}",
                                         "Failed to load employee image", ex.Message);
                MessageBox.Show($"Error loading employee image: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            UploadImageForEmployee(_employeeNumber);
            _ = LoadEmployeeImageAsync();
        }

        private void UploadImageForEmployee(int employeeId)
        {
            var ofd = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"
            };

            if (ofd.ShowDialog() != DialogResult.OK) return;

            string fileName = Path.GetFileName(ofd.FileName);

            var confirm = MessageBox.Show(
                $"Upload '{fileName}' for Employee ID: {employeeId}? This will deactivate existing images.",
                "Confirm Image Upload", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                using (var fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read))
                using (var original = Image.FromStream(fs))
                using (var thumbnail = CreateThumbnail(original, 300))
                {
                    byte[] imageData = ConvertImageToByteArray(thumbnail);

                    using (var conn = new SqlConnection(_connectionString))
                    {
                        conn.Open();
                        using (var transaction = conn.BeginTransaction())
                        {
                            try
                            {
                                const string deactivate = @"UPDATE ImageStore SET IsActive = 0 WHERE EmployeeID=@empId AND IsActive=1";
                                using (var cmd = new SqlCommand(deactivate, conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@empId", employeeId);
                                    cmd.ExecuteNonQuery();
                                }

                                const string insert = @"INSERT INTO ImageStore (ImageName, ImageData, EmployeeID, IsActive, [Date])
                                                        VALUES (@name, @data, @empId, 1, GETDATE())";
                                using (var cmd = new SqlCommand(insert, conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@name", fileName);
                                    cmd.Parameters.AddWithValue("@data", imageData);
                                    cmd.Parameters.AddWithValue("@empId", employeeId);
                                    cmd.ExecuteNonQuery();
                                }

                                transaction.Commit();
                                MessageBox.Show("Image uploaded successfully!", "Success",
                                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch
                            {
                                transaction.Rollback();
                                throw;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing image: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Image CreateThumbnail(Image original, int maxSize)
        {
            int newWidth, newHeight;
            if (original.Width > original.Height)
            {
                newWidth = maxSize;
                newHeight = (int)(original.Height * (maxSize / (float)original.Width));
            }
            else
            {
                newHeight = maxSize;
                newWidth = (int)(original.Width * (maxSize / (float)original.Height));
            }

            var thumbnail = new Bitmap(newWidth, newHeight);
            using (var g = Graphics.FromImage(thumbnail))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(original, 0, 0, newWidth, newHeight);
            }
            return thumbnail;
        }

        private byte[] ConvertImageToByteArray(Image img)
        {
            using (var ms = new MemoryStream())
            {
                var encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, 80L);
                ImageCodecInfo codec = ImageCodecInfo.GetImageEncoders().FirstOrDefault(c =>
                    c.MimeType.Equals("image/jpeg", StringComparison.OrdinalIgnoreCase) ||
                    c.MimeType.Equals("image/png", StringComparison.OrdinalIgnoreCase));

                img.Save(ms, codec, encoderParams);
                return ms.ToArray();
            }
        }

        #endregion

        #region Roles & Permissions

        private async Task<string> GetEmployeeRoleAsync(int employeeId)
        {
            const string query = @"
                SELECT TOP 1 r.name AS Role
                FROM [users] u
                LEFT JOIN [user_groups] ug ON u.user_group_id = ug.id
                LEFT JOIN [group_roles] gr ON ug.id = gr.group_id
                LEFT JOIN [roles] r ON gr.role_id = r.id
                WHERE u.id = @EmployeeID;";

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                    await conn.OpenAsync();

                    var result = await cmd.ExecuteScalarAsync();
                    return result == DBNull.Value || result == null ? "No role assigned" : result.ToString();
                }
            }
            catch (Exception ex)
            {
                await LogAuditEntryAsync(_deviceInternetId,
                                         $"{_employeeFirstName} {_employeeSurname}",
                                         "Failed to retrieve role", ex.Message);
                return "Unknown";
            }
        }

        private async Task LoadEmployeePermissionsAsync()
        {
            if (string.IsNullOrEmpty(_connectionString)) return;

            var employeePermissions = new HashSet<int>();

            const string query = @"
                SELECT rp.permission_code
                FROM [users] u
                INNER JOIN user_groups ug ON u.user_group_id = ug.id
                INNER JOIN group_roles gr ON ug.id = gr.group_id
                INNER JOIN RolePermissions rp ON gr.role_id = rp.role
                WHERE u.id = @EmployeeID";

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EmployeeID", _employeeNumber);
                    await conn.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            employeePermissions.Add(Convert.ToInt32(reader[0]));
                        }
                    }
                }

                btnOpenPOS.Enabled = employeePermissions.Contains(1);
                btnManageProduct.Enabled = employeePermissions.Contains(2);
                manageCategory.Enabled = employeePermissions.Contains(3);
                btnRecords.Enabled = employeePermissions.Contains(4);
                btnSalesHistory.Enabled = employeePermissions.Contains(4);
                btnUserSetting.Enabled = employeePermissions.Contains(5);
                btnsettings.Enabled = employeePermissions.Contains(6);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load permissions: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async Task LogAuditEntryAsync(string device, string employee,
                                             string stepDescription, string errorMessage = null)
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
                    command.Parameters.AddWithValue("@ErrorMessage",
                        string.IsNullOrEmpty(errorMessage) ? (object)DBNull.Value : errorMessage);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to log audit entry: {ex.Message}",
                                "Logging Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Button Click Handlers

        private void btnOpenPOS_Click(object sender, EventArgs e)
        {
            OpenForm<frmSales>(() => new frmSales(_employeeFirstName, _employeeSurname,
                                                _employeeNumber, _deviceInternetId));
        }

        private void btnManageProduct_Click(object sender, EventArgs e)
        {
            OpenForm<frmProducts>(() => new frmProducts(_employeeFirstName, _employeeSurname,
                                                       _employeeNumber, _deviceInternetId));
        }

        private void manageCategory_Click(object sender, EventArgs e)
        {
            OpenForm<frmCategory>(() => new frmCategory(_employeeFirstName, _employeeSurname,
                                                       _employeeNumber, _deviceInternetId));
        }

        private void btnRecords_Click(object sender, EventArgs e)
        {
            OpenForm<frmReports>(() => new frmReports(_employeeFirstName, _employeeSurname,
                                                     _employeeNumber, _deviceInternetId));
        }

        private void btnSalesHistory_Click(object sender, EventArgs e)
        {
            OpenForm<frmReports>(() => new frmReports(_employeeFirstName, _employeeSurname,
                                                     _employeeNumber, _deviceInternetId));
        }

        private void btnUserSetting_Click(object sender, EventArgs e)
        {
            OpenForm<FormUserPermissions>(() => new FormUserPermissions(_employeeFirstName,
                                                                      _employeeSurname,
                                                                      _employeeNumber,
                                                                      _deviceInternetId));
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void btnsettings_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Store settings functionality is not yet implemented.",
                            "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        // Empty designer-required event handlers
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void lblName_Click(object sender, EventArgs e) { }
        private void panel2_Paint(object sender, PaintEventArgs e) { }
        private void btnVendor_Click(object sender, EventArgs e) { }
    }
}
