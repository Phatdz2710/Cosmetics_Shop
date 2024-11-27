using Cosmetics_Shop.Services.EventAggregatorMessages;
using Cosmetics_Shop.Services.Interfaces;
using Cosmetics_Shop.ViewModels;
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
using Windows.UI.ViewManagement;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Cosmetics_Shop
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            this.InitializeComponent();
            this.AppWindow.Resize(new Windows.Graphics.SizeInt32(1200, 750));
            this.LoginSignupFrame.Navigate(typeof(Views.Pages.LoginSignupPage));

            var eventAggregator = App.ServiceProvider.GetService(typeof(IEventAggregator)) as IEventAggregator;
            eventAggregator.Subscribe<CloseLoginWindowMessage>((param) =>
            {
                    this.Close();
            });
        }
    }
}
