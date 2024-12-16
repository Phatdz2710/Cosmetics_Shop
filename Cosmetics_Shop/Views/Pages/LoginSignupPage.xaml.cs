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
using Cosmetics_Shop.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Cosmetics_Shop.Views.Pages
{
    /// <summary>
    /// Login and Signup Page
    /// </summary>
    public sealed partial class LoginSignupPage : Page
    {
        public LoginViewModel ViewModel { get; }
        public LoginSignupPage()
        {
            this.InitializeComponent();
            ViewModel = App.ServiceProvider.GetService(typeof(LoginViewModel)) as LoginViewModel;

            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            string theme = localSettings.Values["Theme"] as string;

            if (theme == "Light")
            {
                this.RequestedTheme = ElementTheme.Light;
            }
            else if (theme == "Dark")
            {
                this.RequestedTheme = ElementTheme.Dark;
            }
            else
            {
                this.RequestedTheme = ElementTheme.Default;
            }
        }
    }
}
