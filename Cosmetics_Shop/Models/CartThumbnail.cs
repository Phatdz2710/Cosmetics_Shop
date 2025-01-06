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
    /// Represents a thumbnail view of a cart, inheriting from the Cart class.
    /// </summary>
    public class CartThumbnail : Cart, INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CartThumbnail"/> class with specified values.
        /// </summary>
        /// <param name="id">The unique identifier for the cart.</param>
        /// <param name="productId">The unique identifier for the product.</param>
        /// <param name="productImage">The image of the product.</param>
        /// <param name="productName">The name of the product.</param>
        /// <param name="price">The price of the product.</param>
        /// <param name="amount">The amount of the product.</param>
        /// <param name="totalPrice">The total price of the product.</param>
        public CartThumbnail(int id, int productId, string productImage, string productName,
            int price, int amount, int totalPrice)
            : base(id, productId, productImage, productName, price, amount, totalPrice)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CartThumbnail"/> class with specified user id, product id, and quantity.
        /// </summary>
        /// <param name="userId">The unique identifier for the user.</param>
        /// <param name="productId">The unique identifier for the product.</param>
        /// <param name="quantity">The quantity of the product.</param>
        public CartThumbnail(int userId, int productId, int quantity)
            : base(userId, productId, quantity)
        { }
    }
}
