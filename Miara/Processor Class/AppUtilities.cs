using System;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Miara.Utilities
{
    public static class AppUtilities
    {
        private static readonly string LogFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt");
        private static readonly SemaphoreSlim _logSemaphore = new SemaphoreSlim(1, 1);

        /// <summary>
        /// Asynchronously writes a message to the application log file, using a semaphore to prevent concurrent file access.
        /// </summary>
        public static async Task WriteToLogFileAsync(string message)
        {
            await _logSemaphore.WaitAsync();
            try
            {
                string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}{Environment.NewLine}";
                // Use FileStream with appropriate options for robust writing
                using (var stream = new FileStream(LogFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                using (var writer = new StreamWriter(stream))
                {
                    await writer.WriteAsync(logMessage);
                }
            }
            catch (Exception ex)
            {
                // Fallback logging if the file write fails
                Console.WriteLine($"Failed to write to log file: {ex.Message}");
            }
            finally
            {
                _logSemaphore.Release();
            }
        }

        /// <summary>
        /// Computes the SHA256 hash of a given password string.
        /// </summary>
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return string.Concat(hashBytes.Select(b => b.ToString("x2")));
            }
        }

        /// <summary>
        /// Retrieves the MAC address of the first active, non-loopback network interface.
        /// </summary>
        public static string GetMacAddress()
        {
            return NetworkInterface.GetAllNetworkInterfaces()
                .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .Select(nic => nic.GetPhysicalAddress().ToString())
                .FirstOrDefault() ?? "MAC-NOT-FOUND";
        }
    }
}