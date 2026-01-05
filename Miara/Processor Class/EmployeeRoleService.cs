using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Miara.Processor_Class
{
    internal class EmployeeRoleService
    {
        private readonly string _connectionString;
        private readonly Action<string> _logTrace;

        public EmployeeRoleService(string connectionString, Action<string> logTrace = null)
        {
            _connectionString = connectionString;
            _logTrace = logTrace ?? (_ => { });
        }


        public async Task<string> GetEmployeeRoleAsync(int employeeId)
        {
            const string query = @"
                SELECT
                    u.id AS EmployeeID,
                    u.first_name AS EmployeeFirstName,
                    u.last_name  AS EmployeeSurname,
                    r.name       AS Role
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

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return reader["Role"]?.ToString() ?? "No role assigned";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logTrace($"Error retrieving employee role: {ex.Message}");
            }

            return "Unknown";
        }
    }
}
