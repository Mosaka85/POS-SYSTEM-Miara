// frmPrinter.cs
using System;
using System.ComponentModel;
using System.Drawing.Printing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Miara.Forms
{
    public partial class frmPrinter : Form
    {
        private readonly PrinterService _printerService;
        private PrinterRepository _printerRepository;
        private readonly string _currentDeviceMacAddress;
        private readonly BindingList<Printer> _printers;

        // A single, thread-safe semaphore for all logging operations
        private static readonly SemaphoreSlim _logSemaphore = new SemaphoreSlim(1, 1);

        public frmPrinter(string macAddress)
        {
            InitializeComponent();
            _currentDeviceMacAddress = macAddress;
            _printerService = new PrinterService();
            _printers = new BindingList<Printer>();
        }

        private async void frmPrinter_Load(object sender, EventArgs e)
        {
            SetupDataGrid(); // FIX: This method isn't async, so it shouldn't be named or called as such.

            // FIX: Must 'await' the async method to ensure the repository is configured before use.
            if (!await LoadSQLConnectionInfoAsync())
            {
                MessageBox.Show("Failed to load database configuration. Save functionality will be disabled.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
            }

            // Default to searching all printers on load
            cmbPrinterTypes.SelectedItem = "All Printers";
            await SearchPrintersAsync();
           
            _ = WriteToLogFileAsync($"Printer form loaded. Device MAC: {_currentDeviceMacAddress}");
        }

       
        private void SetupDataGrid()
        {
            dgvPrinters.AutoGenerateColumns = false;
            dgvPrinters.DataSource = _printers;

           
            if (dgvPrinters.Columns.Count == 0)
            {
                dgvPrinters.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Name", HeaderText = "Printer Name", Name = "colName", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
                dgvPrinters.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PortName", HeaderText = "Port", Name = "colPort" });
                dgvPrinters.Columns.Add(new DataGridViewCheckBoxColumn { DataPropertyName = "IsNetwork", HeaderText = "Network", Name = "colIsNetwork" });
                dgvPrinters.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "IpAddress", HeaderText = "IP Address", Name = "colIpAddress" });
                dgvPrinters.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MacAddress", HeaderText = "MAC Address", Name = "colMacAddress" });
                dgvPrinters.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Status", HeaderText = "Status", Name = "colStatus" });

                foreach (DataGridViewColumn col in dgvPrinters.Columns)
                {
                    col.ReadOnly = true;
                }
            }
            _ = WriteToLogFileAsync("Data grid setup complete.");
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            await SearchPrintersAsync();
        }

        private async Task SearchPrintersAsync()
        {
            btnSearch.Enabled = false;
            lblStatus.Text = "Searching for printers, please wait...";

            try
            {
                _printers.Clear();
                string filter = cmbPrinterTypes.SelectedItem?.ToString() ?? "All Printers";
                bool includeLocal = filter == "All Printers" || filter == "Local Printers";
                bool includeNetwork = filter == "All Printers" || filter == "Network Printers";

                var foundPrinters = await _printerService.GetPrintersAsync(includeLocal, includeNetwork);
                foreach (var printer in foundPrinters)
                {
                    _printers.Add(printer);
                }

                lblStatus.Text = $"Search complete. Found {_printers.Count} printers.";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Error during search.";
                MessageBox.Show($"An error occurred while searching for printers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                await WriteToLogFileAsync($"Error during printer search: {ex.Message}");
            }
            finally
            {
                btnSearch.Enabled = true;
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!TryGetSelectedPrinter(out var selectedPrinter))
            {
                MessageBox.Show("Please select a printer to save.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                await _printerRepository.SavePrinterConfigurationAsync(selectedPrinter, _currentDeviceMacAddress);
                MessageBox.Show("Printer configuration saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblStatus.Text = $"Configuration for '{selectedPrinter.Name}' saved.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save configuration: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Error saving configuration.";
                await WriteToLogFileAsync($"Error saving printer configuration: {ex.Message}");
            }
        }

        // FIX: Event handlers must be 'async void', not 'async Task'.
       

        // IMPROVEMENT: Helper method to avoid duplicating selection logic.
        private bool TryGetSelectedPrinter(out Printer selectedPrinter)
        {
            selectedPrinter = null;
            if (dgvPrinters.CurrentRow?.DataBoundItem is Printer printer)
            {
                selectedPrinter = printer;
                return true;
            }
            return false;
        }

        #region Configuration and Logging

        private async Task<bool> LoadSQLConnectionInfoAsync()
        {
            var configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config.xml");
            if (!File.Exists(configFile)) return false;

            try
            {
                var serializer = new XmlSerializer(typeof(LoginInfo));
                using (var fileStream = new FileStream(configFile, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, useAsync: true))
                {
                    // Deserialization itself is synchronous, but we read the file async.
                    var loginInfo = (LoginInfo)serializer.Deserialize(fileStream);
                    var connectionString = $"Data Source={loginInfo.DataSource};Initial Catalog={loginInfo.SelectedDatabase};User ID={loginInfo.Username};Password={loginInfo.Password}";
                    _printerRepository = new PrinterRepository(connectionString);
                    return true;
                }
            }
            catch (Exception ex)
            {
                await WriteToLogFileAsync($"Error loading SQL connection info: {ex.Message}");
                return false;
            }
        }

        private async Task WriteToLogFileAsync(string message)
        {
            string logFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt");
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

        // Dummy LoginInfo class for compilation.
        public class LoginInfo
        {
            public string DataSource { get; set; }
            public string SelectedDatabase { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
        }

        #endregion

        private async void btnPrintTestPage_Click(object sender, EventArgs e)
        {
            if (!TryGetSelectedPrinter(out var selectedPrinter))
            {
                MessageBox.Show("Please select a printer to test.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                lblStatus.Text = $"Sending test page to {selectedPrinter.Name}...";
                using (var printDoc = new PrintDocument())
                {
                    printDoc.PrinterSettings.PrinterName = selectedPrinter.Name;

                    if (!printDoc.PrinterSettings.IsValid)
                    {
                        MessageBox.Show($"The printer '{selectedPrinter.Name}' is not valid or cannot be found.", "Invalid Printer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    printDoc.PrintPage += (s, ev) =>
                    {
                        ev.Graphics.DrawString($"Test Page for {selectedPrinter.Name}", new System.Drawing.Font("Arial", 14), System.Drawing.Brushes.Black, new System.Drawing.PointF(100, 100));
                        ev.HasMorePages = false;
                    };

                    printDoc.Print(); // This is a blocking call, but typically fast.
                }
                lblStatus.Text = "Test page sent successfully.";
                await WriteToLogFileAsync($"Test page sent to printer: {selectedPrinter.Name}");
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Error sending test page.";
                MessageBox.Show($"Could not print test page: {ex.Message}", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                await WriteToLogFileAsync($"Error sending test page: {ex.Message}");
            }
        }
    }
}