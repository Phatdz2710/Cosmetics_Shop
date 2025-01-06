using Cosmetics_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.DataAccessObject.Data
{

    /// <summary>
    /// Represents the result of a product search.
    /// </summary>
    public struct SearchResult
    {
        /// <summary>
        /// List of products retrieved from the search.
        /// </summary>
        public List<ProductThumbnail>   Products        { get; set; } // List of products

        /// <summary>
        /// The total number of pages available for the product search.
        /// </summary>
        public int                      TotalPages      { get; set; } // Total pages of products
        /// <summary>
        /// The total number of products found in the search.
        /// </summary>
        public int                      TotalProducts   { get; set; } // Total products
        /// <summary>
        /// List of brands related to the products.
        /// </summary>
        public List<string>             Brands          { get; set; } // List of brands
        /// <summary>
        /// List of categories related to the products.
        /// </summary>
        public List<string>             Categories      { get; set; } // List of categories
    }
}
