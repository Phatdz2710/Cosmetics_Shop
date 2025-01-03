using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models
{
    public class PaymentNavigationData
    {
        public List<PaymentProductThumbnail> Products { get; set; }
        public Models.ShippingMethod CurrentShippingMethod { get; set; }
        public Models.Voucher CurrentVoucher { get; set; }

        public PaymentNavigationData(List<PaymentProductThumbnail> products, 
                                    Models.ShippingMethod shippingMethod, 
                                    Models.Voucher voucher)
        {
            Products = products;
            CurrentShippingMethod = shippingMethod;
            CurrentVoucher = voucher;
        }
    }
}
