using System;
using System.Data.SqlClient;

public class EmployeeImageService
{
    private readonly string _connectionString;

    public EmployeeImageService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void UploadImage(int employeeId, string fileName, byte[] imageData)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            conn.Open();
            SqlTransaction transaction = conn.BeginTransaction();

            try
            {
                // Deactivate old images
                string deactivateQuery = "UPDATE ImageStore SET IsActive = 0 WHERE EmployeeID = @empId AND IsActive = 1";
                using (SqlCommand deactivateCmd = new SqlCommand(deactivateQuery, conn, transaction))
                {
                    deactivateCmd.Parameters.AddWithValue("@empId", employeeId);
                    deactivateCmd.ExecuteNonQuery();
                }

                // Insert new image
                string insertQuery = @"INSERT INTO ImageStore (ImageName, ImageData, EmployeeID, IsActive, [Date]) 
                                       VALUES (@name, @data, @empId, 1, GETDATE())";
                using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn, transaction))
                {
                    insertCmd.Parameters.AddWithValue("@name", fileName);
                    insertCmd.Parameters.AddWithValue("@data", imageData);
                    insertCmd.Parameters.AddWithValue("@empId", employeeId);
                    insertCmd.ExecuteNonQuery();
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
