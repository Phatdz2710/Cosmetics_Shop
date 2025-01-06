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
    /// Converts user level to color.
    /// </summary>
    public class UserLevelConverter : IValueConverter
    {
        /// <summary>
        /// Converts a user level string to a SolidColorBrush.
        /// </summary>
        /// <param name="value">The user level string to convert.</param>
        /// <param name="targetType">The type of the target property. This parameter is not used.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic. This parameter is not used.</param>
        /// <param name="language">The language of the conversion. This parameter is not used.</param>
        /// <returns>A <see cref="SolidColorBrush"/> representing the color based on the user level.</returns>
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
            return new SolidColorBrush(Colors.Green);
        }

        
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}