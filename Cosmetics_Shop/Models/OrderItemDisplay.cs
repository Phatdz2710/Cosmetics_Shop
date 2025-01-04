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
    /// Represents an order item display in the order list
    /// </summary>
    public class OrderItemDisplay
    {
        public int ProductId {get; set;}
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string ImageSource { get; set; }
        public int Price { get; set; }
        public int TotalPrice { get; set; }

        public ICommand OpenProductDetailCommand { get; set; }

        public OrderItemDisplay(int productId, string productName, int quantity, string imageSource, int price,int totalPrice)
        {
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            ImageSource = imageSource;
            Price = price;
            TotalPrice = Quantity*Price;

            OpenProductDetailCommand = new RelayCommand(OpenProductDetail);
        }

        private void OpenProductDetail()
        {
            var navigationService = App.ServiceProvider.GetService(typeof(INavigationService)) as INavigationService;
            navigationService.NavigateTo<ProductDetailPage>(ProductId);
        }
    }
}
