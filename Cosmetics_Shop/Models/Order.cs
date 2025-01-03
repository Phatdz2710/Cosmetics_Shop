using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models
{
    /// <summary>
    /// Represents an order in the system.
    /// </summary>
    public class Order
    {
        #region Properties
        /// <summary>
        /// Gets or sets the unique identifier for the order.
        /// </summary>
        public int Id { get; set; } // Id of order

        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public int UserId { get; set; } // Id of user

        /// <summary>
        /// Gets or sets the status of the order.
        /// </summary>
        public int OrderStatus { get; set; } // Status of order

        /// <summary>
        /// Gets or sets the date of the order.
        /// </summary>
        public DateTime OrderDate { get; set; } // Date of order

        /// <summary>
        /// Gets or sets the shipping method for the order.
        /// </summary>
        public int ShippingMethod { get; set; } // Shipping method

        /// <summary>
        /// Gets or sets the payment method for the order.
        /// </summary>
        public int PaymentMethod { get; set; } // Payment method

        /// <summary>
        /// Gets or sets the unique identifier for the voucher applied to the order.
        /// </summary>
        public int? VoucherId { get; set; } // Id of voucher

        /// <summary>
        /// Gets or sets the shipping address for the order.
        /// </summary>
        public string ShippingAddress { get; set; } // Shipping address

        /// <summary>
        /// Gets or sets the total price of the order.
        /// </summary>
        public int TotalPrice { get; set; } // Total price
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Order"/> class with default values.
        /// </summary>
        public Order()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Order"/> class with specified values.
        /// </summary>
        /// <param name="id">The unique identifier for the order.</param>
        /// <param name="userId">The unique identifier for the user.</param>
        /// <param name="orderStatus">The status of the order.</param>
        /// <param name="orderDate">The date of the order.</param>
        /// <param name="shippingMethod">The shipping method for the order.</param>
        /// <param name="paymentMethod">The payment method for the order.</param>
        /// <param name="voucherId">The unique identifier for the voucher applied to the order.</param>
        /// <param name="shippingAddress">The shipping address for the order.</param>
        /// <param name="totalPrice">The total price of the order.</param>
        public Order(int id, int userId, int orderStatus, DateTime orderDate, int shippingMethod, int paymentMethod, int voucherId, string shippingAddress, int totalPrice)
        {
            Id = id;
            UserId = userId;
            OrderStatus = orderStatus;
            OrderDate = orderDate;
            ShippingMethod = shippingMethod;
            PaymentMethod = paymentMethod;
            VoucherId = voucherId;
            ShippingAddress = shippingAddress;
            TotalPrice = totalPrice;
        }
    }
}
