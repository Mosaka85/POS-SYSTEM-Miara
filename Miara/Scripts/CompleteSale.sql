CREATE PROCEDURE CompleteSale
    @TotalAmount DECIMAL(18, 2),
    @PaymentMethod NVARCHAR(50),
    @EmployeeID INT,
    @DiscountPercentage DECIMAL(5, 2) = NULL,
    @DiscountValue DECIMAL(18, 2) = NULL
AS
BEGIN
    DECLARE @SaleID INT;
    DECLARE @Tax DECIMAL(18, 2);
    DECLARE @FinalTotal DECIMAL(18, 2);

    SET @Tax = @TotalAmount * 0.15;

    SET @FinalTotal = @TotalAmount;

    IF @DiscountPercentage IS NOT NULL AND @DiscountValue IS NOT NULL
    BEGIN
        SET @FinalTotal = @TotalAmount - @DiscountValue;
    END

    BEGIN TRANSACTION;

    INSERT INTO Sale (SaleDate, TotalAmount, PaymentMethod, EmployeeID)
    VALUES (GETDATE(), @FinalTotal, @PaymentMethod, @EmployeeID);

    SET @SaleID = SCOPE_IDENTITY();

    INSERT INTO SalesDetails (SaleID, ProductID, Quantity, UnitPrice, Subtotal, Tax)
    SELECT @SaleID, ProductID, Quantity, UnitPrice, Subtotal, Subtotal * 0.15
    FROM Sale
	WHERE Sale = @SaleID;

    IF @DiscountPercentage IS NOT NULL AND @DiscountValue IS NOT NULL
    BEGIN
        INSERT INTO Discounts (SaleID, DiscountPercentage, DiscountValue, DiscountDate)
        VALUES (@SaleID, @DiscountPercentage, @DiscountValue, GETDATE());
    END

    INSERT INTO Payments (SaleID, AmountPaid, PaymentDate, PaymentMethod)
    VALUES (@SaleID, @FinalTotal, GETDATE(), @PaymentMethod);

    COMMIT TRANSACTION;


    SELECT @SaleID AS SaleID;
END