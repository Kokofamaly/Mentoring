CREATE TABLE [dbo].[Orders]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Status] NVARCHAR(30) NOT NULL, 
    [CreatedDate] DATETIMEOFFSET NOT NULL, 
    [UpdatedDate] DATETIMEOFFSET NULL, 
    [ProductId] INT NOT NULL, 
    CONSTRAINT [FK_Orders_Products] FOREIGN KEY (ProductId) REFERENCES [dbo].[Products](Id)
)
