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
                color: #007bff;
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
            return rand.Next(100000000, 199999999).ToString();
        }

        private void btnSendOtp_Click(object sender, EventArgs e)
        {
            SendPasswordResetOTP(txtUsername.Text);
            MessageBox.Show("Reset Password email sent Email  Sent");
            lblEmailsent.Text = $"Email sent to {userEmail}";
            btnSendOtp.Enabled = false;
            flowLayoutPanel1.Visible = true;

        }

        private void btnSaveupdate_Click(object sender, EventArgs e)
        {
            if (txtOTPEmail.Text == generatedOTP)
            {
                if (txtPassword.Text == txtConfirmPassword.Text && !string.IsNullOrEmpty(txtPassword.Text))
                {
                    string hashedPassword = HashPassword(txtPassword.Text);
                    string query = "UPDATE Employees SET PasswordHash = @password WHERE Username = @username";

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@password", hashedPassword);
                        cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Password successfully changed.");
                    flowLayoutPanel1.Visible = false;
                }
                else
                {
                    MessageBox.Show("Passwords do not match or are empty.");
                }
            }
            else
            {
                MessageBox.Show("Invalid OTP. Please try again.");
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
            btnSendOtp.Enabled = true;
        }

        private void frmResetPassword_Load(object sender, EventArgs e)
        {
            flowLayoutPanel1.Visible = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
