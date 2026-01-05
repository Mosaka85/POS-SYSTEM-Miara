using System;
using System.Data.SqlClient;

public class CouponService
{
    private readonly string _connectionString;

    public CouponService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public decimal GetDiscountBySaleId(int saleId)
    {
        decimal discount = 0;

        try
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT DiscountApplied FROM CouponRedemptions WHERE SaleID = @saleId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@saleId", saleId);
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                        discount = Convert.ToDecimal(result);
                }
            }
        }
        catch (Exception ex)
        {
           
            Console.WriteLine("Error retrieving discount: " + ex.Message);
        }

        return discount;
    }


}
