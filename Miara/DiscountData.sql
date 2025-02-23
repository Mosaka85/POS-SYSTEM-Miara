SELECT 
    d.[DiscountID],
    d.[SaleID],
    d.[DiscountPercentage],
    d.[DiscountValue],
    d.[DiscountDate], 
    CONCAT_WS(' ', E.EmployeeFirstName, E.EmployeeSurname) AS Employee
FROM [Discounts] d
INNER JOIN Sale SH ON d.SaleID = SH.SaleID
LEFT JOIN Employees E ON SH.EmployeeID = E.EmployeeID;
