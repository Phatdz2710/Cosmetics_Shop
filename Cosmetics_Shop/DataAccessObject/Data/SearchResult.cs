using Cosmetics_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.DataAccessObject.Data
{

    /// <summary>
    /// Search Result
    /// </summary>
    public struct SearchResult
    {
        public List<ProductThumbnail>   Products        { get; set; } // List of products
        public int                      TotalPages      { get; set; } // Total pages of products
        public int                      TotalProducts   { get; set; } // Total products
        public List<string>             Brands          { get; set; } // List of brands
        public List<string>             Categories      { get; set; } // List of categories
    }
}
