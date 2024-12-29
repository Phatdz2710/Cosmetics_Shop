using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models
{
    /// <summary>
    /// Represents an order in the system
    /// </summary>
    public class OrderItem
    {
        #region Properties
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        #endregion

        // Constructor
        public OrderItem()
        {

        }

        // Parameterized constructor
        public OrderItem(int id, int orderId, int productId, int quantity)
        {
            Id = id;
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
