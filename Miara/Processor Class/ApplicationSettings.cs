using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miara.Processor_Class
{
    // A class that represents the application's settings
    [Serializable]
    public class ApplicationSettings
    {
        public string StoreName { get; set; } = "Miara Supermart";
        public bool AllowLocalLogs { get; set; } = true;
        public bool AllowDatabaseLogs { get; set; } = true;
        public bool AutoSave { get; set; } = false;
        public string StoreAddress { get; set; } = "123 Main Street, Johannesburg, South Africa";
        public string StorePhone { get; set; } = "+27 11 123 4567";
        public string StoreEmail { get; set; } = "info@miarastore.co.za";
        public string StoreWebsite { get; set; } = "https://www.miarastore.co.za";
        public string TaxNumber { get; set; } = "ZA-1234567890";
        public string Currency { get; set; } = "ZAR"; // South African Rand
        public int ReceiptCopies { get; set; } = 2;
        public string FooterMessage1 { get; set; } = "Thank you for shopping with us!";
        public string FooterMessage2 { get; set; } = "Visit again soon.";
        public string FooterMessage3 { get; set; } = "Customer Support: +27 82 123 4567";
        public string FooterMessage4 { get; set; } = "Powered by Miara POS System";
    }
}
