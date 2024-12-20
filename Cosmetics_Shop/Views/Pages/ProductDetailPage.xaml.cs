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
    /// Product detail page
    /// </summary>
    public sealed partial class ProductDetailPage : Page
    {
        public ProductDetailViewModel ViewModel { get; }

        public int proId = 1;
        public ProductDetailPage()
        {
            this.InitializeComponent();
            ViewModel = App.ServiceProvider.GetService(typeof(ProductDetailViewModel)) as ProductDetailViewModel;

            ViewModel.LoadInitialReviews(1);

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
        private void minusButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.Amount > 1)
            {
                ViewModel.Amount--; // Decrease the amount
            }
        }
        private void plusButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Amount++; // Increase the amount
        }

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
        #endregion

    }
}
