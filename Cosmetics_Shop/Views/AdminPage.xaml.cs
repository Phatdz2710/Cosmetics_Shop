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
using Cosmetics_Shop.Services;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Cosmetics_Shop.Views.Pages
{
    /// <summary>
    /// Admin page for admin
    /// </summary>
    public sealed partial class AdminPage : Page
    {
        public AdminViewModel ViewModel { get; set; }
        public AdminPage()
        {
            this.InitializeComponent();
            var navigationService = App.ServiceProvider.GetService(typeof(INavigationService)) as INavigationService;
            navigationService.Initialize(MainFrame);

            var viewModel = App.ServiceProvider.GetService(typeof(AdminViewModel)) as AdminViewModel;
            this.ViewModel = viewModel;

        }
    }
}
