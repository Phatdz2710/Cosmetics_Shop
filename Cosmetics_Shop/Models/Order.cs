using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int OrderStatus { get; set; }

        public DateTime OrderDate { get; set; }

        public int PaymentMethod { get; set; }

        public int ShippingMethod { get; set; }

        public int? VoucherId { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public virtual User User { get; set; }

        public virtual Voucher Voucher { get; set; }
    }
}
