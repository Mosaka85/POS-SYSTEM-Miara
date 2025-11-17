using System.Drawing;
using System.Windows.Forms;

namespace Miara
{
    partial class frmSales
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSales));
            this.lblNextSalesNumber = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.btnCompleteSale = new MaterialSkin.Controls.MaterialButton();
            this.lblTax = new System.Windows.Forms.Label();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.lblSubtotalorder = new System.Windows.Forms.Label();
            this.btnClear = new MaterialSkin.Controls.MaterialButton();
            this.lblrendered = new System.Windows.Forms.Label();
            this.txtrenderedamount = new MaterialSkin.Controls.MaterialTextBox();
            this.lblChange = new System.Windows.Forms.Label();
            this.btnCalculatechange = new MaterialSkin.Controls.MaterialButton();
            this.lbldiscountvalue = new System.Windows.Forms.Label();
            this.txtdiscount = new MaterialSkin.Controls.MaterialTextBox();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblBusineess = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkEmail = new MaterialSkin.Controls.MaterialCheckbox();
            this.txtRecipientEmail = new MaterialSkin.Controls.MaterialTextBox();
            this.btnVoidLine = new MaterialSkin.Controls.MaterialButton();
            this.btnRefund = new MaterialSkin.Controls.MaterialButton();
            this.btnCupons = new MaterialSkin.Controls.MaterialButton();
            this.btnPriceCheck = new MaterialSkin.Controls.MaterialButton();
            this.btnOpenCashDrawer = new MaterialSkin.Controls.MaterialButton();
            this.button2 = new MaterialSkin.Controls.MaterialButton();
            this.btnReprint = new MaterialSkin.Controls.MaterialButton();
            this.lblCashBack = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripChangeBackColour = new System.Windows.Forms.ToolStripTextBox();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.checkdiscount = new MaterialSkin.Controls.MaterialCheckbox();
            this.comboProductCategory = new MaterialSkin.Controls.MaterialComboBox();
            this.comboBoxProducts = new MaterialSkin.Controls.MaterialComboBox();
            this.combopaymentmentod = new MaterialSkin.Controls.MaterialComboBox();
            this.btnAddProduct = new MaterialSkin.Controls.MaterialButton();
            this.txtQuantity = new MaterialSkin.Controls.MaterialTextBox();
            this.Subtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewSaleDetails = new System.Windows.Forms.DataGridView();
            this.txtBarcodeLabel = new MaterialSkin.Controls.MaterialTextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblScan = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnReportSales = new MaterialSkin.Controls.MaterialButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnBack = new MaterialSkin.Controls.MaterialButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblinternet = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblemailvalid = new System.Windows.Forms.Label();
            this.lblUserRole = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSaleDetails)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblNextSalesNumber
            // 
            this.lblNextSalesNumber.AutoSize = true;
            this.lblNextSalesNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold);
            this.lblNextSalesNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblNextSalesNumber.Location = new System.Drawing.Point(458, 121);
            this.lblNextSalesNumber.Name = "lblNextSalesNumber";
            this.lblNextSalesNumber.Size = new System.Drawing.Size(286, 32);
            this.lblNextSalesNumber.TabIndex = 1;
            this.lblNextSalesNumber.Text = "SALES ORDER NO:";
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblTotalAmount.Location = new System.Drawing.Point(3, 0);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(204, 108);
            this.lblTotalAmount.TabIndex = 7;
            this.lblTotalAmount.Text = "Total Amount:";
            // 
            // btnCompleteSale
            // 
            this.btnCompleteSale.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCompleteSale.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnCompleteSale.Depth = 0;
            this.btnCompleteSale.HighEmphasis = true;
            this.btnCompleteSale.Icon = null;
            this.btnCompleteSale.Location = new System.Drawing.Point(1347, 729);
            this.btnCompleteSale.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnCompleteSale.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnCompleteSale.Name = "btnCompleteSale";
            this.btnCompleteSale.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnCompleteSale.Size = new System.Drawing.Size(136, 36);
            this.btnCompleteSale.TabIndex = 8;
            this.btnCompleteSale.Text = "Complete Sale\r\n(F2)";
            this.btnCompleteSale.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnCompleteSale.UseAccentColor = true;
            this.btnCompleteSale.Click += new System.EventHandler(this.btnCompleteSale_Click);
            // 
            // lblTax
            // 
            this.lblTax.AutoSize = true;
            this.lblTax.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTax.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblTax.Location = new System.Drawing.Point(3, 108);
            this.lblTax.Name = "lblTax";
            this.lblTax.Size = new System.Drawing.Size(177, 32);
            this.lblTax.TabIndex = 9;
            this.lblTax.Text = "VAT(15%) : ";
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // lblSubtotalorder
            // 
            this.lblSubtotalorder.AutoSize = true;
            this.lblSubtotalorder.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold);
            this.lblSubtotalorder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblSubtotalorder.Location = new System.Drawing.Point(3, 172);
            this.lblSubtotalorder.Name = "lblSubtotalorder";
            this.lblSubtotalorder.Size = new System.Drawing.Size(137, 32);
            this.lblSubtotalorder.TabIndex = 22;
            this.lblSubtotalorder.Text = "Subtotal:";
            // 
            // btnClear
            // 
            this.btnClear.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClear.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnClear.Depth = 0;
            this.btnClear.HighEmphasis = true;
            this.btnClear.Icon = null;
            this.btnClear.Location = new System.Drawing.Point(1264, 788);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnClear.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnClear.Name = "btnClear";
            this.btnClear.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnClear.Size = new System.Drawing.Size(66, 36);
            this.btnClear.TabIndex = 23;
            this.btnClear.Text = "Clear";
            this.btnClear.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnClear.UseAccentColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lblrendered
            // 
            this.lblrendered.AutoSize = true;
            this.lblrendered.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold);
            this.lblrendered.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblrendered.Location = new System.Drawing.Point(12, 476);
            this.lblrendered.Name = "lblrendered";
            this.lblrendered.Size = new System.Drawing.Size(170, 20);
            this.lblrendered.TabIndex = 26;
            this.lblrendered.Text = "Amount Rendered :";
            // 
            // txtrenderedamount
            // 
            this.txtrenderedamount.AnimateReadOnly = false;
            this.txtrenderedamount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtrenderedamount.Depth = 0;
            this.txtrenderedamount.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtrenderedamount.Hint = "Enter Amount";
            this.txtrenderedamount.LeadingIcon = null;
            this.txtrenderedamount.Location = new System.Drawing.Point(240, 476);
            this.txtrenderedamount.MaxLength = 50;
            this.txtrenderedamount.MouseState = MaterialSkin.MouseState.OUT;
            this.txtrenderedamount.Multiline = false;
            this.txtrenderedamount.Name = "txtrenderedamount";
            this.txtrenderedamount.Size = new System.Drawing.Size(200, 50);
            this.txtrenderedamount.TabIndex = 27;
            this.txtrenderedamount.Text = "";
            this.txtrenderedamount.TrailingIcon = null;
            // 
            // lblChange
            // 
            this.lblChange.AutoSize = true;
            this.lblChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold);
            this.lblChange.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.lblChange.Location = new System.Drawing.Point(15, 514);
            this.lblChange.Name = "lblChange";
            this.lblChange.Size = new System.Drawing.Size(114, 20);
            this.lblChange.TabIndex = 28;
            this.lblChange.Text = "Change due:";
            // 
            // btnCalculatechange
            // 
            this.btnCalculatechange.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCalculatechange.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnCalculatechange.Depth = 0;
            this.btnCalculatechange.HighEmphasis = true;
            this.btnCalculatechange.Icon = null;
            this.btnCalculatechange.Location = new System.Drawing.Point(360, 535);
            this.btnCalculatechange.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnCalculatechange.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnCalculatechange.Name = "btnCalculatechange";
            this.btnCalculatechange.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnCalculatechange.Size = new System.Drawing.Size(80, 36);
            this.btnCalculatechange.TabIndex = 29;
            this.btnCalculatechange.Text = "Change";
            this.btnCalculatechange.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnCalculatechange.UseAccentColor = true;
            this.btnCalculatechange.Click += new System.EventHandler(this.btnCalculatechange_Click);
            // 
            // lbldiscountvalue
            // 
            this.lbldiscountvalue.AutoSize = true;
            this.lbldiscountvalue.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold);
            this.lbldiscountvalue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lbldiscountvalue.Location = new System.Drawing.Point(3, 204);
            this.lbldiscountvalue.Name = "lbldiscountvalue";
            this.lbldiscountvalue.Size = new System.Drawing.Size(239, 32);
            this.lbldiscountvalue.TabIndex = 31;
            this.lbldiscountvalue.Text = "Discount: R 0,00";
            // 
            // txtdiscount
            // 
            this.txtdiscount.AnimateReadOnly = false;
            this.txtdiscount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtdiscount.Depth = 0;
            this.txtdiscount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.6F);
            this.txtdiscount.Hint = "Discount Amount";
            this.txtdiscount.LeadingIcon = null;
            this.txtdiscount.Location = new System.Drawing.Point(15, 327);
            this.txtdiscount.MaxLength = 50;
            this.txtdiscount.MouseState = MaterialSkin.MouseState.OUT;
            this.txtdiscount.Multiline = false;
            this.txtdiscount.Name = "txtdiscount";
            this.txtdiscount.Size = new System.Drawing.Size(80, 50);
            this.txtdiscount.TabIndex = 32;
            this.txtdiscount.Text = "0";
            this.txtdiscount.TrailingIcon = null;
            this.txtdiscount.TextChanged += new System.EventHandler(this.txtdiscount_TextChanged);
            // 
            // lblDateTime
            // 
            this.lblDateTime.AutoSize = true;
            this.lblDateTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateTime.ForeColor = System.Drawing.Color.White;
            this.lblDateTime.Location = new System.Drawing.Point(707, 74);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(192, 36);
            this.lblDateTime.TabIndex = 33;
            this.lblDateTime.Text = "DATE TIME:";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblBusineess
            // 
            this.lblBusineess.AutoSize = true;
            this.lblBusineess.BackColor = System.Drawing.Color.Transparent;
            this.lblBusineess.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBusineess.ForeColor = System.Drawing.Color.White;
            this.lblBusineess.Location = new System.Drawing.Point(697, -10);
            this.lblBusineess.Name = "lblBusineess";
            this.lblBusineess.Size = new System.Drawing.Size(516, 91);
            this.lblBusineess.TabIndex = 34;
            this.lblBusineess.Text = "Point Of Sale";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(180, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 20);
            this.label2.TabIndex = 35;
            this.label2.Text = "label2";
            // 
            // checkEmail
            // 
            this.checkEmail.AutoSize = true;
            this.checkEmail.Depth = 0;
            this.checkEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.checkEmail.Location = new System.Drawing.Point(16, 689);
            this.checkEmail.Margin = new System.Windows.Forms.Padding(0);
            this.checkEmail.MouseLocation = new System.Drawing.Point(-1, -1);
            this.checkEmail.MouseState = MaterialSkin.MouseState.HOVER;
            this.checkEmail.Name = "checkEmail";
            this.checkEmail.ReadOnly = false;
            this.checkEmail.Ripple = true;
            this.checkEmail.Size = new System.Drawing.Size(131, 37);
            this.checkEmail.TabIndex = 37;
            this.checkEmail.Text = "Email Receipt";
            this.checkEmail.CheckedChanged += new System.EventHandler(this.checkEmail_CheckedChanged);
            // 
            // txtRecipientEmail
            // 
            this.txtRecipientEmail.AnimateReadOnly = false;
            this.txtRecipientEmail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRecipientEmail.Depth = 0;
            this.txtRecipientEmail.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtRecipientEmail.Hint = "Recipient Email";
            this.txtRecipientEmail.LeadingIcon = null;
            this.txtRecipientEmail.Location = new System.Drawing.Point(16, 729);
            this.txtRecipientEmail.MaxLength = 100;
            this.txtRecipientEmail.MouseState = MaterialSkin.MouseState.OUT;
            this.txtRecipientEmail.Multiline = false;
            this.txtRecipientEmail.Name = "txtRecipientEmail";
            this.txtRecipientEmail.Size = new System.Drawing.Size(450, 50);
            this.txtRecipientEmail.TabIndex = 38;
            this.txtRecipientEmail.Text = "";
            this.txtRecipientEmail.TrailingIcon = null;
            this.txtRecipientEmail.TextChanged += new System.EventHandler(this.txtRecipientEmail_TextChanged);
            // 
            // btnVoidLine
            // 
            this.btnVoidLine.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnVoidLine.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnVoidLine.Depth = 0;
            this.btnVoidLine.HighEmphasis = true;
            this.btnVoidLine.Icon = null;
            this.btnVoidLine.Location = new System.Drawing.Point(1689, 320);
            this.btnVoidLine.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnVoidLine.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnVoidLine.Name = "btnVoidLine";
            this.btnVoidLine.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnVoidLine.Size = new System.Drawing.Size(90, 36);
            this.btnVoidLine.TabIndex = 39;
            this.btnVoidLine.Text = "Void Line";
            this.btnVoidLine.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnVoidLine.UseAccentColor = true;
            this.btnVoidLine.Click += new System.EventHandler(this.btnVoidLine_Click);
            // 
            // btnRefund
            // 
            this.btnRefund.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnRefund.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnRefund.Depth = 0;
            this.btnRefund.HighEmphasis = true;
            this.btnRefund.Icon = null;
            this.btnRefund.Location = new System.Drawing.Point(1689, 210);
            this.btnRefund.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnRefund.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnRefund.Name = "btnRefund";
            this.btnRefund.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnRefund.Size = new System.Drawing.Size(77, 36);
            this.btnRefund.TabIndex = 40;
            this.btnRefund.Text = "Refund";
            this.btnRefund.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnRefund.UseAccentColor = true;
            this.btnRefund.Click += new System.EventHandler(this.btnRefund_Click);
            // 
            // btnCupons
            // 
            this.btnCupons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCupons.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnCupons.Depth = 0;
            this.btnCupons.HighEmphasis = true;
            this.btnCupons.Icon = null;
            this.btnCupons.Location = new System.Drawing.Point(1689, 368);
            this.btnCupons.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnCupons.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnCupons.Name = "btnCupons";
            this.btnCupons.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnCupons.Size = new System.Drawing.Size(130, 36);
            this.btnCupons.TabIndex = 44;
            this.btnCupons.Text = "Apply Coupon";
            this.btnCupons.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnCupons.UseAccentColor = true;
            this.btnCupons.Click += new System.EventHandler(this.btnCupons_Click);
            // 
            // btnPriceCheck
            // 
            this.btnPriceCheck.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnPriceCheck.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnPriceCheck.Depth = 0;
            this.btnPriceCheck.HighEmphasis = true;
            this.btnPriceCheck.Icon = null;
            this.btnPriceCheck.Location = new System.Drawing.Point(1689, 422);
            this.btnPriceCheck.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnPriceCheck.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnPriceCheck.Name = "btnPriceCheck";
            this.btnPriceCheck.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnPriceCheck.Size = new System.Drawing.Size(113, 36);
            this.btnPriceCheck.TabIndex = 45;
            this.btnPriceCheck.Text = "Price Check";
            this.btnPriceCheck.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnPriceCheck.UseAccentColor = true;
            // 
            // btnOpenCashDrawer
            // 
            this.btnOpenCashDrawer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnOpenCashDrawer.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnOpenCashDrawer.Depth = 0;
            this.btnOpenCashDrawer.HighEmphasis = true;
            this.btnOpenCashDrawer.Icon = null;
            this.btnOpenCashDrawer.Location = new System.Drawing.Point(1689, 156);
            this.btnOpenCashDrawer.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnOpenCashDrawer.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnOpenCashDrawer.Name = "btnOpenCashDrawer";
            this.btnOpenCashDrawer.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnOpenCashDrawer.Size = new System.Drawing.Size(165, 36);
            this.btnOpenCashDrawer.TabIndex = 46;
            this.btnOpenCashDrawer.Text = "Open Cash Drawer";
            this.btnOpenCashDrawer.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnOpenCashDrawer.UseAccentColor = true;
            this.btnOpenCashDrawer.Click += new System.EventHandler(this.btnOpenCashDrawer_Click);
            // 
            // button2
            // 
            this.button2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button2.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.button2.Depth = 0;
            this.button2.HighEmphasis = true;
            this.button2.Icon = null;
            this.button2.Location = new System.Drawing.Point(1689, 264);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.button2.MouseState = MaterialSkin.MouseState.HOVER;
            this.button2.Name = "button2";
            this.button2.NoAccentTextColor = System.Drawing.Color.Empty;
            this.button2.Size = new System.Drawing.Size(98, 36);
            this.button2.TabIndex = 49;
            this.button2.Text = "CashBack";
            this.button2.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.button2.UseAccentColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnReprint
            // 
            this.btnReprint.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnReprint.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnReprint.Depth = 0;
            this.btnReprint.HighEmphasis = true;
            this.btnReprint.Icon = null;
            this.btnReprint.Location = new System.Drawing.Point(1723, 925);
            this.btnReprint.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnReprint.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnReprint.Name = "btnReprint";
            this.btnReprint.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnReprint.Size = new System.Drawing.Size(143, 36);
            this.btnReprint.TabIndex = 54;
            this.btnReprint.Text = "Reprint Receipt";
            this.btnReprint.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnReprint.UseAccentColor = true;
            this.btnReprint.Click += new System.EventHandler(this.btnReprint_Click);
            // 
            // lblCashBack
            // 
            this.lblCashBack.AutoSize = true;
            this.lblCashBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold);
            this.lblCashBack.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblCashBack.Location = new System.Drawing.Point(3, 140);
            this.lblCashBack.Name = "lblCashBack";
            this.lblCashBack.Size = new System.Drawing.Size(165, 32);
            this.lblCashBack.TabIndex = 55;
            this.lblCashBack.Text = "Cashback :";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripChangeBackColour,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.ShowImageMargin = false;
            this.contextMenuStrip1.Size = new System.Drawing.Size(136, 54);
            this.contextMenuStrip1.Text = "BackColor";
            // 
            // toolStripChangeBackColour
            // 
            this.toolStripChangeBackColour.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.toolStripChangeBackColour.Name = "toolStripChangeBackColour";
            this.toolStripChangeBackColour.Size = new System.Drawing.Size(100, 24);
            this.toolStripChangeBackColour.Text = "BackColor";
            this.toolStripChangeBackColour.ToolTipText = "Change BackColor";
            this.toolStripChangeBackColour.Click += new System.EventHandler(this.toolStripChangeBackColour_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(135, 24);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            // 
            // checkdiscount
            // 
            this.checkdiscount.AutoSize = true;
            this.checkdiscount.Depth = 0;
            this.checkdiscount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.checkdiscount.Location = new System.Drawing.Point(342, 284);
            this.checkdiscount.Margin = new System.Windows.Forms.Padding(0);
            this.checkdiscount.MouseLocation = new System.Drawing.Point(-1, -1);
            this.checkdiscount.MouseState = MaterialSkin.MouseState.HOVER;
            this.checkdiscount.Name = "checkdiscount";
            this.checkdiscount.ReadOnly = false;
            this.checkdiscount.Ripple = true;
            this.checkdiscount.Size = new System.Drawing.Size(98, 37);
            this.checkdiscount.TabIndex = 56;
            this.checkdiscount.Text = "Discount";
            // 
            // comboProductCategory
            // 
            this.comboProductCategory.AutoResize = false;
            this.comboProductCategory.BackColor = System.Drawing.Color.White;
            this.comboProductCategory.Depth = 0;
            this.comboProductCategory.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboProductCategory.DropDownHeight = 174;
            this.comboProductCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboProductCategory.DropDownWidth = 121;
            this.comboProductCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.comboProductCategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.comboProductCategory.Hint = "Select Product Category";
            this.comboProductCategory.IntegralHeight = false;
            this.comboProductCategory.ItemHeight = 43;
            this.comboProductCategory.Location = new System.Drawing.Point(10, 140);
            this.comboProductCategory.MaxDropDownItems = 4;
            this.comboProductCategory.MouseState = MaterialSkin.MouseState.OUT;
            this.comboProductCategory.Name = "comboProductCategory";
            this.comboProductCategory.Size = new System.Drawing.Size(430, 49);
            this.comboProductCategory.StartIndex = 0;
            this.comboProductCategory.TabIndex = 57;
            this.comboProductCategory.SelectedIndexChanged += new System.EventHandler(this.comboProductCategory_SelectedIndexChanged);
            // 
            // comboBoxProducts
            // 
            this.comboBoxProducts.AutoResize = false;
            this.comboBoxProducts.BackColor = System.Drawing.Color.White;
            this.comboBoxProducts.Depth = 0;
            this.comboBoxProducts.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBoxProducts.DropDownHeight = 174;
            this.comboBoxProducts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProducts.DropDownWidth = 121;
            this.comboBoxProducts.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.comboBoxProducts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.comboBoxProducts.Hint = "Select Product";
            this.comboBoxProducts.IntegralHeight = false;
            this.comboBoxProducts.ItemHeight = 43;
            this.comboBoxProducts.Location = new System.Drawing.Point(10, 210);
            this.comboBoxProducts.MaxDropDownItems = 4;
            this.comboBoxProducts.MouseState = MaterialSkin.MouseState.OUT;
            this.comboBoxProducts.Name = "comboBoxProducts";
            this.comboBoxProducts.Size = new System.Drawing.Size(430, 49);
            this.comboBoxProducts.StartIndex = 0;
            this.comboBoxProducts.TabIndex = 58;
            // 
            // combopaymentmentod
            // 
            this.combopaymentmentod.AutoResize = false;
            this.combopaymentmentod.BackColor = System.Drawing.Color.White;
            this.combopaymentmentod.Depth = 0;
            this.combopaymentmentod.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.combopaymentmentod.DropDownHeight = 174;
            this.combopaymentmentod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combopaymentmentod.DropDownWidth = 121;
            this.combopaymentmentod.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.combopaymentmentod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.combopaymentmentod.Hint = "Select Payment Method";
            this.combopaymentmentod.IntegralHeight = false;
            this.combopaymentmentod.ItemHeight = 43;
            this.combopaymentmentod.Location = new System.Drawing.Point(10, 409);
            this.combopaymentmentod.MaxDropDownItems = 4;
            this.combopaymentmentod.MouseState = MaterialSkin.MouseState.OUT;
            this.combopaymentmentod.Name = "combopaymentmentod";
            this.combopaymentmentod.Size = new System.Drawing.Size(430, 49);
            this.combopaymentmentod.StartIndex = 0;
            this.combopaymentmentod.TabIndex = 59;
            this.combopaymentmentod.SelectedIndexChanged += new System.EventHandler(this.combopaymentmentod_SelectedIndexChanged);
            // 
            // btnAddProduct
            // 
            this.btnAddProduct.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAddProduct.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnAddProduct.Depth = 0;
            this.btnAddProduct.HighEmphasis = true;
            this.btnAddProduct.Icon = null;
            this.btnAddProduct.Location = new System.Drawing.Point(319, 341);
            this.btnAddProduct.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnAddProduct.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnAddProduct.Size = new System.Drawing.Size(121, 36);
            this.btnAddProduct.TabIndex = 60;
            this.btnAddProduct.Text = "Add Product";
            this.btnAddProduct.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnAddProduct.UseAccentColor = true;
            this.btnAddProduct.Click += new System.EventHandler(this.btnAddProduct_Click);
            // 
            // txtQuantity
            // 
            this.txtQuantity.AnimateReadOnly = false;
            this.txtQuantity.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtQuantity.Depth = 0;
            this.txtQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.6F);
            this.txtQuantity.Hint = "Quantity";
            this.txtQuantity.LeadingIcon = null;
            this.txtQuantity.Location = new System.Drawing.Point(10, 271);
            this.txtQuantity.MaxLength = 50;
            this.txtQuantity.MouseState = MaterialSkin.MouseState.OUT;
            this.txtQuantity.Multiline = false;
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(140, 50);
            this.txtQuantity.TabIndex = 61;
            this.txtQuantity.Text = "1";
            this.txtQuantity.TrailingIcon = null;
            // 
            // Subtotal
            // 
            this.Subtotal.HeaderText = "SUBTOTAL";
            this.Subtotal.MinimumWidth = 120;
            this.Subtotal.Name = "Subtotal";
            this.Subtotal.ReadOnly = true;
            // 
            // Price
            // 
            this.Price.HeaderText = "PRICE";
            this.Price.MinimumWidth = 100;
            this.Price.Name = "Price";
            this.Price.ReadOnly = true;
            // 
            // Quantity
            // 
            this.Quantity.HeaderText = "QTY";
            this.Quantity.MinimumWidth = 80;
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            // 
            // ProductName
            // 
            this.ProductName.HeaderText = "PRODUCT NAME";
            this.ProductName.MinimumWidth = 200;
            this.ProductName.Name = "ProductName";
            this.ProductName.ReadOnly = true;
            // 
            // ProductID
            // 
            this.ProductID.HeaderText = "PRODUCT ID";
            this.ProductID.MinimumWidth = 100;
            this.ProductID.Name = "ProductID";
            this.ProductID.ReadOnly = true;
            // 
            // dataGridViewSaleDetails
            // 
            this.dataGridViewSaleDetails.AllowUserToAddRows = false;
            this.dataGridViewSaleDetails.AllowUserToResizeRows = false;
            this.dataGridViewSaleDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewSaleDetails.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewSaleDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewSaleDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.dataGridViewSaleDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewSaleDetails.ColumnHeadersHeight = 40;
            this.dataGridViewSaleDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProductID,
            this.ProductName,
            this.Quantity,
            this.Price,
            this.Subtotal});
            this.dataGridViewSaleDetails.EnableHeadersVisualStyles = false;
            this.dataGridViewSaleDetails.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridViewSaleDetails.Location = new System.Drawing.Point(464, 156);
            this.dataGridViewSaleDetails.Name = "dataGridViewSaleDetails";
            this.dataGridViewSaleDetails.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewSaleDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewSaleDetails.RowHeadersVisible = false;
            this.dataGridViewSaleDetails.RowHeadersWidth = 51;
            this.dataGridViewSaleDetails.RowTemplate.Height = 35;
            this.dataGridViewSaleDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewSaleDetails.Size = new System.Drawing.Size(866, 623);
            this.dataGridViewSaleDetails.TabIndex = 6;
            // 
            // txtBarcodeLabel
            // 
            this.txtBarcodeLabel.AnimateReadOnly = false;
            this.txtBarcodeLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBarcodeLabel.Depth = 0;
            this.txtBarcodeLabel.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtBarcodeLabel.Hint = "Scan Barcode";
            this.txtBarcodeLabel.LeadingIcon = null;
            this.txtBarcodeLabel.Location = new System.Drawing.Point(89, 3);
            this.txtBarcodeLabel.MaxLength = 50;
            this.txtBarcodeLabel.MouseState = MaterialSkin.MouseState.OUT;
            this.txtBarcodeLabel.Multiline = false;
            this.txtBarcodeLabel.Name = "txtBarcodeLabel";
            this.txtBarcodeLabel.Size = new System.Drawing.Size(330, 50);
            this.txtBarcodeLabel.TabIndex = 62;
            this.txtBarcodeLabel.Text = "";
            this.txtBarcodeLabel.TrailingIcon = null;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.pictureBox3);
            this.flowLayoutPanel1.Controls.Add(this.txtBarcodeLabel);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(16, 615);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(450, 50);
            this.flowLayoutPanel1.TabIndex = 63;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Miara.Properties.Resources.barcode_scan;
            this.pictureBox3.Location = new System.Drawing.Point(3, 3);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(80, 40);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 65;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Miara.Properties.Resources.MIARA_POS;
            this.pictureBox2.Location = new System.Drawing.Point(0, 900);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(200, 120);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 64;
            this.pictureBox2.TabStop = false;
            // 
            // lblScan
            // 
            this.lblScan.AutoSize = true;
            this.lblScan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.lblScan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblScan.Location = new System.Drawing.Point(12, 588);
            this.lblScan.Name = "lblScan";
            this.lblScan.Size = new System.Drawing.Size(97, 17);
            this.lblScan.TabIndex = 66;
            this.lblScan.Text = "Scan Barcode";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(170, 110);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 67;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // btnReportSales
            // 
            this.btnReportSales.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnReportSales.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnReportSales.Depth = 0;
            this.btnReportSales.HighEmphasis = true;
            this.btnReportSales.Icon = null;
            this.btnReportSales.Location = new System.Drawing.Point(1790, 984);
            this.btnReportSales.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnReportSales.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnReportSales.Name = "btnReportSales";
            this.btnReportSales.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnReportSales.Size = new System.Drawing.Size(76, 36);
            this.btnReportSales.TabIndex = 68;
            this.btnReportSales.Text = "Report";
            this.btnReportSales.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnReportSales.UseAccentColor = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.panel2.Controls.Add(this.lblUserRole);
            this.panel2.Controls.Add(this.btnBack);
            this.panel2.Controls.Add(this.lblBusineess);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.lblDateTime);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1908, 110);
            this.panel2.TabIndex = 69;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // btnBack
            // 
            this.btnBack.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnBack.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnBack.Depth = 0;
            this.btnBack.HighEmphasis = true;
            this.btnBack.Icon = null;
            this.btnBack.Location = new System.Drawing.Point(1790, 10);
            this.btnBack.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnBack.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnBack.Name = "btnBack";
            this.btnBack.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnBack.Size = new System.Drawing.Size(64, 36);
            this.btnBack.TabIndex = 1;
            this.btnBack.Text = "X";
            this.btnBack.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnBack.UseAccentColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.panel1.Location = new System.Drawing.Point(0, 1040);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1908, 45);
            this.panel1.TabIndex = 70;
            // 
            // lblinternet
            // 
            this.lblinternet.AutoSize = true;
            this.lblinternet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.lblinternet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblinternet.Location = new System.Drawing.Point(-3, 1020);
            this.lblinternet.Name = "lblinternet";
            this.lblinternet.Size = new System.Drawing.Size(56, 17);
            this.lblinternet.TabIndex = 71;
            this.lblinternet.Text = "internet";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.lblTotalAmount);
            this.flowLayoutPanel2.Controls.Add(this.lblTax);
            this.flowLayoutPanel2.Controls.Add(this.lblCashBack);
            this.flowLayoutPanel2.Controls.Add(this.lblSubtotalorder);
            this.flowLayoutPanel2.Controls.Add(this.lbldiscountvalue);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(1388, 156);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(277, 302);
            this.flowLayoutPanel2.TabIndex = 72;
            // 
            // lblemailvalid
            // 
            this.lblemailvalid.AutoSize = true;
            this.lblemailvalid.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold);
            this.lblemailvalid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.lblemailvalid.Location = new System.Drawing.Point(15, 782);
            this.lblemailvalid.Name = "lblemailvalid";
            this.lblemailvalid.Size = new System.Drawing.Size(151, 20);
            this.lblemailvalid.TabIndex = 73;
            this.lblemailvalid.Text = "Email Not Valid!!";
            this.lblemailvalid.Visible = false;
            // 
            // lblUserRole
            // 
            this.lblUserRole.AutoSize = true;
            this.lblUserRole.BackColor = System.Drawing.Color.Transparent;
            this.lblUserRole.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold);
            this.lblUserRole.ForeColor = System.Drawing.Color.White;
            this.lblUserRole.Location = new System.Drawing.Point(180, 30);
            this.lblUserRole.Name = "lblUserRole";
            this.lblUserRole.Size = new System.Drawing.Size(87, 20);
            this.lblUserRole.TabIndex = 68;
            this.lblUserRole.Text = "UserRole";
            // 
            // frmSales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(1908, 1084);
            this.ControlBox = false;
            this.Controls.Add(this.lblemailvalid);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.lblinternet);
            this.Controls.Add(this.btnCompleteSale);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnReportSales);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.lblScan);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.btnAddProduct);
            this.Controls.Add(this.combopaymentmentod);
            this.Controls.Add(this.comboBoxProducts);
            this.Controls.Add(this.comboProductCategory);
            this.Controls.Add(this.checkdiscount);
            this.Controls.Add(this.btnReprint);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnOpenCashDrawer);
            this.Controls.Add(this.btnPriceCheck);
            this.Controls.Add(this.btnCupons);
            this.Controls.Add(this.btnRefund);
            this.Controls.Add(this.btnVoidLine);
            this.Controls.Add(this.txtRecipientEmail);
            this.Controls.Add(this.checkEmail);
            this.Controls.Add(this.txtdiscount);
            this.Controls.Add(this.btnCalculatechange);
            this.Controls.Add(this.lblChange);
            this.Controls.Add(this.txtrenderedamount);
            this.Controls.Add(this.lblrendered);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.dataGridViewSaleDetails);
            this.Controls.Add(this.lblNextSalesNumber);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSales";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Point of Sale";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmSales_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSaleDetails)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblNextSalesNumber;
        private System.Windows.Forms.Label lblTotalAmount;
        private MaterialSkin.Controls.MaterialButton btnCompleteSale;
        private System.Windows.Forms.Label lblTax;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.Label lblSubtotalorder;
        private MaterialSkin.Controls.MaterialButton btnClear;
        private System.Windows.Forms.Label lblrendered;
        private MaterialSkin.Controls.MaterialTextBox txtrenderedamount;
        private System.Windows.Forms.Label lblChange;
        private MaterialSkin.Controls.MaterialButton btnCalculatechange;
        private System.Windows.Forms.Label lbldiscountvalue;
        private MaterialSkin.Controls.MaterialTextBox txtdiscount;
        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblBusineess;
        public System.Windows.Forms.Label label2;
        private MaterialSkin.Controls.MaterialCheckbox checkEmail;
        private MaterialSkin.Controls.MaterialTextBox txtRecipientEmail;
        private MaterialSkin.Controls.MaterialButton btnVoidLine;
        private MaterialSkin.Controls.MaterialButton btnRefund;
        private MaterialSkin.Controls.MaterialButton btnCupons;
        private MaterialSkin.Controls.MaterialButton btnPriceCheck;
        private MaterialSkin.Controls.MaterialButton btnOpenCashDrawer;
        private MaterialSkin.Controls.MaterialButton button2;
        private MaterialSkin.Controls.MaterialButton btnReprint;
        private System.Windows.Forms.Label lblCashBack;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripTextBox toolStripChangeBackColour;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private MaterialSkin.Controls.MaterialCheckbox checkdiscount;
        private MaterialSkin.Controls.MaterialComboBox comboProductCategory;
        private MaterialSkin.Controls.MaterialComboBox comboBoxProducts;
        private MaterialSkin.Controls.MaterialComboBox combopaymentmentod;
        private MaterialSkin.Controls.MaterialButton btnAddProduct;
        private MaterialSkin.Controls.MaterialTextBox txtQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Subtotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private new System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductID;
        private System.Windows.Forms.DataGridView dataGridViewSaleDetails;
        private MaterialSkin.Controls.MaterialTextBox txtBarcodeLabel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label lblScan;
        private System.Windows.Forms.PictureBox pictureBox1;
        private MaterialSkin.Controls.MaterialButton btnReportSales;
        private System.Windows.Forms.Panel panel2;
        private MaterialSkin.Controls.MaterialButton btnBack;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblinternet;
        private FlowLayoutPanel flowLayoutPanel2;
        private Label lblemailvalid;
        public Label lblUserRole;
    }
}