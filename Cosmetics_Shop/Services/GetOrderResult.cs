
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Services
{
    /// <summary>  
    /// Represents the result of a get order operation, including a list of orders, total pages, and total orders.  
    /// </summary>  
    public struct GetOrderResult
    {
        /// <summary>  
        /// Gets or sets the list of orders.  
        /// </summary>  
        public List<Models.Order> ListOrders { get; set; }

        /// <summary>  
        /// Gets or sets the total number of pages.  
        /// </summary>  
        public int TotalPages { get; set; }

        /// <summary>  
        /// Gets or sets the total number of orders.  
        /// </summary>  
        public int TotalOrders { get; set; }
    }
}