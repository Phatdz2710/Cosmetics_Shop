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
        DateAscending   = 0, // Sort by date in ascending order.
        PriceDescending = 1, // Sort by price in descending order.
        PriceAscending  = 2, // Sort by price in ascending order.
        NameAscending   = 3, // Sort by name in ascending order.
        NameDescending  = 4, // Sort by name in descending order.
    }
}
