using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Views.Converters.OrderStatus
{
    public class OrderStatusToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is int status)
            {
                return status switch
                {
                    0 => "Đang chờ duyệt",       // Trạng thái 0
                    1 => "Đã duyệt",             // Trạng thái 1
                    2 => "Hoàn tất", // Trạng thái 2
                    _ => "Không xác định"        // Trạng thái mặc định
                };
            }
            return "Không xác định"; // Trả về "Không xác định" nếu giá trị không hợp lệ
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
