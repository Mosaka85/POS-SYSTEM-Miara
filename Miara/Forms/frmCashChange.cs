using Miara.Models;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Miara.Forms
{
    public partial class frmCashChange : Form
    {
        public decimal RenderedAmount { get; private set; } = 0m;
        public decimal ChangeAmount { get; private set; } = 0m;

        private readonly decimal TotalAmount;
        private readonly int EmployeeID;
        private readonly string EmployeeName;
        private readonly string DeviceName;
        private readonly string SessionID;
        private string connectionString;

        private readonly string configFile =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config.xml");

        // ✅ CONSTRUCTOR (POS CONTEXT PASSED IN)
        public frmCashChange(
            decimal totalAmount,
            int employeeID,
            string employeeName,
            string deviceName,
            string sessionID)
        {
            InitializeComponent();

            TotalAmount = totalAmount;
            EmployeeID = employeeID;
            EmployeeName = employeeName ?? throw new ArgumentNullException(nameof(employeeName));
            DeviceName = deviceName ?? throw new ArgumentNullException(nameof(deviceName));
            SessionID = sessionID ?? throw new ArgumentNullException(nameof(sessionID));

            lblTotal.Text = $"Total: R {TotalAmount:F2}";
            lblChange.Text = "Change: R 0.00";

            LoadSQLConnectionInfo();
        }

        // 🔐 LOAD SQL CONNECTION FROM CONFIG.XML
        private void LoadSQLConnectionInfo()
        {
            if (!File.Exists(configFile))
            {
                MessageBox.Show("Connection configuration file not found.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Abort;
                this.Close();
                return;
            }

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(LoginInfo));
                using (FileStream fs = new FileStream(configFile, FileMode.Open))
                {
                    LoginInfo info = (LoginInfo)serializer.Deserialize(fs);
                    connectionString =
                        $"Data Source={info.DataSource};" +
                        $"Initial Catalog={info.SelectedDatabase};" +
                        $"User ID={info.Username};Password={info.Password}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load connection information:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Abort;
                this.Close();
            }
        }

        private void frmCashChange_Load(object sender, EventArgs e)
        {
            txtRendered.Focus();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtRendered.Text, out decimal rendered))
            {
                MessageBox.Show("Enter a valid amount.", "Invalid Input",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (rendered < TotalAmount)
            {
                MessageBox.Show("Amount rendered is less than the total.",
                    "Insufficient Amount",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ FIXED: Properly assign to class properties
            RenderedAmount = rendered;
            ChangeAmount = rendered - TotalAmount;

            lblChange.Text = $"Change: R {ChangeAmount:F2}";
        }

        // 🧾 LOG CASH CHANGE
        private void LogCashChange()
        {
            const string sql = @"
                INSERT INTO dbo.CashChangeLog
                (
                    SessionID,
                    DeviceName,
                    EmployeeID,
                    EmployeeName,
                    TotalAmount,
                    RenderedAmount,
                    ChangeAmount
                )
                VALUES
                (
                    @SessionID,
                    @DeviceName,
                    @EmployeeID,
                    @EmployeeName,
                    @TotalAmount,
                    @RenderedAmount,
                    @ChangeAmount
                )";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@SessionID", SessionID);
                cmd.Parameters.AddWithValue("@DeviceName", DeviceName);
                cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                cmd.Parameters.AddWithValue("@EmployeeName", EmployeeName);
                cmd.Parameters.AddWithValue("@TotalAmount", TotalAmount);
                cmd.Parameters.AddWithValue("@RenderedAmount", RenderedAmount);
                cmd.Parameters.AddWithValue("@ChangeAmount", ChangeAmount);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            // ✅ Improved check: ensures calculation was done
            if (RenderedAmount <= 0 || ChangeAmount < 0)
            {
                MessageBox.Show("Please calculate the change first.",
                    "Action Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                LogCashChange();

                // Success: return OK so calling form knows it succeeded
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to log cash change:\n" + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}