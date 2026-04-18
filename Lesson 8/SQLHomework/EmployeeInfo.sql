CREATE VIEW [dbo].[EmployeeInfo]
	AS SELECT Employee.Id, 
	COALESCE (Employee.EmployeeName, CONCAT(Person.FirstName, ' ', Person.LastName)) AS EmployeeFullName, 
	CONCAT(Address.ZipCode, '_', Address.State, ' ', Address.City, '-', Address.Street) AS EmployeeFullAddress,
	CONCAT(Employee.CompanyName, ' ', Employee.Position) AS EmployeeCompanyInfo,
	Employee.CompanyName, 
	Address.City
	FROM [dbo].[Employee]
	JOIN [dbo].[Address] ON Address.Id = Employee.AddressId
	JOIN [dbo].[Person] ON Person.Id = Employee.PersonId;
