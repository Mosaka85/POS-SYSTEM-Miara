using Miara.Forms;
using MiaraPOS.Services;
using Microsoft.Reporting.WebForms;
using Microsoft.Reporting.WinForms;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using WECPOFLogic;

namespace Miara
{
    public partial class frmSales : Form
    {
        private static readonly string configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config.xml");
        private string connectionString;
        private int nextSaleIdn;

 


        public frmSales(string firstName, string surname, int EMID, string macaddress)
        {
            InitializeComponent();

            this.KeyPreview = true;
            btnCompleteSale.Enabled = false;
            EmployeeNumber = EMID;
            employeeFirstName = firstName;
            employeeSurname = surname;
            DeviceinternetID = macaddress;

            LoadSQLConnectionInfo();
            LoadProducts();
            LoadCategories();
            LoadPaymentMethods();
            int nextSaleId = GetNextSaleId();
            ScrapOldLogCashback(nextSaleId);

            nextSaleIdn = nextSaleId;
            lblNextSalesNumber.Text = $"SALES ORDER NO: {nextSaleId}";
            label2.Text = $"WELCOME {firstName} {surname} ";
            txtRecipientEmail.Visible = checkEmail.Checked;
            EnsureRequiredColumns();

            txtBarcodeLabel.KeyDown += txtBarcodeLabel_KeyDown;
            txtQuantity.KeyDown += Control_KeyDown;
            comboBoxProducts.KeyDown += Control_KeyDown;
            this.MouseDown += frmSales_MouseDown;
            this.KeyDown += frmSales_KeyDown;
            toolStripChangeBackColour.Click += toolStripChangeBackColour_Click;
            timer1.Start();
            this.FormClosed += (sender, e) => Application.Exit();
        }

     
        private void frmSales_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2 && btnCompleteSale.Enabled)
            {
                btnCompleteSale.PerformClick();
                e.Handled = true; // Optional: stops further processing
                logTrace(" pressed F2 to complete sale"); 
            }
        }

        string employeeFirstName;
        string employeeSurname;
        int EmployeeNumber;
        string DeviceinternetID;
        private void LoadSQLConnectionInfo()
        {
            if (File.Exists(configFile))
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(LoginInfo));
                    using (FileStream fileStream = new FileStream(configFile, FileMode.Open))
                    {
                        LoginInfo loginInfo = (LoginInfo)serializer.Deserialize(fileStream);
                        connectionString = $"Data Source={loginInfo.DataSource};Initial Catalog={loginInfo.SelectedDatabase};User ID={loginInfo.Username};Password={loginInfo.Password}";
                    }
                }
                catch (Exception ex)
                {
                    LogAuditEntry(DeviceinternetID, employeeSurname + " ," + employeeFirstName + " ; " + EmployeeNumber, "Failed to load connection information", ex.Message);
                    logTrace($"Failed to load connection information: {ex.Message}");
                    MessageBox.Show("Failed to load connection information: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                LogAuditEntry(DeviceinternetID, employeeSurname + " ," + employeeFirstName + " ; " + EmployeeNumber, "Connection configuration file not found", "File: " + configFile);
                logTrace("Connection configuration file not found: " + configFile);
                MessageBox.Show("Connection configuration file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProducts()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT ProductID, ProductName, Price, StockQuantity FROM Products WITH(NOLOCK) WHERE IsActive = 1";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable products = new DataTable();
                    adapter.Fill(products);

                    comboBoxProducts.DataSource = products;
                    comboBoxProducts.DisplayMember = "ProductName";
                    comboBoxProducts.ValueMember = "ProductID";
                }
            }
            catch (Exception ex)
            {
                LogAuditEntry(DeviceinternetID, employeeSurname + " ," + employeeFirstName + " ; " + EmployeeNumber, "Error loading products", ex.Message);
                logTrace($"Error loading products: {ex.Message}");
                MessageBox.Show($"Error loading products: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRemoveProduct_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewSaleDetails.SelectedRows)
            {
                dataGridViewSaleDetails.Rows.Remove(row);
                LogAuditEntry(DeviceinternetID, employeeSurname + " ," + employeeFirstName + " ; " + EmployeeNumber, $"Removed product from sale: {row.Cells["ProductName"].Value}");
                logTrace($"Product removed from sale: {row.Cells["ProductName"].Value}");
            }
            UpdateTotalAmount();
            LogAuditEntry(DeviceinternetID, employeeSurname + " ," + employeeFirstName + " ; " + EmployeeNumber, "Updated total amount after removing product(s)");
            logTrace("Product(s) removed from sale and total amount updated");

        }
        private void UpdateTotalAmount()
        {
            try
            {
                decimal total = 0;

                if (dataGridViewSaleDetails != null)
                {
                    foreach (DataGridViewRow row in dataGridViewSaleDetails.Rows)
                    {
                        if (row != null && row.Cells["Subtotal"] != null && row.Cells["Subtotal"].Value != null &&
                            decimal.TryParse(row.Cells["Subtotal"].Value.ToString(), out decimal subtotal))
                        {
                            total += subtotal;
                        }
                    }
                }

                decimal discountValue = 0;

                if (checkdiscount.Checked && !string.IsNullOrWhiteSpace(txtdiscount.Text) &&
                    decimal.TryParse(txtdiscount.Text, out decimal parsedDiscount) && parsedDiscount >= 0 && parsedDiscount <= 100)
                {
                    decimal discountPercentage = parsedDiscount / 100;
                    discountValue = total * discountPercentage;
                }

                decimal subtotalAfterDiscount = total - discountValue;
                if (subtotalAfterDiscount < 0) subtotalAfterDiscount = 0;



                decimal tax = subtotalAfterDiscount * 0.15m;
                decimal FinalSubtotal = total - tax;

                // Fetch cashback amount using saleID
                decimal cashbackAmount = MVTransactionAmountBySaleID(nextSaleIdn);

              
                decimal finalTotal = subtotalAfterDiscount + cashbackAmount;

           
                lbldiscountvalue.Text = $"Discount: R {discountValue:F2}";
                lblSubtotalorder.Text = $"Subtotal: R {FinalSubtotal:F2}";
                lblTax.Text = $"TAX (15%): R {tax:F2}";
                lblCashBack.Text = $"Cashback: R {cashbackAmount:F2}";
                lblTotalAmount.Text = $"Total: R {finalTotal:F2}";
            }
            catch (Exception ex)
            {
                LogAuditEntry(DeviceinternetID, employeeSurname + " ," + employeeFirstName + " ; " + EmployeeNumber, "Error updating total amount", ex.Message);
                logTrace($"Error updating total amount: {ex.Message}");
                MessageBox.Show($"An error occurred while calculating the total: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private int GetNextSaleId()
        {
            int nextSaleId = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT ISNULL(MAX(SaleID), 0) + 1 FROM Sale WITH(NOLOCK)"; // Get the next SaleID
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        var result = command.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            nextSaleId = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                LogAuditEntry(DeviceinternetID, employeeSurname + " ," + employeeFirstName + " ; " + EmployeeNumber, "SQL Error retrieving next SaleID", sqlEx.Message);
                logtrace($"SQL Error retrieving next SaleID: {sqlEx.Message}");
                MessageBox.Show($"SQL Error retrieving next SaleID: {sqlEx.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                LogAuditEntry(DeviceinternetID, employeeSurname + " ," + employeeFirstName + " ; " + EmployeeNumber, "Error retrieving next SaleID", ex.Message);
                logTrace($"Error retrieving next SaleID: {ex.Message}");
                MessageBox.Show($"Error retrieving next SaleID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            LogAuditEntry(ReceivedValue, employeeSurname + " ," + employeeFirstName + " ; " + EmployeeNumber, $"Next SaleID retrieved: {nextSaleId}");
            return nextSaleId;
        }

        private void logtrace(string v)
        {
            throw new NotImplementedException();
        }

        private static readonly string logFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt");
        //
        //private static readonly string logFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt");
        private void btnCompleteSale_Click(object sender, EventArgs e)
        {
            btnCompleteSale.Enabled = false;
            LogAuditEntry(DeviceInternetID, $"{employeeSurname}, {employeeFirstName}; {EmployeeNumber}", "Started sale processing");
            logTrace("Sale processing started");

            try
            {
                logTrace("Validating required columns in DataGridView");
                var processor = new SaleProcessor(connectionString, DeviceInternetID, EmployeeNumber.ToString());
                logTrace("SaleProcessor initialized successfully");

                logTrace("Preparing sale details for processing");
                processor.CompleteSale(
                    saleDetails: dataGridViewSaleDetails,
                    discountText: txtdiscount.Text?.Trim() ?? "0",
                    discountChecked: checkdiscount.Checked,
                    paymentMethod: combopaymentmentod.SelectedValue?.ToString() ?? "N/A",
                    cashbackText: lblCashBack.Text?.Trim() ?? "Cashback: R 0.00",
                    renderedAmountText: txtrenderedamount.Text?.Trim() ?? "0",
                    sendEmail: checkEmail.Checked && !string.IsNullOrWhiteSpace(txtRecipientEmail.Text),
                    logAudit: msg =>
                    {
                        LogAuditEntry(DeviceInternetID, $"{employeeSurname}, {employeeFirstName}; {EmployeeNumber}", msg);
                        logTrace($"Audit log: {msg}");
                    },
                    resetForm: () =>
                    {
                        logTrace("Resetting form data");
                        dataGridViewSaleDetails.Rows.Clear();
                        UpdateTotalAmount();
                        nextSaleIdn = GetNextSaleId();
                        lblNextSalesNumber.Text = $"SALES ORDER NO: {nextSaleIdn}";
                        txtQuantity.Text = "1";
                        comboBoxProducts.SelectedItem = null;
                        combopaymentmentod.SelectedIndex = -1;
                        txtrenderedamount.Text = string.Empty;
                        lblChange.Text = "Change: R 0.00";
                        lblTotalAmount.Text = "Total: R 0.00";
                        lblCashBack.Text = "Cashback: R 0.00";
                        txtdiscount.Text = string.Empty;
                        logTrace("Form reset completed");
                    },
                    generateReceipt: () =>
                    {
                        logTrace("Generating receipt content");
                        var emailItems = dataGridViewSaleDetails.Rows
                            .Cast<DataGridViewRow>()
                            .Where(r => !r.IsNewRow && r.Cells["ProductName"].Value != null)
                            .Select(r => new ReceiptEmailBuilder.ReceiptItem
                            {
                                ProductName = r.Cells["ProductName"].Value?.ToString() ?? string.Empty,
                                Quantity = Convert.ToInt32(r.Cells["Quantity"].Value ?? 0),
                                Price = Convert.ToDecimal(r.Cells["Price"].Value ?? 0),
                                Subtotal = Convert.ToDecimal(r.Cells["Subtotal"].Value ?? 0)
                            }).ToList();
                        logTrace($"Generated {emailItems.Count} receipt items");

                        var builder = new ReceiptEmailBuilder();
                        var receiptContent = builder.GenerateReceiptContent(
                            saleId: nextSaleIdn,
                            employeeFirstName: employeeFirstName ?? string.Empty,
                            employeeSurname: employeeSurname ?? string.Empty,
                            paymentMethod: combopaymentmentod.SelectedValue?.ToString() ?? "N/A",
                            items: emailItems,
                            discountPercentage: checkdiscount.Checked && decimal.TryParse(txtdiscount.Text?.Trim(), out var disc) ? disc : 0,
                            cashbackAmount: decimal.TryParse(lblCashBack.Text?.Replace("Cashback:", "").Replace("R", "").Trim(), out var cb) ? cb : 0,
                            renderedAmount: combopaymentmentod.SelectedValue?.ToString() == "CASH" && decimal.TryParse(txtrenderedamount.Text?.Trim(), out var rend) ? rend : (decimal?)null
                        );
                        logTrace("Receipt content generated successfully");
                        return receiptContent;
                    },
                    sendEmailAction: receiptHtml =>
                    {
                        logTrace("Initiating email sending process");
                        SendReceiptEmail(receiptHtml);
                        logTrace("Email sent successfully");
                    },
                    printReceipt: () =>
                    {
                        try
                        {
                            logTrace("Preparing to print receipt");

                            var items = dataGridViewSaleDetails.Rows
                                .Cast<DataGridViewRow>()
                                .Where(r => !r.IsNewRow && r.Cells["ProductName"].Value != null)
                                .Select(r => new SaleItem
                                {
                                    ProductName = r.Cells["ProductName"].Value?.ToString() ?? string.Empty,
                                    Quantity = Convert.ToInt32(r.Cells["Quantity"].Value ?? 0),
                                    Price = Convert.ToDecimal(r.Cells["Price"].Value ?? 0),
                                    Subtotal = Convert.ToDecimal(r.Cells["Subtotal"].Value ?? 0)
                                }).ToList();
                            logTrace($"Prepared {items.Count} items for printing");

                            // Initialize ReceiptPrinter
                            var printer = new ReceiptPrinter(
                                deviceInternetId: DeviceInternetID,
                                employeeFirstName: employeeFirstName ?? string.Empty,
                                employeeSurname: employeeSurname ?? string.Empty,
                                employeeNumber: EmployeeNumber.ToString(),
                                receiptNo: nextSaleIdn.ToString(),
                                paymentMethod: combopaymentmentod.SelectedValue?.ToString() ?? "N/A",
                                saleItems: items,
                                discountPercentage: checkdiscount.Checked && decimal.TryParse(txtdiscount.Text?.Trim(), out var disc) ? disc : 0,
                                cashbackAmount: decimal.TryParse(lblCashBack.Text?.Replace("Cashback:", "").Replace("R", "").Trim(), out var cb) ? cb : 0,
                                renderedAmount: combopaymentmentod.SelectedValue?.ToString() == "CASH" && decimal.TryParse(txtrenderedamount.Text?.Trim(), out var rend) ? rend : (decimal?)null
                            );
                            logTrace("ReceiptPrinter initialized");

                            // Show PrintDialog
                            using (PrintDialog printDialog = new PrintDialog())
                            {
                                printDialog.AllowSomePages = false;
                                printDialog.AllowSelection = false;

                                if (printDialog.ShowDialog() == DialogResult.OK)
                                {
                                    string printerName = GetPrinterByDeviceMac();
                                    logTrace($"User selected printer: {printerName}");

                                    // Always show preview before print
                                    printer.Print(printerName, showPrintPreview: true);
                                    logTrace("Print preview shown. User may continue to print.");
                                }
                                else
                                {
                                    logTrace("User cancelled printing.");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            logTrace($"Error during receipt printing: {ex.Message}");
                            MessageBox.Show($"Error printing receipt: {ex.Message}", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    },
                    completeCashback: saleId =>
                    {
                        logTrace($"Initiating cashback completion for sale ID: {saleId}");
                        CompleteCashback(saleId);
                        logTrace("Cashback completed successfully");
                    },
                    getNextSaleId: () =>
                    {
                        var saleId = GetNextSaleId();
                        logTrace($"Generated next sale ID: {saleId}");
                        return saleId;
                    },
                    out nextSaleIdn
                );
                logTrace("Sale completed successfully");
            }
            catch (Exception ex)
            {
                LogAuditEntry(DeviceInternetID, $"{employeeSurname}, {employeeFirstName}; {EmployeeNumber}", "Sale failed", ex.Message);
                logTrace($"Error completing sale: {ex.Message}");
                MessageBox.Show($"Error completing sale: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                logTrace("Re-enabling Complete Sale button");
                btnCompleteSale.Enabled = true;
            }
        }

        public void logTrace(string message)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(logFile, true))
                {
                    writer.WriteLine($"{DateTime.Now} :  {EmployeeNumber},{employeeFirstName},{employeeSurname}, {DeviceinternetID} ,  {message}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to log trace: {ex.Message}", "Logging Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void LogAuditEntry(string device, string employee, string stepDescription, string errorMessage = null)
        {
            string query = @"
        INSERT INTO DeviceAudit (Device, Employee, AuditDate, StepDescription, ErrorMessage)
        VALUES (@Device, @Employee, @AuditDate, @StepDescription, @ErrorMessage);";

            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Device", device ?? string.Empty);
                    command.Parameters.AddWithValue("@Employee", employee ?? string.Empty);
                    command.Parameters.AddWithValue("@AuditDate", DateTime.Now);
                    command.Parameters.AddWithValue("@StepDescription", stepDescription ?? string.Empty);
                    command.Parameters.AddWithValue("@ErrorMessage",
                        string.IsNullOrEmpty(errorMessage) ? DBNull.Value : (object)errorMessage);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to log audit entry: {ex.Message}", "Logging Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public string GetPrinterByDeviceMac()
        {
            string deviceMac = DeviceinternetID;

            if (string.IsNullOrWhiteSpace(deviceMac) || deviceMac == "MAC Address Not Found")
            {
                LogAuditEntry(DeviceinternetID, employeeSurname + " ," + employeeFirstName + " ; " + EmployeeNumber, "Failed to get printer by device MAC", "MAC Address is empty or invalid");
                return null;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT PrinterName FROM Printers WHERE Device = @Device";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Device", DeviceinternetID);

                    connection.Open();

                    var result = command.ExecuteScalar(); // Use ExecuteReader if expecting multiple rows

                    if (result != null)
                    {
                        return result.ToString();
                    }
                    else
                    {
                        LogAuditEntry(DeviceinternetID, employeeSurname + " ," + employeeFirstName + " ; " + EmployeeNumber, "Failed to get printer by device MAC", "No printer found for the given MAC address");
                        return null;
                    }
                }
            }
        }

     


        private string GetPrinterNameByMac(string macAddress)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT PrinterName FROM Printers WHERE Device = @Device";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Device", macAddress);
                        connection.Open();
                        object result = command.ExecuteScalar();
                        return result?.ToString();
                    }
                }
            }
            catch
            {
                LogAuditEntry(DeviceinternetID, employeeSurname + " ," + employeeFirstName + " ; " + EmployeeNumber, "Error getting printer name by MAC", $"MAC Address: {macAddress}");
                return null;
            }
        }


        private void LoadCategories()
        {
            // Clear existing items in the ComboBox
            comboProductCategory.DataSource = null;
            comboProductCategory.Items.Clear();

            // SQL query to fetch CategoryID and CategoryName
            string query = "SELECT CategoryID, CategoryName FROM Categories WITH(NOLOCK) WHERE IsActive = 1";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
            {
                DataTable Categories = new DataTable();
                adapter.Fill(Categories);

                if (Categories.Rows.Count > 0)
                {
                    // Set DisplayMember and ValueMember
                    comboProductCategory.DisplayMember = "CategoryName"; // Show category names
                    comboProductCategory.ValueMember = "CategoryID";     // Store category IDs
                    comboProductCategory.DataSource = Categories;
                }
                else
                {
                    LogAuditEntry(query, employeeSurname + " ," + employeeFirstName + " ; " + EmployeeNumber, "No active categories found");
                    comboProductCategory.DataSource = null;
                }
            }
        }





        private void SendReceiptEmail(string receiptContent)
        {
            string toEmail = txtRecipientEmail.Text;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SendReceiptEmail", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@receiptContent", receiptContent);
                cmd.Parameters.AddWithValue("@toEmail", toEmail);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        private bool EnsureRequiredColumns()
        {
            string[] requiredColumns = { "ProductID", "ProductName", "Quantity", "Price", "Subtotal" };
            foreach (string column in requiredColumns)
            {
                if (!dataGridViewSaleDetails.Columns.Contains(column))
                {
                    LogAuditEntry(column, employeeSurname + " ," + employeeFirstName + " ; " + EmployeeNumber, $"Missing required column: {column}");
                    return false;
                }
            }
            return true;
        }

      
        public string ReceivedValue { get; set; }
        public string DeviceInternetID { get; private set; }

        private async void frmSales_Load(object sender, EventArgs e)
        {
            DeleteCouponRedemptionBySaleId(nextSaleIdn);

       
            await LoadEmployeeImageAsync(EmployeeNumber);

          
            foreach (Control control in this.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.Enter += TextBox_Enter;
                }
            }
        }

      
        private TextBox activeTextBox;
        private void TextBox_Enter(object sender, EventArgs e)
        {
            activeTextBox = sender as TextBox;
        }
        private void NumberButton_Click(object sender, EventArgs e)
        {
            if (activeTextBox != null)
            {
                if (sender is Button button)
                {
                    activeTextBox.Text += button.Text;
                }
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (activeTextBox != null && !string.IsNullOrEmpty(activeTextBox.Text))
            {
                activeTextBox.Text = activeTextBox.Text.Substring(0, activeTextBox.Text.Length - 1);
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
        private void btnClear_Click(object sender, EventArgs e)
        {
            string username = Interaction.InputBox("Enter your username:", "User Validation", "");
            string password = Interaction.InputBox("Enter your password:", "User Validation", "");
            string hashedPassword = HashPassword(password);
            ScrapOldLogCashback(nextSaleIdn);
            DeleteCouponRedemptionBySaleId(nextSaleIdn);

            // Validate user
            string query = @"
        SELECT 1 
        FROM Employees  WITH(NOLOCK)
        WHERE [Username] = @Username
        AND PasswordHash = @PasswordHash 
        AND IsActive = 1";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@PasswordHash", hashedPassword);

                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                {
                    dataGridViewSaleDetails.Rows.Clear();
                    UpdateTotalAmount();
                    txtQuantity.Clear();
                    comboBoxProducts.SelectedItem = null;
                }
                else
                {
                    MessageBox.Show("Invalid credentials. Access denied.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void btnCalculatechange_Click(object sender, EventArgs e)
        {
            if (combopaymentmentod.SelectedValue?.ToString() == "Cash")
            {
                try
                {
                    decimal cost = decimal.Parse(lblTotalAmount.Text.Replace("Total: ", "").Replace("R", "").Trim());
                    decimal renderedAmount;
                    if (!decimal.TryParse(txtrenderedamount.Text, out renderedAmount))
                    {
                        MessageBox.Show("Please enter a valid amount rendered.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (renderedAmount < cost)
                    {
                        MessageBox.Show("The amount rendered is less than the total cost.", "Insufficient Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        lblChange.Text = "R 0.00";
                        return;
                    }
                    decimal change = renderedAmount - cost;
                    lblChange.Text = $"Change: R {change:F2}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while calculating change: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Change calculation is only available for the CASH payment method.", "Invalid Payment Method", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



        private void checkdiscount_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTotalAmount();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy h:mm:ss tt");
            CheckConnectionButton_Click();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            new frmMainForm(employeeFirstName, employeeSurname, EmployeeNumber,DeviceinternetID).ShowDialog();
        }



        private void LoadProductsByCategory(int categoryID)
        {
            comboBoxProducts.DataSource = null;
            comboBoxProducts.Items.Clear();
            string query = "SELECT ProductID, ProductName, Price, StockQuantity FROM Products WITH(NOLOCK) WHERE IsActive = 1 AND CategoryID = @CategoryID "; ;

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@CategoryID", categoryID);

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable products = new DataTable();
                    adapter.Fill(products);

                    if (products.Rows.Count > 0)
                    {
                        comboBoxProducts.DisplayMember = "ProductName";
                        comboBoxProducts.ValueMember = "ProductID";
                        comboBoxProducts.DataSource = products;
                    }
                    else
                    {
                        comboBoxProducts.DataSource = null;
                    }
                }
            }
        }


 



        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Call the btnAddProduct_Click method
                btnAddProduct_Click(sender, e);
            }
        }

        private void btnReprint_Click(object sender, EventArgs e)
        {
            try
            {
                string receiptNumber = Interaction.InputBox("Enter Receipt Number:", "Reprint Receipt", "");

                if (string.IsNullOrEmpty(receiptNumber))
                {
                    MessageBox.Show("Receipt number cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataTable receiptData = GetReceiptData(receiptNumber);

                if (receiptData == null || receiptData.Rows.Count == 0)
                {
                    MessageBox.Show("Receipt not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataTable saleDetails = GetSaleDetails(receiptData.Rows[0]["ReceiptID"].ToString());

                if (saleDetails == null || saleDetails.Rows.Count == 0)
                {
                    MessageBox.Show("No sale details found for this receipt.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                PopulateReceiptData(receiptData, saleDetails);

               // PrintReceipt();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while reprinting the receipt: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable GetReceiptData(string receiptNumber)
        {
            DataTable receiptData = new DataTable();

            try
            {
                string query = @"
            SELECT TOP 1 [ReceiptID], [ReceiptNumber], [Date], [EmployeeName], [PaymentMethod], 
                   [Subtotal], [Discount], [Tax], [Total], [AmountRendered], [Change]
            FROM  [Receipts] WITH(NOLOCK)
            WHERE [ReceiptNumber] LIKE '%@ReceiptNumber'";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ReceiptNumber", receiptNumber);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(receiptData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while fetching receipt data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return receiptData;
        }

        private DataTable GetSaleDetails(string receiptID)
        {
            DataTable saleDetails = new DataTable();

            try
            {
                string query = @"
            SELECT [ProductID], [Quantity], [UnitPrice], [Subtotal], [TAX]
            FROM [POS_MOSAKA].[dbo].[SalesDetails] WITH(NOLOCK)
            WHERE [SaleID] = @ReceiptID";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ReceiptID", receiptID);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(saleDetails);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while fetching sale details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return saleDetails;
        }

        private void PopulateReceiptData(DataTable receiptData, DataTable saleDetails)
        {
            try
            {
                // Populate receipt details
                lblNextSalesNumber.Text = receiptData.Rows[0]["ReceiptNumber"].ToString();
                employeeFirstName = receiptData.Rows[0]["EmployeeName"].ToString().Split(' ')[0];
                employeeSurname = receiptData.Rows[0]["EmployeeName"].ToString().Split(' ')[1];
                combopaymentmentod.SelectedValue = receiptData.Rows[0]["PaymentMethod"].ToString();

                if (receiptData.Rows[0]["PaymentMethod"].ToString() == "CASH")
                {
                    txtrenderedamount.Text = receiptData.Rows[0]["AmountRendered"].ToString();
                }

                // Populate sale details into the dataGridViewSaleDetails
                dataGridViewSaleDetails.Rows.Clear();
                foreach (DataRow row in saleDetails.Rows)
                {
                    string productName = GetProductName(row["ProductID"].ToString());
                    int quantity = Convert.ToInt32(row["Quantity"]);
                    decimal unitPrice = Convert.ToDecimal(row["UnitPrice"]);
                    decimal subtotal = Convert.ToDecimal(row["Subtotal"]);

                    dataGridViewSaleDetails.Rows.Add(productName, quantity, unitPrice, subtotal);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while populating receipt data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string GetProductName(string productID)
        {
            string productName = "";

            try
            {
                string query = "SELECT [ProductName] FROM [Products]  WITH(NOLOCK) WHERE [ProductID] = @ProductID";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductID", productID);
                        productName = command.ExecuteScalar()?.ToString() ?? "Unknown Product";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while fetching product name: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return productName;
        }

        private void btnOpenCashDrawer_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsCashDrawerConnected())
                {
                    OpenCashDrawer();
                }
                else
                {
                    MessageBox.Show("No cash drawer detected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsCashDrawerConnected()
        {

            string[] ports = SerialPort.GetPortNames();
            return ports.Length > 0;
        }

        private void OpenCashDrawer()
        {
            try
            {
                using (SerialPort serialPort = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One))
                {
                    serialPort.Open();
                    byte[] openDrawerCommand = { 27, 112, 0, 25, 250 };
                    serialPort.Write(openDrawerCommand, 0, openDrawerCommand.Length);
                    serialPort.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to open cash drawer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkEmail_CheckedChanged(object sender, EventArgs e)
        {
            txtRecipientEmail.Visible = checkEmail.Checked;
        }

        private void btnCupons_Click(object sender, EventArgs e)
        {
            new frmCupons(nextSaleIdn).ShowDialog();

            // Get the discount applied for the sale
            decimal discount = GetDiscountBySaleId(nextSaleIdn);

            if (discount < 1)
            {
                try
                {
                    checkdiscount.Checked = true;
                    txtdiscount.Text = (discount * 100).ToString("0.00"); // For percentage
                    MessageBox.Show($"Percentage discount applied: {discount * 100:0.##}%");
                    UpdateTotalAmount();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error handling percentage discount: " + ex.Message);
                }
            }
            else
            {
                try
                {
                    // Check if voucher is already added
                    bool voucherAlreadyApplied = dataGridViewSaleDetails.Rows
                        .Cast<DataGridViewRow>()
                        .Any(r => r.Cells["ProductName"] != null && r.Cells["ProductName"].Value?.ToString() == "VOUCHER_APPLIED");

                    if (voucherAlreadyApplied)
                    {
                        MessageBox.Show("A voucher has already been applied to this sale.");
                        return;
                    }

                    // Calculate current total
                    decimal total = 0;
                    foreach (DataGridViewRow row in dataGridViewSaleDetails.Rows)
                    {
                        if (row != null && row.Cells["Subtotal"] != null && row.Cells["Subtotal"].Value != null &&
                            decimal.TryParse(row.Cells["Subtotal"].Value.ToString(), out decimal subtotal))
                        {
                            total += subtotal;
                        }
                    }

                    // Ensure voucher doesn't exceed total
                    if (discount > total)
                    {
                        MessageBox.Show("Voucher value exceeds the total. Please add more items to use the voucher.");
                        return;
                    }

                    // Add the voucher as a negative line
                    dataGridViewSaleDetails.Rows.Add(0, "VOUCHER_APPLIED", 0, -1 * discount, -1 * discount);
                    MessageBox.Show($"Voucher discount applied: R{discount:0.00}");
                    UpdateTotalAmount();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error applying voucher: " + ex.Message);
                }
            }
        }


        public void DeleteCouponRedemptionBySaleId(int saleId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM CouponRedemptions WHERE SaleID = @saleId";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@saleId", saleId);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                // Silently ignore errors
            }
        }



        public decimal GetDiscountBySaleId(int saleId)
        {
            decimal discount = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT DiscountApplied FROM CouponRedemptions WHERE SaleID = @saleId";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@saleId", saleId);

                        conn.Open();
                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            discount = Convert.ToDecimal(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving discount: " + ex.Message);
            }

            return discount;
        }



        private void RemoveSelectedRow()
        {
            if (dataGridViewSaleDetails.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to remove the selected item?",
                    "Confirm Removal",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Remove the selected row
                    dataGridViewSaleDetails.Rows.RemoveAt(dataGridViewSaleDetails.SelectedRows[0].Index);
                    MessageBox.Show("Item removed successfully.", "Removed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a row to remove.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            nextSaleIdn = GetNextSaleId();
            new frmCashback(employeeFirstName, employeeSurname, EmployeeNumber, nextSaleIdn).ShowDialog();
            UpdateTotalAmount();
            combopaymentmentod.SelectedItem = 2;
            combopaymentmentod.SelectedItem = "Debit/Credit Card";

        }




        private decimal MVTransactionAmountBySaleID(int saleid)
        {
            decimal amount = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT SUM(Amount) FROM CashbackTransactions  WITH(NOLOCK) WHERE SaleID = @SaleID AND [status] <> 99";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SaleID", saleid);
                        conn.Open();

                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            amount = Convert.ToDecimal(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving transaction amount: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return amount;
        }


        private void ScrapOldLogCashback(int saleid)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE CashbackTransactions SET [status]=99 WHERE SaleID = @SaleID AND [status] = 1 ";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SaleID", saleid);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        private void CompleteCashback(int saleId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // First, check if a pending transaction exists
                    string checkQuery = "SELECT COUNT(*) FROM CashbackTransactions WHERE SaleID = @SaleID AND [Status] = 1";

                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.Add("@SaleID", SqlDbType.Int).Value = saleId;
                        conn.Open();

                        int count = (int)checkCmd.ExecuteScalar();

                        if (count > 0)
                        {
                            // Ask user for confirmation
                            DialogResult result = MessageBox.Show(
                                "A pending cashback transaction was found. Do you want to complete it?",
                                "Confirm Completion",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question);

                            if (result == DialogResult.Yes)
                            {
                                string updateQuery = @"
                            UPDATE CashbackTransactions 
                            SET [Status] = 2, 
                                Notes = 'Transaction Completed'
                            WHERE SaleID = @SaleID 
                            AND [Status] = 1";

                                using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                                {
                                    updateCmd.Parameters.Add("@SaleID", SqlDbType.Int).Value = saleId;

                                    int rowsAffected = updateCmd.ExecuteNonQuery();

                                    if (rowsAffected > 0)
                                    {
                                        MessageBox.Show("Cashback transaction completed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("A database error occurred: " + sqlEx.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVoidLine_Click(object sender, EventArgs e)
        {
            if (dataGridViewSaleDetails.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridViewSaleDetails.SelectedRows[0];

                int productId = (int)selectedRow.Cells["ProductID"].Value;
                string productName = selectedRow.Cells["ProductName"].Value.ToString();
                int quantity = (int)selectedRow.Cells["Quantity"].Value;
                decimal price = (decimal)selectedRow.Cells["Price"].Value;
                decimal subtotal = (decimal)selectedRow.Cells["Subtotal"].Value;


                string voidedBy = $"{employeeFirstName} {employeeSurname} ({EmployeeNumber})";

                LogVoidOperation(nextSaleIdn, productId, productName, quantity, price, subtotal,
                                 "LineItem", "Employee/Customer agreement", voidedBy);
                RemoveSelectedRow();
                UpdateTotalAmount();
            }
        }
        private void LogVoidOperation(int saleId, int productId, string productName, int quantity, decimal price,
                              decimal subtotal, string voidType, string voidReason,
                              string voidedBy)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO VoidAuditLog (
                                SaleID, ProductID, ProductName, Quantity, Price, Subtotal,
                                VoidType, VoidReason, VoidedBy, VoidDate , Workstation
                             ) VALUES (
                                @SaleID, @ProductID, @ProductName, @Quantity, @Price, @Subtotal,
                                @VoidType, @VoidReason, @VoidedBy, GETDATE(), @Workstation
                             )";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SaleID", saleId);
                        command.Parameters.AddWithValue("@ProductID", productId);
                        command.Parameters.AddWithValue("@ProductName", productName);
                        command.Parameters.AddWithValue("@Quantity", quantity);
                        command.Parameters.AddWithValue("@Price", price);
                        command.Parameters.AddWithValue("@Subtotal", subtotal);
                        command.Parameters.AddWithValue("@VoidType", voidType);
                        command.Parameters.AddWithValue("@VoidReason", voidReason ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@VoidedBy", voidedBy);
                        command.Parameters.AddWithValue("@Workstation", Environment.MachineName);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error logging void operation: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefund_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Refunds are currently blocked by management.", "Refund Not Available",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void frmSales_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(this, e.Location);
            }
        }

        // Color picker menu item click handler
        private void toolStripChangeBackColour_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                colorDialog.AllowFullOpen = true;
                colorDialog.FullOpen = true;
                colorDialog.Color = this.BackColor;

                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    // Apply color to form
                    this.BackColor = colorDialog.Color;


                    // Optional: Apply to child controls
                    ApplyBackgroundToControls(this, colorDialog.Color);
                }
            }
        }

        // Helper method to apply color to child controls
        private void ApplyBackgroundToControls(Control parent, Color color)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is Panel || c is GroupBox)
                {
                    c.BackColor = color;
                    ApplyBackgroundToControls(c, color); // Recursive for nested controls
                }
            }
        }



        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
           
        }

        private void txtdiscount_TextChanged(object sender, EventArgs e)
        {
            UpdateTotalAmount();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void LoadPaymentMethods()
        {
            // The query should ideally select both the ID and the name.
            string query = "SELECT PaymentMethodID, MethodName FROM PaymentMethods WHERE IsActive = 1";

            try
            {
                // Use a using statement for the connection to ensure it's always closed.
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open(); // Explicitly open the connection

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Add a default "Select an option" row for better user experience.
                        DataRow defaultRow = dt.NewRow();
                        defaultRow["PaymentMethodID"] = DBNull.Value;
                        defaultRow["MethodName"] = "-- Select Payment Method --";
                        dt.Rows.InsertAt(defaultRow, 0);
                        combopaymentmentod.DataSource = dt;
                        combopaymentmentod.DisplayMember = "MethodName";
                        combopaymentmentod.ValueMember = "PaymentMethodID";
                        combopaymentmentod.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                logTrace($"Error loading payment methods: {ex.Message}");
                MessageBox.Show("An error occurred while loading payment methods. Please try again or contact support.", "Loading Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            EnsureRequiredColumns();

            if (comboBoxProducts.SelectedItem != null && int.TryParse(txtQuantity.Text, out int quantity) && quantity > 0)
            {
                DataRowView selectedProduct = (DataRowView)comboBoxProducts.SelectedItem;
                int productId = (int)selectedProduct["ProductID"];
                string productName = selectedProduct["ProductName"].ToString();
                decimal price = (decimal)selectedProduct["Price"];
                decimal subtotal = price * quantity;

                bool productExists = false;

                foreach (DataGridViewRow row in dataGridViewSaleDetails.Rows)
                {
                    if (row.Cells["ProductID"].Value != null && (int)row.Cells["ProductID"].Value == productId)
                    {
                        int existingQuantity = (int)row.Cells["Quantity"].Value;
                        row.Cells["Quantity"].Value = existingQuantity + quantity;
                        row.Cells["Subtotal"].Value = (existingQuantity + quantity) * price;
                        productExists = true;
                        break;
                    }
                }

                if (!productExists)
                {
                    dataGridViewSaleDetails.Rows.Add(productId, productName, quantity, price, subtotal);
                }

                UpdateTotalAmount();
            }
            else
            {
                MessageBox.Show("Invalid product selection or quantity.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void combopaymentmentod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combopaymentmentod.SelectedItem?.ToString() == "Cash")
            {
                txtrenderedamount.Visible = true;
                lblChange.Visible = true;
                lblrendered.Visible = true;
                btnCalculatechange.Visible = true;
                btnCalculatechange.Enabled = true;
                btnCompleteSale.Enabled = true;
            }
            else
            {
                // Parse the total cost
                decimal cost = decimal.Parse(lblTotalAmount.Text.Replace("Total: ", "").Replace("R", "").Trim());
                txtrenderedamount.Text = Convert.ToString(cost);
                btnCalculatechange.Enabled = false;
                btnCompleteSale.Enabled = true;
            }

            btnCompleteSale.Enabled = true;


        }

        private void find_product_using_barcode(object sender, EventArgs e)
        {
            EnsureRequiredColumns();

            string Productbarcode = txtBarcodeLabel.Text.Trim();

            if (string.IsNullOrEmpty(Productbarcode))
            {
                MessageBox.Show("Please enter a barcode.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "SELECT ProductID, ProductName, Price, StockQuantity FROM Products WITH(NOLOCK) WHERE IsActive = 1 AND [BARCODE]= @barcode";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@barcode", Productbarcode);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int productId = reader.GetInt32(reader.GetOrdinal("ProductID"));
                            string productName = reader.GetString(reader.GetOrdinal("ProductName"));
                            decimal price = reader.GetDecimal(reader.GetOrdinal("Price"));

                            int quantity = 1; // Default quantity when scanned

                            bool productExists = false;

                            foreach (DataGridViewRow row in dataGridViewSaleDetails.Rows)
                            {
                                if (row.Cells["ProductID"].Value != null && (int)row.Cells["ProductID"].Value == productId)
                                {
                                    int existingQuantity = (int)row.Cells["Quantity"].Value;
                                    row.Cells["Quantity"].Value = existingQuantity + quantity;
                                    row.Cells["Subtotal"].Value = (existingQuantity + quantity) * price;
                                    productExists = true;
                                    break;
                                }
                            }

                            if (!productExists)
                            {
                                decimal subtotal = quantity * price;
                                dataGridViewSaleDetails.Rows.Add(productId, productName, quantity, price, subtotal);
                            }

                            UpdateTotalAmount();
                            txtBarcodeLabel.Clear();
                        }
                        else
                        {
                            MessageBox.Show("Product not found for the given barcode.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while fetching product: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void txtBarcodeLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true; // Prevents ding sound
                find_product_using_barcode(sender, e);
            }
        }

        private void txtrenderedamount_TextChanged(object sender, EventArgs e)
        {
            btnCalculatechange.Enabled = true;
        }

        private void comboProductCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboProductCategory.SelectedValue != null)
            {
                int selectedCategoryID = (int)comboProductCategory.SelectedValue;
                LoadProductsByCategory(selectedCategoryID);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Hide();
        }


        private async Task LoadEmployeeImageAsync(int employeeId)
        {
            pictureBox1.Image = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                SELECT TOP 1 ImageData 
                FROM ImageStore 
                WHERE EmployeeID = @empId AND IsActive = 1";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@empId", employeeId);
                        await conn.OpenAsync();

                        var result = await cmd.ExecuteScalarAsync();

                        if (result != null && result != DBNull.Value)
                        {
                            byte[] imgData = (byte[])result;
                            using (MemoryStream ms = new MemoryStream(imgData))
                            {
                                pictureBox1.Image = Image.FromStream(ms);
                            }
                        }
                        else
                        {
                            MessageBox.Show("No active image found for this employee.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               
                MessageBox.Show("Error loading image: " + ex.Message);
            }
        }


        private async void CheckConnectionButton_Click()
        {
            lblinternet.Text = "Checking...";
            lblinternet.ForeColor = Color.Orange;
            bool isConnected = await IsInternetAvailableAsync();

          
            if (isConnected)
            {
                lblinternet.Text = "";
                lblinternet.ForeColor = Color.Green;
            }
            else
            {
                lblinternet.Text = "No Internet Connection";
                lblinternet.ForeColor = Color.Red;
            }
        }

        
        public async Task<bool> IsInternetAvailableAsync()
        {
            try
            {
                using (var ping = new Ping())
                {
                    
                    var reply = await ping.SendPingAsync("8.8.8.8", 2000); // 2-second timeout

                   
                    return reply.Status == IPStatus.Success;
                }
            }
            catch (PingException)
            {
                
                return false;
            }
            catch (Exception)
            {
    
                return false;
            }
        }


     

        private void txtRecipientEmail_TextChanged(object sender, EventArgs e)
        {
            VerifyEmailAddress();
        }

        private void VerifyEmailAddress()
        {
            string email = txtRecipientEmail.Text.Trim();

            // Reset label color
            txtRecipientEmail.ForeColor = Color.Black;
            lblemailvalid.Visible = false;
            // Check if email is empty
            if (string.IsNullOrEmpty(email))
            {
                txtRecipientEmail.ForeColor = Color.Red;
                lblemailvalid.Visible = true;
                return;
            }

            try
            {
                // Check email format using MailAddress
                var addr = new System.Net.Mail.MailAddress(email);

                // Additional checks for common email validation
                if (!email.Contains("@") || !email.Contains("."))
                {
                    txtRecipientEmail.ForeColor = Color.Red;
                    lblemailvalid.Visible = true;
                    return;
                }

                // Check for valid domain (basic check)
                string[] domainParts = email.Split('@');
                if (domainParts.Length != 2 || string.IsNullOrEmpty(domainParts[1]))
                {
                    txtRecipientEmail.ForeColor = Color.Red;
                    lblemailvalid.Visible = true;
                    return;
                }
            }
            catch (FormatException)
            {
                txtRecipientEmail.ForeColor = Color.Red;
                lblemailvalid.Visible = true;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            UploadImageForEmployee(EmployeeNumber);
            _ = LoadEmployeeImageAsync(EmployeeNumber);
        }

        private void UploadImageForEmployee(int employeeId)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Create thumbnail (e.g., max 300px width/height)
                    Image originalImage = Image.FromFile(ofd.FileName);
                    Image thumbnail = CreateThumbnail(originalImage, 300);

                    // Convert thumbnail to byte array
                    byte[] imageData;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        // Save in JPEG format with 80% quality (adjust as needed)
                        var encoderParams = new EncoderParameters(1);
                        encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 80L);

                        ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");
                        thumbnail.Save(ms, jpegCodec, encoderParams);
                        imageData = ms.ToArray();
                    }

                    string fileName = Path.GetFileName(ofd.FileName);

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        SqlTransaction transaction = conn.BeginTransaction();

                        try
                        {
                            // Step 1: Deactivate old images
                            string deactivateQuery = @"
                        UPDATE ImageStore 
                        SET IsActive = 0 
                        WHERE EmployeeID = @empId AND IsActive = 1";

                            SqlCommand deactivateCmd = new SqlCommand(deactivateQuery, conn, transaction);
                            deactivateCmd.Parameters.AddWithValue("@empId", employeeId);
                            deactivateCmd.ExecuteNonQuery();

                            // Step 2: Insert new image
                            string insertQuery = @"
                        INSERT INTO ImageStore (ImageName, ImageData, EmployeeID, IsActive, [Date]) 
                        VALUES (@name, @data, @empId, 1, GETDATE())";

                            SqlCommand insertCmd = new SqlCommand(insertQuery, conn, transaction);
                            insertCmd.Parameters.AddWithValue("@name", fileName);
                            insertCmd.Parameters.AddWithValue("@data", imageData);
                            insertCmd.Parameters.AddWithValue("@empId", employeeId);

                            insertCmd.ExecuteNonQuery();

                            transaction.Commit();
                            MessageBox.Show("Image uploaded successfully as thumbnail!");
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Error: " + ex.Message);
                        }
                        finally
                        {
                            originalImage.Dispose();
                            thumbnail.Dispose();
                            conn.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error processing image: " + ex.Message);
                }
            }
        }

        private Image CreateThumbnail(Image original, int maxSize)
        {
            // Calculate new dimensions
            int newWidth, newHeight;
            if (original.Width > original.Height)
            {
                newWidth = maxSize;
                newHeight = (int)(original.Height * ((float)maxSize / original.Width));
            }
            else
            {
                newHeight = maxSize;
                newWidth = (int)(original.Width * ((float)maxSize / original.Height));
            }

            // Create thumbnail
            Bitmap thumbnail = new Bitmap(newWidth, newHeight);
            using (Graphics g = Graphics.FromImage(thumbnail))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(original, 0, 0, newWidth, newHeight);
            }

            return thumbnail;
        }

        private ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.MimeType == mimeType)
                    return codec;
            }
            return null;
        }
    }
}





