using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Enums
{
    /// <summary>
    /// Enum representing the status of an order.
    /// InProcess: The order is being processed.
    /// Success: The order was successful.
    /// Failed: The order failed.
    /// </summary>
    public enum OrderStatus
    {
        InProcess = 0,
        Success = 2,
        Failed = 3
    }
}
