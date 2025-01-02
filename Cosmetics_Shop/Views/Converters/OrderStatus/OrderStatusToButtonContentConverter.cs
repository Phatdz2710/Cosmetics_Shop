using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Views.Converters.OrderStatus
{
    public class OrderStatusToButtonContentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is int status)
            {
                return status switch
                {
                    1 => "Đã nhận hàng",       // Trạng thái 1
                    2 => "Đánh giá",           // Trạng thái 2
                    _ => "Lỗi"
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
