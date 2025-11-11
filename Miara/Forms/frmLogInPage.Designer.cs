
namespace Miara
{
    partial class frmLogInPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogInPage));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sQLConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printerSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generalSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.txtEmployeePassword = new System.Windows.Forms.TextBox();
            this.txtEmployeeUsername = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.btnFogortPassword = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lblDevice = new System.Windows.Forms.Label();
            this.lblPrinter = new System.Windows.Forms.Label();
            this.lblSessionID = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.BackColor = System.Drawing.Color.LightGray;
            this.contextMenuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configurationToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStrip1.Size = new System.Drawing.Size(174, 52);
            // 
            // configurationToolStripMenuItem
            // 
            this.configurationToolStripMenuItem.CheckOnClick = true;
            this.configurationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sQLConnectionToolStripMenuItem,
            this.databaseSetupToolStripMenuItem,
            this.printerSetupToolStripMenuItem,
            this.generalSettingsToolStripMenuItem});
            this.configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            this.configurationToolStripMenuItem.Size = new System.Drawing.Size(173, 24);
            this.configurationToolStripMenuItem.Text = "configuration";
            // 
            // sQLConnectionToolStripMenuItem
            // 
            this.sQLConnectionToolStripMenuItem.Name = "sQLConnectionToolStripMenuItem";
            this.sQLConnectionToolStripMenuItem.Size = new System.Drawing.Size(207, 26);
            this.sQLConnectionToolStripMenuItem.Text = "SQL Connection";
            this.sQLConnectionToolStripMenuItem.Click += new System.EventHandler(this.sQLConnectionToolStripMenuItem_Click);
            // 
            // databaseSetupToolStripMenuItem
            // 
            this.databaseSetupToolStripMenuItem.Name = "databaseSetupToolStripMenuItem";
            this.databaseSetupToolStripMenuItem.Size = new System.Drawing.Size(207, 26);
            this.databaseSetupToolStripMenuItem.Text = "Database Setup";
            this.databaseSetupToolStripMenuItem.Click += new System.EventHandler(this.databaseSetupToolStripMenuItem_Click);
            // 
            // printerSetupToolStripMenuItem
            // 
            this.printerSetupToolStripMenuItem.Name = "printerSetupToolStripMenuItem";
            this.printerSetupToolStripMenuItem.Size = new System.Drawing.Size(207, 26);
            this.printerSetupToolStripMenuItem.Text = "Printer Setup";
            this.printerSetupToolStripMenuItem.Click += new System.EventHandler(this.printerSetupToolStripMenuItem_Click);
            // 
            // generalSettingsToolStripMenuItem
            // 
            this.generalSettingsToolStripMenuItem.Name = "generalSettingsToolStripMenuItem";
            this.generalSettingsToolStripMenuItem.Size = new System.Drawing.Size(207, 26);
            this.generalSettingsToolStripMenuItem.Text = "General Settings";
            this.generalSettingsToolStripMenuItem.Click += new System.EventHandler(this.generalSettingsToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(173, 24);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(116)))), ((int)(((byte)(166)))));
            this.button1.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(355, 296);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 31);
            this.button1.TabIndex = 1;
            this.button1.Text = "LogIn";
            this.toolTip1.SetToolTip(this.button1, "Click here to login into\r\nMIARA POS");
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtEmployeePassword
            // 
            this.txtEmployeePassword.BackColor = System.Drawing.SystemColors.Control;
            this.txtEmployeePassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmployeePassword.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold);
            this.txtEmployeePassword.Location = new System.Drawing.Point(44, 3);
            this.txtEmployeePassword.MaxLength = 50;
            this.txtEmployeePassword.Name = "txtEmployeePassword";
            this.txtEmployeePassword.PasswordChar = '●';
            this.txtEmployeePassword.Size = new System.Drawing.Size(381, 31);
            this.txtEmployeePassword.TabIndex = 2;
            this.txtEmployeePassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtEmployeePassword.UseSystemPasswordChar = true;
            // 
            // txtEmployeeUsername
            // 
            this.txtEmployeeUsername.BackColor = System.Drawing.SystemColors.Control;
            this.txtEmployeeUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmployeeUsername.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold);
            this.txtEmployeeUsername.Location = new System.Drawing.Point(48, 3);
            this.txtEmployeeUsername.MaxLength = 50;
            this.txtEmployeeUsername.Name = "txtEmployeeUsername";
            this.txtEmployeeUsername.Size = new System.Drawing.Size(377, 31);
            this.txtEmployeeUsername.TabIndex = 3;
            this.txtEmployeeUsername.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("HP Simplified", 16.2F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(116)))), ((int)(((byte)(166)))));
            this.label1.Location = new System.Drawing.Point(25, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 32);
            this.label1.TabIndex = 4;
            this.label1.Text = "UserName";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("HP Simplified", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(116)))), ((int)(((byte)(166)))));
            this.label2.Location = new System.Drawing.Point(25, 157);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 32);
            this.label2.TabIndex = 5;
            this.label2.Text = "Password";
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.LightCoral;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Location = new System.Drawing.Point(454, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(41, 28);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "X";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.LightCoral;
            this.button2.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Location = new System.Drawing.Point(3, 296);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 31);
            this.button2.TabIndex = 8;
            this.button2.Text = "Exit";
            this.toolTip1.SetToolTip(this.button2, "Click here to exit the application");
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("HP Simplified", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.LightCoral;
            this.label3.Location = new System.Drawing.Point(179, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(159, 55);
            this.label3.TabIndex = 9;
            this.label3.Text = "Sign In";
            this.toolTip1.SetToolTip(this.label3, "Sign in page");
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.pictureBox2);
            this.flowLayoutPanel1.Controls.Add(this.txtEmployeeUsername);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(26, 114);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(441, 40);
            this.flowLayoutPanel1.TabIndex = 10;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(3, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(39, 33);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.pictureBox3);
            this.flowLayoutPanel2.Controls.Add(this.txtEmployeePassword);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(26, 192);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(441, 42);
            this.flowLayoutPanel2.TabIndex = 11;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(3, 3);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(35, 31);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 4;
            this.pictureBox3.TabStop = false;
            // 
            // btnFogortPassword
            // 
            this.btnFogortPassword.FlatAppearance.BorderColor = System.Drawing.Color.MistyRose;
            this.btnFogortPassword.FlatAppearance.MouseDownBackColor = System.Drawing.Color.MistyRose;
            this.btnFogortPassword.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MistyRose;
            this.btnFogortPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFogortPassword.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFogortPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(116)))), ((int)(((byte)(166)))));
            this.btnFogortPassword.Location = new System.Drawing.Point(207, 252);
            this.btnFogortPassword.Name = "btnFogortPassword";
            this.btnFogortPassword.Size = new System.Drawing.Size(131, 27);
            this.btnFogortPassword.TabIndex = 12;
            this.btnFogortPassword.Text = "Fogort Password";
            this.toolTip1.SetToolTip(this.btnFogortPassword, "Click here to reset the password");
            this.btnFogortPassword.UseVisualStyleBackColor = true;
            this.btnFogortPassword.Click += new System.EventHandler(this.btnFogortPassword_Click);
            // 
            // lblDevice
            // 
            this.lblDevice.AutoSize = true;
            this.lblDevice.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDevice.Location = new System.Drawing.Point(12, 365);
            this.lblDevice.Name = "lblDevice";
            this.lblDevice.Size = new System.Drawing.Size(61, 17);
            this.lblDevice.TabIndex = 13;
            this.lblDevice.Text = "Device :  ";
            // 
            // lblPrinter
            // 
            this.lblPrinter.AutoSize = true;
            this.lblPrinter.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrinter.Location = new System.Drawing.Point(245, 365);
            this.lblPrinter.Name = "lblPrinter";
            this.lblPrinter.Size = new System.Drawing.Size(57, 17);
            this.lblPrinter.TabIndex = 14;
            this.lblPrinter.Text = "Printer : ";
            this.lblPrinter.Click += new System.EventHandler(this.lblPrinter_Click);
            // 
            // lblSessionID
            // 
            this.lblSessionID.AutoSize = true;
            this.lblSessionID.Location = new System.Drawing.Point(3, 3);
            this.lblSessionID.Name = "lblSessionID";
            this.lblSessionID.Size = new System.Drawing.Size(75, 16);
            this.lblSessionID.TabIndex = 15;
            this.lblSessionID.Text = "Session ID:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblSessionID);
            this.panel1.Controls.Add(this.flowLayoutPanel2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btnFogortPassword);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Location = new System.Drawing.Point(425, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(495, 350);
            this.panel1.TabIndex = 16;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Miara.Properties.Resources.MIARA_POS;
            this.pictureBox1.Location = new System.Drawing.Point(13, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(406, 350);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            // 
            // frmLogInPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(946, 398);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblPrinter);
            this.Controls.Add(this.lblDevice);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLogInPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sign in";
            this.Load += new System.EventHandler(this.frmLogInPage_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtEmployeePassword;
        private System.Windows.Forms.TextBox txtEmployeeUsername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ToolStripMenuItem configurationToolStripMenuItem;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button btnFogortPassword;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem sQLConnectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem databaseSetupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printerSetupToolStripMenuItem;
        private System.Windows.Forms.Label lblDevice;
        private System.Windows.Forms.Label lblPrinter;
        private System.Windows.Forms.Label lblSessionID;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem generalSettingsToolStripMenuItem;
    }
}