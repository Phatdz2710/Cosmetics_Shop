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
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CartPage : Page
    {
<<<<<<< HEAD
        public CartPageViewModel ViewModel { get; }
=======
        public CartPageViewModel ViewModel { get; set; } = new CartPageViewModel();
>>>>>>> ad4311b5a9c311e96d94838537af8decb763063a
        public CartPage()
        {
            this.InitializeComponent();
            ViewModel = App.ServiceProvider.GetService(typeof(CartPageViewModel)) as CartPageViewModel;
        }
    }
}
