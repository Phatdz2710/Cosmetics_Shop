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

namespace Cosmetics_Shop.Views.Controls
{
    public sealed partial class VoucherDialog : UserControl
    {
        public VoucherDialog()
        {
            this.InitializeComponent();
            LoadVouchers();
        }

        private void LoadVouchers()
        {
            // Load vouchers from your data source and bind to voucherListView
            //voucherListView.ItemsSource = ;
        }

        private void voucherListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedVoucher = (Voucher)voucherListView.SelectedItem;

            // Pass the selected voucher back to the CartPage (or ViewModel)
            if (selectedVoucher != null)
            {
                var viewModel = (CartPageViewModel)DataContext;
                viewModel.ApplyVoucher(selectedVoucher);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            // Close the dialog
            var parentDialog = GetParentDialog();
            parentDialog.Hide();
        }

        private ContentDialog GetParentDialog()
        {
            return FindParent<ContentDialog>(this);
        }

        public static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            // Get the parent of the child
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            // If the parent is null, we've reached the end of the tree
            if (parentObject == null) return null;

            // Check if the parent matches the type we're looking for
            T parent = parentObject as T;
            if (parent != null)
            {
                return parent;
            }
            else
            {
                // Recursively drill up the tree
                return FindParent<T>(parentObject);
            }
        }
    }
}
