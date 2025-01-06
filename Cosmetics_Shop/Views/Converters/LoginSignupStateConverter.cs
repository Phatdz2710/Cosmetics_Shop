
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Views.Converters
{
    /// <summary>  
    /// Converts login/signup state to visibility.  
    /// </summary>  
    public class LoginSignupStateConverter : IValueConverter
    {
        /// <summary>  
        /// Converts a login/signup state string to a visibility value.  
        /// </summary>  
        /// <param name="value">The state string to convert.</param>  
        /// <param name="targetType">The type of the target property. This parameter is not used.</param>  
        /// <param name="parameter">The parameter string to compare with the state.</param>  
        /// <param name="language">The language of the conversion. This parameter is not used.</param>  
        /// <returns><see cref="Visibility.Visible"/> if the state matches the parameter, otherwise <see cref="Visibility.Collapsed"/>.</returns>  
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string state && parameter is string parameterStr)
            {
                return state == parameterStr ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }


        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}