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
    /// Store Cart Information
    /// </summary>
    public class Cart : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the unique identifier for the cart.
        /// </summary>
        public int Id { get; set; } // Id of cart

        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public int UserId { get; set; } // Id of user

        /// <summary>
        /// Gets or sets the unique identifier for the product.
        /// </summary>
        public int ProductId { get; set; } // Id of product

        /// <summary>
        /// Gets or sets the image of the product.
        /// </summary>
        public string ProductImage { get; set; } // Image of product

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string ProductName { get; set; } // Name of product

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public int Price { get; set; } // Price of product

        private int _amount; // Amount of product

        /// <summary>
        /// Gets or sets the amount of the product.
        /// </summary>
        public int Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));
                OnPropertyChanged(nameof(TotalPrice)); // Notify that TotalPrice has changed
            }
        }

        /// <summary>
        /// Gets the total price of the product.
        /// </summary>
        public int TotalPrice
        {
            get
            {
                return Amount * Price;
            }
            set
            {
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cart"/> class with specified values.
        /// </summary>
        /// <param name="id">The unique identifier for the cart.</param>
        /// <param name="productId">The unique identifier for the product.</param>
        /// <param name="productImage">The image of the product.</param>
        /// <param name="productName">The name of the product.</param>
        /// <param name="price">The price of the product.</param>
        /// <param name="amount">The amount of the product.</param>
        /// <param name="totalPrice">The total price of the product.</param>
        public Cart(int id,
                    int productId,
                    string productImage,
                    string productName,
                    int price,
                    int amount,
                    int totalPrice)
        {
            Id = id;
            ProductId = productId;
            ProductImage = productImage;
            ProductName = productName;
            Price = price;
            Amount = amount;
            TotalPrice = totalPrice;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cart"/> class with specified user id, product id, and quantity.
        /// </summary>
        /// <param name="userId">The unique identifier for the user.</param>
        /// <param name="productId">The unique identifier for the product.</param>
        /// <param name="quantity">The quantity of the product.</param>
        public Cart(int userId,
                    int productId,
                    int quantity)
        {
            UserId = userId;
            ProductId = productId;
            Amount = quantity;
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
