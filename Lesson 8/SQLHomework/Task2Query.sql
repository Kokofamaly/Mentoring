SELECT e.Id, e.EmployeeFullName, e.EmployeeFullAddress, e.EmployeeCompanyInfo FROM [dbo].[EmployeeInfo] AS e
ORDER BY e.CompanyName ASC, e.City ASC;