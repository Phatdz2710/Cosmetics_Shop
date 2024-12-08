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
        public List<ProductThumbnail>   Products        { get; set; }
        public int                      TotalPages      { get; set; }
        public int                      TotalProducts   { get; set; }
        public List<string>             Brands          { get; set; }
        public List<string>             Categories      { get; set; }
    }
}
