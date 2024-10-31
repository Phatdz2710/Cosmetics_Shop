using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Cosmetics_Shop.ViewModels;
using Cosmetics_Shop.Services;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Cosmetics_Shop.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WindowContent : Page
    {
        public MainViewModel ViewModel;
        public WindowContent()
        {
            this.InitializeComponent();
            var navigationService = (INavigationService)App.ServiceProvider.GetService(typeof(INavigationService));
            navigationService.Initialize(rootFrame);

            rootFrame.NavigationFailed += RootFrame_NavigationFailed;
            rootFrame.Navigate(typeof(Pages.DashboardPage));
            
            ViewModel = (MainViewModel)App.ServiceProvider.GetService(typeof(MainViewModel));
        }

        private void RootFrame_NavigationFailed(object sender, Microsoft.UI.Xaml.Navigation.NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }
    }
}
