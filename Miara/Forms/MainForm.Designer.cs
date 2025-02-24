
namespace Miara
{
    partial class frmMainForm
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
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCategoryCatalog = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(27, 129);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(359, 85);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start New Sale";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(27, 330);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(359, 85);
            this.button2.TabIndex = 1;
            this.button2.Text = "View Reports";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(27, 519);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(359, 79);
            this.button3.TabIndex = 2;
            this.button3.Text = "Sales Summary";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(27, 719);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(359, 79);
            this.button4.TabIndex = 3;
            this.button4.Text = "Product Catalog";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(1147, 404);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(359, 75);
            this.button5.TabIndex = 4;
            this.button5.Text = "Refunds/Returns";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(1147, 221);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(359, 92);
            this.button6.TabIndex = 5;
            this.button6.Text = "Employee Maintenance";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(1147, 741);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(359, 84);
            this.button7.TabIndex = 6;
            this.button7.Text = "Logout";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(1147, 61);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(359, 92);
            this.button8.TabIndex = 7;
            this.button8.Text = "End Of Day (Close Registrar)";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(685, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "label1";
            // 
            // btnCategoryCatalog
            // 
            this.btnCategoryCatalog.Location = new System.Drawing.Point(1147, 545);
            this.btnCategoryCatalog.Name = "btnCategoryCatalog";
            this.btnCategoryCatalog.Size = new System.Drawing.Size(359, 79);
            this.btnCategoryCatalog.TabIndex = 9;
            this.btnCategoryCatalog.Text = "Category Catalog";
            this.btnCategoryCatalog.UseVisualStyleBackColor = true;
            this.btnCategoryCatalog.Click += new System.EventHandler(this.btnCategoryCatalog_Click);
            // 
            // frmMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1559, 872);
            this.Controls.Add(this.btnCategoryCatalog);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "frmMainForm";
            this.Text = " ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCategoryCatalog;
    }
}