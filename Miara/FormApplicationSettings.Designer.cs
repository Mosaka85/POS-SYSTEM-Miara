namespace Miara
{
    partial class FormApplicationSettings
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.txtFooterMassage3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblReceiptCopies = new System.Windows.Forms.Label();
            this.numReceiptCopies = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboCurrency = new System.Windows.Forms.ComboBox();
            this.txtTaxNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtStoreWebsite = new System.Windows.Forms.TextBox();
            this.lblStoreWebsite = new System.Windows.Forms.Label();
            this.txtStoreEmail = new System.Windows.Forms.TextBox();
            this.lblStoreEmail = new System.Windows.Forms.Label();
            this.txtFooterMassage2 = new System.Windows.Forms.TextBox();
            this.lblFooterMassage2 = new System.Windows.Forms.Label();
            this.txtFooterMassage1 = new System.Windows.Forms.TextBox();
            this.lblFooterMassage1 = new System.Windows.Forms.Label();
            this.txtStorePhone = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.labelStoreName = new System.Windows.Forms.Label();
            this.txtStoreName = new System.Windows.Forms.TextBox();
            this.chkAutoSave = new System.Windows.Forms.CheckBox();
            this.tabLogging = new System.Windows.Forms.TabPage();
            this.chkLocalLogs = new System.Windows.Forms.CheckBox();
            this.chkDatabaseLogs = new System.Windows.Forms.CheckBox();
            this.cmbLogLevel = new System.Windows.Forms.ComboBox();
            this.labelLogLevel = new System.Windows.Forms.Label();
            this.tabSecurity = new System.Windows.Forms.TabPage();
            this.chkRequireLogin = new System.Windows.Forms.CheckBox();
            this.chkEnable2FA = new System.Windows.Forms.CheckBox();
            this.chkEncryptFiles = new System.Windows.Forms.CheckBox();
            this.tabAppearance = new System.Windows.Forms.TabPage();
            this.chkDarkMode = new System.Windows.Forms.CheckBox();
            this.cmbFontSize = new System.Windows.Forms.ComboBox();
            this.labelFontSize = new System.Windows.Forms.Label();
            this.tabAdvanced = new System.Windows.Forms.TabPage();
            this.labelApiUrl = new System.Windows.Forms.Label();
            this.txtApiUrl = new System.Windows.Forms.TextBox();
            this.chkCloudBackup = new System.Windows.Forms.CheckBox();
            this.chkPerformanceMode = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.tabLogging.SuspendLayout();
            this.tabSecurity.SuspendLayout();
            this.tabAppearance.SuspendLayout();
            this.tabAdvanced.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl.Controls.Add(this.tabGeneral);
            this.tabControl.Controls.Add(this.tabLogging);
            this.tabControl.Controls.Add(this.tabSecurity);
            this.tabControl.Controls.Add(this.tabAppearance);
            this.tabControl.Controls.Add(this.tabAdvanced);
            this.tabControl.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl.HotTrack = true;
            this.tabControl.Location = new System.Drawing.Point(20, 20);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1015, 521);
            this.tabControl.TabIndex = 0;
            // 
            // tabGeneral
            // 
            this.tabGeneral.BackColor = System.Drawing.Color.White;
            this.tabGeneral.Controls.Add(this.txtFooterMassage3);
            this.tabGeneral.Controls.Add(this.label3);
            this.tabGeneral.Controls.Add(this.lblReceiptCopies);
            this.tabGeneral.Controls.Add(this.numReceiptCopies);
            this.tabGeneral.Controls.Add(this.label2);
            this.tabGeneral.Controls.Add(this.comboCurrency);
            this.tabGeneral.Controls.Add(this.txtTaxNumber);
            this.tabGeneral.Controls.Add(this.label1);
            this.tabGeneral.Controls.Add(this.txtStoreWebsite);
            this.tabGeneral.Controls.Add(this.lblStoreWebsite);
            this.tabGeneral.Controls.Add(this.txtStoreEmail);
            this.tabGeneral.Controls.Add(this.lblStoreEmail);
            this.tabGeneral.Controls.Add(this.txtFooterMassage2);
            this.tabGeneral.Controls.Add(this.lblFooterMassage2);
            this.tabGeneral.Controls.Add(this.txtFooterMassage1);
            this.tabGeneral.Controls.Add(this.lblFooterMassage1);
            this.tabGeneral.Controls.Add(this.txtStorePhone);
            this.tabGeneral.Controls.Add(this.lblPhone);
            this.tabGeneral.Controls.Add(this.txtAddress);
            this.tabGeneral.Controls.Add(this.lblAddress);
            this.tabGeneral.Controls.Add(this.labelStoreName);
            this.tabGeneral.Controls.Add(this.txtStoreName);
            this.tabGeneral.Controls.Add(this.chkAutoSave);
            this.tabGeneral.Location = new System.Drawing.Point(4, 28);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Size = new System.Drawing.Size(1007, 489);
            this.tabGeneral.TabIndex = 0;
            this.tabGeneral.Text = "General";
            // 
            // txtFooterMassage3
            // 
            this.txtFooterMassage3.Location = new System.Drawing.Point(104, 410);
            this.txtFooterMassage3.Multiline = true;
            this.txtFooterMassage3.Name = "txtFooterMassage3";
            this.txtFooterMassage3.Size = new System.Drawing.Size(300, 54);
            this.txtFooterMassage3.TabIndex = 22;
            this.txtFooterMassage3.Text = "Thank you for shopping with us!";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 413);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 51);
            this.label3.TabIndex = 21;
            this.label3.Text = "Footer Massage Line 3";
            // 
            // lblReceiptCopies
            // 
            this.lblReceiptCopies.Location = new System.Drawing.Point(468, 15);
            this.lblReceiptCopies.Name = "lblReceiptCopies";
            this.lblReceiptCopies.Size = new System.Drawing.Size(100, 23);
            this.lblReceiptCopies.TabIndex = 20;
            this.lblReceiptCopies.Text = "ReceiptCopies";
            // 
            // numReceiptCopies
            // 
            this.numReceiptCopies.Location = new System.Drawing.Point(574, 12);
            this.numReceiptCopies.Name = "numReceiptCopies";
            this.numReceiptCopies.Size = new System.Drawing.Size(84, 22);
            this.numReceiptCopies.TabIndex = 19;
            this.numReceiptCopies.Text = "1";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 244);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 18;
            this.label2.Text = "Currency";
            // 
            // comboCurrency
            // 
            this.comboCurrency.FormattingEnabled = true;
            this.comboCurrency.Items.AddRange(new object[] {
            "ZAR",
            "BWP",
            "NAD",
            "LSL",
            "SZL",
            "ZWL",
            "MZN",
            "ZMW",
            "MWK",
            "AOA",
            "TZS",
            "KES",
            "UGX",
            "RWF",
            "BIF",
            "CDF",
            "MUR",
            "SCR",
            "KMF",
            "MGA",
            "ETB",
            "SOS",
            "DJF",
            "ERN",
            "SDG",
            "SSP",
            "EGP",
            "LYD",
            "TND",
            "DZD",
            "MAD",
            "CVE",
            "GMD",
            "GNF",
            "SLL",
            "LRD",
            "GHS",
            "NGN",
            "XAF",
            "XOF"});
            this.comboCurrency.Location = new System.Drawing.Point(104, 244);
            this.comboCurrency.Name = "comboCurrency";
            this.comboCurrency.Size = new System.Drawing.Size(300, 24);
            this.comboCurrency.TabIndex = 17;
            // 
            // txtTaxNumber
            // 
            this.txtTaxNumber.Location = new System.Drawing.Point(104, 202);
            this.txtTaxNumber.Name = "txtTaxNumber";
            this.txtTaxNumber.Size = new System.Drawing.Size(300, 22);
            this.txtTaxNumber.TabIndex = 16;
            this.txtTaxNumber.Text = "000000000";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 202);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 15;
            this.label1.Text = "Tax Number";
            // 
            // txtStoreWebsite
            // 
            this.txtStoreWebsite.Location = new System.Drawing.Point(104, 165);
            this.txtStoreWebsite.Name = "txtStoreWebsite";
            this.txtStoreWebsite.Size = new System.Drawing.Size(300, 22);
            this.txtStoreWebsite.TabIndex = 14;
            this.txtStoreWebsite.Text = "012-345-6789";
            // 
            // lblStoreWebsite
            // 
            this.lblStoreWebsite.Location = new System.Drawing.Point(3, 168);
            this.lblStoreWebsite.Name = "lblStoreWebsite";
            this.lblStoreWebsite.Size = new System.Drawing.Size(100, 23);
            this.lblStoreWebsite.TabIndex = 13;
            this.lblStoreWebsite.Text = "Store Website";
            // 
            // txtStoreEmail
            // 
            this.txtStoreEmail.Location = new System.Drawing.Point(104, 129);
            this.txtStoreEmail.Name = "txtStoreEmail";
            this.txtStoreEmail.Size = new System.Drawing.Size(300, 22);
            this.txtStoreEmail.TabIndex = 12;
            this.txtStoreEmail.Text = "012-345-6789";
            // 
            // lblStoreEmail
            // 
            this.lblStoreEmail.Location = new System.Drawing.Point(3, 129);
            this.lblStoreEmail.Name = "lblStoreEmail";
            this.lblStoreEmail.Size = new System.Drawing.Size(100, 23);
            this.lblStoreEmail.TabIndex = 11;
            this.lblStoreEmail.Text = "Store Email";
            // 
            // txtFooterMassage2
            // 
            this.txtFooterMassage2.Location = new System.Drawing.Point(104, 343);
            this.txtFooterMassage2.Multiline = true;
            this.txtFooterMassage2.Name = "txtFooterMassage2";
            this.txtFooterMassage2.Size = new System.Drawing.Size(300, 54);
            this.txtFooterMassage2.TabIndex = 10;
            this.txtFooterMassage2.Text = "Thank you for shopping with us!";
            // 
            // lblFooterMassage2
            // 
            this.lblFooterMassage2.Location = new System.Drawing.Point(3, 343);
            this.lblFooterMassage2.Name = "lblFooterMassage2";
            this.lblFooterMassage2.Size = new System.Drawing.Size(100, 51);
            this.lblFooterMassage2.TabIndex = 9;
            this.lblFooterMassage2.Text = "Footer Massage Line 2";
            // 
            // txtFooterMassage1
            // 
            this.txtFooterMassage1.Location = new System.Drawing.Point(104, 286);
            this.txtFooterMassage1.Multiline = true;
            this.txtFooterMassage1.Name = "txtFooterMassage1";
            this.txtFooterMassage1.Size = new System.Drawing.Size(300, 54);
            this.txtFooterMassage1.TabIndex = 8;
            this.txtFooterMassage1.Text = "Thank you for shopping with us!";
            // 
            // lblFooterMassage1
            // 
            this.lblFooterMassage1.Location = new System.Drawing.Point(3, 289);
            this.lblFooterMassage1.Name = "lblFooterMassage1";
            this.lblFooterMassage1.Size = new System.Drawing.Size(100, 51);
            this.lblFooterMassage1.TabIndex = 7;
            this.lblFooterMassage1.Text = "Footer Massage Line 1";
            // 
            // txtStorePhone
            // 
            this.txtStorePhone.Location = new System.Drawing.Point(104, 91);
            this.txtStorePhone.Name = "txtStorePhone";
            this.txtStorePhone.Size = new System.Drawing.Size(300, 22);
            this.txtStorePhone.TabIndex = 6;
            this.txtStorePhone.Text = "012-345-6789";
            // 
            // lblPhone
            // 
            this.lblPhone.Location = new System.Drawing.Point(3, 91);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(100, 23);
            this.lblPhone.TabIndex = 5;
            this.lblPhone.Text = "Store Phone";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(104, 53);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(300, 22);
            this.txtAddress.TabIndex = 4;
            this.txtAddress.Text = "123 Mosaka St";
            // 
            // lblAddress
            // 
            this.lblAddress.Location = new System.Drawing.Point(3, 53);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(100, 23);
            this.lblAddress.TabIndex = 3;
            this.lblAddress.Text = "Store Address";
            // 
            // labelStoreName
            // 
            this.labelStoreName.Location = new System.Drawing.Point(3, 19);
            this.labelStoreName.Name = "labelStoreName";
            this.labelStoreName.Size = new System.Drawing.Size(100, 23);
            this.labelStoreName.TabIndex = 0;
            this.labelStoreName.Text = "Store Name:";
            // 
            // txtStoreName
            // 
            this.txtStoreName.Location = new System.Drawing.Point(104, 16);
            this.txtStoreName.Name = "txtStoreName";
            this.txtStoreName.Size = new System.Drawing.Size(300, 22);
            this.txtStoreName.TabIndex = 1;
            this.txtStoreName.Text = "MIARA TRADING PTY LTD";
            // 
            // chkAutoSave
            // 
            this.chkAutoSave.Location = new System.Drawing.Point(835, 3);
            this.chkAutoSave.Name = "chkAutoSave";
            this.chkAutoSave.Size = new System.Drawing.Size(169, 24);
            this.chkAutoSave.TabIndex = 2;
            this.chkAutoSave.Text = "Enable Discount";
            // 
            // tabLogging
            // 
            this.tabLogging.BackColor = System.Drawing.Color.White;
            this.tabLogging.Controls.Add(this.chkLocalLogs);
            this.tabLogging.Controls.Add(this.chkDatabaseLogs);
            this.tabLogging.Controls.Add(this.cmbLogLevel);
            this.tabLogging.Controls.Add(this.labelLogLevel);
            this.tabLogging.Location = new System.Drawing.Point(4, 28);
            this.tabLogging.Name = "tabLogging";
            this.tabLogging.Size = new System.Drawing.Size(1007, 489);
            this.tabLogging.TabIndex = 1;
            this.tabLogging.Text = "Logging";
            // 
            // chkLocalLogs
            // 
            this.chkLocalLogs.Location = new System.Drawing.Point(17, 47);
            this.chkLocalLogs.Name = "chkLocalLogs";
            this.chkLocalLogs.Size = new System.Drawing.Size(165, 24);
            this.chkLocalLogs.TabIndex = 0;
            this.chkLocalLogs.Text = "Allow Local Logs";
            // 
            // chkDatabaseLogs
            // 
            this.chkDatabaseLogs.Location = new System.Drawing.Point(17, 77);
            this.chkDatabaseLogs.Name = "chkDatabaseLogs";
            this.chkDatabaseLogs.Size = new System.Drawing.Size(175, 24);
            this.chkDatabaseLogs.TabIndex = 1;
            this.chkDatabaseLogs.Text = "Allow Database Logs";
            this.chkDatabaseLogs.CheckedChanged += new System.EventHandler(this.chkDatabaseLogs_CheckedChanged);
            // 
            // cmbLogLevel
            // 
            this.cmbLogLevel.Items.AddRange(new object[] {
            "Info",
            "Warning",
            "Error",
            "Debug"});
            this.cmbLogLevel.Location = new System.Drawing.Point(120, 129);
            this.cmbLogLevel.Name = "cmbLogLevel";
            this.cmbLogLevel.Size = new System.Drawing.Size(121, 24);
            this.cmbLogLevel.TabIndex = 2;
            // 
            // labelLogLevel
            // 
            this.labelLogLevel.Location = new System.Drawing.Point(3, 129);
            this.labelLogLevel.Name = "labelLogLevel";
            this.labelLogLevel.Size = new System.Drawing.Size(100, 23);
            this.labelLogLevel.TabIndex = 3;
            this.labelLogLevel.Text = "Log Level:";
            // 
            // tabSecurity
            // 
            this.tabSecurity.BackColor = System.Drawing.Color.White;
            this.tabSecurity.Controls.Add(this.chkRequireLogin);
            this.tabSecurity.Controls.Add(this.chkEnable2FA);
            this.tabSecurity.Controls.Add(this.chkEncryptFiles);
            this.tabSecurity.Location = new System.Drawing.Point(4, 28);
            this.tabSecurity.Name = "tabSecurity";
            this.tabSecurity.Size = new System.Drawing.Size(1007, 489);
            this.tabSecurity.TabIndex = 2;
            this.tabSecurity.Text = "Security";
            // 
            // chkRequireLogin
            // 
            this.chkRequireLogin.Location = new System.Drawing.Point(20, 20);
            this.chkRequireLogin.Name = "chkRequireLogin";
            this.chkRequireLogin.Size = new System.Drawing.Size(104, 24);
            this.chkRequireLogin.TabIndex = 0;
            this.chkRequireLogin.Text = "Require Login at Startup";
            // 
            // chkEnable2FA
            // 
            this.chkEnable2FA.Location = new System.Drawing.Point(20, 50);
            this.chkEnable2FA.Name = "chkEnable2FA";
            this.chkEnable2FA.Size = new System.Drawing.Size(104, 24);
            this.chkEnable2FA.TabIndex = 1;
            this.chkEnable2FA.Text = "Enable Two-Factor Authentication";
            // 
            // chkEncryptFiles
            // 
            this.chkEncryptFiles.Location = new System.Drawing.Point(20, 80);
            this.chkEncryptFiles.Name = "chkEncryptFiles";
            this.chkEncryptFiles.Size = new System.Drawing.Size(104, 24);
            this.chkEncryptFiles.TabIndex = 2;
            this.chkEncryptFiles.Text = "Encrypt Local Files";
            // 
            // tabAppearance
            // 
            this.tabAppearance.BackColor = System.Drawing.Color.White;
            this.tabAppearance.Controls.Add(this.chkDarkMode);
            this.tabAppearance.Controls.Add(this.cmbFontSize);
            this.tabAppearance.Controls.Add(this.labelFontSize);
            this.tabAppearance.Location = new System.Drawing.Point(4, 28);
            this.tabAppearance.Name = "tabAppearance";
            this.tabAppearance.Size = new System.Drawing.Size(1007, 489);
            this.tabAppearance.TabIndex = 3;
            this.tabAppearance.Text = "Appearance";
            // 
            // chkDarkMode
            // 
            this.chkDarkMode.Location = new System.Drawing.Point(20, 20);
            this.chkDarkMode.Name = "chkDarkMode";
            this.chkDarkMode.Size = new System.Drawing.Size(104, 24);
            this.chkDarkMode.TabIndex = 0;
            this.chkDarkMode.Text = "Enable Dark Mode";
            // 
            // cmbFontSize
            // 
            this.cmbFontSize.Items.AddRange(new object[] {
            "Small",
            "Medium",
            "Large"});
            this.cmbFontSize.Location = new System.Drawing.Point(100, 60);
            this.cmbFontSize.Name = "cmbFontSize";
            this.cmbFontSize.Size = new System.Drawing.Size(121, 24);
            this.cmbFontSize.TabIndex = 1;
            // 
            // labelFontSize
            // 
            this.labelFontSize.Location = new System.Drawing.Point(20, 60);
            this.labelFontSize.Name = "labelFontSize";
            this.labelFontSize.Size = new System.Drawing.Size(100, 23);
            this.labelFontSize.TabIndex = 2;
            this.labelFontSize.Text = "Font Size:";
            // 
            // tabAdvanced
            // 
            this.tabAdvanced.BackColor = System.Drawing.Color.White;
            this.tabAdvanced.Controls.Add(this.labelApiUrl);
            this.tabAdvanced.Controls.Add(this.txtApiUrl);
            this.tabAdvanced.Controls.Add(this.chkCloudBackup);
            this.tabAdvanced.Controls.Add(this.chkPerformanceMode);
            this.tabAdvanced.Location = new System.Drawing.Point(4, 28);
            this.tabAdvanced.Name = "tabAdvanced";
            this.tabAdvanced.Size = new System.Drawing.Size(1007, 489);
            this.tabAdvanced.TabIndex = 4;
            this.tabAdvanced.Text = "Advanced";
            // 
            // labelApiUrl
            // 
            this.labelApiUrl.Location = new System.Drawing.Point(20, 20);
            this.labelApiUrl.Name = "labelApiUrl";
            this.labelApiUrl.Size = new System.Drawing.Size(100, 23);
            this.labelApiUrl.TabIndex = 0;
            this.labelApiUrl.Text = "API Endpoint:";
            // 
            // txtApiUrl
            // 
            this.txtApiUrl.Location = new System.Drawing.Point(120, 20);
            this.txtApiUrl.Name = "txtApiUrl";
            this.txtApiUrl.Size = new System.Drawing.Size(300, 22);
            this.txtApiUrl.TabIndex = 1;
            // 
            // chkCloudBackup
            // 
            this.chkCloudBackup.Location = new System.Drawing.Point(20, 60);
            this.chkCloudBackup.Name = "chkCloudBackup";
            this.chkCloudBackup.Size = new System.Drawing.Size(104, 24);
            this.chkCloudBackup.TabIndex = 2;
            this.chkCloudBackup.Text = "Enable Cloud Backup";
            // 
            // chkPerformanceMode
            // 
            this.chkPerformanceMode.Location = new System.Drawing.Point(20, 90);
            this.chkPerformanceMode.Name = "chkPerformanceMode";
            this.chkPerformanceMode.Size = new System.Drawing.Size(104, 24);
            this.chkPerformanceMode.TabIndex = 3;
            this.chkPerformanceMode.Text = "Performance Mode";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(859, 569);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(960, 569);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            // 
            // FormApplicationSettings
            // 
            this.ClientSize = new System.Drawing.Size(1047, 613);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Name = "FormApplicationSettings";
            this.Text = "Application Settings";
            this.Load += new System.EventHandler(this.FormApplicationSettings_Load);
            this.tabControl.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            this.tabLogging.ResumeLayout(false);
            this.tabSecurity.ResumeLayout(false);
            this.tabAppearance.ResumeLayout(false);
            this.tabAdvanced.ResumeLayout(false);
            this.tabAdvanced.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.TabPage tabLogging;
        private System.Windows.Forms.TabPage tabSecurity;
        private System.Windows.Forms.TabPage tabAppearance;
        private System.Windows.Forms.TabPage tabAdvanced;

        private System.Windows.Forms.Label labelStoreName;
        private System.Windows.Forms.TextBox txtStoreName;
        private System.Windows.Forms.CheckBox chkAutoSave;

        private System.Windows.Forms.CheckBox chkLocalLogs;
        private System.Windows.Forms.CheckBox chkDatabaseLogs;
        private System.Windows.Forms.ComboBox cmbLogLevel;
        private System.Windows.Forms.Label labelLogLevel;

        private System.Windows.Forms.CheckBox chkRequireLogin;
        private System.Windows.Forms.CheckBox chkEnable2FA;
        private System.Windows.Forms.CheckBox chkEncryptFiles;

        private System.Windows.Forms.CheckBox chkDarkMode;
        private System.Windows.Forms.ComboBox cmbFontSize;
        private System.Windows.Forms.Label labelFontSize;

        private System.Windows.Forms.TextBox txtApiUrl;
        private System.Windows.Forms.Label labelApiUrl;
        private System.Windows.Forms.CheckBox chkCloudBackup;
        private System.Windows.Forms.CheckBox chkPerformanceMode;

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtStorePhone;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtFooterMassage1;
        private System.Windows.Forms.Label lblFooterMassage1;
        private System.Windows.Forms.Label lblFooterMassage2;
        private System.Windows.Forms.TextBox txtFooterMassage2;
        private System.Windows.Forms.TextBox txtStoreEmail;
        private System.Windows.Forms.Label lblStoreEmail;
        private System.Windows.Forms.Label lblStoreWebsite;
        private System.Windows.Forms.TextBox txtStoreWebsite;
        private System.Windows.Forms.TextBox txtTaxNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboCurrency;
        private System.Windows.Forms.Label lblReceiptCopies;
        private System.Windows.Forms.TextBox numReceiptCopies;
        private System.Windows.Forms.TextBox txtFooterMassage3;
        private System.Windows.Forms.Label label3;
    }
}
