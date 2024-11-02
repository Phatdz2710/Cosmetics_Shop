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
<<<<<<< HEAD
using Cosmetics_Shop.ViewModels;
=======
>>>>>>> ad4311b5a9c311e96d94838537af8decb763063a

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Cosmetics_Shop.Views.Pages
{
    public sealed partial class ProductDetailPage : Page
    {
<<<<<<< HEAD
        public ProductDetailViewModel ViewModel { get; }
        public ProductDetailPage()
        {
            this.InitializeComponent();
            ViewModel = App.ServiceProvider.GetService(typeof(ProductDetailViewModel)) as ProductDetailViewModel;

            int productId = 2;
            ViewModel.LoadProductDetail(productId);
            ViewModel.LoadInitialReviews(productId);
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
=======
        public ProductDetailPage()
        {
            this.InitializeComponent();
        }

>>>>>>> ad4311b5a9c311e96d94838537af8decb763063a
    }
}
