using System;
using System.Windows.Forms;

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
            frmProducts newForm = new frmProducts(employeeFirstName, employeeSurname, EmployeeNumber);
            newForm.ShowDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            new frmSales(employeeFirstName, employeeSurname, EmployeeNumber).ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
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
            new frmEmployeeDetails().ShowDialog();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            new frmCategory(employeeFirstName, employeeSurname, EmployeeNumber).ShowDialog();
        }
    }
}
