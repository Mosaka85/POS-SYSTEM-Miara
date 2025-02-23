using System;
using System.Windows.Forms;

namespace Miara
{
    public partial class frmMainForm : Form
    {
        public frmMainForm(string firstName, string surname, int EMID)
        {
            InitializeComponent();

            label1.Text = $"WELCOME {firstName} {surname}   CHECK {EMID}";
            employeeFirstName = firstName;
            employeeSurname = surname;
            EmployeeNumber = EMID;
            this.FormClosed += (sender, e) => Application.Exit();

        }

        private void button1_Click(object sender, EventArgs e)
        {

            new frmEmployeeDetails().ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private string employeeFirstName;
        private string employeeSurname;
        private int EmployeeNumber;

        private void button3_Click_1(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmProducts newForm = new frmProducts();
            newForm.ShowDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Hide();
            new frmSales(employeeFirstName, employeeSurname, EmployeeNumber).ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Hide();
            new frmEmployeeDetails().ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void frmMainForm_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            new frmReports().ShowDialog();
        }
    }
}
