using System;
using System.Data.SqlClient;

namespace Miara.POS.Services
{
    public class ProductService
    {
        private readonly string _connectionString;

        public ProductService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ProductModel GetProductByBarcode(string barcode)
        {
            const string query = @"
                SELECT ProductID, ProductName, Price, StockQuantity 
                FROM Products WITH(NOLOCK) 
                WHERE IsActive = 1 AND [BARCODE] = @barcode";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@barcode", barcode);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new ProductModel
                        {
                            ProductID = reader.GetInt32(reader.GetOrdinal("ProductID")),
                            ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                            Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                            StockQuantity = reader.GetInt32(reader.GetOrdinal("StockQuantity"))
                        };
                    }
                }
            }

            return null;
        }
    }

    public class ProductModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
