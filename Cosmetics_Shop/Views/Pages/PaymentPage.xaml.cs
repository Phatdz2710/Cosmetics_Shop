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
using Microsoft.Extensions.DependencyInjection;
using Cosmetics_Shop.DataAccessObject.Interfaces;
using Cosmetics_Shop.Services;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Cosmetics_Shop.Views.Pages
{
    /// <summary>
    /// Payment page for user
    /// </summary>
    public sealed partial class PaymentPage : Page
    {
        public PaymentPageViewModel ViewModel { get; set; }

        public PaymentPage()
        {
            this.InitializeComponent();
        }

        #region Navigation
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is List<PaymentProductThumbnail> products)
            {
                // Create the ViewModel manually
                var navigationService = App.ServiceProvider.GetRequiredService<INavigationService>();
                var dao = App.ServiceProvider.GetRequiredService<IDao>();
                var serviceProvider = App.ServiceProvider; // Use the service provider
                var userSession = App.ServiceProvider.GetRequiredService<UserSession>();

                // Create the ViewModel with the product parameter
                ViewModel = new PaymentPageViewModel(navigationService, dao, serviceProvider, userSession, products);

                voucherComboBox.ItemsSource = await ViewModel.GetAllVouchersAsync();
                deliveryComboBox.ItemsSource = await ViewModel.GetShippingMethodsAsync();

                ViewModel.ShowDialogRequested += ShowDialog;

            }
        }
        #endregion

        private async void ShowDialog(string message)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = "Thông báo",
                Content = message,
                CloseButtonText = "OK"
            };

            dialog.XamlRoot = this.XamlRoot;

            await dialog.ShowAsync(); // Hiển thị hộp thoại
        }

        #region Combobox
        private void voucherComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (voucherComboBox.SelectedItem is Models.Voucher selectedVoucher)
            {
                ViewModel.ApplyVoucher(selectedVoucher);
            }
        }

        private void deliveryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (deliveryComboBox.SelectedItem is Models.ShippingMethod selectedShippingMethod)
            {
                ViewModel.ApplyShipping(selectedShippingMethod);
            }
        }

        private void PaymentMethod_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton && radioButton.Tag is Models.PaymentMethod selectedMethod)
            {
                // Update SelectedPaymentMethod in ViewModel
                ViewModel.SelectedPaymentMethod = selectedMethod; // Assign the selected PaymentMethod object
            }
        }
        #endregion

    }
}
