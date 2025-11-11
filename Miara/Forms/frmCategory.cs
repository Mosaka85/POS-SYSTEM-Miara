using System;
using System.Data;
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

        public string ActiveUser { get; private set; }
        public string NameUser { get; private set; }
        public string SurnameUser { get; private set; }
        public int EmployeeID { get; private set; }
        public string currentDevice { get; private set; }

        public frmCategory(string firstName, string surname, int EMID, string mcaddress)
        {
            InitializeComponent();

            ActiveUser = $"User: {firstName} {surname}, EMID: {EMID}";
            NameUser = firstName;
            SurnameUser = surname;
            EmployeeID = EMID;
            currentDevice = mcaddress;

            try
            {
                LoadSQLConnectionInfo();
                _ = SafeLogAsync(currentDevice, ActiveUser, "Category form initialized");
            }
            catch (Exception ex)
            {
                _ = SafeLogAsync(currentDevice, "System", "Category form initialization failed", ex.Message);
                MessageBox.Show($"Initialization error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void frmCategory_Load(object sender, EventArgs e)
        {
            lblUserActive.Text = ActiveUser;
            _ = SafeLogAsync(currentDevice, ActiveUser, "Loading categories form");

            try
            {
                await LoadCategoriesAsync();
                dataGridView2.SelectionChanged += dataGridView2_SelectionChanged;
                _ = SafeLogAsync(currentDevice, ActiveUser, "Categories form loaded successfully");
            }
            catch (Exception ex)
            {
                _ = SafeLogAsync(currentDevice, ActiveUser, "Failed to load categories form", ex.Message);
                MessageBox.Show($"Error loading form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSQLConnectionInfo()
        {
            if (!File.Exists(configFile))
                throw new FileNotFoundException("Config.xml not found in application directory.");

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(LoginInfo));
                using (FileStream fileStream = new FileStream(configFile, FileMode.Open))
                {
                    LoginInfo loginInfo = (LoginInfo)serializer.Deserialize(fileStream);
                    connectionString = $"Data Source={loginInfo.DataSource};Initial Catalog={loginInfo.SelectedDatabase};User ID={loginInfo.Username};Password={loginInfo.Password}";
                }

                _ = SafeLogAsync(currentDevice, "System", "Database connection info loaded");
            }
            catch (Exception ex)
            {
                _ = SafeLogAsync(currentDevice, "System", "Failed to load connection info", ex.Message);
                throw;
            }
        }

        private async Task LoadCategoriesAsync()
        {
            const string query = "SELECT CategoryID, CategoryName, Description, IsActive FROM Categories";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dataGridView2.DataSource = dt;
                if (dataGridView2.Columns.Contains("CategoryID"))
                    dataGridView2.Columns["CategoryID"].Visible = false;

                _ = SafeLogAsync(currentDevice, ActiveUser, $"Loaded {dt.Rows.Count} categories");
            }
        }

        private async void btnaddCategory_Click(object sender, EventArgs e)
        {
            if (!ValidateCategoryInputs())
            {
                MessageBox.Show("Please fill in all category details.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _ = SafeLogAsync(currentDevice, ActiveUser, "Add category validation failed");
                return;
            }

            string categoryName = txtCategoryName.Text.Trim();
            string description = txtCategoryDescription.Text.Trim();
            bool isActive = chkCategoryIsActive.Checked;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();

                    string checkQuery = "SELECT COUNT(*) FROM [Categories] WHERE [CategoryName] = @CategoryName";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@CategoryName", categoryName);

                    int existingCount = (int)await checkCmd.ExecuteScalarAsync();
                    if (existingCount > 0)
                    {
                        MessageBox.Show("Category already exists.", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        _ = SafeLogAsync(currentDevice, ActiveUser, "Duplicate category attempted", categoryName);
                        return;
                    }

                    string insertQuery = @"INSERT INTO [Categories] (CategoryName, Description, IsActive) 
                                           VALUES (@CategoryName, @Description, @IsActive)";
                    SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                    insertCmd.Parameters.AddWithValue("@CategoryName", categoryName);
                    insertCmd.Parameters.AddWithValue("@Description", description);
                    insertCmd.Parameters.AddWithValue("@IsActive", isActive ? 1 : 0);

                    await insertCmd.ExecuteNonQueryAsync();

                    MessageBox.Show("Category added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _ = SafeLogAsync(currentDevice, ActiveUser, $"Category added: {categoryName}");

                    ClearCategoryFields();
                    await LoadCategoriesAsync();
                }
            }
            catch (Exception ex)
            {
                _ = SafeLogAsync(currentDevice, ActiveUser, "Error adding category", ex.Message);
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnCategoryUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a category to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dataGridView2.SelectedRows[0];
            int categoryId = Convert.ToInt32(selectedRow.Cells["CategoryID"].Value);
            string categoryName = txtCategoryName.Text.Trim();
            string description = txtCategoryDescription.Text.Trim();
            bool isActive = chkCategoryIsActive.Checked;

            string query = @"UPDATE [Categories] SET [CategoryName] = @CategoryName, [Description] = @Description, [IsActive] = @IsActive
                             WHERE [CategoryID] = @CategoryID";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CategoryName", categoryName);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@IsActive", isActive ? 1 : 0);
                    cmd.Parameters.AddWithValue("@CategoryID", categoryId);

                    int rows = await cmd.ExecuteNonQueryAsync();
                    MessageBox.Show(rows > 0 ? "Category updated successfully!" : "No changes were made.", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _ = SafeLogAsync(currentDevice, ActiveUser, $"Category updated: {categoryId}");

                    await LoadCategoriesAsync();
                }
            }
            catch (Exception ex)
            {
                _ = SafeLogAsync(currentDevice, ActiveUser, "Error updating category", ex.Message);
                MessageBox.Show($"Error updating: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a category to delete.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dataGridView2.SelectedRows[0];
            int categoryId = Convert.ToInt32(selectedRow.Cells["CategoryID"].Value);

            if (MessageBox.Show("Are you sure you want to delete this category?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                await DeleteCategoryAsync(categoryId);
            }
        }

        private async Task DeleteCategoryAsync(int categoryId)
        {
            string query = "DELETE FROM [Categories] WHERE [CategoryID] = @CategoryID";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CategoryID", categoryId);

                    int rows = await cmd.ExecuteNonQueryAsync();
                    MessageBox.Show(rows > 0 ? "Category deleted successfully!" : "No category found.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _ = SafeLogAsync(currentDevice, ActiveUser, $"Category deleted: {categoryId}");

                    await LoadCategoriesAsync();
                }
            }
            catch (Exception ex)
            {
                _ = SafeLogAsync(currentDevice, ActiveUser, "Error deleting category", ex.Message);
                MessageBox.Show($"Error deleting: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                var row = dataGridView2.SelectedRows[0];
                txtCategoryName.Text = row.Cells["CategoryName"].Value?.ToString() ?? "";
                txtCategoryDescription.Text = row.Cells["Description"].Value?.ToString() ?? "";
                chkCategoryIsActive.Checked = Convert.ToBoolean(row.Cells["IsActive"].Value);
            }
        }

        private bool ValidateCategoryInputs() =>
            !string.IsNullOrWhiteSpace(txtCategoryName.Text) &&
            !string.IsNullOrWhiteSpace(txtCategoryDescription.Text);

        private void ClearCategoryFields()
        {
            txtCategoryName.Clear();
            txtCategoryDescription.Clear();
            chkCategoryIsActive.Checked = true;
        }

        private void btnExit_Click(object sender, EventArgs e) => Close();
        private void btnBack_Click(object sender, EventArgs e) => Close();
        private void btnNewCate_Click(object sender, EventArgs e) => ClearCategoryFields();

        private async void frmCategory_FormClosing(object sender, FormClosingEventArgs e)
        {
            _ = SafeLogAsync(currentDevice, ActiveUser, "Category form closing");
        }

        // ✅ Safe Logging Helper (never crashes UI)
        private async Task SafeLogAsync(string device, string employee, string stepDescription, string errorMessage = null)
        {
            if (string.IsNullOrEmpty(connectionString)) return;

            const string query = @"INSERT INTO DeviceAudit (Device, Employee, AuditDate, StepDescription, ErrorMessage)
                                   VALUES (@Device, @Employee, GETDATE(), @StepDescription, @ErrorMessage)";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Device", device ?? "Unknown");
                    cmd.Parameters.AddWithValue("@Employee", employee ?? "Unknown");
                    cmd.Parameters.AddWithValue("@StepDescription", stepDescription ?? "N/A");
                    cmd.Parameters.AddWithValue("@ErrorMessage", errorMessage ?? "");

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch
            {
                // fallback: write to local text file if logging DB fails
                try
                {
                    string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogErrors.txt");
                    await Task.Run(() =>
                    {
                        File.AppendAllText(logPath,
                            $"{DateTime.Now}: {employee} - {stepDescription} - {errorMessage}{Environment.NewLine}");
                    });
                }
                catch { /* ignored */ }
            }

        }

        private void EditCategory_Click(object sender, EventArgs e)
        { 


        }
    }

}
