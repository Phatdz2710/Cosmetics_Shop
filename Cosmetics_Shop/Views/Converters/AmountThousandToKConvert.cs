using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Views.Converters
{
    public class AmountThousandToKConvert : IValueConverter
    {
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
