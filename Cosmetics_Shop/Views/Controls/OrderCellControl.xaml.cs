using Cosmetics_Shop.ViewModels.UserControlViewModels;
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

namespace Cosmetics_Shop.Views.Controls
{
    /// <summary>
    /// Order cell user control
    /// </summary>
    public sealed partial class OrderCellControl : UserControl
    {
        public OrderCellViewModel ViewModel
        {
            get { return (OrderCellViewModel)GetValue(ViewModelProperty); }
            set
            {
                SetValue(ViewModelProperty, value);
                DataContext = value;
            }
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel",
                                        typeof(OrderCellViewModel),
                                        typeof(OrderCellControl),
                                        new PropertyMetadata(null));
        
        public OrderCellControl()
        {
            this.InitializeComponent();
        }
    }
}
