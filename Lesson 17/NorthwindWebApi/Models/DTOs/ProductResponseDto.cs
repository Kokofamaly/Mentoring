namespace NorthwindWebApi.Models.DTOs
{
    public class ProductResponseDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public string? QuantityPerUnit { get; set; }

        public decimal? UnitPrice { get; set; }

        public short? UnitsInStock { get; set; }

        public short? UnitsOnOrder { get; set; }

        public short? ReorderLevel { get; set; }

        public bool Discontinued { get; set; }
        public string? SupplierName { get; set; }
        public string? CategoryName { get; set; }

    }
}