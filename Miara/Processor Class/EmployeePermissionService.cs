using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Miara.Processor_Class
{
    public class EmployeePermissionService
    {
        private readonly string _connectionString;

        public EmployeePermissionService(string connectionString)
        {
            _connectionString = connectionString
                ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task<HashSet<int>> GetEmployeePermissionsAsync(int employeeId)
        {
            var permissions = new HashSet<int>();

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
                    cmd.Parameters.Add("@EmployeeID", System.Data.SqlDbType.Int)
                       .Value = employeeId;

                    await conn.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            if (reader.IsDBNull(0))
                                continue;

                            // ✅ SAFE conversion
                            if (int.TryParse(reader[0].ToString(), out int permissionId))
                            {
                                permissions.Add(permissionId);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"Failed to load permissions for employee {employeeId}", ex);
            }

            return permissions;
        }
    }
}
