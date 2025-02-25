
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSales));
            this.comboBoxProducts = new System.Windows.Forms.ComboBox();
            this.lblNextSalesNumber = new System.Windows.Forms.Label();
            this.lblScanPruduct = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridViewSaleDetails = new System.Windows.Forms.DataGridView();
            this.ProductID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Subtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.btnCompleteSale = new System.Windows.Forms.Button();
            this.lblTax = new System.Windows.Forms.Label();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.btn1 = new System.Windows.Forms.Button();
            this.btn2 = new System.Windows.Forms.Button();
            this.btn3 = new System.Windows.Forms.Button();
            this.btn4 = new System.Windows.Forms.Button();
            this.btn5 = new System.Windows.Forms.Button();
            this.btn6 = new System.Windows.Forms.Button();
            this.btn7 = new System.Windows.Forms.Button();
            this.btn8 = new System.Windows.Forms.Button();
            this.btn9 = new System.Windows.Forms.Button();
            this.btn00 = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btn0 = new System.Windows.Forms.Button();
            this.lblSubtotalorder = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.combopaymentmentod = new System.Windows.Forms.ComboBox();
            this.lblrendered = new System.Windows.Forms.Label();
            this.txtrenderedamount = new System.Windows.Forms.TextBox();
            this.lblChange = new System.Windows.Forms.Label();
            this.btnCalculatechange = new System.Windows.Forms.Button();
            this.checkdiscount = new System.Windows.Forms.CheckBox();
            this.lbldiscountvalue = new System.Windows.Forms.Label();
            this.txtdiscount = new System.Windows.Forms.TextBox();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblBusineess = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnExitSaleform = new System.Windows.Forms.Button();
            this.checkEmail = new System.Windows.Forms.CheckBox();
            this.txtRecipientEmail = new System.Windows.Forms.TextBox();
            this.btnVoidLine = new System.Windows.Forms.Button();
            this.btnRefund = new System.Windows.Forms.Button();
            this.btnAirtime = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnBillsPaytment = new System.Windows.Forms.Button();
            this.btnCupons = new System.Windows.Forms.Button();
            this.btnPriceCheck = new System.Windows.Forms.Button();
            this.btnOpenCashDrawer = new System.Windows.Forms.Button();
            this.btnOTT = new System.Windows.Forms.Button();
            this.btnBetway = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.comboProductCategory = new System.Windows.Forms.ComboBox();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.btnReprint = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSaleDetails)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxProducts
            // 
            this.comboBoxProducts.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.comboBoxProducts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxProducts.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxProducts.FormattingEnabled = true;
            this.comboBoxProducts.Location = new System.Drawing.Point(24, 222);
            this.comboBoxProducts.Name = "comboBoxProducts";
            this.comboBoxProducts.Size = new System.Drawing.Size(339, 26);
            this.comboBoxProducts.TabIndex = 0;
            // 
            // lblNextSalesNumber
            // 
            this.lblNextSalesNumber.AutoSize = true;
            this.lblNextSalesNumber.Font = new System.Drawing.Font("Arial", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNextSalesNumber.Location = new System.Drawing.Point(3, 46);
            this.lblNextSalesNumber.Name = "lblNextSalesNumber";
            this.lblNextSalesNumber.Size = new System.Drawing.Size(471, 56);
            this.lblNextSalesNumber.TabIndex = 1;
            this.lblNextSalesNumber.Text = "SALES ORDER NO:";
            // 
            // lblScanPruduct
            // 
            this.lblScanPruduct.AutoSize = true;
            this.lblScanPruduct.Font = new System.Drawing.Font("Arial Black", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScanPruduct.Location = new System.Drawing.Point(16, 179);
            this.lblScanPruduct.Name = "lblScanPruduct";
            this.lblScanPruduct.Size = new System.Drawing.Size(262, 40);
            this.lblScanPruduct.TabIndex = 2;
            this.lblScanPruduct.Text = "Select Product :";
            // 
            // txtQuantity
            // 
            this.txtQuantity.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuantity.Location = new System.Drawing.Point(120, 254);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(249, 30);
            this.txtQuantity.TabIndex = 3;
            this.txtQuantity.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(17, 260);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "Quantity";
            // 
            // dataGridViewSaleDetails
            // 
            this.dataGridViewSaleDetails.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewSaleDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewSaleDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridViewSaleDetails.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewSaleDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewSaleDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSaleDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProductID,
            this.ProductName,
            this.Quantity,
            this.Price,
            this.Subtotal});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewSaleDetails.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewSaleDetails.GridColor = System.Drawing.SystemColors.Control;
            this.dataGridViewSaleDetails.Location = new System.Drawing.Point(1014, 191);
            this.dataGridViewSaleDetails.Name = "dataGridViewSaleDetails";
            this.dataGridViewSaleDetails.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewSaleDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewSaleDetails.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridViewSaleDetails.RowTemplate.Height = 24;
            this.dataGridViewSaleDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewSaleDetails.Size = new System.Drawing.Size(753, 677);
            this.dataGridViewSaleDetails.TabIndex = 6;
            // 
            // ProductID
            // 
            this.ProductID.HeaderText = "ProductID";
            this.ProductID.MinimumWidth = 6;
            this.ProductID.Name = "ProductID";
            this.ProductID.Width = 125;
            // 
            // ProductName
            // 
            this.ProductName.HeaderText = "ProductName";
            this.ProductName.MinimumWidth = 6;
            this.ProductName.Name = "ProductName";
            this.ProductName.Width = 125;
            // 
            // Quantity
            // 
            this.Quantity.HeaderText = "Quantity";
            this.Quantity.MinimumWidth = 6;
            this.Quantity.Name = "Quantity";
            this.Quantity.Width = 125;
            // 
            // Price
            // 
            this.Price.HeaderText = "Price";
            this.Price.MinimumWidth = 6;
            this.Price.Name = "Price";
            this.Price.Width = 125;
            // 
            // Subtotal
            // 
            this.Subtotal.HeaderText = "Subtotal";
            this.Subtotal.MinimumWidth = 6;
            this.Subtotal.Name = "Subtotal";
            this.Subtotal.Width = 125;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmount.Location = new System.Drawing.Point(1009, 12);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(212, 35);
            this.lblTotalAmount.TabIndex = 7;
            this.lblTotalAmount.Text = "Total Amount:";
            // 
            // btnCompleteSale
            // 
            this.btnCompleteSale.BackColor = System.Drawing.Color.Yellow;
            this.btnCompleteSale.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCompleteSale.ForeColor = System.Drawing.Color.Black;
            this.btnCompleteSale.Location = new System.Drawing.Point(1361, 874);
            this.btnCompleteSale.Name = "btnCompleteSale";
            this.btnCompleteSale.Size = new System.Drawing.Size(145, 123);
            this.btnCompleteSale.TabIndex = 8;
            this.btnCompleteSale.Text = "Complete Sale";
            this.btnCompleteSale.UseVisualStyleBackColor = false;
            this.btnCompleteSale.Click += new System.EventHandler(this.btnCompleteSale_Click);
            // 
            // lblTax
            // 
            this.lblTax.AutoSize = true;
            this.lblTax.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTax.Location = new System.Drawing.Point(1011, 46);
            this.lblTax.Name = "lblTax";
            this.lblTax.Size = new System.Drawing.Size(119, 24);
            this.lblTax.TabIndex = 9;
            this.lblTax.Text = "VAT(15%) : ";
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // btn1
            // 
            this.btn1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn1.Font = new System.Drawing.Font("Arial Black", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn1.Location = new System.Drawing.Point(3, 3);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(152, 112);
            this.btn1.TabIndex = 10;
            this.btn1.Text = "1";
            this.btn1.UseVisualStyleBackColor = false;
            // 
            // btn2
            // 
            this.btn2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn2.Font = new System.Drawing.Font("Arial Black", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn2.Location = new System.Drawing.Point(161, 3);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(157, 112);
            this.btn2.TabIndex = 11;
            this.btn2.Text = "2";
            this.btn2.UseVisualStyleBackColor = false;
            // 
            // btn3
            // 
            this.btn3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn3.Font = new System.Drawing.Font("Arial Black", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn3.Location = new System.Drawing.Point(324, 3);
            this.btn3.Name = "btn3";
            this.btn3.Size = new System.Drawing.Size(150, 112);
            this.btn3.TabIndex = 12;
            this.btn3.Text = "3";
            this.btn3.UseVisualStyleBackColor = false;
            // 
            // btn4
            // 
            this.btn4.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn4.Font = new System.Drawing.Font("Arial Black", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn4.Location = new System.Drawing.Point(3, 121);
            this.btn4.Name = "btn4";
            this.btn4.Size = new System.Drawing.Size(152, 120);
            this.btn4.TabIndex = 13;
            this.btn4.Text = "4";
            this.btn4.UseVisualStyleBackColor = false;
            // 
            // btn5
            // 
            this.btn5.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn5.Font = new System.Drawing.Font("Arial Black", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn5.Location = new System.Drawing.Point(161, 121);
            this.btn5.Name = "btn5";
            this.btn5.Size = new System.Drawing.Size(157, 120);
            this.btn5.TabIndex = 14;
            this.btn5.Text = "5";
            this.btn5.UseVisualStyleBackColor = false;
            // 
            // btn6
            // 
            this.btn6.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn6.Font = new System.Drawing.Font("Arial Black", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn6.Location = new System.Drawing.Point(324, 121);
            this.btn6.Name = "btn6";
            this.btn6.Size = new System.Drawing.Size(150, 120);
            this.btn6.TabIndex = 15;
            this.btn6.Text = "6";
            this.btn6.UseVisualStyleBackColor = false;
            // 
            // btn7
            // 
            this.btn7.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn7.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn7.Font = new System.Drawing.Font("Arial Black", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn7.Location = new System.Drawing.Point(3, 247);
            this.btn7.Name = "btn7";
            this.btn7.Size = new System.Drawing.Size(152, 118);
            this.btn7.TabIndex = 16;
            this.btn7.Text = "7";
            this.btn7.UseVisualStyleBackColor = false;
            // 
            // btn8
            // 
            this.btn8.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn8.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn8.Font = new System.Drawing.Font("Arial Black", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn8.Location = new System.Drawing.Point(161, 247);
            this.btn8.Name = "btn8";
            this.btn8.Size = new System.Drawing.Size(157, 118);
            this.btn8.TabIndex = 17;
            this.btn8.Text = "8";
            this.btn8.UseVisualStyleBackColor = false;
            // 
            // btn9
            // 
            this.btn9.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn9.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn9.Font = new System.Drawing.Font("Arial Black", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn9.Location = new System.Drawing.Point(324, 247);
            this.btn9.Name = "btn9";
            this.btn9.Size = new System.Drawing.Size(150, 118);
            this.btn9.TabIndex = 18;
            this.btn9.Text = "9";
            this.btn9.UseVisualStyleBackColor = false;
            // 
            // btn00
            // 
            this.btn00.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn00.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn00.Font = new System.Drawing.Font("Arial Black", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn00.Location = new System.Drawing.Point(161, 371);
            this.btn00.Name = "btn00";
            this.btn00.Size = new System.Drawing.Size(157, 108);
            this.btn00.TabIndex = 19;
            this.btn00.Text = "00";
            this.btn00.UseVisualStyleBackColor = false;
            // 
            // btnDel
            // 
            this.btnDel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDel.Font = new System.Drawing.Font("Arial Black", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDel.Location = new System.Drawing.Point(324, 371);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(150, 108);
            this.btnDel.TabIndex = 20;
            this.btnDel.Text = "<- DEL";
            this.btnDel.UseVisualStyleBackColor = false;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btn0
            // 
            this.btn0.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn0.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn0.Font = new System.Drawing.Font("Arial Black", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn0.Location = new System.Drawing.Point(3, 371);
            this.btn0.Name = "btn0";
            this.btn0.Size = new System.Drawing.Size(152, 108);
            this.btn0.TabIndex = 21;
            this.btn0.Text = "0";
            this.btn0.UseVisualStyleBackColor = false;
            // 
            // lblSubtotalorder
            // 
            this.lblSubtotalorder.AutoSize = true;
            this.lblSubtotalorder.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblSubtotalorder.Location = new System.Drawing.Point(1011, 71);
            this.lblSubtotalorder.Name = "lblSubtotalorder";
            this.lblSubtotalorder.Size = new System.Drawing.Size(96, 24);
            this.lblSubtotalorder.TabIndex = 22;
            this.lblSubtotalorder.Text = "Subtotal:";
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Crimson;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClear.ForeColor = System.Drawing.Color.Black;
            this.btnClear.Location = new System.Drawing.Point(1015, 874);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(155, 123);
            this.btnClear.TabIndex = 23;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(12, 355);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(233, 24);
            this.label1.TabIndex = 24;
            this.label1.Text = "Select Payment Method";
            // 
            // combopaymentmentod
            // 
            this.combopaymentmentod.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.combopaymentmentod.FormattingEnabled = true;
            this.combopaymentmentod.Items.AddRange(new object[] {
            "CASH",
            "Debit\\Credit Card",
            "Cedit"});
            this.combopaymentmentod.Location = new System.Drawing.Point(13, 395);
            this.combopaymentmentod.Name = "combopaymentmentod";
            this.combopaymentmentod.Size = new System.Drawing.Size(363, 32);
            this.combopaymentmentod.TabIndex = 25;
            this.combopaymentmentod.SelectedIndexChanged += new System.EventHandler(this.combopaymentmentod_SelectedIndexChanged);
            // 
            // lblrendered
            // 
            this.lblrendered.AutoSize = true;
            this.lblrendered.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblrendered.Location = new System.Drawing.Point(11, 440);
            this.lblrendered.Name = "lblrendered";
            this.lblrendered.Size = new System.Drawing.Size(193, 24);
            this.lblrendered.TabIndex = 26;
            this.lblrendered.Text = "Amount Rendered :";
            // 
            // txtrenderedamount
            // 
            this.txtrenderedamount.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.txtrenderedamount.Location = new System.Drawing.Point(210, 434);
            this.txtrenderedamount.Name = "txtrenderedamount";
            this.txtrenderedamount.Size = new System.Drawing.Size(158, 30);
            this.txtrenderedamount.TabIndex = 27;
            // 
            // lblChange
            // 
            this.lblChange.AutoSize = true;
            this.lblChange.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblChange.ForeColor = System.Drawing.Color.Red;
            this.lblChange.Location = new System.Drawing.Point(11, 482);
            this.lblChange.Name = "lblChange";
            this.lblChange.Size = new System.Drawing.Size(130, 24);
            this.lblChange.TabIndex = 28;
            this.lblChange.Text = "Change due:";
            // 
            // btnCalculatechange
            // 
            this.btnCalculatechange.Location = new System.Drawing.Point(382, 395);
            this.btnCalculatechange.Name = "btnCalculatechange";
            this.btnCalculatechange.Size = new System.Drawing.Size(111, 60);
            this.btnCalculatechange.TabIndex = 29;
            this.btnCalculatechange.Text = "Change";
            this.btnCalculatechange.UseVisualStyleBackColor = true;
            this.btnCalculatechange.Click += new System.EventHandler(this.btnCalculatechange_Click);
            // 
            // checkdiscount
            // 
            this.checkdiscount.AutoSize = true;
            this.checkdiscount.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.checkdiscount.Location = new System.Drawing.Point(19, 301);
            this.checkdiscount.Name = "checkdiscount";
            this.checkdiscount.Size = new System.Drawing.Size(116, 28);
            this.checkdiscount.TabIndex = 30;
            this.checkdiscount.Text = "Discount";
            this.checkdiscount.UseVisualStyleBackColor = true;
            this.checkdiscount.CheckedChanged += new System.EventHandler(this.checkdiscount_CheckedChanged);
            // 
            // lbldiscountvalue
            // 
            this.lbldiscountvalue.AutoSize = true;
            this.lbldiscountvalue.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lbldiscountvalue.Location = new System.Drawing.Point(1011, 95);
            this.lbldiscountvalue.Name = "lbldiscountvalue";
            this.lbldiscountvalue.Size = new System.Drawing.Size(166, 24);
            this.lbldiscountvalue.TabIndex = 31;
            this.lbldiscountvalue.Text = "Discount: R 0,00";
            // 
            // txtdiscount
            // 
            this.txtdiscount.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.txtdiscount.Location = new System.Drawing.Point(141, 301);
            this.txtdiscount.Name = "txtdiscount";
            this.txtdiscount.Size = new System.Drawing.Size(228, 30);
            this.txtdiscount.TabIndex = 32;
            this.txtdiscount.Text = "0";
            // 
            // lblDateTime
            // 
            this.lblDateTime.AutoSize = true;
            this.lblDateTime.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblDateTime.Location = new System.Drawing.Point(11, 18);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(123, 24);
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
            this.lblBusineess.Font = new System.Drawing.Font("Arial", 28.2F, System.Drawing.FontStyle.Bold);
            this.lblBusineess.Location = new System.Drawing.Point(651, 4);
            this.lblBusineess.Name = "lblBusineess";
            this.lblBusineess.Size = new System.Drawing.Size(347, 56);
            this.lblBusineess.TabIndex = 34;
            this.lblBusineess.Text = "MOSAKA POS";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(657, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 24);
            this.label2.TabIndex = 35;
            this.label2.Text = "label2";
            // 
            // btnExitSaleform
            // 
            this.btnExitSaleform.Location = new System.Drawing.Point(1666, 12);
            this.btnExitSaleform.Name = "btnExitSaleform";
            this.btnExitSaleform.Size = new System.Drawing.Size(124, 62);
            this.btnExitSaleform.TabIndex = 36;
            this.btnExitSaleform.Text = "Exit";
            this.btnExitSaleform.UseVisualStyleBackColor = true;
            this.btnExitSaleform.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkEmail
            // 
            this.checkEmail.AutoSize = true;
            this.checkEmail.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkEmail.Location = new System.Drawing.Point(570, 951);
            this.checkEmail.Name = "checkEmail";
            this.checkEmail.Size = new System.Drawing.Size(160, 28);
            this.checkEmail.TabIndex = 37;
            this.checkEmail.Text = "Email Receipt";
            this.checkEmail.UseVisualStyleBackColor = true;
            // 
            // txtRecipientEmail
            // 
            this.txtRecipientEmail.Location = new System.Drawing.Point(570, 974);
            this.txtRecipientEmail.Name = "txtRecipientEmail";
            this.txtRecipientEmail.Size = new System.Drawing.Size(254, 22);
            this.txtRecipientEmail.TabIndex = 38;
            // 
            // btnVoidLine
            // 
            this.btnVoidLine.BackColor = System.Drawing.Color.DeepPink;
            this.btnVoidLine.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnVoidLine.ForeColor = System.Drawing.Color.Black;
            this.btnVoidLine.Location = new System.Drawing.Point(1193, 874);
            this.btnVoidLine.Name = "btnVoidLine";
            this.btnVoidLine.Size = new System.Drawing.Size(153, 123);
            this.btnVoidLine.TabIndex = 39;
            this.btnVoidLine.Text = "Void Line";
            this.btnVoidLine.UseVisualStyleBackColor = false;
            // 
            // btnRefund
            // 
            this.btnRefund.BackColor = System.Drawing.Color.Crimson;
            this.btnRefund.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRefund.ForeColor = System.Drawing.Color.Black;
            this.btnRefund.Location = new System.Drawing.Point(843, 597);
            this.btnRefund.Name = "btnRefund";
            this.btnRefund.Size = new System.Drawing.Size(153, 123);
            this.btnRefund.TabIndex = 40;
            this.btnRefund.Text = "Refund";
            this.btnRefund.UseVisualStyleBackColor = false;
            // 
            // btnAirtime
            // 
            this.btnAirtime.BackColor = System.Drawing.Color.AliceBlue;
            this.btnAirtime.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAirtime.ForeColor = System.Drawing.Color.Black;
            this.btnAirtime.Location = new System.Drawing.Point(841, 191);
            this.btnAirtime.Name = "btnAirtime";
            this.btnAirtime.Size = new System.Drawing.Size(155, 123);
            this.btnAirtime.TabIndex = 41;
            this.btnAirtime.Text = "Airtime";
            this.btnAirtime.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.AliceBlue;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(841, 327);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(155, 123);
            this.button1.TabIndex = 42;
            this.button1.Text = "Eskom";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // btnBillsPaytment
            // 
            this.btnBillsPaytment.BackColor = System.Drawing.Color.AliceBlue;
            this.btnBillsPaytment.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBillsPaytment.ForeColor = System.Drawing.Color.Black;
            this.btnBillsPaytment.Location = new System.Drawing.Point(661, 456);
            this.btnBillsPaytment.Name = "btnBillsPaytment";
            this.btnBillsPaytment.Size = new System.Drawing.Size(155, 123);
            this.btnBillsPaytment.TabIndex = 43;
            this.btnBillsPaytment.Text = "Pay Bills";
            this.btnBillsPaytment.UseVisualStyleBackColor = false;
            // 
            // btnCupons
            // 
            this.btnCupons.BackColor = System.Drawing.Color.BurlyWood;
            this.btnCupons.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCupons.ForeColor = System.Drawing.Color.Black;
            this.btnCupons.Location = new System.Drawing.Point(1521, 874);
            this.btnCupons.Name = "btnCupons";
            this.btnCupons.Size = new System.Drawing.Size(153, 123);
            this.btnCupons.TabIndex = 44;
            this.btnCupons.Text = "Apply Coupon/Voucher";
            this.btnCupons.UseVisualStyleBackColor = false;
            // 
            // btnPriceCheck
            // 
            this.btnPriceCheck.BackColor = System.Drawing.Color.MediumPurple;
            this.btnPriceCheck.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPriceCheck.ForeColor = System.Drawing.Color.Black;
            this.btnPriceCheck.Location = new System.Drawing.Point(841, 736);
            this.btnPriceCheck.Name = "btnPriceCheck";
            this.btnPriceCheck.Size = new System.Drawing.Size(153, 123);
            this.btnPriceCheck.TabIndex = 45;
            this.btnPriceCheck.Text = "Price Check";
            this.btnPriceCheck.UseVisualStyleBackColor = false;
            // 
            // btnOpenCashDrawer
            // 
            this.btnOpenCashDrawer.BackColor = System.Drawing.Color.LightCoral;
            this.btnOpenCashDrawer.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOpenCashDrawer.ForeColor = System.Drawing.Color.Black;
            this.btnOpenCashDrawer.Location = new System.Drawing.Point(841, 874);
            this.btnOpenCashDrawer.Name = "btnOpenCashDrawer";
            this.btnOpenCashDrawer.Size = new System.Drawing.Size(153, 123);
            this.btnOpenCashDrawer.TabIndex = 46;
            this.btnOpenCashDrawer.Text = "Open Cash Drawer";
            this.btnOpenCashDrawer.UseVisualStyleBackColor = false;
            this.btnOpenCashDrawer.Click += new System.EventHandler(this.btnOpenCashDrawer_Click);
            // 
            // btnOTT
            // 
            this.btnOTT.BackColor = System.Drawing.Color.AliceBlue;
            this.btnOTT.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOTT.ForeColor = System.Drawing.Color.Black;
            this.btnOTT.Location = new System.Drawing.Point(661, 191);
            this.btnOTT.Name = "btnOTT";
            this.btnOTT.Size = new System.Drawing.Size(155, 123);
            this.btnOTT.TabIndex = 47;
            this.btnOTT.Text = "OTT Voucher";
            this.btnOTT.UseVisualStyleBackColor = false;
            // 
            // btnBetway
            // 
            this.btnBetway.BackColor = System.Drawing.Color.AliceBlue;
            this.btnBetway.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBetway.ForeColor = System.Drawing.Color.Black;
            this.btnBetway.Location = new System.Drawing.Point(661, 327);
            this.btnBetway.Name = "btnBetway";
            this.btnBetway.Size = new System.Drawing.Size(155, 123);
            this.btnBetway.TabIndex = 48;
            this.btnBetway.Text = "Betway";
            this.btnBetway.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.LightSlateGray;
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Location = new System.Drawing.Point(841, 456);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(155, 123);
            this.button2.TabIndex = 49;
            this.button2.Text = "CashBack";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.Window;
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flowLayoutPanel1.Controls.Add(this.btn1);
            this.flowLayoutPanel1.Controls.Add(this.btn2);
            this.flowLayoutPanel1.Controls.Add(this.btn3);
            this.flowLayoutPanel1.Controls.Add(this.btn4);
            this.flowLayoutPanel1.Controls.Add(this.btn5);
            this.flowLayoutPanel1.Controls.Add(this.btn6);
            this.flowLayoutPanel1.Controls.Add(this.btn7);
            this.flowLayoutPanel1.Controls.Add(this.btn8);
            this.flowLayoutPanel1.Controls.Add(this.btn9);
            this.flowLayoutPanel1.Controls.Add(this.btn0);
            this.flowLayoutPanel1.Controls.Add(this.btn00);
            this.flowLayoutPanel1.Controls.Add(this.btnDel);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(5, 522);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(487, 492);
            this.flowLayoutPanel1.TabIndex = 50;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Black", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(284, 40);
            this.label4.TabIndex = 51;
            this.label4.Text = "Select Category :";
            // 
            // comboProductCategory
            // 
            this.comboProductCategory.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.comboProductCategory.FormattingEnabled = true;
            this.comboProductCategory.Location = new System.Drawing.Point(13, 150);
            this.comboProductCategory.Name = "comboProductCategory";
            this.comboProductCategory.Size = new System.Drawing.Size(350, 26);
            this.comboProductCategory.TabIndex = 52;
            this.comboProductCategory.SelectedIndexChanged += new System.EventHandler(this.comboProductCategory_SelectedIndexChanged);
            // 
            // btnAddProduct
            // 
            this.btnAddProduct.BackColor = System.Drawing.Color.AliceBlue;
            this.btnAddProduct.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddProduct.ForeColor = System.Drawing.Color.Black;
            this.btnAddProduct.Location = new System.Drawing.Point(382, 206);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Size = new System.Drawing.Size(155, 123);
            this.btnAddProduct.TabIndex = 53;
            this.btnAddProduct.Text = "ADD";
            this.btnAddProduct.UseVisualStyleBackColor = false;
            this.btnAddProduct.Click += new System.EventHandler(this.btnAddProduct_Click);
            // 
            // btnReprint
            // 
            this.btnReprint.BackColor = System.Drawing.Color.BurlyWood;
            this.btnReprint.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReprint.ForeColor = System.Drawing.Color.Black;
            this.btnReprint.Location = new System.Drawing.Point(1706, 873);
            this.btnReprint.Name = "btnReprint";
            this.btnReprint.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnReprint.Size = new System.Drawing.Size(153, 123);
            this.btnReprint.TabIndex = 54;
            this.btnReprint.Text = "Reprint Receipt";
            this.btnReprint.UseVisualStyleBackColor = false;
            this.btnReprint.Click += new System.EventHandler(this.btnReprint_Click);
            // 
            // frmSales
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1871, 1053);
            this.Controls.Add(this.btnReprint);
            this.Controls.Add(this.btnAddProduct);
            this.Controls.Add(this.comboProductCategory);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnBetway);
            this.Controls.Add(this.btnOTT);
            this.Controls.Add(this.btnOpenCashDrawer);
            this.Controls.Add(this.btnPriceCheck);
            this.Controls.Add(this.btnCupons);
            this.Controls.Add(this.btnBillsPaytment);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnAirtime);
            this.Controls.Add(this.btnRefund);
            this.Controls.Add(this.btnVoidLine);
            this.Controls.Add(this.txtRecipientEmail);
            this.Controls.Add(this.checkEmail);
            this.Controls.Add(this.btnExitSaleform);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblBusineess);
            this.Controls.Add(this.lblDateTime);
            this.Controls.Add(this.txtdiscount);
            this.Controls.Add(this.lbldiscountvalue);
            this.Controls.Add(this.checkdiscount);
            this.Controls.Add(this.btnCalculatechange);
            this.Controls.Add(this.lblChange);
            this.Controls.Add(this.txtrenderedamount);
            this.Controls.Add(this.lblrendered);
            this.Controls.Add(this.combopaymentmentod);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.lblSubtotalorder);
            this.Controls.Add(this.lblTax);
            this.Controls.Add(this.btnCompleteSale);
            this.Controls.Add(this.lblTotalAmount);
            this.Controls.Add(this.dataGridViewSaleDetails);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.lblScanPruduct);
            this.Controls.Add(this.lblNextSalesNumber);
            this.Controls.Add(this.comboBoxProducts);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSales";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form2";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmSales_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSaleDetails)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxProducts;
        private System.Windows.Forms.Label lblNextSalesNumber;
        private System.Windows.Forms.Label lblScanPruduct;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridViewSaleDetails;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Button btnCompleteSale;
        private System.Windows.Forms.Label lblTax;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.Button btn1;
        private System.Windows.Forms.Button btn2;
        private System.Windows.Forms.Button btn3;
        private System.Windows.Forms.Button btn4;
        private System.Windows.Forms.Button btn5;
        private System.Windows.Forms.Button btn6;
        private System.Windows.Forms.Button btn7;
        private System.Windows.Forms.Button btn8;
        private System.Windows.Forms.Button btn9;
        private System.Windows.Forms.Button btn00;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btn0;
        private System.Windows.Forms.Label lblSubtotalorder;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox combopaymentmentod;
        private System.Windows.Forms.Label lblrendered;
        private System.Windows.Forms.TextBox txtrenderedamount;
        private System.Windows.Forms.Label lblChange;
        private System.Windows.Forms.Button btnCalculatechange;
        private System.Windows.Forms.CheckBox checkdiscount;
        private System.Windows.Forms.Label lbldiscountvalue;
        private System.Windows.Forms.TextBox txtdiscount;
        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblBusineess;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnExitSaleform;
        private System.Windows.Forms.CheckBox checkEmail;
        private System.Windows.Forms.TextBox txtRecipientEmail;
        private System.Windows.Forms.Button btnVoidLine;
        private System.Windows.Forms.Button btnRefund;
        private System.Windows.Forms.Button btnAirtime;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnBillsPaytment;
        private System.Windows.Forms.Button btnCupons;
        private System.Windows.Forms.Button btnPriceCheck;
        private System.Windows.Forms.Button btnOpenCashDrawer;
        private System.Windows.Forms.Button btnOTT;
        private System.Windows.Forms.Button btnBetway;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboProductCategory;
        private System.Windows.Forms.Button btnAddProduct;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn Subtotal;
        private System.Windows.Forms.Button btnReprint;
    }
}