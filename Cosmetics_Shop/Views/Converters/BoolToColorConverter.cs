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
        /// <summary>
        /// Converts a boolean value to a SolidColorBrush.
        /// </summary>
        /// <param name="value">The boolean value to convert.</param>
        /// <param name="targetType">The type of the target property. This parameter is not used.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic. This parameter is not used.</param>
        /// <param name="language">The language of the conversion. This parameter is not used.</param>
        /// <returns>A <see cref="SolidColorBrush"/> representing Gold if true, otherwise Gray.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool isActive && isActive)
            {
                return new SolidColorBrush(Microsoft.UI.Colors.Gold); // Gold color for true
            }
            return new SolidColorBrush(Microsoft.UI.Colors.Gray); // Gray color for false
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}