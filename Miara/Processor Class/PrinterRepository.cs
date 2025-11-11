// PrinterRepository.cs
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

public class PrinterRepository
{
    private readonly string _connectionString;

    public PrinterRepository(string connectionString)
    {
        _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
    }

    public async Task SavePrinterConfigurationAsync(Printer printer, string deviceMacAddress)
    {
        const string query = @"
            MERGE INTO Printers AS Target
            USING (SELECT @Device AS Device) AS Source
            ON Target.Device = Source.Device
            WHEN MATCHED THEN
                UPDATE SET
                    PrinterName = @PrinterName,
                    IPAddress = @IPAddress,
                    PortName = @PortName,
                    IsNetworkPrinter = @IsNetworkPrinter,
                    LastUpdated = GETDATE()
            WHEN NOT MATCHED BY TARGET THEN
                INSERT (PrinterName, MACAddress, IPAddress, PortName, IsNetworkPrinter, DateAdded, LastUpdated, Device)
                VALUES (@PrinterName, @MACAddress, @IPAddress, @PortName, @IsNetworkPrinter, GETDATE(), GETDATE(), @Device);
        ";

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Device", deviceMacAddress);
                command.Parameters.AddWithValue("@PrinterName", printer.Name);
                command.Parameters.AddWithValue("@MACAddress", (object)printer.MacAddress ?? DBNull.Value);
                command.Parameters.AddWithValue("@IPAddress", (object)printer.IpAddress ?? DBNull.Value);
                command.Parameters.AddWithValue("@PortName", printer.PortName);
                command.Parameters.AddWithValue("@IsNetworkPrinter", printer.IsNetwork);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}