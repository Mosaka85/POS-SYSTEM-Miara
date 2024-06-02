using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Xml.Serialization;

namespace Miara
{
    public partial class frmEmployeeDetails : Form
    {
        private const string configFile = @"C:\Users\TSHEP\source\repos\Miara\Miara\Config.xml"; // Adjust path as needed
        private string SQLservername;
        private string SQLDatabase;
        private string SQLUsername;
        private string SQLPassword;
        private DataView employeeDataView;
        private DataTable employeeDataTable;

        public frmEmployeeDetails()
        {
            InitializeComponent();
            LoadSQLConnectionInfo();
            LoadEmployeeData();
            LoadDepartments();
            txtEmployeeEmail.Enabled = false;
            txtEmployeeFirstname.Enabled = false;
            txtEmployeeID.Enabled = false;
            txtEmployeeSurname.Enabled = false;
            txtlogInEmployee.Enabled = false;
            lblPhone.Enabled = false;
            txtEmployeePhone.Enabled = false;
            comboEmployeeDepartment.Enabled = false;
            dataGridEmployeeList.SelectionChanged += new EventHandler(dataGridEmployeeList_SelectionChanged);
            dataGridEmployeeList.CellDoubleClick += new DataGridViewCellEventHandler(dataGridEmployeeList_CellDoubleClick);
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
                        SQLservername = loginInfo.DataSource;
                        SQLDatabase = loginInfo.SelectedDatabase;
                        SQLUsername = loginInfo.Username;
                        SQLPassword = loginInfo.Password;
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

        private void LoadEmployeeData()
        {
            if (string.IsNullOrEmpty(SQLservername) || string.IsNullOrEmpty(SQLDatabase) || string.IsNullOrEmpty(SQLUsername) || string.IsNullOrEmpty(SQLPassword))
            {
                MessageBox.Show("Missing SQL connection information.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string connectionString = $"Data Source={SQLservername};Initial Catalog={SQLDatabase};User ID={SQLUsername};Password={SQLPassword};";
            string query = "SELECT TOP (1000) [ID], [Name], [Surname], [Active], [Department], [LogInName], [Password], [Admin_Indicator], [Email_Address], [PhoneNumber], [SiteManager] FROM [MiaradbBlue].[dbo].[Employees]";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    employeeDataTable = new DataTable();
                    adapter.Fill(employeeDataTable);
                    DataGridViewCheckBoxColumn activeColumn = new DataGridViewCheckBoxColumn();
                    activeColumn.Name = "Active";
                    activeColumn.HeaderText = "Active";
                    activeColumn.DataPropertyName = "Active";

                    DataGridViewCheckBoxColumn siteManagerColumn = new DataGridViewCheckBoxColumn();
                    siteManagerColumn.Name = "SiteManager";
                    siteManagerColumn.HeaderText = "Site Manager";
                    siteManagerColumn.DataPropertyName = "SiteManager";

                    DataGridViewCheckBoxColumn adminIndicatorColumn = new DataGridViewCheckBoxColumn();
                    adminIndicatorColumn.Name = "Admin_Indicator";
                    adminIndicatorColumn.HeaderText = "Admin Indicator";
                    adminIndicatorColumn.DataPropertyName = "Admin_Indicator";

  
                    dataGridEmployeeList.DataSource = employeeDataTable;

                    if (dataGridEmployeeList.Columns.Contains("Active"))
                        dataGridEmployeeList.Columns.Remove("Active");
                    if (dataGridEmployeeList.Columns.Contains("SiteManager"))
                        dataGridEmployeeList.Columns.Remove("SiteManager");
                    if (dataGridEmployeeList.Columns.Contains("Admin_Indicator"))
                        dataGridEmployeeList.Columns.Remove("Admin_Indicator");

                    dataGridEmployeeList.Columns.Add(activeColumn);
                    dataGridEmployeeList.Columns.Add(siteManagerColumn);
                    dataGridEmployeeList.Columns.Add(adminIndicatorColumn);

                    dataGridEmployeeList.Columns["ID"].Visible = false;
                    dataGridEmployeeList.Columns["Password"].Visible = false;

                    employeeDataView = new DataView(employeeDataTable);

                    ApplyFilters();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void checkBoxActiveEmployee_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxActiveEmployee.Checked)
            {
                checkBoxAllemployees.Checked = false;
                checkBoxInActiveEmployees.Checked = false;
            }
            ApplyFilters();
        }

        private void checkBoxInActiveEmployees_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxInActiveEmployees.Checked)
            {
                checkBoxAllemployees.Checked = false;
                checkBoxActiveEmployee.Checked = false;
            }
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            if (employeeDataTable == null || employeeDataView == null)
                return;

            string filterExpression = "";

            if (checkBoxAllemployees.Checked)
            {

                filterExpression = ""; 
            }
            else
            {
                if (checkBoxActiveEmployee.Checked && !checkBoxInActiveEmployees.Checked)
                {
                    filterExpression = "[Active] = 1"; 
                }
                else if (!checkBoxActiveEmployee.Checked && checkBoxInActiveEmployees.Checked)
                {
                    filterExpression = "[Active] = 0";
                }
            }

            employeeDataView.RowFilter = filterExpression;

            dataGridEmployeeList.DataSource = employeeDataView;
        }

