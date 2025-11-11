namespace Miara
{
    partial class frmResetPassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmResetPassword));
            this.label1 = new System.Windows.Forms.Label();
            this.btnSendOtp = new System.Windows.Forms.Button();
            this.lblEmailsent = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.btnSaveupdate = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtOTPEmail = new System.Windows.Forms.TextBox();
            this.panelForm = new System.Windows.Forms.Panel();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.panelForm.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(116)))), ((int)(((byte)(166)))));
            this.label1.Location = new System.Drawing.Point(32, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Username";
            // 
            // btnSendOtp
            // 
            this.btnSendOtp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(116)))), ((int)(((byte)(166)))));
            this.btnSendOtp.FlatAppearance.BorderSize = 0;
            this.btnSendOtp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSendOtp.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendOtp.ForeColor = System.Drawing.Color.White;
            this.btnSendOtp.Location = new System.Drawing.Point(350, 51);
            this.btnSendOtp.Name = "btnSendOtp";
            this.btnSendOtp.Size = new System.Drawing.Size(100, 30);
            this.btnSendOtp.TabIndex = 2;
            this.btnSendOtp.Text = "Send OTP";
            this.btnSendOtp.UseVisualStyleBackColor = false;
            this.btnSendOtp.Click += new System.EventHandler(this.btnSendOtp_Click);
            // 
            // lblEmailsent
            // 
            this.lblEmailsent.AutoSize = true;
            this.lblEmailsent.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmailsent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(116)))), ((int)(((byte)(166)))));
            this.lblEmailsent.Location = new System.Drawing.Point(32, 96);
            this.lblEmailsent.Name = "lblEmailsent";
            this.lblEmailsent.Size = new System.Drawing.Size(110, 17);
            this.lblEmailsent.TabIndex = 3;
            this.lblEmailsent.Text = "OTP sent to email";
            this.lblEmailsent.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(116)))), ((int)(((byte)(166)))));
            this.label3.Location = new System.Drawing.Point(32, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 23);
            this.label3.TabIndex = 6;
            this.label3.Text = "Password";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(116)))), ((int)(((byte)(166)))));
            this.label4.Location = new System.Drawing.Point(32, 220);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(146, 23);
            this.label4.TabIndex = 7;
            this.label4.Text = "Confirm Password";
            // 
            // txtUsername
            // 
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.Location = new System.Drawing.Point(36, 51);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(308, 30);
            this.txtUsername.TabIndex = 8;
            this.txtUsername.TextChanged += new System.EventHandler(this.txtUsername_TextChanged);
            // 
            // txtPassword
            // 
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(36, 176);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.Size = new System.Drawing.Size(414, 30);
            this.txtPassword.TabIndex = 9;
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConfirmPassword.Location = new System.Drawing.Point(36, 246);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.PasswordChar = '●';
            this.txtConfirmPassword.Size = new System.Drawing.Size(414, 30);
            this.txtConfirmPassword.TabIndex = 10;
            // 
            // btnSaveupdate
            // 
            this.btnSaveupdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(116)))), ((int)(((byte)(166)))));
            this.btnSaveupdate.FlatAppearance.BorderSize = 0;
            this.btnSaveupdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveupdate.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveupdate.ForeColor = System.Drawing.Color.White;
            this.btnSaveupdate.Location = new System.Drawing.Point(350, 342);
            this.btnSaveupdate.Name = "btnSaveupdate";
            this.btnSaveupdate.Size = new System.Drawing.Size(100, 40);
            this.btnSaveupdate.TabIndex = 11;
            this.btnSaveupdate.Text = "SAVE";
            this.btnSaveupdate.UseVisualStyleBackColor = false;
            this.btnSaveupdate.Click += new System.EventHandler(this.btnSaveupdate_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(116)))), ((int)(((byte)(166)))));
            this.label5.Location = new System.Drawing.Point(32, 280);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 23);
            this.label5.TabIndex = 12;
            this.label5.Text = "Enter OTP";
            // 
            // txtOTPEmail
            // 
            this.txtOTPEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOTPEmail.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOTPEmail.Location = new System.Drawing.Point(36, 306);
            this.txtOTPEmail.Name = "txtOTPEmail";
            this.txtOTPEmail.Size = new System.Drawing.Size(414, 30);
            this.txtOTPEmail.TabIndex = 13;
            // 
            // panelForm
            // 
            this.panelForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.panelForm.Controls.Add(this.btnSaveupdate);
            this.panelForm.Controls.Add(this.btnExit);
            this.panelForm.Controls.Add(this.label1);
            this.panelForm.Controls.Add(this.txtUsername);
            this.panelForm.Controls.Add(this.btnSendOtp);
            this.panelForm.Controls.Add(this.txtOTPEmail);
            this.panelForm.Controls.Add(this.lblEmailsent);
            this.panelForm.Controls.Add(this.label5);
            this.panelForm.Controls.Add(this.label3);
            this.panelForm.Controls.Add(this.txtConfirmPassword);
            this.panelForm.Controls.Add(this.txtPassword);
            this.panelForm.Controls.Add(this.label4);
            this.panelForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelForm.Location = new System.Drawing.Point(0, 60);
            this.panelForm.Name = "panelForm";
            this.panelForm.Padding = new System.Windows.Forms.Padding(30);
            this.panelForm.Size = new System.Drawing.Size(579, 437);
            this.panelForm.TabIndex = 16;
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(116)))), ((int)(((byte)(166)))));
            this.panelHeader.Controls.Add(this.labelTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(579, 60);
            this.panelHeader.TabIndex = 17;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Location = new System.Drawing.Point(30, 15);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(173, 31);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Reset Password";
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.White;
            this.btnExit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(116)))), ((int)(((byte)(166)))));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(116)))), ((int)(((byte)(166)))));
            this.btnExit.Location = new System.Drawing.Point(217, 342);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(100, 40);
            this.btnExit.TabIndex = 15;
            this.btnExit.Text = "Back";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // frmResetPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(579, 497);
            this.Controls.Add(this.panelForm);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmResetPassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reset Password";
            this.Load += new System.EventHandler(this.frmResetPassword_Load);
            this.panelForm.ResumeLayout(false);
            this.panelForm.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSendOtp;
        private System.Windows.Forms.Label lblEmailsent;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.Button btnSaveupdate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtOTPEmail;
        private System.Windows.Forms.Panel panelForm;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Button btnExit;
    }
}