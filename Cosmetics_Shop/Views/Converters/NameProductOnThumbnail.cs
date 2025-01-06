using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Chat;

namespace Cosmetics_Shop.Views.Converters
{
    /// <summary>
    /// Converts a product name to a thumbnail representation by truncating it if necessary.
    /// </summary>
    public class NameProductOnThumbnail : IValueConverter
    {
        /// <summary>
        /// Converts a product name to a truncated version suitable for a thumbnail.
        /// </summary>
        /// <param name="value">The product name to convert.</param>
        /// <param name="targetType">The type of the target property. This parameter is not used.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic. This parameter is not used.</param>
        /// <param name="language">The language of the conversion. This parameter is not used.</param>
        /// <returns>A truncated version of the product name if it exceeds 22 characters, otherwise the original name.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string nameProduct && nameProduct.Length > 22)
            {
                return nameProduct.Substring(0, 22) + "...";
            }

            return value;
        }
        

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}