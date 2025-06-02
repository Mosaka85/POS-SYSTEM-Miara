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
        private static readonly string configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config.xml");
        private string connectionString;

        public frmProducts(string firstName, string surname, int EMID)
        {
            InitializeComponent();
            LoadSQLConnectionInfo();
            ActiveUser = $"User: {firstName} {surname} ,  EMID: {EMID} ";
            NameUser = firstName;
            SurnameUSer = surname;
            EmployeeID = EMID;
            this.FormClosed += (sender, e) => Application.Exit();

        }

        public string ActiveUser = "";
        public string NameUser = "";
        public string SurnameUSer = "";
        public int EmployeeID = 0;

        private void frmProducts_Load(object sender, EventArgs e)
        {
            
            
            
            LoadCategories(); // Ensure category names are loaded first
            LoadProducts();
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            lblUserActive.Text = ActiveUser;


        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];

                txtProductName.Text = row.Cells["ProductName"].Value?.ToString() ?? "";
                txtPrice.Text = row.Cells["Price"].Value?.ToString() ?? "";
                txtStockQuantity.Text = row.Cells["StockQuantity"].Value?.ToString() ?? "";
                txtDescription.Text = row.Cells["Description"].Value?.ToString() ?? "";
                chkIsActive.Checked = row.Cells["IsActive"].Value != DBNull.Value && Convert.ToBoolean(row.Cells["IsActive"].Value);
                txtBarcodeLabel.Text = row.Cells["BARCODE"].Value?.ToString() ?? "";

                // Select the correct category in ComboCategoryID
                if (row.Cells["CategoryName"].Value != null)
                {
                    ComboCategoryID.Text = row.Cells["CategoryName"].Value.ToString(); // Match category name
                }

                txtProductName.Enabled = false;
                txtPrice.Enabled = false;
                txtStockQuantity.Enabled = false;
                txtDescription.Enabled = false;
                chkIsActive.Enabled = false;
                ComboCategoryID.Enabled = false;
                txtBarcodeLabel.Enabled = false;
            }
        }


        private void LoadProducts()
        {
            string query = @"
                SELECT p.ProductID, p.ProductName, c.CategoryName, p.Price, 
                       p.StockQuantity, p.Description, p.IsActive , [BARCODE]
                FROM Products p
                INNER JOIN Categories c ON p.CategoryID = c.CategoryID"; // Join to get CategoryName

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
                int categoryId = Convert.ToInt32(ComboCategoryID.SelectedValue);
                string productName = txtProductName.Text.Trim();
                decimal price = decimal.Parse(txtPrice.Text.Trim());
                int stockQuantity = int.Parse(txtStockQuantity.Text.Trim());
                string description = txtDescription.Text.Trim();
                bool isActive = chkIsActive.Checked;
                string barcode = txtBarcodeLabel.Text.Trim();

                string checkQuery = @"
            SELECT COUNT(*) 
            FROM [Products] 
            WHERE [ProductName] = @ProductName 
            AND [Description] = @Description 
            AND [Price] = @Price";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // First, check for duplicates
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@ProductName", productName);
                        checkCmd.Parameters.AddWithValue("@Description", description);
                        checkCmd.Parameters.AddWithValue("@Price", price);

                        int existingCount = (int)checkCmd.ExecuteScalar();

                        if (existingCount > 0)
                        {
                            MessageBox.Show("This product already exists!", "Duplicate Product", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string insertQuery = @"
                INSERT INTO [Products] 
                ([ProductName], [CategoryID], [Price], [StockQuantity], [Description], [IsActive], [BARCODE]) 
                VALUES (@ProductName, @CategoryID, @Price, @StockQuantity, @Description, @IsActive , @BARCODE)";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductName", productName);
                        cmd.Parameters.AddWithValue("@CategoryID", categoryId);
                        cmd.Parameters.AddWithValue("@Price", price);
                        cmd.Parameters.AddWithValue("@StockQuantity", stockQuantity);
                        cmd.Parameters.AddWithValue("@Description", description);
                        cmd.Parameters.AddWithValue("@IsActive", isActive ? 1 : 0);
                        cmd.Parameters.AddWithValue("@BARCODE", barcode);

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

        private void LoadCategories()
        {
            string query = "SELECT CategoryID, CategoryName FROM Categories";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable categoryTable = new DataTable();
                        adapter.Fill(categoryTable);

                        ComboCategoryID.DataSource = categoryTable;
                        ComboCategoryID.DisplayMember = "CategoryName"; // Show category name
                        ComboCategoryID.ValueMember = "CategoryID"; // Store ID internally
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            int productId = Convert.ToInt32(selectedRow.Cells["ProductID"].Value); // Get ProductID

            // Enable fields for editing
            txtProductName.Enabled = true;
            txtPrice.Enabled = true;
            txtStockQuantity.Enabled = true;
            txtDescription.Enabled = true;
            chkIsActive.Enabled = true;
            ComboCategoryID.Enabled = true;

            // Confirm update
            if (MessageBox.Show("Are you sure you want to update this product?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            try
            {
                // Ensure all required fields are filled
                if (ComboCategoryID.SelectedValue == null || string.IsNullOrEmpty(txtProductName.Text) ||
                    string.IsNullOrEmpty(txtPrice.Text) || string.IsNullOrEmpty(txtStockQuantity.Text))
                {
                    MessageBox.Show("Please fill in all product details.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }



                string productName = txtProductName.Text.Trim();
                int categoryId = Convert.ToInt32(ComboCategoryID.SelectedValue);
                decimal price = decimal.Parse(txtPrice.Text.Trim());
                int stockQuantity = int.Parse(txtStockQuantity.Text.Trim());
                string description = txtDescription.Text.Trim();
                bool isActive = chkIsActive.Checked;

                string query = @"UPDATE Products 
                         SET ProductName = @ProductName, 
                             CategoryID = @CategoryID, 
                             Price = @Price, 
                             StockQuantity = @StockQuantity, 
                             Description = @Description, 
                             IsActive = @IsActive 
                         WHERE ProductID = @ProductID";

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
                        cmd.Parameters.AddWithValue("@ProductID", productId);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Product updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadProducts(); // Refresh the product list
                        }
                        else
                        {
                            MessageBox.Show("No changes were made.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating product: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Disable fields again after updating
                txtProductName.Enabled = false;
                txtPrice.Enabled = false;
                txtStockQuantity.Enabled = false;
                txtDescription.Enabled = false;
                chkIsActive.Enabled = false;
                ComboCategoryID.Enabled = false;
            }
        }


        private void btnEditproduct_Click(object sender, EventArgs e)
        {
            txtProductName.Enabled = true;
            txtPrice.Enabled = true;
            txtStockQuantity.Enabled = true;
            txtDescription.Enabled = true;
            chkIsActive.Enabled = true;
            ComboCategoryID.Enabled = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Hide();
            new frmMainForm(NameUser, SurnameUSer, EmployeeID).ShowDialog();
        }

        private void btnDeactivate_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product to deactivate.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {

                int productId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ProductID"].Value);

                // Confirm deactivation
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to deactivate this product?",
                    "Confirm Deactivation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    string query = @"
                UPDATE [Products] 
                SET [IsActive] = 0 
                WHERE [ProductID] = @ProductID";

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@ProductID", productId);
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Product deactivated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadProducts();
                            }
                            else
                            {
                                MessageBox.Show("Product not found or already deactivated.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deactivating product: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNewProduct_Click(object sender, EventArgs e)
        {
            // Enable all controls (if needed)
            txtProductName.Enabled = true;
            txtPrice.Enabled = true;
            txtStockQuantity.Enabled = true;
            txtDescription.Enabled = true;
            chkIsActive.Enabled = true;
            ComboCategoryID.Enabled = true;
            txtBarcodeLabel.Enabled = true;

            // Clear all input fields
            txtProductName.Clear();
            txtPrice.Clear();
            txtStockQuantity.Clear();
            txtDescription.Clear();
            chkIsActive.Checked = true; // Default to "Active" (or false if preferred)
            ComboCategoryID.SelectedIndex = -1; // Reset dropdown (no selection)
            txtBarcodeLabel.Clear();
            // Optional: Set focus to the first field for quick typing
            txtProductName.Focus();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
