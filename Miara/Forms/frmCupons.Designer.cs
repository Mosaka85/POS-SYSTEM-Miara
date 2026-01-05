using System.Windows.Forms;
using System.Drawing;

namespace Miara.Forms
{
    partial class frmCupons
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.txtVoucher = new System.Windows.Forms.TextBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.lblInstruction = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblSessionID = new System.Windows.Forms.Label();
            this.lblEMID = new System.Windows.Forms.Label();
            this.panelCard = new System.Windows.Forms.Panel();
            this.panelCard.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtVoucher
            // 
            this.txtVoucher.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtVoucher.Location = new System.Drawing.Point(25, 65);
            this.txtVoucher.Name = "txtVoucher";
            this.txtVoucher.Size = new System.Drawing.Size(551, 32);
            this.txtVoucher.TabIndex = 1;
            // 
            // btnApply
            // 
            this.btnApply.BackColor = System.Drawing.Color.Crimson;
            this.btnApply.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnApply.FlatAppearance.BorderSize = 0;
            this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApply.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnApply.ForeColor = System.Drawing.Color.White;
            this.btnApply.Location = new System.Drawing.Point(598, 60);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(120, 40);
            this.btnApply.TabIndex = 2;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = false;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // lblInstruction
            // 
            this.lblInstruction.AutoSize = true;
            this.lblInstruction.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblInstruction.ForeColor = System.Drawing.Color.White;
            this.lblInstruction.Location = new System.Drawing.Point(20, 25);
            this.lblInstruction.Name = "lblInstruction";
            this.lblInstruction.Size = new System.Drawing.Size(293, 28);
            this.lblInstruction.TabIndex = 0;
            this.lblInstruction.Text = "Enter voucher or coupon code:";
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblResult.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblResult.Location = new System.Drawing.Point(20, 105);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(0, 25);
            this.lblResult.TabIndex = 3;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTotal.ForeColor = System.Drawing.Color.DimGray;
            this.lblTotal.Location = new System.Drawing.Point(20, 180);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(67, 23);
            this.lblTotal.TabIndex = 1;
            this.lblTotal.Text = "SaleID: ";
            // 
            // lblSessionID
            // 
            this.lblSessionID.AutoSize = true;
            this.lblSessionID.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSessionID.ForeColor = System.Drawing.Color.DimGray;
            this.lblSessionID.Location = new System.Drawing.Point(200, 180);
            this.lblSessionID.Name = "lblSessionID";
            this.lblSessionID.Size = new System.Drawing.Size(92, 23);
            this.lblSessionID.TabIndex = 2;
            this.lblSessionID.Text = "SessionID: ";
            // 
            // lblEMID
            // 
            this.lblEMID.AutoSize = true;
            this.lblEMID.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEMID.ForeColor = System.Drawing.Color.DimGray;
            this.lblEMID.Location = new System.Drawing.Point(615, 180);
            this.lblEMID.Name = "lblEMID";
            this.lblEMID.Size = new System.Drawing.Size(60, 23);
            this.lblEMID.TabIndex = 3;
            this.lblEMID.Text = "EMID: ";
            // 
            // panelCard
            // 
            this.panelCard.BackColor = System.Drawing.Color.Black;
            this.panelCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCard.Controls.Add(this.lblInstruction);
            this.panelCard.Controls.Add(this.txtVoucher);
            this.panelCard.Controls.Add(this.btnApply);
            this.panelCard.Controls.Add(this.lblResult);
            this.panelCard.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.panelCard.Location = new System.Drawing.Point(20, 20);
            this.panelCard.Name = "panelCard";
            this.panelCard.Padding = new System.Windows.Forms.Padding(15);
            this.panelCard.Size = new System.Drawing.Size(756, 150);
            this.panelCard.TabIndex = 0;
            // 
            // frmCupons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(812, 220);
            this.Controls.Add(this.panelCard);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblSessionID);
            this.Controls.Add(this.lblEMID);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmCupons";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Coupon Redemption";
            this.Load += new System.EventHandler(this.frmCupons_Load);
            this.panelCard.ResumeLayout(false);
            this.panelCard.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox txtVoucher;
        private Button btnApply;
        private Label lblInstruction;
        private Label lblResult;
        private Label lblTotal;
        private Label lblSessionID;
        private Label lblEMID;
        private Panel panelCard;
    }
}
