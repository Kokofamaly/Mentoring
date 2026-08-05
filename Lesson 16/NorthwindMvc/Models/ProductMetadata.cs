using System.ComponentModel.DataAnnotations;

namespace NorthwindMvc.Models
{
    public class ProductMetadata
    {
        [Required]
        [MaxLength(50)]
        public string ProductName { get; set; }
        [Required]
        public decimal? UnitPrice { get; set; }
        [Required]
        [System.ComponentModel.DataAnnotations.DataType("integer")]
        public short? UnitsInStock { get; set; }

    }
}
