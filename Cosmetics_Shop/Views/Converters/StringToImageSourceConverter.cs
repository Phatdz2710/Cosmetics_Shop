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
    /// Converts a string to an ImageSource
    /// </summary>
    public class StringToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string path && !string.IsNullOrWhiteSpace(path))
            {
                try
                {
                    // Tạo một BitmapImage từ đường dẫn chuỗi
                    return new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
                }
                catch (Exception)
                {
                    // Nếu có lỗi khi tạo BitmapImage, trả về null hoặc giá trị mặc định
                    return null;
                }
            }

            return null; // Trả về null nếu input không hợp lệ
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
