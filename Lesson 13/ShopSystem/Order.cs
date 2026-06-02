using System;
using System.Collections.Generic;
using System.Text;

namespace ShopSystem
{
    public class Order
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public Status OrderStatus { get; set; }
    }
}
