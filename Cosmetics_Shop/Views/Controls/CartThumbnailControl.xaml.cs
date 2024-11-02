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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CartThumbnailControl : UserControl
    {
        public CartThumbnailViewModel ViewModel
        {
            get { return (CartThumbnailViewModel)GetValue(ViewModelProperty); }
            set
            {
                SetValue(ViewModelProperty, value);
                DataContext = value;
            }
        }

        // Sử dụng DependencyProperty để hỗ trợ binding
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel",
                                        typeof(CartThumbnailViewModel),
                                        typeof(CartThumbnailControl),
                                        new PropertyMetadata(null));

        public CartThumbnailControl()
        {
            this.InitializeComponent();
        }

        private void minusButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(amountTextBox.Text, out int currentAmount) && currentAmount > 1)
            {
                amountTextBox.Text = (currentAmount - 1).ToString();
            }
        }

        private void plusButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(amountTextBox.Text, out int currentAmount))
            {
                amountTextBox.Text = (currentAmount + 1).ToString();
            }
        }

    }
}
