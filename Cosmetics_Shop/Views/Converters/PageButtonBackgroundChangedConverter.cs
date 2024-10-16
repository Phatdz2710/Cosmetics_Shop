using Microsoft.UI;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace Cosmetics_Shop.Views.Converters
{
    public class PageButtonBackgroundChangedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is int selectedPage && parameter is string pageNumberStr && int.TryParse(pageNumberStr, out int pageNumber))
            {
                return selectedPage == pageNumber ? new SolidColorBrush(Color.FromArgb(200, 174, 254, 87)) 
                    : new SolidColorBrush(Color.FromArgb(200, 138, 228, 41));
            }
            return new SolidColorBrush(Color.FromArgb(200, 138, 228, 41));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
