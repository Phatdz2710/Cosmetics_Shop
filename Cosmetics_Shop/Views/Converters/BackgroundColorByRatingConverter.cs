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
    /// Converts rating to background color
    /// </summary>
    public class BackgroundColorByRatingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is double rating)
            {
                if (rating >= 4.5)
                {
                    return new SolidColorBrush(Colors.DarkBlue); // Màu vàng
                }
                else if (rating >= 3.5)
                {
                    return new SolidColorBrush(Colors.Gold); // Màu bạc
                }
                else if (rating >= 2.5)
                {
                    return new SolidColorBrush(Colors.Tomato); // Màu bạc
                }
                else
                {
                    return new SolidColorBrush(Colors.Silver); // Màu đỏ
                }
            }
            return new SolidColorBrush(Colors.Silver); // Giá trị mặc định
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
