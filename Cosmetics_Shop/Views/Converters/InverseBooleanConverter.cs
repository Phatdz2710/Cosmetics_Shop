
using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Views.Converters
{
    /// <summary>  
    /// Inverts a boolean value.  
    /// </summary>  
    public class InverseBooleanConverter : IValueConverter
    {
        /// <summary>  
        /// Converts a boolean value to its inverse.  
        /// </summary>  
        /// <param name="value">The boolean value to convert.</param>  
        /// <param name="targetType">The type of the target property. This parameter is not used.</param>  
        /// <param name="parameter">An optional parameter to be used in the converter logic. This parameter is not used.</param>  
        /// <param name="language">The language of the conversion. This parameter is not used.</param>  
        /// <returns>The inverse of the boolean value, or false if the value is not a boolean.</returns>  
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool booleanValue)
            {
                return !booleanValue; // Invert the boolean value  
            }
            return false; // Default to false if the value is not a boolean  
        }

        /// <summary>  
        /// Converts a boolean value back to its inverse.  
        /// </summary>  
        /// <param name="value">The boolean value to convert back.</param>  
        /// <param name="targetType">The type to convert back to.</param>  
        /// <param name="parameter">An optional parameter to be used in the converter logic. This parameter is not used.</param>  
        /// <param name="language">The language of the conversion. This parameter is not used.</param>  
        /// <returns>The inverse of the boolean value, or false if the value is not a boolean.</returns>  
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is bool booleanValue)
            {
                return !booleanValue; // Invert the boolean value back  
            }
            return false; // Default to false if the value is not a boolean  
        }
    }
}