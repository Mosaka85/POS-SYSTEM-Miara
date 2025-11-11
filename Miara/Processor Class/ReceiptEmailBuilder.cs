using System;
using System.Collections.Generic;
using System.Text;

namespace MiaraPOS.Services
{
    public class ReceiptEmailBuilder
    {
        public class ReceiptItem
        {
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
            public decimal Subtotal { get; set; }
        }

        public string GenerateReceiptContent(
            int saleId,
            string employeeFirstName,
            string employeeSurname,
            string paymentMethod,
            List<ReceiptItem> items,
            decimal discountPercentage,
            decimal cashbackAmount,
            decimal? renderedAmount = null)
        {
            decimal total = 0;
            foreach (var item in items)
            {
                total += item.Subtotal;
            }

            // Discount
            decimal discountValue = total * (discountPercentage / 100m);
            decimal subtotalAfterDiscount = Math.Max(0, total - discountValue);

            // Tax (15%)
            decimal tax = subtotalAfterDiscount * 0.15m;

            // Final Total
            decimal finalTotal = subtotalAfterDiscount + tax + cashbackAmount;

            // Build HTML
            StringBuilder receiptContent = new StringBuilder();
            receiptContent.AppendLine("<!DOCTYPE html>");
            receiptContent.AppendLine("<html lang='en'>");
            receiptContent.AppendLine("<head>");
            receiptContent.AppendLine("<meta charset='UTF-8'>");
            receiptContent.AppendLine("<meta name='viewport' content='width=device-width, initial-scale=1.0'>");
            receiptContent.AppendLine("<title>Receipt</title>");
            receiptContent.AppendLine("<style>");
            receiptContent.AppendLine("body { font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; margin: 0; padding: 20px; background-color: #f9f9f9; }");
            receiptContent.AppendLine(".receipt { max-width: 350px; margin: 0 auto; background: #fff; border-radius: 8px; box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1); padding: 20px; }");
            receiptContent.AppendLine(".header { text-align: center; margin-bottom: 20px; }");
            receiptContent.AppendLine(".header h2 { margin: 0; font-size: 1.5em; color: #333; }");
            receiptContent.AppendLine(".header p { margin: 5px 0; font-size: 0.9em; color: #666; }");
            receiptContent.AppendLine(".divider { border-top: 1px dashed #ddd; margin: 15px 0; }");
            receiptContent.AppendLine(".text-right { text-align: right; }");
            receiptContent.AppendLine(".text-center { text-align: center; }");
            receiptContent.AppendLine(".items-header { font-weight: bold; margin-bottom: 10px; font-size: 0.9em; color: #333; }");
            receiptContent.AppendLine(".items-list p { margin: 5px 0; font-size: 0.9em; color: #555; }");
            receiptContent.AppendLine(".totals p { margin: 5px 0; font-size: 0.9em; color: #333; }");
            receiptContent.AppendLine("</style>");
            receiptContent.AppendLine("</head>");
            receiptContent.AppendLine("<body>");
            receiptContent.AppendLine("<div class='receipt'>");

            // Header
            receiptContent.AppendLine("<div class='header'>");
            receiptContent.AppendLine("<h2>MIARA TRADING PTY LTD</h2>");
            receiptContent.AppendLine("<p>123 Mosaka St.</p>");
            receiptContent.AppendLine("<p>Phone: 012-345-6789 | Email: info@miaratrading.com</p>");
            receiptContent.AppendLine("</div>");

            // Info
            receiptContent.AppendLine("<div class='divider'></div>");
            receiptContent.AppendLine("<p><strong>Receipt No:</strong> " + saleId + "</p>");
            receiptContent.AppendLine("<p><strong>Date:</strong> " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "</p>");
            receiptContent.AppendLine("<p><strong>Employee:</strong> " + employeeFirstName + " " + employeeSurname + "</p>");
            receiptContent.AppendLine("<p><strong>Payment Method:</strong> " + paymentMethod + "</p>");

            // Items
            receiptContent.AppendLine("<div class='divider'></div>");
            receiptContent.AppendLine("<div class='items-header'><p><strong>Item | Qty | Price | Total</strong></p></div>");
            receiptContent.AppendLine("<div class='items-list'>");
            foreach (var item in items)
            {
                receiptContent.AppendLine($"<p>{item.ProductName} ({item.Quantity} × {item.Price:C}) = {item.Subtotal:C}</p>");
            }
            receiptContent.AppendLine("</div>");

            // Totals
            receiptContent.AppendLine("<div class='divider'></div>");
            receiptContent.AppendLine("<div class='totals'>");
            receiptContent.AppendLine($"<p class='text-right'><strong>Subtotal:</strong> {total:C}</p>");
            if (discountPercentage > 0)
                receiptContent.AppendLine($"<p class='text-right'><strong>Discount ({discountPercentage:F0}%):</strong> -{discountValue:C}</p>");
            receiptContent.AppendLine($"<p class='text-right'><strong>Subtotal After Discount:</strong> {subtotalAfterDiscount:C}</p>");
            receiptContent.AppendLine($"<p class='text-right'><strong>VAT (15%):</strong> {tax:C}</p>");
            if (cashbackAmount > 0)
                receiptContent.AppendLine($"<p class='text-right'><strong>Cashback:</strong> {cashbackAmount:C}</p>");
            receiptContent.AppendLine($"<p class='text-right'><strong>Total:</strong> {finalTotal:C}</p>");
            receiptContent.AppendLine("</div>");

            // Cash change
            if (paymentMethod == "CASH" && renderedAmount.HasValue)
            {
                decimal change = renderedAmount.Value - finalTotal;
                receiptContent.AppendLine("<div class='divider'></div>");
                receiptContent.AppendLine($"<p class='text-right'><strong>Amount Rendered:</strong> {renderedAmount.Value:C}</p>");
                receiptContent.AppendLine($"<p class='text-right'><strong>Change:</strong> {change:C}</p>");
            }

            // Footer
            receiptContent.AppendLine("<div class='divider'></div>");
            receiptContent.AppendLine("<div class='text-center'>");
            receiptContent.AppendLine("<p>Thank you for shopping with us!</p>");
            receiptContent.AppendLine("<p>Visit us again!</p>");
            receiptContent.AppendLine("<p>---------- MOSAKA SYSTEM ----------</p>");
            receiptContent.AppendLine("</div>");

            receiptContent.AppendLine("</div>");
            receiptContent.AppendLine("</body>");
            receiptContent.AppendLine("</html>");

            return receiptContent.ToString();
        }
    }
}
