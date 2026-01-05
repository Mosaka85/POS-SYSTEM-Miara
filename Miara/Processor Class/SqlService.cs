using Miara.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using static Miara.Utilities.AppUtilities;

namespace Miara.Services
{
    public class SqlService
    {
        private readonly string _connectionString;

        // SQL Query Constants
        private const string GetSessionIdQuery = @"
            INSERT INTO UserSessions (Device) 
            OUTPUT INSERTED.SessionID 
            VALUES (@Device);";
        private const string LogLoginAttemptQuery = @"
            INSERT INTO LoginAudit (Username, AttemptTimestamp, IsSuccess, EmployeeID, Details, GUID) 
            VALUES (@Username, @AttemptTimestamp, @IsSuccess, @EmployeeID, @Details, @GUID);";
        private const string GetPrinterNameQuery = "SELECT PrinterName FROM Printers WHERE DEVICE = @Device;";
        private const string LogDeviceAuditQuery = @"
            INSERT INTO DeviceAudit (Device, Employee, AuditDate, StepDescription, ErrorMessage) 
            VALUES (@Device, @Employee, @AuditDate, @StepDescription, @ErrorMessage);";
        private const string AuthenticateUserQuery = @"
            SELECT TOP 1 
                u.id AS EmployeeID,
                u.first_name AS EmployeeFirstName,
                u.last_name AS EmployeeSurname,
                r.name AS Role
            FROM [users] u
            INNER JOIN [user_groups] ug 
                ON u.user_group_id = ug.id
            INNER JOIN [group_roles] gr 
                ON ug.id = gr.group_id
            INNER JOIN [roles] r 
                ON gr.role_id = r.id
            WHERE u.username = @Username 
                AND u.password_hash = @PasswordHash 
                AND u.active = 1;";
        private const string UpdateSessionWithEmployeeQuery = @"
            UPDATE UserSessions
            SET EmployeeID = @EmployeeID
            WHERE SessionID = @SessionID;";

        public SqlService(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private async Task<SqlConnection> GetOpenConnectionAsync(CancellationToken token)
        {
            var connection = new SqlConnection(_connectionString);
            try
            {
                await connection.OpenAsync(token);
                return connection;
            }
            catch
            {
                // Ensure connection is disposed if opening fails
                connection.Dispose();
                throw;
            }
        }


        public async Task LogAuditEntryAsync(string device, string employee, string stepDescription, string errorMessage, CancellationToken token)
        {
            try
            {
                using (var connection = await GetOpenConnectionAsync(token))
                using (var command = new SqlCommand(LogDeviceAuditQuery, connection))
                {
                    command.Parameters.AddWithValue("@Device", device);
                    command.Parameters.AddWithValue("@Employee", employee);
                    command.Parameters.AddWithValue("@AuditDate", DateTime.Now);
                    command.Parameters.AddWithValue("@StepDescription", stepDescription);
                    command.Parameters.AddWithValue("@ErrorMessage", string.IsNullOrEmpty(errorMessage) ? (object)DBNull.Value : errorMessage);

                    await command.ExecuteNonQueryAsync(token);
                }
            }
            catch (Exception ex)
            {
                await WriteToLogFileAsync($"Failed to log audit entry for {employee} on {device}: {ex.Message}");
            }
        }


        public async Task<string> GetSessionIDAsync(string currentDevice, CancellationToken token)
        {
            try
            {
                using (var connection = await GetOpenConnectionAsync(token))
                using (var command = new SqlCommand(GetSessionIdQuery, connection))
                {
                    command.Parameters.AddWithValue("@Device", currentDevice);
                    object result = await command.ExecuteScalarAsync(token);
                    return result?.ToString() ?? Guid.Empty.ToString();
                }
            }
            catch (Exception ex)
            {
                await WriteToLogFileAsync($"Error creating session for device {currentDevice}: {ex.Message}");
                throw new ApplicationException("Failed to establish a session with the database.", ex);
            }
        }

        public async Task<string> GetPrinterNameAsync(string device, CancellationToken token)
        {
            string printerName = "Not Configured";
            try
            {
                using (var connection = await GetOpenConnectionAsync(token))
                using (var command = new SqlCommand(GetPrinterNameQuery, connection))
                {
                    command.Parameters.AddWithValue("@Device", device);
                    var result = await command.ExecuteScalarAsync(token);
                    printerName = result?.ToString() ?? printerName;
                    await LogAuditEntryAsync(device, "System", "Printer configuration retrieved", $"Printer: {printerName}", token);
                }
            }
            catch (Exception ex)
            {
                await LogAuditEntryAsync(device, "System", "Error retrieving printer", ex.Message, token);
                throw; // Re-throw to allow calling code to handle the display error
            }
            return printerName;
        }

        public async Task LogLoginAttemptAsync(string username, bool isSuccess, int? employeeID, string details, string sessionID, CancellationToken token)
        {
            try
            {
                using (var connection = await GetOpenConnectionAsync(token))
                using (var command = new SqlCommand(LogLoginAttemptQuery, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@AttemptTimestamp", DateTime.Now);
                    command.Parameters.AddWithValue("@IsSuccess", isSuccess);
                    command.Parameters.AddWithValue("@EmployeeID", employeeID ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Details", details);
                    command.Parameters.AddWithValue("@GUID", sessionID);

                    await command.ExecuteNonQueryAsync(token);
                }
            }
            catch (Exception ex)
            {
                await WriteToLogFileAsync($"Failed to log login attempt for {username}: {ex.Message}");
            }
        }


        public async Task<EmployeeDetails> AuthenticateUserAsync(string username, string passwordHash, CancellationToken token)
        {
            using (var connection = await GetOpenConnectionAsync(token))
            using (var command = new SqlCommand(AuthenticateUserQuery, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@PasswordHash", passwordHash);

                using (SqlDataReader reader = await command.ExecuteReaderAsync(token))
                {
                    if (await reader.ReadAsync(token))
                    {
                        return new EmployeeDetails
                        {
                            EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                            FirstName = reader["EmployeeFirstName"].ToString(),
                            Surname = reader["EmployeeSurname"].ToString(),
                            Role = reader["Role"].ToString()
                        };
                    }
                    return null;
                }
            }
        }

        public async Task UpdateSessionWithEmployeeAsync(int employeeID, string sessionID)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString)) // Use standard approach since it's fire-and-forget post-login
                using (var command = new SqlCommand(UpdateSessionWithEmployeeQuery, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeID", employeeID);
                    command.Parameters.AddWithValue("@SessionID", sessionID ?? (object)DBNull.Value);

                    await connection.OpenAsync();
                    int rows = await command.ExecuteNonQueryAsync();

                    if (rows <= 0)
                    {
                        await WriteToLogFileAsync($"Warning: no session row was updated for SessionID={sessionID} (EmployeeID={employeeID}).");
                    }
                    else
                    {
                        await WriteToLogFileAsync($"Session updated: SessionID={sessionID} set to EmployeeID={employeeID}.");
                    }
                }
            }
            catch (Exception ex)
            {
                await WriteToLogFileAsync($"Failed to update session {sessionID} with EmployeeID {employeeID}: {ex.Message}");
            }
        }
    }
}