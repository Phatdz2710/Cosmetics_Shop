using Microsoft.UI;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using System;

namespace Cosmetics_Shop.Views.Converters
{
    /// <summary>
    /// Converts a rating value to a background color.
    /// </summary>
    public class BackgroundColorByRatingConverter : IValueConverter
    {
        /// <summary>
        /// Converts a rating value to a background color.
        /// </summary>
        /// <param name="value">The rating value to convert.</param>
        /// <param name="targetType">The type of the target property. This parameter is not used.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic. This parameter is not used.</param>
        /// <param name="language">The language of the conversion. This parameter is not used.</param>
        /// <returns>A <see cref="SolidColorBrush"/> representing the background color based on the rating value.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is double rating)
            {
                if (rating >= 4.5)
                {
                    return new SolidColorBrush(Colors.DarkBlue); // Dark blue color for high ratings
                }
                else if (rating >= 3.5)
                {
                    return new SolidColorBrush(Colors.Orange); // Orange color for medium-high ratings
                }
                else if (rating >= 2.5)
                {
                    return new SolidColorBrush(Colors.Tomato); // Tomato color for medium ratings
                }
                else
                {
                    return new SolidColorBrush(Colors.Silver); // Silver color for low ratings
                }
            }
            return new SolidColorBrush(Colors.Silver); // Default color
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
