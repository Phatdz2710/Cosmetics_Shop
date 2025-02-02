﻿
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
    /// Converts message to foreground color.  
    /// If the message indicates success, the color is LightSeaGreen.  
    /// If the message indicates an error, the color is Red.  
    /// </summary>  
    public class MessageForegroundConverter : IValueConverter
    {
        /// <summary>  
        /// Converts a message string to a SolidColorBrush based on the message content.  
        /// </summary>  
        /// <param name="value">The message string to convert.</param>  
        /// <param name="targetType">The type of the target property. This parameter is not used.</param>  
        /// <param name="parameter">An optional parameter to be used in the converter logic. This parameter is not used.</param>  
        /// <param name="language">The language of the conversion. This parameter is not used.</param>  
        /// <returns>A <see cref="SolidColorBrush"/> representing the foreground color based on the message content.</returns>  
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
            return new SolidColorBrush(Colors.Red);
        }

        
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}