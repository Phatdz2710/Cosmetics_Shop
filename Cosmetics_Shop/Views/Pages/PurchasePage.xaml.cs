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
using Cosmetics_Shop.Services.Interfaces;
using Cosmetics_Shop.Services;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Cosmetics_Shop.Views.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PurchasePage : Page
    {
        public PurchasePageViewModel ViewModel { get; }
        public PurchasePage()
        {
            this.InitializeComponent();
            ViewModel = App.ServiceProvider.GetService(typeof(PurchasePageViewModel)) as PurchasePageViewModel;

            IEventAggregator eventAggregator = App.ServiceProvider.GetService(typeof(IEventAggregator)) as IEventAggregator;
            eventAggregator.Subscribe<InvalidPriceMinMaxMessageBox>((message) =>
            {
                ShowContentDialog(message.Message);
            });
        }

        private async void ShowContentDialog(string message)
        {
            var dialog = new ContentDialog
            {
                Title = message,
                CloseButtonText = "OK",
                XamlRoot = this.Content.XamlRoot
            };
            await dialog.ShowAsync();
        }
    }
}
