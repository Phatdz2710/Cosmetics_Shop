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
    /// Payment page
    /// </summary>
    public sealed partial class PaymentPage : Page
    {
        public PaymentPageViewModel ViewModel { get; }

        public PaymentPage()
        {
            this.InitializeComponent();
            ViewModel = App.ServiceProvider.GetService(typeof(PaymentPageViewModel)) as PaymentPageViewModel;
            voucherComboBox.ItemsSource = ViewModel.GetAllVouchers();
            deliveryComboBox.ItemsSource = ViewModel.GetShippingMethods();
        }

        private void voucherComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (voucherComboBox.SelectedItem is Voucher selectedVoucher)
            {
                ViewModel.ApplyVoucher(selectedVoucher);
            }
        }

        private void deliveryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (deliveryComboBox.SelectedItem is ShippingMethod selectedShippingMethod)
            {
                ViewModel.ApplyShipping(selectedShippingMethod);
            }
        }

    }
}
