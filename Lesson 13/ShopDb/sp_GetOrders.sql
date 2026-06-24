CREATE PROCEDURE [dbo].[sp_GetOrders]
	@Month int = NULL,
	@Year int = NULL,
	@Status NVARCHAR(30) = NULL,
	@ProductId int = NULL
AS
	BEGIN
		SET NOCOUNT ON;

		SELECT * FROM [dbo].[Orders] WHERE 
		(@Month IS NULL OR MONTH(CreatedDate) = @Month) 
		AND (@Year IS NULL OR YEAR(CreatedDate) = @Year) 
		AND (@Status IS NULL OR Status = @Status) 
		AND (@ProductId IS NULL OR ProductId = @ProductId);
	END;

