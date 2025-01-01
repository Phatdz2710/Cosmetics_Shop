using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Cosmetics_Shop.ViewModels.PageViewModels;
using Cosmetics_Shop.Models;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Cosmetics_Shop.Views.Pages
{
    /// <summary>
    /// Product detail page for user
    /// </summary>
    public sealed partial class ProductDetailPage : Page
    {
        public ProductDetailViewModel ViewModel { get; }

        public ProductDetailPage()
        {
            this.InitializeComponent();
            ViewModel = App.ServiceProvider.GetService(typeof(ProductDetailViewModel)) as ProductDetailViewModel;

            //ViewModel.LoadReviews();

            // Moved the async code to Loaded event
            this.Loaded += ProductDetailPage_Loaded;
        }

        private async void ProductDetailPage_Loaded(object sender, RoutedEventArgs e)
        {
            // Now you can safely call async code here
            deliveryComboBox.ItemsSource = await ViewModel.GetShippingMethodsAsync();
            
        }

        #region Navigation
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is int productId)
            {
                await ViewModel.LoadProductDetailAsync(productId);
                await ViewModel.LoadInitialReviews(productId);

                // Remove currency symbol and commas, and trim whitespace
                string priceText = priceTextBlock.Text.Replace(" đ", "").Replace(",", "").Trim();

                // Initialize variables for price and amount
                int price = 0;
                int amount = 0;

                // Try to parse the price
                if (!int.TryParse(priceText, out price))
                {
                    price = 0; // Default value
                }

                // Try to parse the amount
                if (!int.TryParse(amountTextBox.Text, out amount))
                {
                    amount = 1; // Default value
                }

                // Create the product instance
                var product = new PaymentProductThumbnail(productId, null, productDetailName.Text, price, amount);
                ViewModel.SetInfo(product);
            }
        }
        #endregion

        #region Click button
        /// <summary>
        /// Click to minus the quantity
        /// </summary>
        private void minusButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.Amount > 1)
            {
                ViewModel.Amount--; // Decrease the amount
            }
        }

        /// <summary>
        /// Click to plus the quantity
        /// </summary>
        private void plusButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.Amount < ViewModel.ProductDetail.availableAmount)
            {
                ViewModel.Amount++; // Increase the amount
            }
        }

        /// <summary>
        /// Click to category the product rating 
        /// </summary>
        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string tag)
            {
                if (tag == "All")
                {
                    // Call the method to show all reviews
                    ViewModel.ShowAllReviews();
                }
                else if (int.TryParse(tag, out int starNumber))
                {
                    // Call the filter method in the ViewModel
                    ViewModel.FilterReviewsByStarNumber(starNumber);
                }
            }
        }

        /// <summary>
        /// Click to add a product to cart 
        /// </summary>
        private async void themvaogiohangButton_Click(object sender, RoutedEventArgs e)
        {
            // Lấy productId từ ViewModel
            int productId = ViewModel.ProductDetail.Id; // Giả sử bạn có thuộc tính Id trong ProductDetail
            int quantity = ViewModel.Amount; // Lấy số lượng từ ViewModel

            // Gọi phương thức AddToCartAsync
            bool result = await ViewModel.AddToCartAsync(productId, quantity);

            // Tạo ContentDialog
            var dialog = new ContentDialog
            {
                Title = result ? "Thành công" : "Lỗi",
                Content = result ? "Sản phẩm đã được thêm vào giỏ hàng." : "Không thể thêm sản phẩm vào giỏ hàng.",
                CloseButtonText = "OK"
            };

            // Hiển thị ContentDialog
            dialog.XamlRoot = this.XamlRoot; // Sử dụng XamlRoot của trang hiện tại

            // Hiển thị ContentDialog
            await dialog.ShowAsync();
        }
        #endregion

    }
}
