namespace Miara.Forms
{
    partial class frmCashback
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
            this.button1 = new System.Windows.Forms.Button();
            this.lblTransactionID = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.lblAmount = new System.Windows.Forms.Label();
            this.btnCashback = new System.Windows.Forms.Button();
            this.lblSaleID = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelForm = new System.Windows.Forms.Panel();
            this.panelHeader.SuspendLayout();
            this.panelForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.button1.Location = new System.Drawing.Point(974, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(46, 40);
            this.button1.TabIndex = 0;
            this.button1.Text = "✕";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // lblTransactionID
            // 
            this.lblTransactionID.AutoSize = true;
            this.lblTransactionID.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransactionID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(116)))), ((int)(((byte)(166)))));
            this.lblTransactionID.Location = new System.Drawing.Point(41, 20);
            this.lblTransactionID.Name = "lblTransactionID";
            this.lblTransactionID.Size = new System.Drawing.Size(137, 23);
            this.lblTransactionID.TabIndex = 1;
            this.lblTransactionID.Text = "TransactionID:";
            // 
            // txtAmount
            // 
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmount.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Location = new System.Drawing.Point(208, 103);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(337, 30);
            this.txtAmount.TabIndex = 2;
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(116)))), ((int)(((byte)(166)))));
            this.lblAmount.Location = new System.Drawing.Point(41, 103);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(82, 23);
            this.lblAmount.TabIndex = 3;
            this.lblAmount.Text = "Amount:";
            // 
            // btnCashback
            // 
            this.btnCashback.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(116)))), ((int)(((byte)(166)))));
            this.btnCashback.FlatAppearance.BorderSize = 0;
            this.btnCashback.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCashback.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCashback.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnCashback.Location = new System.Drawing.Point(647, 97);
            this.btnCashback.Name = "btnCashback";
            this.btnCashback.Size = new System.Drawing.Size(163, 40);
            this.btnCashback.TabIndex = 4;
            this.btnCashback.Text = "Confirm";
            this.btnCashback.UseVisualStyleBackColor = false;
            this.btnCashback.Click += new System.EventHandler(this.btnCashback_Click);
            // 
            // lblSaleID
            // 
            this.lblSaleID.AutoSize = true;
            this.lblSaleID.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSaleID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(116)))), ((int)(((byte)(166)))));
            this.lblSaleID.Location = new System.Drawing.Point(41, 61);
            this.lblSaleID.Name = "lblSaleID";
            this.lblSaleID.Size = new System.Drawing.Size(75, 23);
            this.lblSaleID.TabIndex = 5;
            this.lblSaleID.Text = "SaleID:";
            this.lblSaleID.Click += new System.EventHandler(this.label3_Click);
            // 
            // txtNotes
            // 
            this.txtNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNotes.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotes.Location = new System.Drawing.Point(208, 150);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(602, 98);
            this.txtNotes.TabIndex = 6;
            this.txtNotes.Text = "Cashback Request by Customer";
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(116)))), ((int)(((byte)(166)))));
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Controls.Add(this.button1);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1032, 65);
            this.panelHeader.TabIndex = 7;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Arial", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.lblTitle.Location = new System.Drawing.Point(22, 17);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(148, 33);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Cashback";
            // 
            // panelForm
            // 
            this.panelForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.panelForm.Controls.Add(this.lblTransactionID);
            this.panelForm.Controls.Add(this.txtAmount);
            this.panelForm.Controls.Add(this.txtNotes);
            this.panelForm.Controls.Add(this.lblAmount);
            this.panelForm.Controls.Add(this.lblSaleID);
            this.panelForm.Controls.Add(this.btnCashback);
            this.panelForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelForm.Location = new System.Drawing.Point(0, 65);
            this.panelForm.Name = "panelForm";
            this.panelForm.Padding = new System.Windows.Forms.Padding(20);
            this.panelForm.Size = new System.Drawing.Size(1032, 293);
            this.panelForm.TabIndex = 8;
            // 
            // frmCashback
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1032, 358);
            this.Controls.Add(this.panelForm);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmCashback";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cashback";
            this.Load += new System.EventHandler(this.frmCashback_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelForm.ResumeLayout(false);
            this.panelForm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblTransactionID;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Button btnCashback;
        private System.Windows.Forms.Label lblSaleID;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelForm;
    }
}