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
    public sealed partial class ProductDetailPage : Page
    {
        public ProductDetailViewModel ViewModel { get; }
        public int productId = 1;
        public ProductDetailPage()
        {
            this.InitializeComponent();
            ViewModel = App.ServiceProvider.GetService(typeof(ProductDetailViewModel)) as ProductDetailViewModel;

            ViewModel.LoadProductDetail(productId);
            ViewModel.LoadInitialReviews(productId);
            deliveryComboBox.ItemsSource = ViewModel.GetShippingMethods();
        }

        private void minusButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(amountTextBox.Text, out int currentAmount) && currentAmount > 1)
            {
                amountTextBox.Text = (currentAmount - 1).ToString();
            }
        }

        private void plusButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(amountTextBox.Text, out int currentAmount))
            {
                amountTextBox.Text = (currentAmount + 1).ToString();
            }
        }

        private void themvaogiohangButton_Click(object sender, RoutedEventArgs e)
        {
            // Create a new product to add to the existing cart   
            var newProduct = new CartThumbnail(7, null, "New Product", "Description of new product", 200000, 1, 20000);

            // Add the new product to the existing list
            ViewModel.AddNewProductToCart(newProduct);
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

    }
}
