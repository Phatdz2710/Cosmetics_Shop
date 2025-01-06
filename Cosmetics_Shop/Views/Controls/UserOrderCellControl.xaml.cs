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
    /// <summary>
    /// User Order Cell control
    /// </summary>
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

        /// <summary>
        /// Handles the tap event on the grid, allowing the execution of a command from the ViewModel when tapped.
        /// </summary>
        /// <remarks>
        /// - If the tapped element is a `Button`, it will not trigger the command.
        /// - Otherwise, it checks if the `ShowHideItemCommand` in the ViewModel can be executed and calls it.
        /// </remarks>
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

        /// <summary>
        /// Handles the tap event on a button, preventing the event from propagating to the parent.
        /// </summary>
        /// <remarks>
        /// - This ensures that when a button is tapped, the event does not trigger the parent tap event (`Grid_Tapped`).
        /// </remarks>
        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true; // Ngăn sự kiện tiếp tục lan lên cha
        }
    }
}
