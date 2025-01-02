using Microsoft.UI;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace Cosmetics_Shop.Views.Converters.OrderStatus
{
    public class OrderStatusToBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is int status)
            {
                return status switch
                {
                    0 => new SolidColorBrush(Colors.LightYellow), // Đang chờ duyệt
                    1 => new SolidColorBrush(Colors.LightGreen),  // Đã duyệt
                    2 => new SolidColorBrush(Colors.LightBlue),   // Đã xác nhận nhận hàng
                    3 => new SolidColorBrush(Colors.Tomato),   // Đã hủy đơn hàng
                    _ => new SolidColorBrush(Colors.Transparent), // Mặc định
                };
            }
            return new SolidColorBrush(Colors.Transparent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
