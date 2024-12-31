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
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Cosmetics_Shop.Views
{
    /// <summary>
    /// Main window for the application
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get; set; }

        public MainWindow()
        {
            this.InitializeComponent();

            // Setup window size and position
            this.AppWindow.Resize(new SizeInt32(1700, 900));
            App.CenterWindow(this.AppWindow);
            
            
            var navigationService = App.ServiceProvider.GetService(typeof(INavigationService)) as INavigationService;
            navigationService.Initialize(MainPageFrame);

            ViewModel = App.ServiceProvider.GetService(typeof(MainViewModel)) as MainViewModel;

            var eventAggregator = App.ServiceProvider.GetService(typeof(IEventAggregator)) as IEventAggregator;

            // Close window
            eventAggregator.Subscribe<LogoutMessage>(WindowClose);

            var filePickerService = App.ServiceProvider.GetService(typeof(IFilePickerService)) as IFilePickerService;
            filePickerService.SetWindowFocus(this);
        }



        /// <summary>
        /// Close main window when log out (viewmodel sends a log out message
        /// </summary>
        /// <param name="message"></param>
        private void WindowClose(LogoutMessage message)
        {
            this.Close();
        }



        
    }
}
