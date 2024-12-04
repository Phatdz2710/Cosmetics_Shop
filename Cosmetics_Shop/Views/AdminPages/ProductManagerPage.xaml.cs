using Cosmetics_Shop.ViewModels.AdminPageViewModels;
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

namespace Cosmetics_Shop.Views.AdminPages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProductManagerPage : Page
    {
        public ProductManagerViewModel ViewModel { get; set; }
        public ProductManagerPage()
        {
            this.InitializeComponent();
            var viewModel = App.ServiceProvider.GetService(typeof(ProductManagerViewModel)) as ProductManagerViewModel;
            this.ViewModel = viewModel;
        }
    }
}
