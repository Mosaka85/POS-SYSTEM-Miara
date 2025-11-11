// Printer.cs
using System.ComponentModel;

public class Printer
{
    [DisplayName("Printer Name")]
    public string Name { get; set; }

    [DisplayName("Port")]
    public string PortName { get; set; }

    [DisplayName("Network Printer")]
    public bool IsNetwork { get; set; }

    [DisplayName("MAC Address")]
    public string MacAddress { get; set; }

    [DisplayName("IP Address")]
    public string IpAddress { get; set; }

    public string Status { get; set; }

    [DisplayName("Driver")]
    public string DriverName { get; set; }
}