        private void checkBoxAllemployees_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAllemployees.Checked)
            {
                checkBoxActiveEmployee.Checked = false;
                checkBoxInActiveEmployees.Checked = false;
            }
            ApplyFilters();
            checkBoxActiveEmployee.Enabled = !checkBoxAllemployees.Checked;
            checkBoxInActiveEmployees.Enabled = !checkBoxAllemployees.Checked;
        }

        private void btnSaveNewEmployee_Click(object sender, EventArgs e)
        {
            string connectionString = $"Data Source={SQLservername};Initial Catalog={SQLDatabase};User ID={SQLUsername};Password={SQLPassword};";
            string checkQuery = "SELECT COUNT(*) FROM Employees WHERE ID = @ID";
            string insertQuery = @"INSERT INTO Employees ( Name, Surname, Department, LogInName, 
                            Email_Address, PhoneNumber, Admin_Indicator, SiteManager, Active)
                           VALUES (@Name, @Surname, @Department, @LogInName, 
                                   @Email_Address, @PhoneNumber, @Admin_Indicator, @SiteManager, @Active)";
            string updateQuery = @"UPDATE Employees SET Name = @Name, Surname = @Surname, Department = @Department, 
                            LogInName = @LogInName, Email_Address = @Email_Address, PhoneNumber = @PhoneNumber, 
                            Admin_Indicator = @Admin_Indicator, SiteManager = @SiteManager, Active = @Active 
                          WHERE ID = @ID";

            string[] requiredFields = {
        txtEmployeeFirstname.Text, txtEmployeeSurname.Text,
        comboEmployeeDepartment.Text, txtlogInEmployee.Text,
        txtEmployeeEmail.Text, txtEmployeePhone.Text
    };

            if (requiredFields.Any(string.IsNullOrWhiteSpace))
            {
                MessageBox.Show("Please fill in all required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                    checkCommand.Parameters.AddWithValue("@ID", txtEmployeeID.Text);
                    int count = (int)checkCommand.ExecuteScalar();

                    SqlCommand command;

                    if (count > 0)
                    {
                        command = new SqlCommand(updateQuery, connection);
                    }
                    else
                    {
                        command = new SqlCommand(insertQuery, connection);
                    }

                    command.Parameters.AddWithValue("@ID", txtEmployeeID.Text);
                    command.Parameters.AddWithValue("@Name", txtEmployeeFirstname.Text);
                    command.Parameters.AddWithValue("@Surname", txtEmployeeSurname.Text);
                    command.Parameters.AddWithValue("@Department", comboEmployeeDepartment.Text);
                    command.Parameters.AddWithValue("@LogInName", txtlogInEmployee.Text);
                    command.Parameters.AddWithValue("@Email_Address", txtEmployeeEmail.Text);
                    command.Parameters.AddWithValue("@PhoneNumber", txtEmployeePhone.Text);
                    command.Parameters.AddWithValue("@Admin_Indicator", checkBoxAdmin.Checked ? 1 : 0);
                    command.Parameters.AddWithValue("@SiteManager", checkBoxsitemanager.Checked ? 1 : 0);
                    command.Parameters.AddWithValue("@Active", checkBoxActive.Checked ? 1 : 0);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        LoadEmployeeData(); // Refresh the grid after successful insert/update
                        ClearFormFields();
                        DisableFormFields();
                    }
                    else
                    {
                        MessageBox.Show("Operation failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("SQL Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("General Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DisableFormFields()
        {
            txtEmployeeEmail.Enabled = false;
            txtEmployeeFirstname.Enabled = false;
            txtEmployeeID.Enabled = false;
            txtEmployeeSurname.Enabled = false;
            txtlogInEmployee.Enabled = false;
            lblPhone.Enabled = false;
            txtEmployeePhone.Enabled = false;
            comboEmployeeDepartment.Enabled = false;
        }


        private void LoadDepartments()
        {
            string connectionString = $"Data Source={SQLservername};Initial Catalog={SQLDatabase};User ID={SQLUsername};Password={SQLPassword};";
            string query = "SELECT Category FROM Departments";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable departmentTable = new DataTable();
                    adapter.Fill(departmentTable);

                    comboEmployeeDepartment.DataSource = departmentTable;
                    comboEmployeeDepartment.DisplayMember = "Category"; // Display the category names
                    comboEmployeeDepartment.ValueMember = "Category"; // Optional: Set value for selection
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading departments: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        

        private void btnNewEmployee_Click(object sender, EventArgs e)
        {
            txtEmployeeEmail.Enabled = true;
            txtEmployeeFirstname.Enabled = true;
            txtEmployeeSurname.Enabled = true;
            txtlogInEmployee.Enabled = true;
            lblPhone.Enabled = true;
            txtEmployeePhone.Enabled = true;
            comboEmployeeDepartment.Enabled = true;
            txtEmployeeEmail.Text = string.Empty;
            txtEmployeeFirstname.Text = string.Empty;
            txtEmployeeSurname.Text = string.Empty;
            txtlogInEmployee.Text = string.Empty;
            txtEmployeePhone.Text = string.Empty;
            comboEmployeeDepartment.SelectedIndex = -1;
            string connectionString = $"Data Source={SQLservername};Initial Catalog={SQLDatabase};User ID={SQLUsername};Password={SQLPassword};";
            string query = "SELECT MAX(ID) + 1 AS NextID FROM Employees";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    object result = command.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        int nextId = Convert.ToInt32(result);
                        txtEmployeeID.Text = nextId.ToString();
                    }
                    else
                    {
                        txtEmployeeID.Text = "1"; // If no existing employees, start with ID 1
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error getting next employee ID: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ClearFormFields()
        {
            txtEmployeeFirstname.Clear();
            txtEmployeeSurname.Clear();
            txtlogInEmployee.Clear();
            txtEmployeeEmail.Clear();
            txtEmployeePhone.Clear();
            comboEmployeeDepartment.SelectedIndex = -1; // Reset combo box selection
            checkBoxAdmin.Checked = false;
            checkBoxsitemanager.Checked = false;
            checkBoxActive.Checked = false;
        }

        private void dataGridEmployeeList_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridEmployeeList.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridEmployeeList.SelectedRows[0];
                string loginName = selectedRow.Cells["LogInName"].Value?.ToString() ?? "";
                LoadEmployeeDetails(loginName);
            }
        }

        private void LoadEmployeeDetails(string loginName)
        {
            string connectionString = $"Data Source={SQLservername};Initial Catalog={SQLDatabase};User ID={SQLUsername};Password={SQLPassword};";
            string query = $"SELECT * FROM Employees WHERE LogInName = @LogInName"; // Use parameterized query for security

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@LogInName", loginName);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        txtEmployeeID.Text = reader["ID"].ToString();
                        txtEmployeeFirstname.Text = reader["Name"].ToString();
                        txtEmployeeSurname.Text = reader["Surname"].ToString();
                        txtlogInEmployee.Text = reader["LogInName"].ToString();
                        txtEmployeeEmail.Text = reader["Email_Address"].ToString();
                        txtEmployeePhone.Text = reader["PhoneNumber"].ToString();
                        comboEmployeeDepartment.Text = reader["Department"].ToString();
                        checkBoxAdmin.Checked = reader["Admin_Indicator"] != DBNull.Value && Convert.ToBoolean(reader["Admin_Indicator"]);
                        checkBoxsitemanager.Checked = reader["SiteManager"] != DBNull.Value && Convert.ToBoolean(reader["SiteManager"]);
                        checkBoxActive.Checked = reader["Active"] != DBNull.Value && Convert.ToBoolean(reader["Active"]);
                            comboEmployeeDepartment.SelectedItem = reader["Department"].ToString();
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading employee details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void dataGridEmployeeList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) 
            {
                DataGridViewRow selectedRow = dataGridEmployeeList.Rows[e.RowIndex];
                string loginName = selectedRow.Cells["LogInName"].Value?.ToString() ?? string.Empty; // Handle null values
                LoadEmployeeDetails(loginName); 
            }
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            txtEmployeeEmail.Enabled = true;
            txtEmployeeFirstname.Enabled = true;
            txtEmployeeSurname.Enabled = true;
            txtlogInEmployee.Enabled = true;
            lblPhone.Enabled = true;
            txtEmployeePhone.Enabled = true;
            comboEmployeeDepartment.Enabled = true;
            LoadDepartments();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            
        }
    }
}
