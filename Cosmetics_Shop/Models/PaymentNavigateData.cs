
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models
{
    /// <summary>  
    /// Represents the data required for navigating to the payment page.  
    /// </summary>  
    public class PaymentNavigationData
    {
        /// <summary>  
        /// Gets or sets the list of products to be displayed on the payment page.  
        /// </summary>  
        public List<PaymentProductThumbnail> Products { get; set; }

        /// <summary>  
        /// Gets or sets the current shipping method selected by the user.  
        /// </summary>  
        public Models.ShippingMethod CurrentShippingMethod { get; set; }

        /// <summary>  
        /// Gets or sets the current voucher applied to the order.  
        /// </summary>  
        public Models.Voucher CurrentVoucher { get; set; }

        /// <summary>  
        /// Initializes a new instance of the <see cref="PaymentNavigationData"/> class with specified products, shipping method, and voucher.  
        /// </summary>  
        /// <param name="products">The list of products to be displayed on the payment page.</param>  
        /// <param name="shippingMethod">The current shipping method selected by the user.</param>  
        /// <param name="voucher">The current voucher applied to the order.</param>  
        public PaymentNavigationData(List<PaymentProductThumbnail> products, Models.ShippingMethod shippingMethod, Models.Voucher voucher)
        {
            Products = products;
            CurrentShippingMethod = shippingMethod;
            CurrentVoucher = voucher;
        }
    }
}
