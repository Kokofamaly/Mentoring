using System;
using System.Collections.Generic;
using System.Text;

namespace ShopSystem.Enum
{
    public enum OrderStatus
    {
        NotStarted,
        Loading,
        InProgress,
        Arrived,
        Unloading,
        Cancelled,
        Done
    }
}
