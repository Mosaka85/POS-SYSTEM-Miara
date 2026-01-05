using System;
using System.Data;
using System.Data.SqlClient;

namespace Miara.Processor_Class
{
    internal class DeviceAuditService
    {
        private readonly string _connectionString;
        private readonly bool _allowDBLogs;

        public DeviceAuditService(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));

            var settings = SettingsManager.LoadSettings();
            _allowDBLogs = settings?.AllowDatabaseLogs ?? false; // Safe fallback
        }

        /// <summary>
        /// Logs an audit entry to the database ONLY if database logging is enabled in settings.
        /// </summary>
        public void LogAuditEntry(string device, string employee, string stepDescription, string errorMessage = null)
        {
            // Early exit if database logging is disabled
            if (!_allowDBLogs)
                return;

            const string query = @"
                INSERT INTO DeviceAudit (Device, Employee, AuditDate, StepDescription, ErrorMessage)
                VALUES (@Device, @Employee, @AuditDate, @StepDescription, @ErrorMessage);";

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@Device", SqlDbType.NVarChar).Value = device ?? (object)DBNull.Value;
                    command.Parameters.Add("@Employee", SqlDbType.NVarChar).Value = employee ?? (object)DBNull.Value;
                    command.Parameters.Add("@AuditDate", SqlDbType.DateTime).Value = DateTime.Now;
                    command.Parameters.Add("@StepDescription", SqlDbType.NVarChar).Value = stepDescription ?? (object)DBNull.Value;
                    command.Parameters.Add("@ErrorMessage", SqlDbType.NVarChar).Value =
                        string.IsNullOrWhiteSpace(errorMessage) ? (object)DBNull.Value : errorMessage;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Critical: Do NOT show MessageBox in a service/background class!
                // This can freeze or crash the app if no UI thread is available.
                // Use proper logging instead (e.g., log file, EventLog, Serilog, etc.)

                // Option 1: Silent fail (recommended for non-critical audit logs)
                // Do nothing — audit failure shouldn't crash the main process

                // Option 2: Write to log file or Event Log (uncomment if needed)
                // System.Diagnostics.Debug.WriteLine($"[Audit Log Failed] {ex.Message}");
                // Or use your app's logger:
                // Logger.Error(ex, "Failed to write audit log entry.");
            }
        }
    }
}