
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Views.Converters
{
    /// <summary>  
    /// Converts a string to an ImageSource.  
    /// </summary>  
    public class StringToImageSourceConverter : IValueConverter
    {
        /// <summary>  
        /// Converts a string path to a BitmapImage.  
        /// </summary>  
        /// <param name="value">The string path to convert.</param>  
        /// <param name="targetType">The type of the target property. This parameter is not used.</param>  
        /// <param name="parameter">An optional parameter to be used in the converter logic. This parameter is not used.</param>  
        /// <param name="language">The language of the conversion. This parameter is not used.</param>  
        /// <returns>A <see cref="BitmapImage"/> created from the string path, or null if the path is invalid or an error occurs.</returns>  
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string path && !string.IsNullOrWhiteSpace(path))
            {
                try
                {
                    // Create a BitmapImage from the string path  
                    return new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
                }
                catch (Exception)
                {
                    // Return null or a default value if an error occurs while creating the BitmapImage  
                    return null;
                }
            }

            return null; // Return null if the input is invalid  
        }

        
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}