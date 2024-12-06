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
using Cosmetics_Shop.Models;
using Cosmetics_Shop.Views.Pages;
using Cosmetics_Shop.Services;
using Cosmetics_Shop.Services.Interfaces;
using Cosmetics_Shop.Services.EventAggregatorMessages;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Cosmetics_Shop.Views
{
    /// <summary>
    /// Main window
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get; set; }

        public MainWindow()
        {
            this.InitializeComponent();
            this.AppWindow.Resize(new Windows.Graphics.SizeInt32(1700, 900));
            
            var navigationService = App.ServiceProvider.GetService(typeof(INavigationService)) as INavigationService;
            navigationService.Initialize(MainPageFrame);

            ViewModel = App.ServiceProvider.GetService(typeof(MainViewModel)) as MainViewModel;

            IEventAggregator eventAggregator = App.ServiceProvider.GetService(typeof(IEventAggregator)) as IEventAggregator;
            // Close window
            eventAggregator.Subscribe<LogoutMessage>(WindowClose);

            IFilePickerService filePickerService = App.ServiceProvider.GetService(typeof(IFilePickerService)) as IFilePickerService;
            filePickerService.SetWindowFocus(this);
        }

        private void WindowClose(LogoutMessage message)
        {
            this.Close();
        }
    }
}
