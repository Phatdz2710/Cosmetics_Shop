using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Views.Converters
{
    /// <summary>
    /// Converter to check if an image is available at the given path.
    /// </summary>
    public class IsImageAvailableConverter : IValueConverter
    {
        /// <summary>
        /// Converts a string path to a <see cref="Visibility"/> value based on the existence of the image file.
        /// </summary>
        /// <param name="value">The path to the image file.</param>
        /// <param name="targetType">The type of the target property. This parameter is not used.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic. This parameter is not used.</param>
        /// <param name="language">The language of the conversion. This parameter is not used.</param>
        /// <returns><see cref="Visibility.Collapsed"/> if the image file exists, otherwise <see cref="Visibility.Visible"/>.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                if (value is string path && !string.IsNullOrWhiteSpace(path))
                {
                    // Create Uri from the path
                    var uri = new Uri(path, UriKind.RelativeOrAbsolute);

                    // If the path is relative, convert it to absolute
                    if (!uri.IsAbsoluteUri)
                    {
                        var basePath = AppContext.BaseDirectory; // Application base directory
                        path = Path.Combine(basePath, path);
                    }

                    // Check if the file exists
                    if (File.Exists(path))
                    {
                        return Visibility.Collapsed;
                    }
                    else return Visibility.Visible;
                }
            }
            catch
            {
                // Handle invalid path cases
                return Visibility.Visible;
            }

            return Visibility.Visible;
        }

        
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}