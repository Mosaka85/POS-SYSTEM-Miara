using System;
using System.Linq;
using System.Windows.Forms;
using MaterialSkin.Controls;

public class SaleDiscountManager
{
    private readonly DataGridView _saleDetailsGrid;
    private readonly CheckBox _checkDiscount;
    private readonly MaterialTextBox _txtDiscount; // <-- Use MaterialTextBox
    private readonly Action _updateTotalAmount;

    public SaleDiscountManager(DataGridView saleDetailsGrid, CheckBox checkDiscount, MaterialTextBox txtDiscount, Action updateTotalAmount)
    {
        _saleDetailsGrid = saleDetailsGrid;
        _checkDiscount = checkDiscount;
        _txtDiscount = txtDiscount;
        _updateTotalAmount = updateTotalAmount;
    }

    public void ApplyDiscount(decimal discount)
    {
        if (discount < 1) // Percentage discount
        {
            try
            {
                _checkDiscount.Checked = true;
                _txtDiscount.Text = (discount * 100).ToString("0.00");
                MessageBox.Show($"Percentage discount applied: {discount * 100:0.##}%");
                _updateTotalAmount();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error handling percentage discount: " + ex.Message);
            }
        }
        else // Voucher discount
        {
            try
            {
                bool voucherAlreadyApplied = _saleDetailsGrid.Rows
                    .Cast<DataGridViewRow>()
                    .Any(r => r.Cells["ProductName"]?.Value?.ToString() == "VOUCHER_APPLIED");

                if (voucherAlreadyApplied)
                {
                    MessageBox.Show("A voucher has already been applied to this sale.");
                    return;
                }

                decimal total = 0;
                foreach (DataGridViewRow row in _saleDetailsGrid.Rows)
                {
                    if (row?.Cells["Subtotal"]?.Value != null && decimal.TryParse(row.Cells["Subtotal"].Value.ToString(), out decimal subtotal))
                        total += subtotal;
                }

                if (discount > total)
                {
                    MessageBox.Show("Voucher value exceeds the total. Please add more items to use the voucher.");
                    return;
                }

                _saleDetailsGrid.Rows.Add(0, "VOUCHER_APPLIED", 1, -discount, -discount);
                MessageBox.Show($"Voucher discount applied: R{discount:0.00}");
                _updateTotalAmount();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error applying voucher: " + ex.Message);
            }
        }
    }
}
