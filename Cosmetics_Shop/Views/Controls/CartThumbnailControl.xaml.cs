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
using System;
using System.Threading.Tasks;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Cosmetics_Shop.Views.Controls
{
    /// <summary>
    /// Cart thumbnail user control
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

        private async void minusButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.CartThumbnail.Amount > 1)
            {
                ViewModel.CartThumbnail.Amount--;
                UpdateCartAsync(ViewModel.CartThumbnail.Id, ViewModel.CartThumbnail.Amount);
            }
            else
            {
                // Tạo ContentDialog để xác nhận xóa
                var dialog = new ContentDialog
                {
                    Title = "Xác nhận xóa",
                    Content = "Bạn có muốn xóa sản phẩm này khỏi giỏ hàng không?",
                    PrimaryButtonText = "Có",
                    CloseButtonText = "Không"
                };

                // Hiển thị ContentDialog
                dialog.XamlRoot = this.XamlRoot; // Sử dụng XamlRoot của trang hiện tại

                // Hiển thị ContentDialog và chờ người dùng phản hồi
                var result = await dialog.ShowAsync();

                if (result == ContentDialogResult.Primary) // Nếu người dùng chọn "Có"
                {
                    // Gọi phương thức xóa sản phẩm khỏi giỏ hàng
                    DeleteProductFromCart(ViewModel.CartThumbnail.Id);
                }
            }
        }

        private void plusButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.CartThumbnail.Amount++;
            UpdateCartAsync(ViewModel.CartThumbnail.Id, ViewModel.CartThumbnail.Amount);
        }

        private async void DeleteProductFromCart(int cartId)
        {
            // Gọi phương thức xóa sản phẩm từ giỏ hàng
            bool isDeleted = await ViewModel._cartPageViewModel.DeleteFromCartAsync(cartId);
            var dialog = new ContentDialog
            {
                Title = isDeleted ? "Thành công" : "Lỗi",
                Content = isDeleted ? "Sản phẩm đã được xóa khỏi giỏ hàng." : "Không thể xóa sản phẩm khỏi giỏ hàng.",
                CloseButtonText = "OK"
            };

            // Hiển thị ContentDialog
            dialog.XamlRoot = this.XamlRoot; // Sử dụng XamlRoot của trang hiện tại

            // Hiển thị ContentDialog
            await dialog.ShowAsync();
        }

        private async void UpdateCartAsync(int cartID, int quantity)
        {
            bool isUpdate = await ViewModel._cartPageViewModel.UpdateCartAsync(cartID, quantity);
            //return isUpdate;
        }
    }
}
