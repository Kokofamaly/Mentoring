CREATE OR ALTER TRIGGER [dbo].[trg_Employee_Insert_CreateCompany]
ON [dbo].[Employee]
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @ToInsert TABLE(
        OldAddressId INT,
        Street NVARCHAR(50),
        City NVARCHAR(20),
        State NVARCHAR(50),
        ZipCode NVARCHAR(50)
    );

    INSERT INTO @ToInsert (OldAddressId, Street, City, State, ZipCode)
    SELECT a.Id, a.Street, a.City, a.State, a.ZipCode
    FROM inserted i
    JOIN [dbo].[Address] a ON a.Id = i.AddressId;

    DECLARE @NewAddresses TABLE(
        OldAddressId INT,
        NewAddressId INT
    );

    MERGE INTO [dbo].[Address] AS target
    USING @ToInsert AS src
    ON 1 = 0
    WHEN NOT MATCHED THEN
        INSERT (Street, City, State, ZipCode)
        VALUES (src.Street, src.City, src.State, src.ZipCode)
    OUTPUT src.OldAddressId, INSERTED.Id
    INTO @NewAddresses (OldAddressId, NewAddressId);

    INSERT INTO [dbo].[Company] (Name, AddressId)
    SELECT i.CompanyName, na.NewAddressId
    FROM inserted i
    JOIN @NewAddresses na ON na.OldAddressId = i.AddressId;
END

--     DECLARE @AddressId INT;
--     DECLARE @CompanyName NVARCHAR(20);

--     DECLARE employee_cursor CURSOR FOR 
--     SELECT AddressId, CompanyName FROM inserted;

--     OPEN employee_cursor;
--     FETCH NEXT FROM employee_cursor INTO @AddressId, @CompanyName;

--     WHILE @@FETCH_STATUS = 0
--     BEGIN
--         INSERT INTO [dbo].[Address] (Street, City, State, ZipCode)
--         SELECT Street, City, State, ZipCode 
--         FROM [dbo].[Address] 
--         WHERE Id = @AddressId;

--         DECLARE @NewAddrId INT = SCOPE_IDENTITY();

--         INSERT INTO [dbo].[Company] (Name, AddressId)
--         VALUES (@CompanyName, @NewAddrId);

--         FETCH NEXT FROM employee_cursor INTO @AddressId, @CompanyName;
--     END

--     CLOSE employee_cursor;
--     DEALLOCATE employee_cursor;
-- END
