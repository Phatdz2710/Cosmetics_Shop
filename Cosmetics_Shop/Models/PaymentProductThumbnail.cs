using Microsoft.Identity.Client;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models
{
    /// <summary>
    /// Represents a thumbnail view of a payment product, inheriting from the PaymentProduct class.
    /// </summary>
    public class PaymentProductThumbnail : PaymentProduct
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentProductThumbnail"/> class with specified values.
        /// </summary>
        /// <param name="cartId">The unique identifier for the cart.</param>
        /// <param name="productId">The unique identifier for the product.</param>
        /// <param name="image">The image of the product.</param>
        /// <param name="productName">The name of the product.</param>
        /// <param name="price">The price of the product.</param>
        /// <param name="amount">The amount of the product.</param>
        public PaymentProductThumbnail(
                int cartId,
                int productId,
                string image,
                string productName,
                int price,
                int amount)
            : base(cartId, productId,
                  image,
                  productName,
                  price,
                  amount)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentProductThumbnail"/> class with default values.
        /// </summary>
        public PaymentProductThumbnail()
            : base(-1, -1, null, null, 0, 0)
        { }
    }
}