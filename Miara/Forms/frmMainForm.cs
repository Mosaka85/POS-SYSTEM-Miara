using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Miara
{
    public partial class frmMainForm : Form
    {
        public frmMainForm(string firstName, string surname, int EMID)
        {
            InitializeComponent();

            lblName.Text = $"WELCOME {firstName} {surname}";

            employeeFirstName = firstName;
            employeeSurname = surname;
            EmployeeNumber = EMID;
            this.FormClosed += (sender, e) => Application.Exit();


        }
        private static readonly string configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config.xml");
        private void button1_Click(object sender, EventArgs e)
        {

            new frmEmployeeDetails(employeeFirstName, employeeSurname, EmployeeNumber).ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private string employeeFirstName;
        private string employeeSurname;
        private int EmployeeNumber;

        private string connectionString;

        private void button3_Click_1(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmProducts newForm = new frmProducts(employeeFirstName, employeeSurname, EmployeeNumber);
            newForm.ShowDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            new frmSales(employeeFirstName, employeeSurname, EmployeeNumber).ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            new frmEmployeeDetails(employeeFirstName, employeeSurname, EmployeeNumber).ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
        private async void frmMainForm_Load(object sender, EventArgs e)
        {
            LoadSQLConnectionInfo();
            await LoadEmployeeImageAsync(EmployeeNumber);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new frmReports(employeeFirstName, employeeSurname, EmployeeNumber).ShowDialog();
        }

        private void btnCategoryCatalog_Click(object sender, EventArgs e)
        {
            new frmCategory(employeeFirstName, employeeSurname, EmployeeNumber).ShowDialog();
        }

        private void panelLeft_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblName_Click(object sender, EventArgs e)
        {

        }

        private void lblUser_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            new frmReports(employeeFirstName, employeeSurname, EmployeeNumber).ShowDialog();
        }

        private void btnSalesHistory_Click(object sender, EventArgs e)
        {
            new frmReports(employeeFirstName, employeeSurname, EmployeeNumber).ShowDialog();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            frmSales newForm = new frmSales(employeeFirstName, employeeSurname, EmployeeNumber);
            newForm.ShowDialog();
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            frmProducts newForm = new frmProducts(employeeFirstName, employeeSurname, EmployeeNumber);
            newForm.ShowDialog();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            new frmEmployeeDetails(employeeFirstName, employeeSurname, EmployeeNumber).ShowDialog();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            new frmCategory(employeeFirstName, employeeSurname, EmployeeNumber).ShowDialog();
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

        private async Task LoadEmployeeImageAsync(int employeeId)
        {
            pictureBox1.Image = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                SELECT TOP 1 ImageData 
                FROM ImageStore 
                WHERE EmployeeID = @empId AND IsActive = 1";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@empId", employeeId);
                        await conn.OpenAsync();

                        var result = await cmd.ExecuteScalarAsync();

                        if (result != null && result != DBNull.Value)
                        {
                            byte[] imgData = (byte[])result;
                            using (MemoryStream ms = new MemoryStream(imgData))
                            {
                                pictureBox1.Image = Image.FromStream(ms);
                            }
                        }
                        else
                        {
                            MessageBox.Show("No active image found for this employee.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading image: " + ex.Message);
            }
        }


    }
}
