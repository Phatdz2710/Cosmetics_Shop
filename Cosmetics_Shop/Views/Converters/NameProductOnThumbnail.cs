using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Chat;

namespace Cosmetics_Shop.Views.Converters
{
    public class NameProductOnThumbnail : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string nameProduct && nameProduct.Length > 20)
            {
                return nameProduct.Substring(0, 20) + "...";
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
