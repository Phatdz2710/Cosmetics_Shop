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

namespace Cosmetics_Shop.Views.Objects
{
    public sealed partial class ProductThumbnailControl : UserControl
    {
        public ProductThumbnailViewModel ViewModel
        {
            get { return (ProductThumbnailViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value);
                DataContext = value;
            }
        }

        // Sử dụng DependencyProperty để hỗ trợ binding
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel",
                                        typeof(ProductThumbnailViewModel),
                                        typeof(ProductThumbnailControl),
                                        new PropertyMetadata(null));

        public ProductThumbnailControl()
        {
            this.InitializeComponent();
        }
    }
}
