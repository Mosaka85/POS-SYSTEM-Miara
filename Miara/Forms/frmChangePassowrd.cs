using System;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Miara
{
    public partial class frmResetPassword : Form
    {
        private static readonly string configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config.xml");
        private string connectionString;
        private string generatedOTP;
        private string userEmail;

        public frmResetPassword()
        {
            InitializeComponent();
            LoadSQLConnectionInfo();
            btnSendOtp.Enabled = false; // Disable button until username is entered
            txtUsername.TextChanged += txtUsername_TextChanged;
            this.FormClosing += frmResetPassword_FormClosing;
        }

        private void LoadSQLConnectionInfo()
        {
            if (!File.Exists(configFile))
            {
                MessageBox.Show("Connection configuration file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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
                MessageBox.Show($"Failed to load connection information: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string GetUserEmail(string username)
        {
            string query = "SELECT TOP 1 Email FROM Employees WHERE Username = @username";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@username", username);
                conn.Open();
                object result = cmd.ExecuteScalar();
                return result?.ToString();
            }
        }

        public void SendPasswordResetOTP(string username)
        {
            string email = GetUserEmail(username);
            userEmail = email;
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("User not found or no email available.");
                return;
            }

            generatedOTP = GenerateOTP();
            string subject = "Password Reset OTP";

            // HTML content for the email
            string receiptContent = $@"
    <html>
    <head>
        <style>
            body {{
                font-family: Arial, sans-serif;
                background-color: #f4f4f4;
                color: #333;
                padding: 20px;
            }}
            .container {{
                background-color: #fff;
                padding: 20px;
                border-radius: 5px;
                box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            }}
            .otp {{
                font-size: 24px;
                font-weight: bold;
                color: #2874A6; /* Using our primary color */
            }}
            .disclaimer {{
                font-size: 12px;
                color: #777;
                margin-top: 20px;
            }}
        </style>
    </head>
    <body>
        <div class='container'>
            <h2>Password Reset OTP</h2>
            <p>Your One-Time Password (OTP) for resetting your password is:</p>
            <p class='otp'>{generatedOTP}</p>
            <p>Please use this OTP to reset your password. This OTP is valid for a limited time only.</p>
            <div class='disclaimer'>
                <p><strong>Disclaimer:</strong> Do not share this OTP with anyone. If you did not request this OTP, please contact our support team immediately.</p>
            </div>
        </div>
    </body>
    </html>";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SendEmailtoUser", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@receiptContent", receiptContent);
                cmd.Parameters.AddWithValue("@toEmail", email);
                cmd.Parameters.AddWithValue("@Subject", subject);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private string GenerateOTP()
        {
            Random rand = new Random();
            return rand.Next(100000, 999999).ToString(); // 6-digit OTP
        }

        private void btnSendOtp_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                MessageBox.Show("Please enter your username first.");
                return;
            }

            try
            {
                SendPasswordResetOTP(txtUsername.Text);
                lblEmailsent.Text = $"OTP sent to {userEmail}";
                lblEmailsent.Visible = true;
                btnSendOtp.Enabled = false;
                btnSendOtp.Enabled= false; // Disable button after sending OTP
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending OTP: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSaveupdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtOTPEmail.Text))
            {
                MessageBox.Show("Please enter the OTP first.");
                return;
            }

            if (txtOTPEmail.Text != generatedOTP)
            {
                MessageBox.Show("Invalid OTP. Please try again.");
                return;
            }

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Please enter a new password.");
                return;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            try
            {
                string hashedPassword = HashPassword(txtPassword.Text);
                string query = "UPDATE Employees SET PasswordHash = @password WHERE Username = @username";

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@password", hashedPassword);
                    cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Password successfully changed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                        Application.Restart(); // Restart the application to apply changes
                    }
                    else
                    {
                        MessageBox.Show("Failed to update password. User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating password: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                StringBuilder hashBuilder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    hashBuilder.Append(b.ToString("x2"));
                }
                return hashBuilder.ToString();
            }
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            lblEmailsent.Visible = false;
            btnSendOtp.Enabled = !string.IsNullOrEmpty(txtUsername.Text);
            btnSendOtp.Enabled = true; // Enable button when username is entered
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Restart();
        }

        private void frmResetPassword_Load(object sender, EventArgs e)
        {
            lblEmailsent.Visible = false;
        }
        private void frmResetPassword_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit(); // Ensure the application exits when the form is closed
        }
    }
}