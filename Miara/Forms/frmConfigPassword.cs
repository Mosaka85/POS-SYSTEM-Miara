using System;
using System.Windows.Forms;

namespace Miara
{
    public partial class frmConfigPassword : Form
    {
        public frmConfigPassword()
        {
            InitializeComponent();
            // Add the KeyDown event handler to the password TextBox
            txtPassword.KeyDown += new KeyEventHandler(txtPassword_KeyDown);
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        // Event handler for the KeyDown event of the password TextBox
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Call the login button click handler when Enter is pressed
                btnEnterConfig_Click(sender, e);
            }
        }

        private void btnEnterConfig_Click(object sender, EventArgs e)
        {
            string password = txtPassword.Text;
            if (password == "9304")
            {
                // Password is correct
                Hide();
                // Open the new form
                new frmConfigurationForm().ShowDialog();
            }
            else
            {
                // Password is incorrect
                MessageBox.Show("Incorrect password. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
