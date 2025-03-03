WITH SaleReporting
AS (
SELECT  SaleID ,ROW_NUMBER() OVER(Partition by Saleid order by SaledetailID) AS Item_number
  FROM [SalesDetails]
)

  SELECT SaleId , MAX(Item_number) AS No_of_Itmes_sold
  FROM SaleReporting
  GROUP BY  SaleId
  order by MAX(Item_number) DESC  
  GO


CREATE VIEW  [dbo].[SalesReport]  
AS
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
                INNER JOIN Employees EH ON SH.EmployeeID = EH.EmployeeID;
GO

SELECT 
    Employee,
    FORMAT(MAX(b.AttemptTimestamp), 'yyyy-MM-dd HH:mm:ss') AS Last_logIn
FROM (
    SELECT 
        a.EmployeeID,
        CONCAT_WS(' ', a.EmployeeFirstName, a.EmployeeSurname) AS Employee
    FROM Employees a
) EmployeeData
LEFT JOIN LoginAudit b ON EmployeeData.EmployeeID = b.EmployeeID AND b.IsSuccess = 1
GROUP BY Employee
ORDER BY FORMAT(MAX(b.AttemptTimestamp), 'yyyy-MM-dd HH:mm:ss')  DESC;
GO