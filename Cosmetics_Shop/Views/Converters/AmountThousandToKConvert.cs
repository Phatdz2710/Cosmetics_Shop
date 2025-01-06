using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Views.Converters
{
    /// <summary>
    /// Converts amount to "k" if it is greater than 1000.
    /// </summary>
    public class AmountThousandToKConvert : IValueConverter
    {
        /// <summary>
        /// Converts an integer amount to a string representation.
        /// If the amount is greater than or equal to 10000, it returns the amount in "k" format.
        /// Otherwise, it returns the amount formatted with commas.
        /// </summary>
        /// <param name="value">The value to convert, expected to be an integer.</param>
        /// <param name="targetType">The type of the target property. This parameter is not used.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic. This parameter is not used.</param>
        /// <param name="language">The language of the conversion. This parameter is not used.</param>
        /// <returns>A string representation of the amount.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is int amount)
            {
                if (amount >= 10000)
                {
                    return $"{amount / 1000}k"; // Returns "10k" for 10000
                }
                return $"{amount:0,0}"; // Formats normally for less than 10000
            }
            return "0";
        }


        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
