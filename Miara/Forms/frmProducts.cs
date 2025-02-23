using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Miara
{
    public partial class frmProducts : Form
    {
        private static readonly string configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config.xml");// Adjust path as needed
        private string connectionString;

        public frmProducts()
        {
            InitializeComponent();
            LoadSQLConnectionInfo();
        }

        private void frmProducts_Load(object sender, EventArgs e)
        {
            LoadProducts();

        }

        private void LoadProducts()
        {
            string query = "SELECT ProductID, ProductName, CategoryID, Price, StockQuantity, Description, IsActive FROM [Products]";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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


        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            if (ComboCategoryID.SelectedValue == null || string.IsNullOrEmpty(txtProductName.Text) ||
                string.IsNullOrEmpty(txtPrice.Text) || string.IsNullOrEmpty(txtStockQuantity.Text))
            {
                MessageBox.Show("Please fill in all product details.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int categoryId = (int)ComboCategoryID.SelectedValue;
                string productName = txtProductName.Text.Trim();
                decimal price = decimal.Parse(txtPrice.Text.Trim());
                int stockQuantity = int.Parse(txtStockQuantity.Text.Trim());
                string description = txtDescription.Text.Trim();
                bool isActive = chkIsActive.Checked;

                string query = @"INSERT INTO [Products] 
                                ([ProductName], [CategoryID], [Price], [StockQuantity], [Description], [IsActive]) 
                                VALUES (@ProductName, @CategoryID, @Price, @StockQuantity, @Description, @IsActive)";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductName", productName);
                        cmd.Parameters.AddWithValue("@CategoryID", categoryId);
                        cmd.Parameters.AddWithValue("@Price", price);
                        cmd.Parameters.AddWithValue("@StockQuantity", stockQuantity);
                        cmd.Parameters.AddWithValue("@Description", description);
                        cmd.Parameters.AddWithValue("@IsActive", isActive ? 1 : 0);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Product added successfully!");
                        LoadProducts();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding product: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      
    }
}
