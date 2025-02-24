using System;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Miara
{
    public partial class frmCategory : Form
    {
        private static readonly string configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config.xml");
        private string connectionString;

        public frmCategory()
        {
            InitializeComponent();
            LoadSQLConnectionInfo();
        }

        private async void frmCategory_Load(object sender, EventArgs e)
        {
            await LoadCategoriesAsync(); // Load categories asynchronously
            dataGridView2.SelectionChanged += dataGridView2_SelectionChanged;
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
                MessageBox.Show("Failed to load connection information: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadCategoriesAsync()
        {
            string query = "SELECT CategoryID, CategoryName, Description, IsActive FROM Categories";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        System.Data.DataTable dt = new System.Data.DataTable();
                        adapter.Fill(dt);

                        dataGridView2.DataSource = dt;
                        dataGridView2.Columns["CategoryID"].Visible = false; // Hide CategoryID column
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnaddCategory_Click(object sender, EventArgs e)
        {
            if (!ValidateCategoryInputs())
            {
                MessageBox.Show("Please fill in all category details.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string categoryName = txtCategoryName.Text.Trim();
                string description = txtCategoryDescription.Text.Trim();
                bool isActive = chkCategoryIsActive.Checked;

                string query = @"INSERT INTO [Categories] 
                                ([CategoryName], [Description], [IsActive]) 
                                VALUES (@CategoryName, @Description, @IsActive)";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CategoryName", categoryName);
                        cmd.Parameters.AddWithValue("@Description", description);
                        cmd.Parameters.AddWithValue("@IsActive", isActive ? 1 : 0);

                        await cmd.ExecuteNonQueryAsync();
                        MessageBox.Show("Category added successfully!");

                        // Reload categories to update the grid
                        await LoadCategoriesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding category: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnCategoryUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a category to update.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dataGridView2.SelectedRows[0];
            int categoryId = Convert.ToInt32(selectedRow.Cells["CategoryID"].Value);

            if (!ValidateCategoryInputs())
            {
                MessageBox.Show("Please fill in all category details.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string categoryName = txtCategoryName.Text.Trim();
            string description = txtCategoryDescription.Text.Trim();
            bool isActive = chkCategoryIsActive.Checked;


            try
            {
                string query = @"UPDATE [Categories] 
                                 SET [CategoryName] = @CategoryName, [Description] = @Description, [IsActive] = @IsActive
                                 WHERE [CategoryID] = @CategoryID";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CategoryName", categoryName);
                        cmd.Parameters.AddWithValue("@Description", description);
                        cmd.Parameters.AddWithValue("@IsActive", isActive ? 1 : 0);
                        cmd.Parameters.AddWithValue("@CategoryID", categoryId);

                        int rowsAffected = await cmd.ExecuteNonQueryAsync();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Category updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            await LoadCategoriesAsync(); // Reload categories to update the grid
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
                MessageBox.Show($"Error updating category: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView2.SelectedRows[0];
                txtCategoryName.Text = selectedRow.Cells["CategoryName"].Value?.ToString() ?? "";
                txtCategoryDescription.Text = selectedRow.Cells["Description"].Value?.ToString() ?? "";
                chkCategoryIsActive.Checked = Convert.ToBoolean(selectedRow.Cells["IsActive"].Value);
            }
            SetFormFieldsEnabled(false);
        }

        private void EditCategory_Click(object sender, EventArgs e)
        {
            SetFormFieldsEnabled(true);
        }

        private void SetFormFieldsEnabled(bool enabled)
        {
            txtCategoryName.Enabled = enabled;
            txtCategoryDescription.Enabled = enabled;
            chkCategoryIsActive.Enabled = enabled;
        }

        private bool ValidateCategoryInputs()
        {
            return !string.IsNullOrEmpty(txtCategoryName.Text) &&
                   !string.IsNullOrEmpty(txtCategoryDescription.Text);
        }
    }
}