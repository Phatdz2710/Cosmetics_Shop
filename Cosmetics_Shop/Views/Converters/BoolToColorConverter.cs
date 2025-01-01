using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Cosmetics_Shop.Views.Converters
{
    /// <summary>
    /// Converts boolean to Brush (Gold for true, Gray for false).
    /// </summary>
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool isActive && isActive)
            {
                return new SolidColorBrush(Microsoft.UI.Colors.Gold); // Màu vàng cho ngôi sao được chọn
            }
            return new SolidColorBrush(Microsoft.UI.Colors.Gray); // Màu xám cho ngôi sao chưa chọn
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
