using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models.DataService
{

    public struct SearchResult
    {
        public List<ProductThumbnail>   Products    { get; set; }
        public int                      TotalPages  { get; set; }
        public int                      TotalProducts   { get; set; }
        public List<string>             Brands          { get; set; }
        public List<string>             Categories      { get; set; }
    }
}
