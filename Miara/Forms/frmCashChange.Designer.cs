namespace Miara.Forms
{
    partial class frmCashChange
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
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblRendered;
        private System.Windows.Forms.TextBox txtRendered;
        private System.Windows.Forms.Label lblChange;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.Button btnConfirm;

        private void InitializeComponent()
        {
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblRendered = new System.Windows.Forms.Label();
            this.txtRendered = new System.Windows.Forms.TextBox();
            this.lblChange = new System.Windows.Forms.Label();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTotal.Location = new System.Drawing.Point(40, 40);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(156, 32);
            this.lblTotal.TabIndex = 5;
            this.lblTotal.Text = "Total: R 0.00";
            // 
            // lblRendered
            // 
            this.lblRendered.AutoSize = true;
            this.lblRendered.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblRendered.Location = new System.Drawing.Point(40, 110);
            this.lblRendered.Name = "lblRendered";
            this.lblRendered.Size = new System.Drawing.Size(164, 25);
            this.lblRendered.TabIndex = 4;
            this.lblRendered.Text = "Amount Rendered";
            // 
            // txtRendered
            // 
            this.txtRendered.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtRendered.Location = new System.Drawing.Point(45, 140);
            this.txtRendered.Name = "txtRendered";
            this.txtRendered.Size = new System.Drawing.Size(402, 34);
            this.txtRendered.TabIndex = 3;
            // 
            // lblChange
            // 
            this.lblChange.AutoSize = true;
            this.lblChange.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblChange.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblChange.Location = new System.Drawing.Point(40, 210);
            this.lblChange.Name = "lblChange";
            this.lblChange.Size = new System.Drawing.Size(185, 32);
            this.lblChange.TabIndex = 2;
            this.lblChange.Text = "Change: R 0.00";
            // 
            // btnCalculate
            // 
            this.btnCalculate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCalculate.Location = new System.Drawing.Point(45, 270);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(120, 45);
            this.btnCalculate.TabIndex = 1;
            this.btnCalculate.Text = "Calculate";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnConfirm.Location = new System.Drawing.Point(175, 270);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(120, 45);
            this.btnConfirm.TabIndex = 0;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // frmCashChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 360);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.lblChange);
            this.Controls.Add(this.txtRendered);
            this.Controls.Add(this.lblRendered);
            this.Controls.Add(this.lblTotal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmCashChange";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cash Payment";
            this.Load += new System.EventHandler(this.frmCashChange_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion
    }
}