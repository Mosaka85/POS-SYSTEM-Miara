using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Miara.Forms
{
    public partial class frmCupons : Form
    {
        public frmCupons(int saleid)
        {
            InitializeComponent();
            LoadSQLConnectionInfo();

            CuoponSaleID = saleid;

        }
        private string configFile = "config.xml";
        private string connectionString;

        public string RedeemedCode { get; private set; }
        public int CuoponSaleID;

        private void LoadSQLConnectionInfo()
        {
            if (File.Exists(configFile))
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(LoginInfo));
                    using (FileStream fileStream = new FileStream(configFile, FileMode.Open))
                    {
                        LoginInfo loginInfo = (LoginInfo)serializer.Deserialize(fileStream);
                        connectionString = $"Data Source={loginInfo.DataSource};Initial Catalog={loginInfo.SelectedDatabase};User ID={loginInfo.Username};Password={loginInfo.Password}";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to load connection information: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Connection configuration file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }







        public decimal ApplyCoupon(string couponCode, out string message)
        {
            decimal discountToApply = 0;
            message = "";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT DiscountAmount, IsPercentage, IsActive, ExpiryDate, Type
                         FROM Coupons
                         WHERE CouponCode = @code";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@code", couponCode);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    decimal discountValue = reader.GetDecimal(0);
                    bool isPercentage = reader.GetBoolean(1);
                    bool isActive = reader.GetBoolean(2);
                    DateTime expiry = reader.GetDateTime(3);
                    string type = reader.GetString(4);

                    if (!isActive)
                    {
                        message = "This coupon is no longer active.";
                        return 0;
                    }

                    if (DateTime.Now > expiry)
                    {
                        message = "This coupon has expired.";
                        return 0;
                    }

                    if (isPercentage)
                    {
                        discountToApply = discountValue / 100;
                        message = $"Coupon applied: {discountValue}% off";
                    }
                    else
                    {
                        discountToApply = discountValue;
                        message = type == "Voucher" ?
                            $"Voucher applied: R{discountValue} deducted." :
                            $"Coupon applied: R{discountValue} off.";
                    }

                    reader.Close();

                    // Save to CouponRedemptions table
                    SqlCommand insertCmd = new SqlCommand(@"
                INSERT INTO CouponRedemptions (CouponCode, SaleID, DiscountApplied)
                VALUES (@code, @saleId, @discount)", conn);

                    insertCmd.Parameters.AddWithValue("@code", couponCode);
                    int cuoponSaleID = CuoponSaleID;
                    insertCmd.Parameters.AddWithValue("@saleId", cuoponSaleID);
                    insertCmd.Parameters.AddWithValue("@discount", discountToApply);

                    insertCmd.ExecuteNonQuery();
                }
                else
                {
                    message = "Invalid coupon code.";
                }
            }

            return discountToApply;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            string code = txtVoucher.Text.Trim();

            string resultMessage;
            decimal discount = ApplyCoupon(code, out resultMessage);

            lblResult.Text = resultMessage;
            lblResult.ForeColor = discount > 0 ? Color.Green : Color.Red;


        }

        private void frmCupons_Load(object sender, EventArgs e)
        {
            lblTotal.Text = $"SaleID : {CuoponSaleID}";
        }
    }
}

