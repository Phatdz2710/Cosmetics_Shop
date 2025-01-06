using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Enums
{
    /// <summary>
    /// Enum representing the status of an order.
    /// 0: Order is in process.
    /// 2: Order was successful.
    /// 3: Order failed.
    /// </summary>
    public enum OrderStatus
    {
        InProcess   = 0, // Order is in process.
        Success     = 2, // Order was successful.
        Failed      = 3  // Order failed.
    }
}
