using Miara.Processor_Class;
using System;
using System.Windows.Forms;

namespace Miara
{
    public partial class FormApplicationSettings : Form
    {
        private ApplicationSettings _currentSettings;

        public FormApplicationSettings()
        {
            InitializeComponent();
        }

        private void FormApplicationSettings_Load(object sender, EventArgs e)
        {
            // Load settings and populate form controls
            _currentSettings = SettingsManager.LoadSettings();
            PopulateControls(_currentSettings);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Update settings object from form controls and save
            UpdateSettingsFromControls(_currentSettings);
            SettingsManager.SaveSettings(_currentSettings);
        }

        private void PopulateControls(ApplicationSettings settings)
        {
            // Fills the form's controls with data from the settings object
            txtStoreName.Text = settings.StoreName;
            chkLocalLogs.Checked = settings.AllowLocalLogs;
            chkDatabaseLogs.Checked = settings.AllowDatabaseLogs;
            chkAutoSave.Checked = settings.AutoSave;
            txtAddress.Text = settings.StoreAddress;
            txtStorePhone.Text = settings.StorePhone;
            txtStoreEmail.Text = settings.StoreEmail;
            txtStoreWebsite.Text = settings.StoreWebsite;
            txtTaxNumber.Text = settings.TaxNumber;
            comboCurrency.Text = settings.Currency;
            numReceiptCopies.Text = settings.ReceiptCopies.ToString();
            txtFooterMassage1.Text = settings.FooterMessage1;
            txtFooterMassage2.Text = settings.FooterMessage2;
            txtFooterMassage3.Text = settings.FooterMessage3;
        }

        private void UpdateSettingsFromControls(ApplicationSettings settings)
        {
            // Updates the settings object with data from the form's controls
            settings.StoreName = txtStoreName.Text.Trim();
            settings.AllowLocalLogs = chkLocalLogs.Checked;
            settings.AllowDatabaseLogs = chkDatabaseLogs.Checked;
            settings.AutoSave = chkAutoSave.Checked;
            settings.StoreAddress = txtAddress.Text.Trim();
            settings.StorePhone = txtStorePhone.Text.Trim();
            settings.StoreEmail = txtStoreEmail.Text.Trim();
            settings.StoreWebsite = txtStoreWebsite.Text.Trim();
            settings.TaxNumber = txtTaxNumber.Text.Trim();
            settings.Currency = comboCurrency.Text.Trim();
            int.TryParse(numReceiptCopies.Text.Trim(), out int copies);
            settings.ReceiptCopies = copies;
            settings.FooterMessage1 = txtFooterMassage1.Text.Trim();
            settings.FooterMessage2 = txtFooterMassage2.Text.Trim();
            settings.FooterMessage3 = txtFooterMassage3.Text.Trim();
        }

        private void chkDatabaseLogs_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}