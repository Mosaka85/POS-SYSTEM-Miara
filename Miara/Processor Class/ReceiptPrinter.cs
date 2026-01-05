using Miara;
using Miara.Processor_Class;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

public class ReceiptPrinter
{
    private readonly string deviceInternetId;
    private readonly string employeeFirstName;
    private readonly string employeeSurname;
    private readonly string employeeNumber;
    private readonly string receiptNo;
    private readonly string paymentMethod;
    private readonly List<SaleItem> saleItems;
    private readonly decimal discountAmount;
    private readonly string couponCode;
    private readonly decimal cashbackAmount;
    private readonly decimal? renderedAmount;

    private readonly ApplicationSettings settings;

    public ReceiptPrinter(
        string deviceInternetId,
        string employeeFirstName,
        string employeeSurname,
        string employeeNumber,
        string receiptNo,
        string paymentMethod,
        List<SaleItem> saleItems,
        decimal discountAmount = 0,
        string couponCode = null,
        decimal cashbackAmount = 0,
        decimal? renderedAmount = null,
        decimal discountPercentage = 0)
    {
        this.deviceInternetId = deviceInternetId;
        this.employeeFirstName = employeeFirstName;
        this.employeeSurname = employeeSurname;
        this.employeeNumber = employeeNumber;
        this.receiptNo = receiptNo;
        this.paymentMethod = paymentMethod;
        this.saleItems = saleItems ?? new List<SaleItem>();
        this.discountAmount = discountAmount;
        this.couponCode = couponCode;
        this.cashbackAmount = cashbackAmount;
        this.renderedAmount = renderedAmount;

        this.settings = SettingsManager.LoadSettings();
    }

    public void Print(string printerName, bool showPrintPreview = true)
    {
        if (string.IsNullOrWhiteSpace(printerName))
            throw new ArgumentException("Printer name cannot be empty.");

        PrintDocument printDocument = new PrintDocument
        {
            PrinterSettings = { PrinterName = printerName }
        };

        if (!printDocument.PrinterSettings.IsValid)
            throw new InvalidOperationException($"Printer '{printerName}' is not valid or available.");

        printDocument.PrintPage += (sender, e) =>
        {
            Graphics g = e.Graphics;
            Font font = new Font("Courier New", 10);
            int y = 20;

            // --- Header ---
            g.DrawString($"******** {settings.StoreName} ********", font, Brushes.Black, 10, y); y += 20;
            g.DrawString($"Address: {settings.StoreAddress}", font, Brushes.Black, 10, y); y += 20;
            g.DrawString($"Tel: {settings.StorePhone} | Email: {settings.StoreEmail}", font, Brushes.Black, 10, y); y += 20;
            g.DrawString($"Website: {settings.StoreWebsite} | Tax: {settings.TaxNumber}", font, Brushes.Black, 10, y); y += 20;
            g.DrawString("-----------------------------------------------", font, Brushes.Black, 10, y); y += 20;

            // --- Receipt info ---
            g.DrawString($"Receipt No: {receiptNo}", font, Brushes.Black, 10, y); y += 20;
            g.DrawString($"Date: {DateTime.Now:dd/MM/yyyy HH:mm:ss}", font, Brushes.Black, 10, y); y += 20;
            g.DrawString($"Employee: {employeeFirstName} {employeeSurname} ({employeeNumber})", font, Brushes.Black, 10, y); y += 20;
            g.DrawString($"Payment Method: {paymentMethod}", font, Brushes.Black, 10, y); y += 20;

            // --- Items Header ---
            g.DrawString("Item                 Qty    Price       Total", font, Brushes.Black, 10, y); y += 20;
            g.DrawString("-----------------------------------------------", font, Brushes.Black, 10, y); y += 20;

            decimal total = 0;
            foreach (var item in saleItems)
            {
                decimal lineTotal = item.Price * item.Quantity;
                total += lineTotal;
                g.DrawString($"{item.ProductName,-18} {item.Quantity,3} {item.Price,9:C} {lineTotal,10:C}", font, Brushes.Black, 10, y);
                y += 20;
            }

            // --- Totals ---
            g.DrawString("-----------------------------------------------", font, Brushes.Black, 10, y); y += 20;
            g.DrawString($"Subtotal: {total,33:C}", font, Brushes.Black, 10, y); y += 20;

            if (discountAmount > 0 && !string.IsNullOrWhiteSpace(couponCode))
            {
                g.DrawString($"Coupon ({couponCode}): -{discountAmount,24:C}", font, Brushes.Black, 10, y); y += 20;
                total -= discountAmount;
            }

            decimal tax = total * 0.15m;
            decimal finalTotal = total + cashbackAmount;

            g.DrawString($"VAT (15%) INCLUDED IN TOTAL: {tax,32:C}", font, Brushes.Black, 10, y); y += 20;

            if (cashbackAmount > 0)
            {
                g.DrawString($"Cashback: {cashbackAmount,30:C}", font, Brushes.Black, 10, y); y += 20;
            }

            g.DrawString($"TOTAL: {finalTotal,32:C}", new Font("Courier New", 10, FontStyle.Bold), Brushes.Black, 10, y); y += 20;

            if (paymentMethod == "CASH" && renderedAmount.HasValue)
            {
                decimal change = renderedAmount.Value - finalTotal;
                g.DrawString("-----------------------------------------------", font, Brushes.Black, 10, y); y += 20;
                g.DrawString($"Amount Rendered: {renderedAmount.Value,23:C}", font, Brushes.Black, 10, y); y += 20;
                g.DrawString($"Change: {change,31:C}", font, Brushes.Black, 10, y); y += 40;
            }

            // --- Footer ---
            g.DrawString(settings.FooterMessage1, font, Brushes.Black, 10, y); y += 20;
            g.DrawString(settings.FooterMessage2, font, Brushes.Black, 10, y); y += 20;
            g.DrawString(settings.FooterMessage3, font, Brushes.Black, 10, y); y += 20;
            g.DrawString(settings.FooterMessage4, font, Brushes.Black, 10, y); y += 20;
            g.DrawString($"Device ID: {deviceInternetId}", font, Brushes.Black, 10, y); y += 20;
        };

        if (showPrintPreview)
        {
            PrintPreviewDialog preview = new PrintPreviewDialog { Document = printDocument };
            preview.ShowDialog();
        }
        else
        {
            printDocument.Print();
        }
    }
}

public class SaleItem
{
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Subtotal { get; internal set; }
}
