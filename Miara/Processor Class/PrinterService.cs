
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

public class PrinterService
{

    [DllImport("iphlpapi.dll", ExactSpelling = true)]
    private static extern int SendARP(int destIp, int srcIp, byte[] macAddr, ref uint physicalAddrLen);

    public async Task<List<Printer>> GetPrintersAsync(bool includeLocal, bool includeNetwork)
    {
        return await Task.Run(() =>
        {
            var printers = new Dictionary<string, Printer>();
            var query = new ObjectQuery("SELECT * FROM Win32_Printer");
            using (var searcher = new ManagementObjectSearcher(query))
            {
                foreach (ManagementObject mo in searcher.Get())
                {
                    var printer = new Printer
                    {
                        Name = mo["Name"]?.ToString(),
                        PortName = mo["PortName"]?.ToString(),
                        DriverName = mo["DriverName"]?.ToString(),
                        Status = mo["WorkOffline"]?.ToString() == "False" ? "Online" : "Offline"
                    };

                    if (string.IsNullOrEmpty(printer.Name)) continue;

                    printer.IsNetwork = IsNetworkPort(printer.PortName);

                    if ((printer.IsNetwork && includeNetwork) || (!printer.IsNetwork && includeLocal))
                    {
                        if (printer.IsNetwork)
                        {
                            printer.IpAddress = GetIpFromPortName(printer.PortName);
                            printer.MacAddress = GetMacAddressFromIP(printer.IpAddress);
                        }
                        else
                        {
                            // For local printers (USB, LPT), IP/MAC are not applicable
                            printer.IpAddress = "N/A";
                            printer.MacAddress = "N/A";
                        }

                        // Avoid adding duplicate printers
                        if (!printers.ContainsKey(printer.Name))
                        {
                            printers.Add(printer.Name, printer);
                        }
                    }
                }
            }
            return printers.Values.ToList();
        });
    }

    private bool IsNetworkPort(string portName)
    {
        if (string.IsNullOrEmpty(portName)) return false;

        string upperPort = portName.ToUpperInvariant();
        return upperPort.StartsWith("IP_") || upperPort.StartsWith("TCP") || IPAddress.TryParse(portName, out _);
    }

    private string GetIpFromPortName(string portName)
    {
        if (string.IsNullOrEmpty(portName)) return "N/A";

        if (portName.StartsWith("IP_", StringComparison.OrdinalIgnoreCase))
        {
            return portName.Substring(3);
        }

        // Add other parsing logic here if needed for different port name formats
        return IPAddress.TryParse(portName, out _) ? portName : "N/A";
    }

    private string GetMacAddressFromIP(string ipAddress)
    {
        if (string.IsNullOrEmpty(ipAddress) || !IPAddress.TryParse(ipAddress, out var parsedIp))
        {
            return "N/A";
        }

        try
        {
            byte[] macAddr = new byte[6];
            uint macAddrLen = (uint)macAddr.Length;
            if (SendARP(BitConverter.ToInt32(parsedIp.GetAddressBytes(), 0), 0, macAddr, ref macAddrLen) == 0)
            {
                return string.Join(":", macAddr.Select(b => b.ToString("X2")));
            }
        }
        catch { /* ARP can fail, fallback or ignore */ }

        return "Not Found";
    }

    public static string GetActiveMacAddress()
    {
        return NetworkInterface.GetAllNetworkInterfaces()
            .FirstOrDefault(nic => nic.OperationalStatus == OperationalStatus.Up &&
                                   nic.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                                   nic.GetIPProperties().GatewayAddresses.Any())
            ?.GetPhysicalAddress().ToString() ?? "Not Found";
    }
}