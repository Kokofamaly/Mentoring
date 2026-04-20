CREATE TRIGGER [dbo].[trg_Employee_Insert_CreateCompany]
ON [dbo].[Employee]
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @AddressId INT;
    DECLARE @CompanyName NVARCHAR(20);

    DECLARE employee_cursor CURSOR FOR 
    SELECT AddressId, CompanyName FROM inserted;

    OPEN employee_cursor;
    FETCH NEXT FROM employee_cursor INTO @AddressId, @CompanyName;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        INSERT INTO [dbo].[Address] (Street, City, State, ZipCode)
        SELECT Street, City, State, ZipCode 
        FROM [dbo].[Address] 
        WHERE Id = @AddressId;

        DECLARE @NewAddrId INT = SCOPE_IDENTITY();

        INSERT INTO [dbo].[Company] (Name, AddressId)
        VALUES (@CompanyName, @NewAddrId);

        FETCH NEXT FROM employee_cursor INTO @AddressId, @CompanyName;
    END

    CLOSE employee_cursor;
    DEALLOCATE employee_cursor;
END
