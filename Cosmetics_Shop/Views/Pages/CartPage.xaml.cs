using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Cosmetics_Shop.ViewModels.PageViewModels;
using Cosmetics_Shop.Views.Controls;
using Cosmetics_Shop.Models; // Adjust the namespace as necessary

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Cosmetics_Shop.Views.Pages
{
    /// <summary>
    /// Cart page
    /// </summary>
    public sealed partial class CartPage : Page
    {

        public CartPageViewModel ViewModel { get; }

        public CartPage()
        {
            this.InitializeComponent();
            ViewModel = App.ServiceProvider.GetService(typeof(CartPageViewModel)) as CartPageViewModel;
            voucherComboBox.ItemsSource = ViewModel.GetAllVouchers();
        }

        /// <summary>
        /// Select voucher
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void voucherComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (voucherComboBox.SelectedItem is Voucher selectedVoucher)
            {
                ViewModel.ApplyVoucher(selectedVoucher);
            }
        }
    }


}
