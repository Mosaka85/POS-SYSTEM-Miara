using System;
using System.Data.SqlClient;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Miara
{
    public partial class frmCategory : Form
    {
        private static readonly string configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config.xml");
        private string connectionString;
        

        public frmCategory(string firstName, string surname, int EMID, string mcaddress )
        {
            InitializeComponent();
            try
            {
                LoadSQLConnectionInfo();
                ActiveUser = $"User: {firstName} {surname}, EMID: {EMID}";
                NameUser = firstName;
                SurnameUser = surname;
                EmployeeID = EMID;
                _ = LogAuditEntryAsync(currentDevice, ActiveUser, "Category form initialized");
            }
            catch (Exception ex)
            {
                _ = LogAuditEntryAsync(currentDevice, "System", "Category form initialization failed", ex.Message);
                MessageBox.Show($"Initialization error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string ActiveUser { get; private set; }
        public string NameUser { get; private set; }
        public string SurnameUser { get; private set; }
        public int EmployeeID { get; private set; }
        public string currentDevice { get; private set; }

        private async void frmCategory_Load(object sender, EventArgs e)
        {
            try
            {
                await LogAuditEntryAsync(currentDevice, ActiveUser, "Loading categories form");
                lblUserActive.Text = ActiveUser;
                await LoadCategoriesAsync();
                dataGridView2.SelectionChanged += dataGridView2_SelectionChanged;
                await LogAuditEntryAsync(currentDevice, ActiveUser, "Categories form loaded successfully");
            }
            catch (Exception ex)
            {
                await LogAuditEntryAsync(currentDevice, ActiveUser, "Failed to load categories form", ex.Message);
                MessageBox.Show($"Error loading form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSQLConnectionInfo()
        {
            try
            {
                if (!File.Exists(configFile))
                {
                    _ = LogAuditEntryAsync(currentDevice, "System", "Config file not found", "Configuration file missing");
                    throw new FileNotFoundException("Connection configuration file not found.");
                }

                XmlSerializer serializer = new XmlSerializer(typeof(LoginInfo));
                using (FileStream fileStream = new FileStream(configFile, FileMode.Open))
                {
                    LoginInfo loginInfo = (LoginInfo)serializer.Deserialize(fileStream);
                    connectionString = $"Data Source={loginInfo.DataSource};Initial Catalog={loginInfo.SelectedDatabase};User ID={loginInfo.Username};Password={loginInfo.Password}";
                    _ = LogAuditEntryAsync(currentDevice, "System", "Database connection info loaded");
                }
            }
            catch (Exception ex)
            {
                _ = LogAuditEntryAsync(currentDevice, "System", "Failed to load connection info", ex.Message);
                MessageBox.Show("Failed to load connection information: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private async Task LoadCategoriesAsync()
        {
            string query = "SELECT CategoryID, CategoryName, Description, IsActive FROM Categories";

            try
            {
                await LogAuditEntryAsync(currentDevice, ActiveUser, "Starting to load categories");

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        System.Data.DataTable dt = new System.Data.DataTable();
                        adapter.Fill(dt);

                        dataGridView2.DataSource = dt;
                        dataGridView2.Columns["CategoryID"].Visible = false;

                        await LogAuditEntryAsync(currentDevice, ActiveUser,
                            $"Successfully loaded {dt.Rows.Count} categories");
                    }
                }
            }
            catch (Exception ex)
            {
                await LogAuditEntryAsync(currentDevice, ActiveUser, "Failed to load categories", ex.Message);
                MessageBox.Show($"Error loading categories: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnaddCategory_Click(object sender, EventArgs e)
        {
            if (!ValidateCategoryInputs())
            {
                await LogAuditEntryAsync(currentDevice, ActiveUser, "Add category validation failed", "Missing required fields");
                MessageBox.Show("Please fill in all category details.", "Validation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string categoryName = txtCategoryName.Text.Trim();
            string description = txtCategoryDescription.Text.Trim();
            bool isActive = chkCategoryIsActive.Checked;

            try
            {
                await LogAuditEntryAsync(currentDevice, ActiveUser, "Starting category addition process");

                string checkQuery = @"SELECT COUNT(*) FROM [Categories] 
                            WHERE [CategoryName] = @CategoryName";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();

                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@CategoryName", categoryName);
                        int existingCount = (int)await checkCmd.ExecuteScalarAsync();

                        if (existingCount > 0)
                        {
                            await LogAuditEntryAsync(currentDevice, ActiveUser, "Add category failed", "Category already exists");
                            MessageBox.Show("Category already exists. Please use a different name.",
                                          "Duplicate Category",
                                          MessageBoxButtons.OK,
                                          MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string insertQuery = @"INSERT INTO [Categories] 
                                 ([CategoryName], [Description], [IsActive]) 
                                 VALUES (@CategoryName, @Description, @IsActive)";

                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@CategoryName", categoryName);
                        insertCmd.Parameters.AddWithValue("@Description", description);
                        insertCmd.Parameters.AddWithValue("@IsActive", isActive ? 1 : 0);

                        await insertCmd.ExecuteNonQueryAsync();
                        await LogAuditEntryAsync(currentDevice, ActiveUser,
                            $"Category added successfully: {categoryName}");

                        MessageBox.Show("Category added successfully!",
                                      "Success",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Information);

                        ClearCategoryFields();
                        await LoadCategoriesAsync();
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                await LogAuditEntryAsync(currentDevice, ActiveUser, "Database error adding category", sqlEx.Message);
                MessageBox.Show($"Database error: {sqlEx.Message}",
                              "Database Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                await LogAuditEntryAsync(currentDevice, ActiveUser, "Error adding category", ex.Message);
                MessageBox.Show($"Error adding category: {ex.Message}",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        }

        private async void btnCategoryUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0)
            {
                await LogAuditEntryAsync(currentDevice, ActiveUser, "Update failed", "No category selected");
                MessageBox.Show("Please select a category to update.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateCategoryInputs())
            {
                await LogAuditEntryAsync(currentDevice, ActiveUser, "Update validation failed", "Missing required fields");
                MessageBox.Show("Please fill in all category details.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dataGridView2.SelectedRows[0];
            int categoryId = Convert.ToInt32(selectedRow.Cells["CategoryID"].Value);
            string categoryName = txtCategoryName.Text.Trim();
            string description = txtCategoryDescription.Text.Trim();
            bool isActive = chkCategoryIsActive.Checked;

            try
            {
                await LogAuditEntryAsync(currentDevice, ActiveUser,
                    $"Starting update for category ID: {categoryId}");

                string query = @"UPDATE [Categories] 
                                 SET [CategoryName] = @CategoryName, 
                                     [Description] = @Description, 
                                     [IsActive] = @IsActive
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
                            await LogAuditEntryAsync(currentDevice, ActiveUser,
                                $"Successfully updated category ID: {categoryId}");
                            MessageBox.Show("Category updated successfully!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            await LoadCategoriesAsync();
                        }
                        else
                        {
                            await LogAuditEntryAsync(currentDevice, ActiveUser,
                                $"No changes made to category ID: {categoryId}");
                            MessageBox.Show("No changes were made.", "Update Failed",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await LogAuditEntryAsync(currentDevice, ActiveUser,
                    $"Error updating category ID: {categoryId}", ex.Message);
                MessageBox.Show($"Error updating category: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0)
            {
                await LogAuditEntryAsync(currentDevice, ActiveUser, "Delete failed", "No category selected");
                MessageBox.Show("Please select a category to delete.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dataGridView2.SelectedRows[0];
            int categoryId = Convert.ToInt32(selectedRow.Cells["CategoryID"].Value);

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this category? This action cannot be undone.",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                await DeleteCategoryAsync(categoryId);
            }
            else
            {
                await LogAuditEntryAsync(currentDevice, ActiveUser,
                    $"Delete canceled for category ID: {categoryId}");
            }
        }

        private async Task DeleteCategoryAsync(int categoryId)
        {
            string query = @"DELETE FROM [Categories] WHERE [CategoryID] = @CategoryID";

            try
            {
                await LogAuditEntryAsync(currentDevice, ActiveUser,
                    $"Attempting to delete category ID: {categoryId}");

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CategoryID", categoryId);
                        int rowsAffected = await cmd.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            await LogAuditEntryAsync(currentDevice, ActiveUser,
                                $"Successfully deleted category ID: {categoryId}");
                            MessageBox.Show("Category deleted successfully!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            await LoadCategoriesAsync();
                        }
                        else
                        {
                            await LogAuditEntryAsync(currentDevice, ActiveUser,
                                $"No category found with ID: {categoryId}");
                            MessageBox.Show("No category found with the specified ID.", "Deletion Failed",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await LogAuditEntryAsync(currentDevice, ActiveUser,
                    $"Error deleting category ID: {categoryId}", ex.Message);
                MessageBox.Show($"Error deleting category: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void ClearCategoryFields()
        {
            txtCategoryName.Text = "";
            txtCategoryDescription.Text = "";
            chkCategoryIsActive.Checked = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNewCate_Click(object sender, EventArgs e)
        {
            ClearCategoryFields();
        }

        private async void frmCategory_FormClosing(object sender, FormClosingEventArgs e)
        {
            await LogAuditEntryAsync(currentDevice, ActiveUser, "Category form closing");
        }

        public async Task LogAuditEntryAsync(string device, string employee, string stepDescription, string errorMessage = null)
        {
            string query = @"
                INSERT INTO DeviceAudit (Device, Employee, AuditDate, StepDescription, ErrorMessage)
                VALUES (@Device, @Employee, @AuditDate, @StepDescription, @ErrorMessage);";

            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Device", device);
                    command.Parameters.AddWithValue("@Employee", employee);
                    command.Parameters.AddWithValue("@AuditDate", DateTime.Now);
                    command.Parameters.AddWithValue("@StepDescription", stepDescription);
                    command.Parameters.AddWithValue("@ErrorMessage",
                        string.IsNullOrEmpty(errorMessage) ? DBNull.Value : (object)errorMessage);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                // If logging fails, we can't log that it failed, so we'll just show a message
                MessageBox.Show($"Failed to log audit entry: {ex.Message}", "Logging Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }

  
}