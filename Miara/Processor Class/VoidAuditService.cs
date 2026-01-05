using System;
using System.Data.SqlClient;
using System.Windows.Forms;

public class VoidAuditService
{
    private readonly string _connectionString;

    public VoidAuditService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void LogVoidOperation(int saleId, int productId, string productName, int quantity,
                                 decimal price, decimal subtotal, string voidType,
                                 string voidReason, string voidedBy)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO VoidAuditLog (
                                   SaleID, ProductID, ProductName, Quantity, Price, Subtotal,
                                   VoidType, VoidReason, VoidedBy, VoidDate, Workstation
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
}
