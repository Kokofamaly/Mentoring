CREATE TRIGGER [dbo].[trg_Employee_Insert_CreateCompany]
	ON [dbo].[Employee]
	AFTER INSERT
	AS
	BEGIN
		SET NOCOUNT ON;

		INSERT INTO [dbo].[Company] (Name, AddressId)
		SELECT DISTINCT i.CompanyName, i.AddressId
		FROM inserted i
		WHERE NOT EXISTS(
			SELECT 1
			FROM [dbo].[Company] c
			WHERE c.Name = i.CompanyName
		);

	END
