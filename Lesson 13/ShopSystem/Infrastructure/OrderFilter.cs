using ShopSystem.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopSystem.Infrastructure
{
    public class OrderFilter
    {
        public int? Year;
        public int? Month;
        public int? ProductId;
        public OrderStatus? Status;
    }
}
