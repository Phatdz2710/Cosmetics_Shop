using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Views.Converters
{
    public class DecimalToThreeDecimalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is double number)
            {
                // Làm tròn đến 3 chữ số thập phân
                return Math.Round(number, 2).ToString("0.###", CultureInfo.InvariantCulture);
            }
            return value;
        }

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
