using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Services
{
    internal class GetOrderResult
    {
        public List<Models.Order> ListOrders { get; set; }
        public int TotalPages { get; set; }
        public int TotalOrders { get; set; }
    }
}
