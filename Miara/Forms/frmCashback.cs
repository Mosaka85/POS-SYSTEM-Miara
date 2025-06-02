using System;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Miara.Forms
{
    public partial class frmCashback : Form
    {
        private string connectionString;
        private string configFile = "config.xml";
        private int employeeID;
        private int saleID;
        private string employeeFirstName;
        private string employeeSurname;
        private int nextTransactionID;

        public frmCashback(string firstName, string surname, int EMID, int SaleID)
        {
            InitializeComponent();
            saleID = SaleID;
            employeeID = EMID;
            employeeFirstName = firstName;
            employeeSurname = surname;
            lblSaleID.Text = $"SaleID : {saleID}";
            this.AcceptButton = btnCashback;
        }

        private int GetNextTransactionID()
        {
            int nextID = 1; // Default if table is empty

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT ISNULL(MAX(TransactionID), 0) + 1 FROM CashbackTransactions";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            nextID = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting next Transaction ID: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return nextID;
        }

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

                        // Now that we have connection string, get the next transaction ID
                        nextTransactionID = GetNextTransactionID();
                        lblTransactionID.Text = $"Transaction ID: {nextTransactionID}";
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


        // Rest of your methods remain the same...
        private string GetLocalIPAddress()
        {
            string localIP = "";
            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        localIP = ip.ToString();
                        break;
                    }
                }
            }
            catch
            {
                localIP = "127.0.0.1"; // fallback
            }
            return localIP;
        }

        private void frmCashback_Load(object sender, EventArgs e)
        {
            LoadSQLConnectionInfo();
            txtAmount.Focus();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            // Empty event handler
        }

        private void btnCashback_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                MessageBox.Show("Connection string is not loaded.");
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                MessageBox.Show("Please enter a valid amount.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string ipAddress = GetLocalIPAddress();
                string notes = txtNotes.Text;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                    INSERT INTO CashbackTransactions 
                    ( SaleID, EmployeeName, Surname, EmployeeID, Amount, LocationIP, [Status], Notes)
                    VALUES 
                    ( @SaleID, @EmployeeName, @Surname, @EmployeeID, @Amount, @LocationIP, @Status, @Notes)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {

                        cmd.Parameters.AddWithValue("@SaleID", saleID);
                        cmd.Parameters.AddWithValue("@EmployeeName", employeeFirstName);
                        cmd.Parameters.AddWithValue("@Surname", employeeSurname);
                        cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                        cmd.Parameters.AddWithValue("@Amount", amount);
                        cmd.Parameters.AddWithValue("@LocationIP", ipAddress);
                        cmd.Parameters.AddWithValue("@Status", 1); // 1 = Not Paid
                        cmd.Parameters.AddWithValue("@Notes", notes);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Cashback transaction saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving cashback: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}