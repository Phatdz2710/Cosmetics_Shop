﻿using System;
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
    /// Cart page for user
    /// </summary>
    public sealed partial class CartPage : Page
    {

        public CartPageViewModel ViewModel { get; }

        public CartPage()
        {
            this.InitializeComponent();
            ViewModel = App.ServiceProvider.GetService(typeof(CartPageViewModel)) as CartPageViewModel;

            // Moved the async code to Loaded event
            this.Loaded += CartPage_Loaded;
            ViewModel.ShowDialogRequested += ShowDialog;
        }

        /// <summary>
        /// Loads data for the cart page when the page is loaded.
        /// </summary>
        /// <remarks>
        /// - Fetches all available vouchers asynchronously and binds them to the `voucherComboBox` for selection.
        /// </remarks>
        private async void CartPage_Loaded(object sender, RoutedEventArgs e)
        {
            // Now you can safely call async code here
            voucherComboBox.ItemsSource = await ViewModel.GetAllVouchersAsync();
        }

        /// <summary>
        /// Handles the selection change event for the voucher ComboBox.
        /// </summary>
        /// <remarks>
        /// - Applies the selected voucher to the cart using the `ApplyVoucher` method in the ViewModel.
        /// - Recalculates the total payment using `RecalculateTotalPay` method.
        /// </remarks>
        private void voucherComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (voucherComboBox.SelectedItem is Models.Voucher selectedVoucher)
            {
                ViewModel.ApplyVoucher(selectedVoucher);
                ViewModel.RecalculateTotalPay();
            }
        }

        /// <summary>
        /// Displays a dialog with a message, typically used for notifications.
        /// </summary>
        /// <param name="message">The message to display in the dialog.</param>
        /// <remarks>
        /// - Creates a `ContentDialog` with the provided message and shows it asynchronously.
        /// </remarks>
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
    }


}
