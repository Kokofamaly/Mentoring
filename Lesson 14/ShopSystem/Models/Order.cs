using ShopSystem.Enum;

namespace ShopSystem.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public OrderStatus Status { get; set; }
    }
}
