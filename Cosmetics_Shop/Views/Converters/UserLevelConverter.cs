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
    /// Converts user level to color
    /// </summary>
    public class UserLevelConverter : IValueConverter
    {
        // If user level is 0, return color green, else return color red
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string level)
            {
                if (level == "ULTRA")
                {
                    return new SolidColorBrush(Colors.DarkRed);
                }
                else if (level == "VIP")
                {
                    return new SolidColorBrush(Colors.DarkBlue);
                }
                else
                {
                    return new SolidColorBrush(Colors.DarkGreen);

                }
            }
            return "Green";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

    }
}
