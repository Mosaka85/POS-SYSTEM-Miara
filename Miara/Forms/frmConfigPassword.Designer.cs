
namespace Miara
{
    partial class frmConfigPassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConfigPassword));
            this.lblConfigPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnEnterConfig = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblConfigPassword
            // 
            this.lblConfigPassword.AutoSize = true;
            this.lblConfigPassword.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold);
            this.lblConfigPassword.Location = new System.Drawing.Point(192, 35);
            this.lblConfigPassword.Name = "lblConfigPassword";
            this.lblConfigPassword.Size = new System.Drawing.Size(99, 23);
            this.lblConfigPassword.TabIndex = 0;
            this.lblConfigPassword.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.SystemColors.Control;
            this.txtPassword.Location = new System.Drawing.Point(136, 61);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.Size = new System.Drawing.Size(217, 22);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // btnEnterConfig
            // 
            this.btnEnterConfig.BackColor = System.Drawing.Color.LightCoral;
            this.btnEnterConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnterConfig.Location = new System.Drawing.Point(196, 89);
            this.btnEnterConfig.Name = "btnEnterConfig";
            this.btnEnterConfig.Size = new System.Drawing.Size(81, 28);
            this.btnEnterConfig.TabIndex = 2;
            this.btnEnterConfig.Text = "Enter";
            this.btnEnterConfig.UseVisualStyleBackColor = false;
            this.btnEnterConfig.Click += new System.EventHandler(this.btnEnterConfig_Click);
            // 
            // frmConfigPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MistyRose;
            this.ClientSize = new System.Drawing.Size(499, 143);
            this.Controls.Add(this.btnEnterConfig);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblConfigPassword);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmConfigPassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuration Password";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblConfigPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnEnterConfig;
    }
}