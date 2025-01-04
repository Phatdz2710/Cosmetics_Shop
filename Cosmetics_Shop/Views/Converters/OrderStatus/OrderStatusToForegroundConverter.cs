using Microsoft.UI;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Views.Converters.OrderStatus
{
    /// <summary>
    /// View to convert order status to foreground color
    /// </summary>
    public class OrderStatusToForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is int status)
            {
                return status switch
                {
                    0 => new SolidColorBrush(Colors.DarkGoldenrod), // Đang chờ duyệt
                    1 => new SolidColorBrush(Colors.DarkGreen),    // Đã duyệt
                    2 => new SolidColorBrush(Colors.DarkBlue),     // Đã xác nhận nhận hàng
                    3 => new SolidColorBrush(Colors.DarkRed),     // Đã hủy đơn hàng
                    _ => new SolidColorBrush(Colors.Black),        // Mặc định
                };
            }
            return new SolidColorBrush(Colors.Black);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
