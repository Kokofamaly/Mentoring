INSERT INTO [dbo].[Address] (Id, Street, City, State, ZipCode) VALUES
(1, 'Main Street', 'Podgorica', 'Montenegro', '81000'),
(2, 'Main Street 2', 'Podgorica', 'Montenegro', '81000');

INSERT INTO [dbo].[Person] (Id, FirstName, LastName) VALUES
(1, 'Vadim', 'Fateev'),
(2, 'Ivan', 'Ivanov');

INSERT INTO [dbo].[Company] (Id, Name, AddressId) VALUES
(1, 'MyCompany', 1);

INSERT INTO [dbo].[Employee] (Id, AddressId, PersonId, CompanyName, Position, EmployeeName) VALUES
(1, 1, 1, 'MyCompany', 'Developer', 'Vadim Fateev')