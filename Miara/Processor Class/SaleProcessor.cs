using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace Miara
{
    public class SaleProcessor
    {
        private readonly string _connectionString;
        private readonly string _deviceInternetID;
        private readonly string _employeeInfo;

        public SaleProcessor(string connectionString, string deviceInternetID, string employeeInfo)
        {
            _connectionString = connectionString;
            _deviceInternetID = deviceInternetID;
            _employeeInfo = employeeInfo;
        }

        // Updated signature to accept renderedAmountText. The 'logReceiptData' Action is no longer needed.
        public void CompleteSale(DataGridView saleDetails, string discountText, bool discountChecked,
                                 string paymentMethod, string cashbackText, string renderedAmountText,
                                 bool sendEmail, Action<string> logAudit, Action resetForm,
                                 Func<string> generateReceipt, Action<string> sendEmailAction,
                                 Action printReceipt, Action<int> completeCashback,
                                 Func<int> getNextSaleId, out int nextSaleId)
        {
            // 1. All calculations are performed once at the beginning.
            decimal subtotal = CalculateSubtotal(saleDetails);
            var (discountPercentage, discountValue) = CalculateDiscount(subtotal, discountText, discountChecked, logAudit);
            decimal subtotalAfterDiscount = Math.Max(0, subtotal - discountValue);
            decimal tax = subtotalAfterDiscount * 0.15m;
            decimal cashbackAmount = ParseCashback(cashbackText, logAudit);
            decimal finalTotal = subtotalAfterDiscount + tax + cashbackAmount;

            decimal renderedAmount = 0;
            decimal change = 0;
            if (paymentMethod == "CASH" && decimal.TryParse(renderedAmountText, out decimal parsedAmount))
            {
                renderedAmount = parsedAmount;
                change = renderedAmount - finalTotal;
            }

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        // 2. All database writes occur within this single, atomic transaction.
                        int saleId = InsertSale(conn, tran, finalTotal, paymentMethod, logAudit);
                        InsertSaleDetails(conn, tran, saleId, saleDetails, logAudit);

                        if (discountValue > 0)
                            InsertDiscount(conn, tran, saleId, discountPercentage, discountValue, logAudit);

                        InsertPayment(conn, tran, saleId, finalTotal, paymentMethod, cashbackAmount, logAudit);

                        // **FIX: The new receipt insertion is now part of the transaction.**
                        InsertReceipt(conn, tran, saleId, paymentMethod, subtotal, discountValue, tax, finalTotal, renderedAmount, change, logAudit);

                        UpdateCouponSale(conn, tran, saleId, "COUPON_CODE_PLACEHOLDER", logAudit); // Replace with actual coupon code if applicable

                        tran.Commit();
                        logAudit("Transaction committed");

                        // 3. Post-transaction actions (printing, emailing) can now safely occur.
                        printReceipt();
                        logAudit("Printed receipt");

                        if (sendEmail)
                        {
                            try
                            {
                                string receiptContent = generateReceipt();
                                sendEmailAction(receiptContent);
                                logAudit("Sent receipt email");
                            }
                            catch (Exception ex)
                            {
                                logAudit("Failed to send receipt email: " + ex.Message);
                                MessageBox.Show($"Failed to send email: {ex.Message}", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                        completeCashback(saleId);
                        logAudit($"Completed cashback for sale ID: {saleId}");

                        MessageBox.Show("Sale completed successfully!");

                        resetForm();
                        nextSaleId = getNextSaleId();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        logAudit("Transaction rolled back: " + ex.Message);
                        throw; // Re-throw the exception so the UI layer can handle it (e.g., re-enable a button).
                    }
                }
            }
        }

        
        private void InsertReceipt(SqlConnection conn, SqlTransaction tran, int saleId, string paymentMethod,
                                   decimal subtotal, decimal discount, decimal tax, decimal total,
                                   decimal amountRendered, decimal change, Action<string> logAudit)
        {
            string query = @"
                INSERT INTO Receipts (ReceiptNumber, Date, EmployeeName, PaymentMethod, Subtotal, Discount, Tax, Total, AmountRendered, [Change])
                VALUES (@ReceiptNumber, @Date, @EmployeeName, @PaymentMethod, @Subtotal, @Discount, @Tax, @Total, @AmountRendered, @Change)";

            using (SqlCommand command = new SqlCommand(query, conn, tran))
            {
                command.Parameters.AddWithValue("@ReceiptNumber", saleId);
                command.Parameters.AddWithValue("@Date", DateTime.Now);
                command.Parameters.AddWithValue("@EmployeeName", _employeeInfo); // Use the employee info from the constructor
                command.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                command.Parameters.AddWithValue("@Subtotal", subtotal);
                command.Parameters.AddWithValue("@Discount", discount);
                command.Parameters.AddWithValue("@Tax", tax);
                command.Parameters.AddWithValue("@Total", total);
                command.Parameters.AddWithValue("@AmountRendered", amountRendered);
                command.Parameters.AddWithValue("@Change", change);

                command.ExecuteNonQuery();
                logAudit($"Inserted receipt data for sale ID: {saleId}");
            }
        }

       
        private decimal CalculateSubtotal(DataGridView saleDetails)
        {
            return saleDetails.Rows
                .Cast<DataGridViewRow>()
                .Where(row => !row.IsNewRow && row.Cells["Subtotal"].Value != null)
                .Sum(row => Convert.ToDecimal(row.Cells["Subtotal"].Value));
        }

      
        private (decimal, decimal) CalculateDiscount(decimal subtotal, string discountText, bool discountChecked, Action<string> logAudit)
        {
            if (discountChecked && decimal.TryParse(discountText, out decimal parsedDiscount) && parsedDiscount >= 0 && parsedDiscount <= 100)
            {
                decimal discountPercentage = parsedDiscount / 100m; // Use 'm' for decimal literal
                decimal discountValue = subtotal * discountPercentage;
                logAudit($"Applied discount {parsedDiscount}%");
                return (discountPercentage, discountValue);
            }
            return (0, 0);
        }

        private decimal ParseCashback(string cashbackText, Action<string> logAudit)
        {
            if (decimal.TryParse(cashbackText.Replace("Cashback:", "").Replace("R", "").Trim(), out decimal cashback))
            {
                return cashback;
            }
            logAudit("Invalid cashback input");
            return 0;
        }

     
        #region Unchanged Database Methods
        private int InsertSale(SqlConnection conn, SqlTransaction tran, decimal finalTotal, string paymentMethod, Action<string> logAudit)
        {
            string query = @"INSERT INTO Sale (SaleDate, TotalAmount, PaymentMethod, EmployeeID) 
                             VALUES (@SaleDate, @TotalAmount, @PaymentMethod, @EmployeeID);
                             SELECT SCOPE_IDENTITY();";
            using (var cmd = new SqlCommand(query, conn, tran))
            {
                cmd.Parameters.AddWithValue("@SaleDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@TotalAmount", finalTotal);
                cmd.Parameters.AddWithValue("@PaymentMethod", paymentMethod ?? "N/A");
                cmd.Parameters.AddWithValue("@EmployeeID", _employeeInfo);
                int saleId = Convert.ToInt32(cmd.ExecuteScalar());
                logAudit($"Inserted sale record ID {saleId}");
                return saleId;
            }
        }

        private void InsertSaleDetails(SqlConnection conn, SqlTransaction tran, int saleId, DataGridView saleDetails, Action<string> logAudit)
        {
            string query = @"INSERT INTO SalesDetails (SaleID, ProductID, Quantity, UnitPrice, Subtotal, Tax) 
                             VALUES (@SaleID, @ProductID, @Quantity, @UnitPrice, @Subtotal, @Tax)";
            using (var cmd = new SqlCommand(query, conn, tran))
            {
                cmd.Parameters.Add("@SaleID", SqlDbType.Int).Value = saleId;
                var productIdParam = cmd.Parameters.Add("@ProductID", SqlDbType.Int);
                var quantityParam = cmd.Parameters.Add("@Quantity", SqlDbType.Int);
                var unitPriceParam = cmd.Parameters.Add("@UnitPrice", SqlDbType.Decimal);
                var subtotalParam = cmd.Parameters.Add("@Subtotal", SqlDbType.Decimal);
                var taxParam = cmd.Parameters.Add("@Tax", SqlDbType.Decimal);

                foreach (DataGridViewRow row in saleDetails.Rows)
                {
                    if (row.IsNewRow || row.Cells["ProductID"].Value == null) continue;
                    productIdParam.Value = (int)row.Cells["ProductID"].Value;
                    quantityParam.Value = Convert.ToInt32(row.Cells["Quantity"].Value);
                    unitPriceParam.Value = Convert.ToDecimal(row.Cells["Price"].Value);
                    subtotalParam.Value = Convert.ToDecimal(row.Cells["Subtotal"].Value);
                    taxParam.Value = Convert.ToDecimal(row.Cells["Subtotal"].Value) * 0.15m;
                    cmd.ExecuteNonQuery();
                }
                logAudit($"Inserted {saleDetails.Rows.Count - 1} sale detail line(s) for Sale ID {saleId}");
            }
        }

        private void InsertDiscount(SqlConnection conn, SqlTransaction tran, int saleId, decimal discountPercentage, decimal discountValue, Action<string> logAudit)
        {
            string query = @"INSERT INTO Discounts (SaleID, DiscountPercentage, DiscountValue, DiscountDate) 
                             VALUES (@SaleID, @DiscountPercentage, @DiscountValue, @DiscountDate)";
            using (var cmd = new SqlCommand(query, conn, tran))
            {
                cmd.Parameters.AddWithValue("@SaleID", saleId);
                cmd.Parameters.AddWithValue("@DiscountPercentage", discountPercentage * 100);
                cmd.Parameters.AddWithValue("@DiscountValue", discountValue);
                cmd.Parameters.AddWithValue("@DiscountDate", DateTime.Now);
                cmd.ExecuteNonQuery();
                logAudit($"Inserted discount for sale {saleId}");
            }
        }

        private void InsertPayment(SqlConnection conn, SqlTransaction tran, int saleId, decimal finalTotal, string paymentMethod, decimal cashbackAmount, Action<string> logAudit)
        {
            string query = @"INSERT INTO Payments (SaleID, AmountPaid, PaymentDate, PaymentMethod, Cashback) 
                             VALUES (@SaleID, @AmountPaid, @PaymentDate, @PaymentMethod, @Cashback)";
            using (var cmd = new SqlCommand(query, conn, tran))
            {
                cmd.Parameters.AddWithValue("@SaleID", saleId);
                cmd.Parameters.AddWithValue("@AmountPaid", finalTotal);
                cmd.Parameters.AddWithValue("@PaymentDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@PaymentMethod", paymentMethod ?? "N/A");
                cmd.Parameters.AddWithValue("@Cashback", cashbackAmount);
                cmd.ExecuteNonQuery();
                logAudit($"Inserted payment for sale {saleId}");
            }
        }


        private void UpdateCouponSale(SqlConnection conn, SqlTransaction tran, int saleId, string couponCode, Action<string> logAudit)
        {
            string query = @"UPDATE Coupons SET SaleID = @SaleID WHERE CouponCode = @CouponCode";
            using (var cmd = new SqlCommand(query, conn, tran))
            {
                cmd.Parameters.AddWithValue("@SaleID", saleId);
                cmd.Parameters.AddWithValue("@CouponCode", couponCode);
                int rowsAffected = cmd.ExecuteNonQuery();
                logAudit($"Updated coupon {couponCode} for sale {saleId}, rows affected: {rowsAffected}");
            }
        }
       
        #endregion
    }
}