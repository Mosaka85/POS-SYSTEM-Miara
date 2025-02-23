using OfficeOpenXml;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Miara
{
    public partial class frmReports : Form
    {
        private static readonly string configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config.xml");
        private string connectionString;

        public frmReports()
        {
            InitializeComponent();
            LoadSQLConnectionInfo();
            this.FormClosed += (sender, e) => Application.Exit();
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

        private void LoadDetailsData()
        {
            ClearDataGrid();
            if (string.IsNullOrEmpty(connectionString))
            {
                MessageBox.Show("Database connection is not set.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = @"
                SELECT 
                    SH.SaleID, 
                    CAST(SH.SaleDate AS DATE) AS SaleDate, 
                    FORMAT(SH.SaleDate, 'HH:mm:ss') AS SaleTime, 
                    SH.PaymentMethod, 
                    PH.ProductName, 
                    SD.UnitPrice, 
                    SD.Quantity, 
                    SD.Subtotal, 
                    SD.TAX,
                    CONCAT(EH.EmployeeFirstName, ' ', EH.EmployeeSurname) AS Employee
                FROM Sale SH
                INNER JOIN SalesDetails SD ON SH.SaleID = SD.SaleID
                INNER JOIN Products PH ON SD.ProductID = PH.ProductID
                INNER JOIN Employees EH ON SH.EmployeeID = EH.EmployeeID;";

            LoadDataIntoGrid(query, dataGridViewHeader);
            lblDatalabel.Text = "DATA GRID VIEW : Sales Data";
        }

        private void LoadDataIntoGrid(string query, DataGridView grid)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        grid.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportToExcel(DataTable dataTable, string filePath)
        {
            if (dataTable == null || dataTable.Rows.Count == 0)
            {
                MessageBox.Show("No data available to export.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (ExcelPackage excelPackage = new ExcelPackage())
                {
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sales Report");

                    // Add headers
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        worksheet.Cells[1, i + 1].Value = dataTable.Columns[i].ColumnName;
                        worksheet.Cells[1, i + 1].Style.Font.Bold = true;
                        worksheet.Cells[1, i + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        worksheet.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    }

                    // Add rows
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataTable.Columns.Count; j++)
                        {
                            worksheet.Cells[i + 2, j + 1].Value = dataTable.Rows[i][j];
                        }
                    }

                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                    excelPackage.SaveAs(new FileInfo(filePath));
                }

                MessageBox.Show("Excel file saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting to Excel: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dataGridViewHeader.DataSource is DataTable dataTable)
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx",
                    Title = "Save an Excel File"
                })
                {
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        ExportToExcel(dataTable, saveFileDialog.FileName);
                    }
                }
            }
            else
            {
                MessageBox.Show("No data available to export.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void frmReports_Load(object sender, EventArgs e)
        {
            LoadDetailsData();
        }

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            LoadDetailsData();
        }

        private void PaymentData()
        {
            ClearDataGrid();
            if (string.IsNullOrEmpty(connectionString))
            {
                MessageBox.Show("Database connection is not set.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = @"
                SELECT  q.[PaymentID]
                ,q.[SaleID]
               ,q.[AmountPaid]
                ,[PaymentDate],r.EmployeeName, r.Total
                ,q.PaymentMethod 
                  FROM  [Payments] q
                LEFT JOIN Receipts r on  CAST(q.SaleID as varchar)= r.ReceiptNumber
                ORDER BY PaymentDate desc";

            LoadDataIntoGrid(query, dataGridViewHeader);
            lblDatalabel.Text = "DATA GRID VIEW : Payment Data";
        }

        private void ClearDataGrid()
        {
            if (dataGridViewHeader.DataSource is DataTable dataTable)
            {
                dataTable.Clear();
            }
        }

        private void btnViewPayments_Click(object sender, EventArgs e)
        {
            PaymentData();
        }

        private void btnDiscount_Click(object sender, EventArgs e)
        {
            DiscountData();
        }

        private void DiscountData()
        {
            ClearDataGrid();
            if (string.IsNullOrEmpty(connectionString))
            {
                MessageBox.Show("Database connection is not set.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = @"
                SELECT [DiscountID]
          ,d.[SaleID]
          ,[DiscountPercentage]
          ,[DiscountValue]
         ,[DiscountDate] , 
    	  CONCAT_WS(' ',E.EmployeeFirstName, E.EmployeeSurname) AS Employee
          FROM  [Discounts] d
         INNER JOIN Sale SH ON d.SaleID = SH.SaleID
         LEFT JOIN Employees E ON SH.EmployeeID= E.EmployeeID";
            LoadDataIntoGrid(query, dataGridViewHeader);
            lblDatalabel.Text = "DATA GRID VIEW : Discount Data";
        }


        private void ReceiptData()
        {
            ClearDataGrid();
            if (string.IsNullOrEmpty(connectionString))
            {
                MessageBox.Show("Database connection is not set.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = @"
               SELECT  [ReceiptID]
                  ,[ReceiptNumber]
                  ,[Date]
                  ,[EmployeeName]
                  ,[PaymentMethod]
                  ,[Subtotal]
                  ,[Discount]
                  ,[Tax]
                  ,[Total]
                  ,[AmountRendered]
                  ,[Change]
              FROM [Receipts]
              ORDER BY Date desc";
            LoadDataIntoGrid(query, dataGridViewHeader);
            lblDatalabel.Text = "DATA GRID VIEW : Receipt Data";
        }

        private void btnReceipts_Click(object sender, EventArgs e)
        {
            ReceiptData();
        }
    }
}