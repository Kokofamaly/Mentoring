CREATE TABLE [dbo].[Products]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [Weight] DECIMAL(10, 2) NOT NULL, 
    [Height] DECIMAL(4, 2) NOT NULL, 
    [Width] DECIMAL(4, 2) NOT NULL, 
    [Length] DECIMAL(4, 2) NOT NULL
)
