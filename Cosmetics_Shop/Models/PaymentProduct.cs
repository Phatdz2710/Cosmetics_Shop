
using CommunityToolkit.Mvvm.Input;
using Cosmetics_Shop.Services;
using Cosmetics_Shop.Views.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cosmetics_Shop.Models
{
    /// <summary>  
    /// Represents a product in the payment process.  
    /// </summary>  
    public class PaymentProduct : INotifyPropertyChanged
    {
        /// <summary>  
        /// Gets or sets the unique identifier for the cart.  
        /// </summary>  
        public int CartId { get; set; }

        /// <summary>  
        /// Gets or sets the unique identifier for the product.  
        /// </summary>  
        public int ProductId { get; set; }

        /// <summary>  
        /// Gets or sets the image of the product.  
        /// </summary>  
        public string ProductImage { get; set; }

        /// <summary>  
        /// Gets or sets the name of the product.  
        /// </summary>  
        public string ProductName { get; set; }

        /// <summary>  
        /// Gets or sets the price of the product.  
        /// </summary>  
        public int Price { get; set; }

        private int _amount;

        /// <summary>  
        /// Gets or sets the amount of the product.  
        /// </summary>  
        public int Amount
        {
            get => _amount;
            set
            {
                if (_amount != value)
                {
                    _amount = value;
                    OnPropertyChanged(nameof(Amount));
                }
            }
        }

        /// <summary>  
        /// Gets or sets the command to open the product detail page.  
        /// </summary>  
        public ICommand OpenProductDetailCommand { get; set; }

        /// <summary>  
        /// Initializes a new instance of the <see cref="PaymentProduct"/> class with specified values.  
        /// </summary>  
        /// <param name="cartId">The unique identifier for the cart.</param>  
        /// <param name="productId">The unique identifier for the product.</param>  
        /// <param name="image">The image of the product.</param>  
        /// <param name="productName">The name of the product.</param>  
        /// <param name="price">The price of the product.</param>  
        /// <param name="amount">The amount of the product.</param>  
        public PaymentProduct(
                    int cartId,
                    int productId,
                    string image,
                    string productName,
                    int price,
                    int amount)
        {
            CartId = cartId;
            ProductId = productId;
            ProductImage = image;
            ProductName = productName;
            Price = price;
            Amount = amount;
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