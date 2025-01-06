using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Views.Converters
{
    /// <summary>
    /// Converts a double value to a string with up to three decimal places.
    /// </summary>
    public class DecimalToThreeDecimalConverter : IValueConverter
    {
        /// <summary>
        /// Converts a double value to a string with up to three decimal places.
        /// </summary>
        /// <param name="value">The double value to convert.</param>
        /// <param name="targetType">The type of the target property. This parameter is not used.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic. This parameter is not used.</param>
        /// <param name="language">The language of the conversion. This parameter is not used.</param>
        /// <returns>A string representation of the double value with up to three decimal places.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is double number)
            {
                // Round to 3 decimal places
                return Math.Round(number, 2).ToString("0.###", CultureInfo.InvariantCulture);
            }
            return value;
        }

        /// <summary>
        /// Converts a string value back to a double.
        /// </summary>
        /// <param name="value">The string value to convert back.</param>
        /// <param name="targetType">The type to convert back to.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic. This parameter is not used.</param>
        /// <param name="language">The language of the conversion. This parameter is not used.</param>
        /// <returns>A double value parsed from the string, or the original value if parsing fails.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is string str && double.TryParse(str, out double result))
            {
                return result;
            }
            return value;
        }
    }
}