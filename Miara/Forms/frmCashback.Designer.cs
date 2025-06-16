
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
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(909, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 47);
            this.button1.TabIndex = 0;
            this.button1.Text = "x";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // lblTransactionID
            // 
            this.lblTransactionID.AutoSize = true;
            this.lblTransactionID.Font = new System.Drawing.Font("Arial Black", 16.2F, System.Drawing.FontStyle.Bold);
            this.lblTransactionID.Location = new System.Drawing.Point(23, 39);
            this.lblTransactionID.Name = "lblTransactionID";
            this.lblTransactionID.Size = new System.Drawing.Size(260, 40);
            this.lblTransactionID.TabIndex = 1;
            this.lblTransactionID.Text = "TransactionID  :";
            // 
            // txtAmount
            // 
            this.txtAmount.Font = new System.Drawing.Font("Arial Black", 16.2F, System.Drawing.FontStyle.Bold);
            this.txtAmount.Location = new System.Drawing.Point(208, 151);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(378, 46);
            this.txtAmount.TabIndex = 2;
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Font = new System.Drawing.Font("Arial Black", 16.2F, System.Drawing.FontStyle.Bold);
            this.lblAmount.Location = new System.Drawing.Point(34, 151);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(145, 40);
            this.lblAmount.TabIndex = 3;
            this.lblAmount.Text = "Amount:";
            // 
            // btnCashback
            // 
            this.btnCashback.Location = new System.Drawing.Point(647, 148);
            this.btnCashback.Name = "btnCashback";
            this.btnCashback.Size = new System.Drawing.Size(163, 56);
            this.btnCashback.TabIndex = 4;
            this.btnCashback.Text = "Confirm";
            this.btnCashback.UseVisualStyleBackColor = true;
            this.btnCashback.Click += new System.EventHandler(this.btnCashback_Click);
            // 
            // lblSaleID
            // 
            this.lblSaleID.AutoSize = true;
            this.lblSaleID.Font = new System.Drawing.Font("Arial Black", 16.2F, System.Drawing.FontStyle.Bold);
            this.lblSaleID.Location = new System.Drawing.Point(34, 98);
            this.lblSaleID.Name = "lblSaleID";
            this.lblSaleID.Size = new System.Drawing.Size(135, 40);
            this.lblSaleID.TabIndex = 5;
            this.lblSaleID.Text = "SaleID :";
            this.lblSaleID.Click += new System.EventHandler(this.label3_Click);
            // 
            // txtNotes
            // 
            this.txtNotes.Font = new System.Drawing.Font("Arial Black", 12.2F, System.Drawing.FontStyle.Bold);
            this.txtNotes.Location = new System.Drawing.Point(208, 227);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(602, 98);
            this.txtNotes.TabIndex = 6;
            this.txtNotes.Text = "Cashback Request by Customer";
            // 
            // frmCashback
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1032, 358);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.lblSaleID);
            this.Controls.Add(this.btnCashback);
            this.Controls.Add(this.lblAmount);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.lblTransactionID);
            this.Controls.Add(this.button1);
            this.Name = "frmCashback";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmCashback_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblTransactionID;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Button btnCashback;
        private System.Windows.Forms.Label lblSaleID;
        private System.Windows.Forms.TextBox txtNotes;
    }
}