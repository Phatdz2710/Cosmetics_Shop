using CommunityToolkit.Mvvm.Input;
using Cosmetics_Shop.Services;
using Cosmetics_Shop.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cosmetics_Shop.Models
{
    /// <summary>
    /// Used to display order items in the order list.
    /// </summary>
    public class OrderItemDisplay
    {
        /// <summary>
        /// Gets or sets the unique identifier for the product.
        /// </summary>
        public int ProductId { get; set; } // Id of product

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string ProductName { get; set; } // Name of product

        /// <summary>
        /// Gets or sets the quantity of the product.
        /// </summary>
        public int Quantity { get; set; } // Quantity of product

        /// <summary>
        /// Gets or sets the image source of the product.
        /// </summary>
        public string ImageSource { get; set; } // Image of product

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public int Price { get; set; } // Price of product

        /// <summary>
        /// Gets or sets the total price of the product.
        /// </summary>
        public int TotalPrice { get; set; } // Total price of product

        /// <summary>
        /// Gets or sets the command to open the product detail page.
        /// </summary>
        public ICommand OpenProductDetailCommand { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderItemDisplay"/> class with specified values.
        /// </summary>
        /// <param name="productId">The unique identifier for the product.</param>
        /// <param name="productName">The name of the product.</param>
        /// <param name="quantity">The quantity of the product.</param>
        /// <param name="imageSource">The image source of the product.</param>
        /// <param name="price">The price of the product.</param>
        /// <param name="totalPrice">The total price of the product.</param>
        public OrderItemDisplay(int productId, string productName, int quantity, string imageSource, int price, int totalPrice)
        {
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            ImageSource = imageSource;
            Price = price;
            TotalPrice = Quantity * Price;

            OpenProductDetailCommand = new RelayCommand(OpenProductDetail);
        }

        /// <summary>
        /// Opens the product detail page.
        /// </summary>
        private void OpenProductDetail()
        {
            var navigationService = App.ServiceProvider.GetService(typeof(INavigationService)) as INavigationService;
            navigationService.NavigateTo<ProductDetailPage>(ProductId);
        }
    }
}
