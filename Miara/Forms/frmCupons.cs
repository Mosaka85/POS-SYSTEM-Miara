using Miara.Models;
using Miara.Services;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Miara.Forms
{
    public partial class frmCupons : Form
    {
        private readonly int _emid;
        private readonly string _device;
        private readonly string _sessionID;
        private readonly int _couponSaleID;

        private readonly CouponProcessor _couponProcessor;
        private readonly string _configFile = "config.xml";
        private string _connectionString;

        public object CouponCode { get; private set; }

        public frmCupons(int saleId, string sessionID, int emid, string device)
        {
            InitializeComponent();

            _couponSaleID = saleId;
            _sessionID = sessionID;
            _emid = emid;
            _device = device;
            lblEMID.Text = $"EMID : {_emid}";
            lblSessionID.Text = $"SessionID : {_sessionID}";
      

            LoadSqlConnectionString();
            _couponProcessor = new CouponProcessor(_connectionString);
        }

        private void LoadSqlConnectionString()
        {
            if (!File.Exists(_configFile))
            {
  
                MessageBox.Show("Connection configuration file not found. Please check the configuration.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var serializer = new XmlSerializer(typeof(LoginInfo));
                using (var fileStream = new FileStream(_configFile, FileMode.Open))
                {
                    var loginInfo = (LoginInfo)serializer.Deserialize(fileStream);
                    _connectionString = $"Data Source={loginInfo.DataSource};Initial Catalog={loginInfo.SelectedDatabase};User ID={loginInfo.Username};Password={loginInfo.Password}";
                }
            }
            catch (Exception ex)
            {
    
                MessageBox.Show($"Failed to load connection information: {ex.Message}", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            string code = txtVoucher.Text.Trim();
            CouponCode = txtVoucher.Text.Trim();

            if (string.IsNullOrWhiteSpace(code))
            {
                lblResult.Text = "Please enter a coupon code.";
                lblResult.ForeColor = Color.Red;
                return;
            }

            decimal discount = _couponProcessor.RedeemCoupon(
                code,
                _couponSaleID,
                _sessionID,
                _emid,
                _device,
                out string resultMessage

            );

            lblResult.Text = resultMessage;
            lblResult.ForeColor = discount > 0 ? Color.Green : Color.Red;
        }

        private void frmCupons_Load(object sender, EventArgs e)
        {
            lblTotal.Text = $"SaleID : {_couponSaleID}";
        }
    }
}
