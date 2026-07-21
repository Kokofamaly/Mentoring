using System;
using System.Collections.Generic;
using System.Text;

namespace RestClient
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string? QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public string? SupplierName { get; set; }
        public string? CategoryName { get; set; }
    }
}
