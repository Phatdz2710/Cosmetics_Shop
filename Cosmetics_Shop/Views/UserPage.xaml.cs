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
    /// User page
    /// </summary>
    public sealed partial class UserPage : Page
    {
        public UserViewModel ViewModel { get; set; }
        public UserPage()
        {
            this.InitializeComponent();
            var navigationService = (INavigationService)App.ServiceProvider.GetService(typeof(INavigationService));
            navigationService.Initialize(rootFrame);

            rootFrame.NavigationFailed += RootFrame_NavigationFailed;

            ViewModel = (UserViewModel)App.ServiceProvider.GetService(typeof(UserViewModel));

            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            string theme = localSettings.Values["Theme"] as string;

            if (theme == "Light")
            {
                ChangeThemeCombobox.SelectedIndex = 0;
                this.RequestedTheme = ElementTheme.Light;
            }
            else if (theme == "Dark")
            {
                ChangeThemeCombobox.SelectedIndex = 1;
                this.RequestedTheme = ElementTheme.Dark;
            }
            else
            {
                ChangeThemeCombobox.SelectedIndex = 2;
                this.RequestedTheme = ElementTheme.Default;
            }
        }

        /// <summary>
        /// Navigation failed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        private void RootFrame_NavigationFailed(object sender, Microsoft.UI.Xaml.Navigation.NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }


        /// <summary>
        /// Change Theme 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThemeComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var themeSelected = ChangeThemeCombobox.SelectedIndex;
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            switch (themeSelected)
            {
                case 0:
                    this.RequestedTheme = ElementTheme.Light;
                    localSettings.Values["Theme"] = "Light";
                    break;
                case 1:
                    this.RequestedTheme = ElementTheme.Dark;
                    localSettings.Values["Theme"] = "Dark";
                    break;
                case 2:
                    this.RequestedTheme = ElementTheme.Default;
                    localSettings.Values["Theme"] = "Default";
                    break;
            }
        }
    }
}
