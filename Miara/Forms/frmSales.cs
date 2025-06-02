using Miara.Forms;
using Microsoft.VisualBasic;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Miara
{
    public partial class frmSales : Form
    {
        private static readonly string configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config.xml");
        private string connectionString;
        private int nextSaleIdn;
        private decimal TaxTotal;
        decimal Discount = 0;


        public frmSales(string firstName, string surname, int EMID)
        {
            InitializeComponent();
            LoadSQLConnectionInfo();
            LoadProducts();
            LoadCategories();
            LoadPaymentMethods();
            int nextSaleId = GetNextSaleId();
            ScrapOldLogCashback(nextSaleId);
            EnsureRequiredColumns();
            this.KeyPreview = true;
            txtRecipientEmail.Visible = checkEmail.Checked;
            nextSaleIdn = nextSaleId;
            lblNextSalesNumber.Text = $"SALES ORDER NO: {nextSaleId}";
            txtBarcodeLabel.KeyDown += txtBarcodeLabel_KeyDown;
            txtQuantity.KeyDown += Control_KeyDown;
            comboBoxProducts.KeyDown += Control_KeyDown;
            timer1.Start();
            this.FormClosed += (sender, e) => Application.Exit();
            label2.Text = $"WELCOME {firstName} {surname} ";
            employeeFirstName = firstName;
            employeeSurname = surname;
            EMployeeNumber = EMID;
            btnCompleteSale.Enabled = false;
            this.MouseDown += frmSales_MouseDown;
            toolStripChangeBackColour.Click += toolStripChangeBackColour_Click;
            RoundButtonCorners(btnCalculatechange, 30);



        }

        private void MakeButtonRound(Button btn)
        {
            int radius = btn.Height;
            GraphicsPath circle = new GraphicsPath();
            circle.AddEllipse(0, 0, radius, radius);
            btn.Region = new Region(circle);
        }




        string employeeFirstName;
        string employeeSurname;
        int EMployeeNumber;
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
                    MessageBox.Show("Failed to load connection information: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
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
                MessageBox.Show($"Error loading products: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRemoveProduct_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewSaleDetails.SelectedRows)
            {
                dataGridViewSaleDetails.Rows.Remove(row);
            }
            UpdateTotalAmount();
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

                // Final total = subtotal after discount + cashback
                decimal finalTotal = subtotalAfterDiscount + cashbackAmount;

                // Update labels
                lbldiscountvalue.Text = $"Discount: R {discountValue:F2}";
                lblSubtotalorder.Text = $"Subtotal: R {FinalSubtotal:F2}";
                lblTax.Text = $"TAX (15%): R {tax:F2}";
                lblCashBack.Text = $"Cashback: R {cashbackAmount:F2}";
                lblTotalAmount.Text = $"Total: R {finalTotal:F2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while calculating the total: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnCompleteSale_Click(object sender, EventArgs e)
        {
            btnCompleteSale.Enabled = false;

            decimal subtotal = 0;

            // Calculate subtotal from DataGridView
            foreach (DataGridViewRow row in dataGridViewSaleDetails.Rows)
            {
                if (!row.IsNewRow && row.Cells["Subtotal"].Value != null)
                {
                    subtotal += Convert.ToDecimal(row.Cells["Subtotal"].Value);
                }
            }

            // Calculate discount
            decimal discountValue = 0;
            decimal discountPercentage = 0;
            if (checkdiscount.Checked && !string.IsNullOrWhiteSpace(txtdiscount.Text) &&
                decimal.TryParse(txtdiscount.Text, out decimal parsedDiscount) && parsedDiscount >= 0 && parsedDiscount <= 100)
            {
                discountPercentage = parsedDiscount / 100;
                discountValue = subtotal * discountPercentage;
            }

            decimal subtotalAfterDiscount = subtotal - discountValue;
            if (subtotalAfterDiscount < 0) subtotalAfterDiscount = 0;

            // Calculate tax (15%)
            decimal tax = subtotalAfterDiscount * 0.15m;

            // Cashback
            decimal cashbackAmount = decimal.Parse(lblCashBack.Text.Replace("Cashback:", "").Replace("R", "").Trim()); ;


            // Final total
            decimal finalTotal = subtotalAfterDiscount + cashbackAmount;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Insert Sale
                    string saleQuery = @"
                INSERT INTO Sale (SaleDate, TotalAmount, PaymentMethod, EmployeeID) 
                VALUES (@SaleDate, @TotalAmount, @PaymentMethod, @EmployeeID);
                SELECT SCOPE_IDENTITY();";

                    SqlCommand saleCommand = new SqlCommand(saleQuery, connection, transaction);
                    saleCommand.Parameters.AddWithValue("@SaleDate", DateTime.Now);
                    saleCommand.Parameters.AddWithValue("@TotalAmount", finalTotal);
                    saleCommand.Parameters.AddWithValue("@PaymentMethod", combopaymentmentod.SelectedValue?.ToString() ?? "N/A");
                    saleCommand.Parameters.AddWithValue("@EmployeeID", EMployeeNumber);

                    int saleId = Convert.ToInt32(saleCommand.ExecuteScalar());

                    // Insert Sale Details
                    foreach (DataGridViewRow row in dataGridViewSaleDetails.Rows)
                    {
                        if (row.IsNewRow || row.Cells["ProductID"].Value == null) continue;

                        var productId = (int)row.Cells["ProductID"].Value;
                        var quantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                        var unitPrice = Convert.ToDecimal(row.Cells["Price"].Value);
                        var rowSubtotal = Convert.ToDecimal(row.Cells["Subtotal"].Value);
                        var rowTax = rowSubtotal * 0.15m;

                        string detailsQuery = @"
                    INSERT INTO SalesDetails (SaleID, ProductID, Quantity, UnitPrice, Subtotal, Tax) 
                    VALUES (@SaleID, @ProductID, @Quantity, @UnitPrice, @Subtotal, @Tax)";

                        SqlCommand detailsCommand = new SqlCommand(detailsQuery, connection, transaction);
                        detailsCommand.Parameters.AddWithValue("@SaleID", saleId);
                        detailsCommand.Parameters.AddWithValue("@ProductID", productId);
                        detailsCommand.Parameters.AddWithValue("@Quantity", quantity);
                        detailsCommand.Parameters.AddWithValue("@UnitPrice", unitPrice);
                        detailsCommand.Parameters.AddWithValue("@Subtotal", rowSubtotal);
                        detailsCommand.Parameters.AddWithValue("@Tax", rowTax);
                        detailsCommand.ExecuteNonQuery();
                    }

                    // Insert Discount if any
                    if (checkdiscount.Checked && discountValue > 0)
                    {
                        string discountQuery = @"
                    INSERT INTO Discounts (SaleID, DiscountPercentage, DiscountValue, DiscountDate) 
                    VALUES (@SaleID, @DiscountPercentage, @DiscountValue, @DiscountDate)";

                        SqlCommand discountCommand = new SqlCommand(discountQuery, connection, transaction);
                        discountCommand.Parameters.AddWithValue("@SaleID", saleId);
                        discountCommand.Parameters.AddWithValue("@DiscountPercentage", discountPercentage * 100);
                        discountCommand.Parameters.AddWithValue("@DiscountValue", discountValue);
                        discountCommand.Parameters.AddWithValue("@DiscountDate", DateTime.Now);
                        discountCommand.ExecuteNonQuery();
                    }

                    // Insert Payment
                    string paymentQuery = @"
                INSERT INTO Payments (SaleID, AmountPaid, PaymentDate, PaymentMethod, Cashback) 
                VALUES (@SaleID, @AmountPaid, @PaymentDate, @PaymentMethod,@Cashback)";

                    SqlCommand paymentCommand = new SqlCommand(paymentQuery, connection, transaction);
                    paymentCommand.Parameters.AddWithValue("@SaleID", saleId);
                    paymentCommand.Parameters.AddWithValue("@AmountPaid", finalTotal);
                    paymentCommand.Parameters.AddWithValue("@PaymentDate", DateTime.Now);
                    paymentCommand.Parameters.AddWithValue("@PaymentMethod", combopaymentmentod.SelectedValue?.ToString() ?? "N/A");
                    paymentCommand.Parameters.AddWithValue("@Cashback", cashbackAmount);
                    paymentCommand.ExecuteNonQuery();

                    PrintReceipt();
                    LogReceiptData();

                    if (checkEmail.Checked)
                    {
                        try
                        {
                            string receiptContent = GenerateReceiptContent();
                            SendReceiptEmail(receiptContent);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Failed to send email: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    CompleteCashback(nextSaleIdn);
                    transaction.Commit();

                    MessageBox.Show("Sale completed successfully!");
                    dataGridViewSaleDetails.Rows.Clear();
                    UpdateTotalAmount();
                    int nextSaleId = GetNextSaleId();
                    nextSaleIdn = nextSaleId;
                    lblNextSalesNumber.Text = $"SALES ORDER NO: {nextSaleId}";
                    txtQuantity.Text = "1";
                    comboBoxProducts.SelectedItem = null;
                    combopaymentmentod.SelectedIndex = -1;
                    txtrenderedamount.Clear();
                    lblChange.Text = "Change: R 0.00";
                    lblTotalAmount.Text = "Total: R 0.00";
                    lblCashBack.Text = "Total: R 0.00";
                    txtdiscount.Text = "";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show($"Error completing sale: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void PrintReceipt()
        {
            try
            {
                PrintDocument printDocument = new PrintDocument();
                printDocument.PrintPage += (sender, e) =>
                {
                    Graphics graphics = e.Graphics;
                    Font font = new Font("Courier New", 10);
                    int y = 20;

                    // Header
                    graphics.DrawString("********* MIARA TRADING PTY LTD *********", font, Brushes.Black, 10, y); y += 20;
                    graphics.DrawString("Address: 123 Mosaka St.", font, Brushes.Black, 10, y); y += 20;
                    graphics.DrawString("Phone: 012-345-6789 | Email: info@miaratrading.com", font, Brushes.Black, 10, y); y += 20;
                    graphics.DrawString("-----------------------------------------", font, Brushes.Black, 10, y); y += 20;

                    // Receipt Info
                    graphics.DrawString($"Receipt No: {nextSaleIdn}", font, Brushes.Black, 10, y); y += 20;
                    graphics.DrawString($"Date: {DateTime.Now:dd/MM/yyyy HH:mm:ss}", font, Brushes.Black, 10, y); y += 20;

                    // Add Employee Name
                    graphics.DrawString($"Employee: {employeeFirstName} {employeeSurname}", font, Brushes.Black, 10, y); y += 20;

                    // Payment Method
                    string paymentMethod = combopaymentmentod.SelectedValue?.ToString() ?? "N/A";
                    graphics.DrawString($"Payment Method: {paymentMethod}", font, Brushes.Black, 10, y); y += 20;

                    // Items Header
                    graphics.DrawString("Item              Qty   Price     Total", font, Brushes.Black, 10, y); y += 20;
                    graphics.DrawString("-----------------------------------------", font, Brushes.Black, 10, y); y += 20;

                    // Items List
                    decimal total = 0;
                    if (dataGridViewSaleDetails != null)
                    {
                        foreach (DataGridViewRow row in dataGridViewSaleDetails.Rows)
                        {
                            if (row.Cells["ProductName"].Value != null)
                            {
                                string productName = row.Cells["ProductName"].Value.ToString();
                                int quantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                                decimal price = Convert.ToDecimal(row.Cells["Price"].Value);
                                decimal subtotal = Convert.ToDecimal(row.Cells["Subtotal"].Value);

                                graphics.DrawString($"{productName,-15} {quantity,3} {price,9:C} {subtotal,10:C}", font, Brushes.Black, 10, y);
                                y += 20;

                                total += subtotal;
                            }
                        }
                    }

                    // Calculate discount
                    decimal discountValue = 0;
                    decimal discountPercentage = 0;

                    if (checkdiscount.Checked && !string.IsNullOrWhiteSpace(txtdiscount.Text) &&
                        decimal.TryParse(txtdiscount.Text, out decimal parsedDiscount) && parsedDiscount >= 0 && parsedDiscount <= 100)
                    {
                        discountPercentage = parsedDiscount / 100;
                        discountValue = total * discountPercentage;
                    }

                    // Subtotal after applying discount
                    decimal subtotalAfterDiscount = total - discountValue;
                    if (subtotalAfterDiscount < 0) subtotalAfterDiscount = 0;

                    // Calculate tax (15% of subtotal after discount)
                    decimal tax = subtotalAfterDiscount * 0.15m;

                    // Get cashback amount using the correct sale ID
                    decimal cashbackAmount = decimal.Parse(lblCashBack.Text.Replace("Cashback:", "").Replace("R", "").Trim());

                    // Final total: Subtotal after discount plus tax minus cashback
                    decimal finalTotal = subtotalAfterDiscount + cashbackAmount;

                    // Print totals and discount
                    graphics.DrawString("-----------------------------------------", font, Brushes.Black, 10, y); y += 20;
                    graphics.DrawString($"Subtotal: {total,25:C}", font, Brushes.Black, 10, y); y += 20;

                    if (checkdiscount.Checked)
                    {
                        graphics.DrawString($"Discount ({discountPercentage * 100:F0}%): {discountValue,17:C}", font, Brushes.Black, 10, y); y += 20;
                    }

                    graphics.DrawString($"Subtotal After Discount: {subtotalAfterDiscount,12:C}", font, Brushes.Black, 10, y); y += 20;
                    graphics.DrawString($"Tax (15%): {tax,26:C}", font, Brushes.Black, 10, y); y += 20;

                    // Add cashback line if there is any cashback
                    if (cashbackAmount > 0)
                    {
                        graphics.DrawString($"Cashback Applied: {cashbackAmount,19:C}", font, Brushes.Black, 10, y); y += 20;
                    }

                    graphics.DrawString($"Total: {finalTotal,29:C}", new Font("Courier New", 10, FontStyle.Bold), Brushes.Black, 10, y); y += 20;

                    if (paymentMethod == "CASH")
                    {
                        // Payment Details for CASH
                        decimal renderedAmount = decimal.Parse(txtrenderedamount.Text);
                        decimal change = renderedAmount - finalTotal;

                        graphics.DrawString("-----------------------------------------", font, Brushes.Black, 10, y); y += 20;
                        graphics.DrawString($"Amount Rendered: {renderedAmount,19:C}", font, Brushes.Black, 10, y); y += 20;
                        graphics.DrawString($"Change: {change,30:C}", font, Brushes.Black, 10, y); y += 40;
                    }

                    // Footer
                    graphics.DrawString("Thank you for shopping with us!", font, Brushes.Black, 10, y); y += 20;
                    graphics.DrawString("Visit us again!", font, Brushes.Black, 10, y); y += 20;
                    graphics.DrawString("-----------------------------------------", font, Brushes.Black, 10, y); y += 20;
                    graphics.DrawString("---------- MOSAKA SYSTEM ----------", font, Brushes.Black, 10, y); y += 20;
                };

                // Create and display the print preview dialog
                PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog
                {
                    Document = printDocument,
                    WindowState = FormWindowState.Maximized
                };

                if (printPreviewDialog.ShowDialog() == DialogResult.OK)
                {
                    printDocument.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while printing the receipt: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void DrawLineSeparator(Graphics graphics, Font font, ref int y)
        {
            graphics.DrawString("-----------------------------------------", font, Brushes.Black, 10, y); y += 20;
        }

        private void DrawStringCentered(Graphics graphics, string text, Font font, ref int y)
        {
            int centerX = (int)(graphics.VisibleClipBounds.Width / 2 - graphics.MeasureString(text, font).Width / 2);
            graphics.DrawString(text, font, Brushes.Black, centerX, y); y += 20;
        }



        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
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
                    comboProductCategory.DataSource = null;
                }
            }
        }





        private DataTable GetData(string query)
        {

            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }
            return dt;
        }

        private void SendReceiptViaEmail(string recipientEmail, string receiptContent)
        {
            try
            {
                // Configure SMTP client for Outlook
                SmtpClient smtpClient = new SmtpClient("smtp.office365.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("your-emai@outlook.com", "your-password"),
                    EnableSsl = true
                };

                // Create the email message
                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("your-email@outlook.com"),
                    Subject = "Your Receipt from MIARA TRADING PTY LTD",
                    Body = receiptContent,
                    IsBodyHtml = false // Set to true if you want to use HTML formatting
                };

                // Add the recipient's email address
                mailMessage.To.Add(recipientEmail);

                // Send the email
                smtpClient.Send(mailMessage);

                MessageBox.Show("Receipt sent successfully via email.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SmtpException smtpEx)
            {
                MessageBox.Show($"SMTP Error: {smtpEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending email: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string GenerateReceiptContent()
        {
            // Get cashback amount for this sale
            decimal cashbackAmount = decimal.Parse(lblCashBack.Text.Replace("Cashback:", "").Replace("R", "").Trim());

            StringBuilder receiptContent = new StringBuilder();
            receiptContent.AppendLine("<!DOCTYPE html>");
            receiptContent.AppendLine("<html lang='en'>");
            receiptContent.AppendLine("<head>");
            receiptContent.AppendLine("<meta charset='UTF-8'>");
            receiptContent.AppendLine("<meta name='viewport' content='width=device-width, initial-scale=1.0'>");
            receiptContent.AppendLine("<title>Receipt</title>");
            receiptContent.AppendLine("<style>");
            receiptContent.AppendLine("body { font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; margin: 0; padding: 20px; background-color: #f9f9f9; }");
            receiptContent.AppendLine(".receipt { max-width: 350px; margin: 0 auto; background: #fff; border-radius: 8px; box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1); padding: 20px; }");
            receiptContent.AppendLine(".header { text-align: center; margin-bottom: 20px; }");
            receiptContent.AppendLine(".header h2 { margin: 0; font-size: 1.5em; color: #333; }");
            receiptContent.AppendLine(".header p { margin: 5px 0; font-size: 0.9em; color: #666; }");
            receiptContent.AppendLine(".divider { border-top: 1px dashed #ddd; margin: 15px 0; }");
            receiptContent.AppendLine(".text-right { text-align: right; }");
            receiptContent.AppendLine(".text-center { text-align: center; }");
            receiptContent.AppendLine(".receipt-info p { margin: 5px 0; font-size: 0.9em; color: #444; }");
            receiptContent.AppendLine(".items-header { font-weight: bold; margin-bottom: 10px; font-size: 0.9em; color: #333; }");
            receiptContent.AppendLine(".items-list p { margin: 5px 0; font-size: 0.9em; color: #555; }");
            receiptContent.AppendLine(".totals p { margin: 5px 0; font-size: 0.9em; color: #333; }");
            receiptContent.AppendLine(".footer { margin-top: 20px; font-size: 0.9em; color: #666; }");
            receiptContent.AppendLine("</style>");
            receiptContent.AppendLine("</head>");
            receiptContent.AppendLine("<body>");

            receiptContent.AppendLine("<div class='receipt'>");
            receiptContent.AppendLine("<div class='header'>");
            receiptContent.AppendLine("<h2>MIARA TRADING PTY LTD</h2>");
            receiptContent.AppendLine("<p>123 Mosaka St.</p>");
            receiptContent.AppendLine("<p>Phone: 012-345-6789 | Email: info@miaratrading.com</p>");
            receiptContent.AppendLine("</div>");
            receiptContent.AppendLine("<div class='divider'></div>");
            receiptContent.AppendLine("<div class='receipt-info'>");
            receiptContent.AppendLine($"<p><strong>Receipt No:</strong> {nextSaleIdn}</p>");
            receiptContent.AppendLine($"<p><strong>Date:</strong> {DateTime.Now:dd/MM/yyyy HH:mm:ss}</p>");
            receiptContent.AppendLine($"<p><strong>Employee:</strong> {employeeFirstName} {employeeSurname}</p>");
            string paymentMethod = combopaymentmentod.SelectedValue?.ToString() ?? "N/A";
            receiptContent.AppendLine($"<p><strong>Payment Method:</strong> {paymentMethod}</p>");
            receiptContent.AppendLine("</div>");
            receiptContent.AppendLine("<div class='divider'></div>");
            receiptContent.AppendLine("<div class='items-header'>");
            receiptContent.AppendLine("<p><strong>Item              Qty   Price     Total</strong></p>");
            receiptContent.AppendLine("</div>");
            receiptContent.AppendLine("<div class='divider'></div>");

            // Items List
            decimal total = 0;
            if (dataGridViewSaleDetails != null)
            {
                foreach (DataGridViewRow row in dataGridViewSaleDetails.Rows)
                {
                    if (row.Cells["ProductName"].Value != null)
                    {
                        string productName = row.Cells["ProductName"].Value.ToString();
                        int quantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                        decimal price = Convert.ToDecimal(row.Cells["Price"].Value);
                        decimal subtotal = Convert.ToDecimal(row.Cells["Subtotal"].Value);

                        receiptContent.AppendLine($"<p>{productName,-15} {quantity,3} {price,9:C} {subtotal,10:C}</p>");
                        total += subtotal;
                    }
                }
            }

            // Calculate discount
            decimal discountValue = 0;
            decimal discountPercentage = 0;

            if (checkdiscount.Checked && !string.IsNullOrWhiteSpace(txtdiscount.Text) &&
                decimal.TryParse(txtdiscount.Text, out decimal parsedDiscount) && parsedDiscount >= 0 && parsedDiscount <= 100)
            {
                discountPercentage = parsedDiscount / 100;
                discountValue = total * discountPercentage;
            }

            // Calculate totals
            decimal subtotalAfterDiscount = total - discountValue;
            if (subtotalAfterDiscount < 0) subtotalAfterDiscount = 0;

            decimal tax = subtotalAfterDiscount * 0.15m;
            decimal finalTotal = subtotalAfterDiscount + cashbackAmount;

            receiptContent.AppendLine("<div class='divider'></div>");
            receiptContent.AppendLine("<div class='totals'>");
            receiptContent.AppendLine($"<p class='text-right'><strong>Subtotal:</strong> {total,25:C}</p>");

            if (checkdiscount.Checked)
            {
                receiptContent.AppendLine($"<p class='text-right'><strong>Discount ({discountPercentage * 100:F0}%):</strong> {discountValue,17:C}</p>");
            }

            receiptContent.AppendLine($"<p class='text-right'><strong>Subtotal After Discount:</strong> {subtotalAfterDiscount,12:C}</p>");
            receiptContent.AppendLine($"<p class='text-right'><strong>VAT (15%):</strong> {tax,26:C}</p>");

            // Add cashback line if applicable
            if (cashbackAmount > 0)
            {
                receiptContent.AppendLine($"<p class='text-right'><strong>Cashback Applied:</strong> {cashbackAmount,19:C}</p>");
            }

            receiptContent.AppendLine($"<p class='text-right'><strong>Total:</strong> {finalTotal,29:C}</p>");
            receiptContent.AppendLine("</div>");

            if (paymentMethod == "CASH")
            {
                decimal renderedAmount = decimal.Parse(txtrenderedamount.Text);
                decimal change = renderedAmount - finalTotal;

                receiptContent.AppendLine("<div class='divider'></div>");
                receiptContent.AppendLine("<div class='totals'>");
                receiptContent.AppendLine($"<p class='text-right'><strong>Amount Rendered:</strong> {renderedAmount,19:C}</p>");
                receiptContent.AppendLine($"<p class='text-right'><strong>Change:</strong> {change,30:C}</p>");
                receiptContent.AppendLine("</div>");
            }

            receiptContent.AppendLine("<div class='divider'></div>");
            receiptContent.AppendLine("<div class='footer text-center'>");
            receiptContent.AppendLine("<p>Thank you for shopping with us!</p>");
            receiptContent.AppendLine("<p>Visit us again!</p>");
            receiptContent.AppendLine("<p>---------- MOSAKA SYSTEM ----------</p>");
            receiptContent.AppendLine("</div>");
            receiptContent.AppendLine("</div>");
            receiptContent.AppendLine("</body>");
            receiptContent.AppendLine("</html>");

            return receiptContent.ToString();
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
                    return false;
                }
            }
            return true;
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
                MessageBox.Show($"SQL Error retrieving next SaleID: {sqlEx.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving next SaleID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return nextSaleId;
        }

        public string ReceivedValue { get; set; }

        private void frmSales_Load(object sender, EventArgs e)
        {
            DeleteCouponRedemptionBySaleId(nextSaleIdn);


            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                {
                    control.Enter += TextBox_Enter;
                }
            }
        }
        private void LogReceiptData()
        {
            try
            {
                decimal total = 0;
                foreach (DataGridViewRow row in dataGridViewSaleDetails.Rows)
                {
                    if (row.Cells["ProductName"].Value != null)
                    {
                        decimal subtotal = Convert.ToDecimal(row.Cells["Subtotal"].Value);
                        total += subtotal;
                    }
                }

                decimal discountValue = 0;
                decimal discountPercentage = 0;

                if (checkdiscount.Checked && !string.IsNullOrWhiteSpace(txtdiscount.Text) &&
                    decimal.TryParse(txtdiscount.Text, out decimal parsedDiscount) && parsedDiscount >= 0 && parsedDiscount <= 100)
                {
                    discountPercentage = parsedDiscount / 100;
                    discountValue = total * discountPercentage;
                }

                decimal subtotalAfterDiscount = total - discountValue;
                if (subtotalAfterDiscount < 0)
                    subtotalAfterDiscount = 0;

                decimal tax = subtotalAfterDiscount * 0.15m;
                decimal finalTotal = subtotalAfterDiscount;

                decimal renderedAmount = 0;
                decimal change = 0;
                string paymentMethod = combopaymentmentod.SelectedValue?.ToString() ?? "N/A";

                if (paymentMethod == "CASH")
                {
                    renderedAmount = decimal.Parse(txtrenderedamount.Text);
                    change = renderedAmount - finalTotal;
                }

                // Insert receipt data into the database
                string query = @"
            INSERT INTO Receipts (ReceiptNumber, Date, EmployeeName, PaymentMethod, Subtotal, Discount, Tax, Total, AmountRendered, Change)
            VALUES (@ReceiptNumber, @Date, @EmployeeName, @PaymentMethod, @Subtotal, @Discount, @Tax, @Total, @AmountRendered, @Change)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ReceiptNumber", nextSaleIdn);
                        command.Parameters.AddWithValue("@Date", DateTime.Now);
                        command.Parameters.AddWithValue("@EmployeeName", $"{employeeFirstName} {employeeSurname}");
                        command.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                        command.Parameters.AddWithValue("@Subtotal", total);
                        command.Parameters.AddWithValue("@Discount", discountValue);
                        command.Parameters.AddWithValue("@Tax", tax);
                        command.Parameters.AddWithValue("@Total", finalTotal);
                        command.Parameters.AddWithValue("@AmountRendered", renderedAmount);
                        command.Parameters.AddWithValue("@Change", change);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while logging receipt data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            new frmMainForm(employeeFirstName, employeeSurname, EMployeeNumber).ShowDialog();
        }

        private void comboProductCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboProductCategory.SelectedValue != null)
            {
                int selectedCategoryID = (int)comboProductCategory.SelectedValue;
                LoadProductsByCategory(selectedCategoryID);
            }
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


        private DataTable GetDataWithParameters(string query, params SqlParameter[] parameters)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddRange(parameters);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
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

                PrintReceipt();
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
            new frmCashback(employeeFirstName, employeeSurname, EMployeeNumber, nextSaleIdn).ShowDialog();
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


        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
      int nLeftRect,
      int nTopRect,
      int nRightRect,
      int nBottomRect,
      int nWidthEllipse,
      int nHeightEllipse
  );



        private void RoundButtonCorners(Button button, int radius)
        {
            button.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button.Width, button.Height, radius, radius));
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


                string voidedBy = $"{employeeFirstName} {employeeSurname} ({EMployeeNumber})";

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
            new frmMainForm(employeeFirstName, employeeSurname, EMployeeNumber).ShowDialog();
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
            string query = "SELECT MethodName FROM PaymentMethods WHERE IsActive = 1";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    combopaymentmentod.DataSource = dt;
                    combopaymentmentod.DisplayMember = "MethodName";
                    combopaymentmentod.ValueMember = "MethodName"; // same as DisplayMember if you only want the name
                    combopaymentmentod.SelectedIndex = -1; // no default selection
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading payment methods: " + ex.Message);
                }
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

        private void btn1_Click(object sender, EventArgs e)
        {

        }

        private void btn2_Click(object sender, EventArgs e)
        {

        }

        private void comboProductCategory_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }


}





