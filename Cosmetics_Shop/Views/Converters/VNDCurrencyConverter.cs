using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Views.Converters
{
    /// <summary>
    /// Converts an integer value to a VND currency format string.
    /// </summary>
    public class VNDCurrencyConverter : IValueConverter
    {
        /// <summary>
        /// Converts an integer value to a VND currency format string.
        /// </summary>
        /// <param name="value">The integer value to convert.</param>
        /// <param name="targetType">The type of the target property. This parameter is not used.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic. This parameter is not used.</param>
        /// <param name="language">The language of the conversion. This parameter is not used.</param>
        /// <returns>A string representing the value in VND currency format.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is int price)
            {
                return $"{price:0,0}";
            }
            return "0";
        }


        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
