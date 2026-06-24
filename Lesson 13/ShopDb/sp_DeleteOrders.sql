CREATE PROCEDURE [dbo].[sp_DeleteOrders]
	@Month int = NULL,
	@Year int = NULL,
	@Status NVARCHAR(30) = NULL,
	@ProductId int = NULL
AS
	BEGIN
		SET NOCOUNT ON;

		DELETE FROM [dbo].[Orders] WHERE 
		(@Month IS NULL OR MONTH(CreatedDate) = @Month) 
		AND (@Year IS NULL OR YEAR(CreatedDate) = @Year) 
		AND (@Status IS NULL OR Status = @Status) 
		AND (@ProductId IS NULL OR ProductId = @ProductId);
	END;
