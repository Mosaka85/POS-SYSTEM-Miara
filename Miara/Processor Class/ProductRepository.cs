using System;
using System.Data;
using System.Data.SqlClient;

namespace Miara.Processor_Class
{
    public class ProductRepository
    {
        private readonly string _connectionString;
        private readonly DeviceAuditService _auditService;

        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString;
            _auditService = new DeviceAuditService(connectionString);
        }

        /// <summary>
        /// Fetch active products from the database.
        /// </summary>
        public DataTable GetActiveProducts(string deviceId, string employeeFullName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    string query = @"
                        SELECT ProductID, ProductName, Price, StockQuantity
                        FROM Products WITH(NOLOCK)
                        WHERE IsActive = 1";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    return dt;
                }
            }
            catch (Exception ex)
            {
                _auditService.LogAuditEntry(
                    deviceId,
                    employeeFullName,
                    "Error loading products",
                    ex.Message);

                throw;
            }
        }

        /// <summary>
        /// Fetch active categories from the database.
        /// </summary>
        public DataTable GetActiveCategories(string deviceId, string employeeFullName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    string query = @"
                        SELECT CategoryID, CategoryName
                        FROM Categories WITH(NOLOCK)
                        WHERE IsActive = 1";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    return dt;
                }
            }
            catch (Exception ex)
            {
                _auditService.LogAuditEntry(
                    deviceId,
                    employeeFullName,
                    "Error loading categories",
                    ex.Message);

                throw;
            }
        }
    }
}
