using Miara;
using Miara.Processor_Class;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms; // Required for PrintPreviewDialog

public class ReceiptPrinter
{
    private readonly string deviceInternetId;
    private readonly string employeeFirstName;
    private readonly string employeeSurname;
    private readonly string employeeNumber;
    private readonly string receiptNo;
    private readonly string paymentMethod;
    private readonly List<SaleItem> saleItems;
    private readonly decimal discountPercentage;
    private readonly decimal cashbackAmount;
    private readonly decimal? renderedAmount;

    // Load application settings once
    private readonly ApplicationSettings settings;

    public ReceiptPrinter(
        string deviceInternetId,
        string employeeFirstName,
        string employeeSurname,
        string employeeNumber,
        string receiptNo,
        string paymentMethod,
        List<SaleItem> saleItems,
        decimal discountPercentage = 0,
        decimal cashbackAmount = 0,
        decimal? renderedAmount = null)
    {
        this.deviceInternetId = deviceInternetId;
        this.employeeFirstName = employeeFirstName;
        this.employeeSurname = employeeSurname;
        this.employeeNumber = employeeNumber;
        this.receiptNo = receiptNo;
        this.paymentMethod = paymentMethod;
        this.saleItems = saleItems ?? new List<SaleItem>();
        this.discountPercentage = discountPercentage;
        this.cashbackAmount = cashbackAmount;
        this.renderedAmount = renderedAmount;

        // Load store/application settings
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
            Graphics graphics = e.Graphics;
            Font font = new Font("Courier New", 10);
            int y = 20;

            // --- Header (from settings) ---
            graphics.DrawString($"********* {settings.StoreName} *********", font, Brushes.Black, 10, y); y += 20;
            graphics.DrawString($"Address: {settings.StoreAddress}", font, Brushes.Black, 10, y); y += 20;
            graphics.DrawString($"Phone: {settings.StorePhone} | Email: {settings.StoreEmail}", font, Brushes.Black, 10, y); y += 20;
            graphics.DrawString($"Website: {settings.StoreWebsite}", font, Brushes.Black, 10, y); y += 20;
            graphics.DrawString($"Tax No: {settings.TaxNumber}", font, Brushes.Black, 10, y); y += 20;
            graphics.DrawString("-----------------------------------------", font, Brushes.Black, 10, y); y += 20;

            // --- Receipt info ---
            graphics.DrawString($"Receipt No: {receiptNo}", font, Brushes.Black, 10, y); y += 20;
            graphics.DrawString($"Date: {DateTime.Now:dd/MM/yyyy HH:mm:ss}", font, Brushes.Black, 10, y); y += 20;
            graphics.DrawString($"Employee: {employeeFirstName} {employeeSurname}", font, Brushes.Black, 10, y); y += 20;
            graphics.DrawString($"Payment Method: {paymentMethod}", font, Brushes.Black, 10, y); y += 20;

            // --- Items ---
            graphics.DrawString("Item               Qty   Price       Total", font, Brushes.Black, 10, y); y += 20;
            graphics.DrawString("-----------------------------------------", font, Brushes.Black, 10, y); y += 20;

            decimal total = 0;
            foreach (var item in saleItems)
            {
                decimal lineTotal = item.Price * item.Quantity;
                total += lineTotal;
                graphics.DrawString($"{item.ProductName,-15} {item.Quantity,3} {item.Price,9:C} {lineTotal,10:C}", font, Brushes.Black, 10, y);
                y += 20;
            }

            decimal discountValue = total * (discountPercentage / 100);
            decimal subtotalAfterDiscount = total - discountValue;
            if (subtotalAfterDiscount < 0) subtotalAfterDiscount = 0;

            decimal tax = subtotalAfterDiscount * 0.15m;
            decimal finalTotal = subtotalAfterDiscount + tax + cashbackAmount;

            // --- Totals ---
            graphics.DrawString("-----------------------------------------", font, Brushes.Black, 10, y); y += 20;
            graphics.DrawString($"Subtotal: {total,25:C}", font, Brushes.Black, 10, y); y += 20;

            if (discountPercentage > 0)
                graphics.DrawString($"Discount ({discountPercentage:F0}%): {discountValue,17:C}", font, Brushes.Black, 10, y); y += 20;

            graphics.DrawString($"Subtotal After Discount: {subtotalAfterDiscount,12:C}", font, Brushes.Black, 10, y); y += 20;
            graphics.DrawString($"Tax (15%): {tax,26:C}", font, Brushes.Black, 10, y); y += 20;

            if (cashbackAmount > 0)
            {
                graphics.DrawString($"Cashback Applied: {cashbackAmount,19:C}", font, Brushes.Black, 10, y); y += 20;
            }

            graphics.DrawString($"Total: {finalTotal,29:C}", new Font("Courier New", 10, FontStyle.Bold), Brushes.Black, 10, y); y += 20;

            if (paymentMethod == "CASH" && renderedAmount.HasValue)
            {
                decimal change = renderedAmount.Value - finalTotal;
                graphics.DrawString("-----------------------------------------", font, Brushes.Black, 10, y); y += 20;
                graphics.DrawString($"Amount Rendered: {renderedAmount.Value,19:C}", font, Brushes.Black, 10, y); y += 20;
                graphics.DrawString($"Change: {change,30:C}", font, Brushes.Black, 10, y); y += 40;
            }

            // --- Footer (from settings) ---
            graphics.DrawString(settings.FooterMessage1, font, Brushes.Black, 10, y); y += 20;
            graphics.DrawString(settings.FooterMessage2, font, Brushes.Black, 10, y); y += 20;
            graphics.DrawString(settings.FooterMessage3, font, Brushes.Black, 10, y); y += 20;
            graphics.DrawString(settings.FooterMessage4, font, Brushes.Black, 10, y); y += 20;
        };

        if (showPrintPreview)
        {
            PrintPreviewDialog previewDialog = new PrintPreviewDialog
            {
                Document = printDocument
            };
            previewDialog.ShowDialog();
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
