using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models
{
    /// <summary>
    /// Represents an order item in the system.
    /// </summary>
    public class OrderItem
    {
        #region Properties
        /// <summary>
        /// Gets or sets the unique identifier for the order item.
        /// </summary>
        public int Id { get; set; } // Id of order item

        /// <summary>
        /// Gets or sets the unique identifier for the order.
        /// </summary>
        public int OrderId { get; set; } // Id of order

        /// <summary>
        /// Gets or sets the unique identifier for the product.
        /// </summary>
        public int ProductId { get; set; } // Id of product

        /// <summary>
        /// Gets or sets the quantity of the product.
        /// </summary>
        public int Quantity { get; set; } // Quantity of product
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderItem"/> class with default values.
        /// </summary>
        public OrderItem()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderItem"/> class with specified values.
        /// </summary>
        /// <param name="id">The unique identifier for the order item.</param>
        /// <param name="orderId">The unique identifier for the order.</param>
        /// <param name="productId">The unique identifier for the product.</param>
        /// <param name="quantity">The quantity of the product.</param>
        public OrderItem(int id, int orderId, int productId, int quantity)
        {
            Id = id;
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
