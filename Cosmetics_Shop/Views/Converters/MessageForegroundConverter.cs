using Microsoft.UI;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Views.Converters
{
    /// <summary>
    /// Converts message to foreground color
    /// If error is Red and if success is Green
    /// </summary>
    public class MessageForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string message)
            {
                if (message == "Tạo tài khoản thành công!" ||
                    message == "Thêm sản phẩm thành công!" ||
                    message == "Thêm tài khoản thành công!" ||
                    message == "Đổi mật khẩu thành công!" ||
                    message == "Xóa sản phẩm thành công!" ||
                    message == "Xóa tài khoản thành công!" ||
                    message == "Sửa sản phẩm thành công!" ||
                    message == "Thay đổi thông tin thành công!")

                {
                    return new SolidColorBrush(Colors.LightSeaGreen);
                }
            }
            return new SolidColorBrush(Colors.Red); ;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}

