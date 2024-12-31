using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models.Enums
{
    /// <summary>
    /// Enum representing the sorting options for products.
    /// DateAscending: Sort by date in ascending order.
    /// PriceDescending: Sort by price in descending order.
    /// PriceAscending: Sort by price in ascending order.
    /// NameAscending: Sort by name in ascending order.
    /// NameDescending: Sort by name in descending order.
    /// </summary>
    public enum SortProduct
    {
        DateAscending   = 0,
        PriceDescending = 1,
        PriceAscending  = 2,
        NameAscending   = 3,
        NameDescending  = 4,
    }
}
