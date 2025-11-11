namespace Miara.Forms
{
    partial class frmPrinter
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvPrinters;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbPrinterTypes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnPrintTestPage;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvPrinters = new System.Windows.Forms.DataGridView();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbPrinterTypes = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnPrintTestPage = new System.Windows.Forms.Button();
            this.lblPrintSent = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrinters)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPrinters
            // 
            this.dgvPrinters.AllowUserToAddRows = false;
            this.dgvPrinters.AllowUserToDeleteRows = false;
            this.dgvPrinters.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPrinters.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.dgvPrinters.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPrinters.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(116)))), ((int)(((byte)(166)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPrinters.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPrinters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(116)))), ((int)(((byte)(166)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPrinters.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPrinters.EnableHeadersVisualStyles = false;
            this.dgvPrinters.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(116)))), ((int)(((byte)(166)))));
            this.dgvPrinters.Location = new System.Drawing.Point(24, 80);
            this.dgvPrinters.Name = "dgvPrinters";
            this.dgvPrinters.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(116)))), ((int)(((byte)(166)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPrinters.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPrinters.RowHeadersWidth = 51;
            this.dgvPrinters.RowTemplate.Height = 28;
            this.dgvPrinters.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPrinters.Size = new System.Drawing.Size(1075, 350);
            this.dgvPrinters.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(116)))), ((int)(((byte)(166)))));
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(372, 24);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(140, 36);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search Printers";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(116)))), ((int)(((byte)(166)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(959, 481);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(140, 36);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save Selection";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(116)))), ((int)(((byte)(166)))));
            this.lblStatus.Location = new System.Drawing.Point(24, 450);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(50, 20);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Ready";
            // 
            // cmbPrinterTypes
            // 
            this.cmbPrinterTypes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.cmbPrinterTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrinterTypes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbPrinterTypes.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbPrinterTypes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(116)))), ((int)(((byte)(166)))));
            this.cmbPrinterTypes.Items.AddRange(new object[] {
            "All Printers",
            "Local Printers",
            "Network Printers"});
            this.cmbPrinterTypes.Location = new System.Drawing.Point(126, 24);
            this.cmbPrinterTypes.Name = "cmbPrinterTypes";
            this.cmbPrinterTypes.Size = new System.Drawing.Size(200, 28);
            this.cmbPrinterTypes.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(116)))), ((int)(((byte)(166)))));
            this.label1.Location = new System.Drawing.Point(24, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Printer Types:";
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(116)))), ((int)(((byte)(166)))));
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(970, 18);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(140, 36);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            // 
            // btnPrintTestPage
            // 
            this.btnPrintTestPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(116)))), ((int)(((byte)(166)))));
            this.btnPrintTestPage.FlatAppearance.BorderSize = 0;
            this.btnPrintTestPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintTestPage.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnPrintTestPage.ForeColor = System.Drawing.Color.White;
            this.btnPrintTestPage.Location = new System.Drawing.Point(797, 481);
            this.btnPrintTestPage.Name = "btnPrintTestPage";
            this.btnPrintTestPage.Size = new System.Drawing.Size(140, 36);
            this.btnPrintTestPage.TabIndex = 7;
            this.btnPrintTestPage.Text = "Print Test Page";
            this.btnPrintTestPage.UseVisualStyleBackColor = false;
            this.btnPrintTestPage.Click += new System.EventHandler(this.btnPrintTestPage_Click);
            // 
            // lblPrintSent
            // 
            this.lblPrintSent.AutoSize = true;
            this.lblPrintSent.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPrintSent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(116)))), ((int)(((byte)(166)))));
            this.lblPrintSent.Location = new System.Drawing.Point(24, 470);
            this.lblPrintSent.Name = "lblPrintSent";
            this.lblPrintSent.Size = new System.Drawing.Size(60, 20);
            this.lblPrintSent.TabIndex = 8;
            this.lblPrintSent.Text = "Printing";
            this.lblPrintSent.Visible = false;
            // 
            // frmPrinter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1122, 529);
            this.Controls.Add(this.lblPrintSent);
            this.Controls.Add(this.btnPrintTestPage);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbPrinterTypes);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dgvPrinters);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmPrinter";
            this.Text = "Printer Setup";
            this.Load += new System.EventHandler(this.frmPrinter_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrinters)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblPrintSent;
    }
}