using Cosmetics_Shop.ViewModels.PageViewModels;
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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Cosmetics_Shop.Views.Pages
{
    /// <summary>
    /// Dashboard page
    /// </summary>
    public sealed partial class DashboardPage : Page
    {
        public DashboardPageViewModel ViewModel { get; }
        public DashboardPage()
        {
            this.InitializeComponent();
            ViewModel = App.ServiceProvider.GetService(typeof(DashboardPageViewModel)) as DashboardPageViewModel;
        }
    }
}
