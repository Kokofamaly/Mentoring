using ShopSystem.Enum;

namespace ShopSystem.Infrastructure
{
    public class OrderFilter
    {
        public int? Year { get; set; }
        public int? Month { get; set; }
        public int? ProductId { get; set; }
        public OrderStatus? Status { get; set; }
    }
}
