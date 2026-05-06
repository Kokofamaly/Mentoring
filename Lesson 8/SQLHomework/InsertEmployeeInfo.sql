CREATE PROCEDURE [dbo].[InsertEmployeeInfo]
	@EmployeeName NVARCHAR(100) = NULL,
	@FirstName NVARCHAR(50) = NULL,
	@LastName NVARCHAR(50) = NULL,
	@CompanyName NVARCHAR(20),
	@Position NVARCHAR(30) = NULL,
	@Street NVARCHAR(50),
	@City NVARCHAR(20) = NULL,
	@State NVARCHAR(50) = NULL,
	@ZipCode NVARCHAR(50) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	    IF (TRIM(ISNULL(@EmployeeName, '')) = '' AND 
        TRIM(ISNULL(@FirstName, '')) = '' AND 
        TRIM(ISNULL(@LastName, '')) = '')
    BEGIN
        RAISERROR('At least one name field (EmployeeName, FirstName, or LastName) must be provided.', 16, 1);
        RETURN;
    END

	DECLARE @TruncatedCompany NVARCHAR(20) = LEFT(@CompanyName, 20);

	BEGIN TRY
		BEGIN TRANSACTION;

			INSERT INTO [dbo].[Address] (Street, City, State, ZipCode) 
			VALUES (@Street, @City, @State, @ZipCode);

			DECLARE @AddrId INT = SCOPE_IDENTITY();

			INSERT INTO [dbo].[Person] (FirstName, LastName) 
			VALUES(ISNULL(@FirstName, ''), ISNULL(@LastName, ''));

			DECLARE @PersId INT = SCOPE_IDENTITY();
		
			INSERT INTO [dbo].[Employee] (AddressId, PersonId, CompanyName, Position, EmployeeName)
			VALUES (@AddrId, @PersId, @TruncatedCompany, @Position, @EmployeeName);

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
		THROW;
	END CATCH
END