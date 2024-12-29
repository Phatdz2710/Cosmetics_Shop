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
    public class Order
    {
        #region Properties
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public int ShippingMethod { get; set; }
        public int PaymentMethod { get; set; }
        public int? VoucherId { get; set; }
        public string ShippingAddress { get; set; }
        #endregion

        // Constructor
        public Order()
        {

        }

        // Parameterized constructor
        public Order(int id, int userId, int orderStatus, DateTime orderDate, int shippingMethod, int paymentMethod, int voucherId, string shippingAddress)
        {
            Id = id;
            UserId = userId;
            OrderStatus = orderStatus;
            OrderDate = orderDate;
            ShippingMethod = shippingMethod;
            PaymentMethod = paymentMethod;
            VoucherId = voucherId;
            ShippingAddress = shippingAddress;
        }
    }
}
