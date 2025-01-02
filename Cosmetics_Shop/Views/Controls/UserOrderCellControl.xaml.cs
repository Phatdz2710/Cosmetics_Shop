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
using Cosmetics_Shop.ViewModels.UserControlViewModels;
using System.Windows.Input;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Cosmetics_Shop.Views.Controls
{
    public sealed partial class UserOrderCellControl : UserControl
    {
        public UserOrderCellViewModel ViewModel
        {
            get { return (UserOrderCellViewModel)GetValue(ViewModelProperty); }
            set
            {
                SetValue(ViewModelProperty, value);
                DataContext = value;
            }
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel",
                                        typeof(UserOrderCellViewModel),
                                        typeof(UserOrderCellControl),
                                        new PropertyMetadata(null));
        public UserOrderCellControl()
        {
            this.InitializeComponent();
        }

        public void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (e.OriginalSource is FrameworkElement element && element is Button)
            {
                // Nếu nguồn sự kiện là Button, không xử lý
                return;
            }

            ICommand command = ViewModel.ShowHideItemCommand;
            if (command.CanExecute(null))
            {
                command.Execute(null);
            }
        }

        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true; // Ngăn sự kiện tiếp tục lan lên cha
        }
    }
}
