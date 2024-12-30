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
    /// Return true or false if the image is available
    /// </summary>
    public class IsImageAvailableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                if (value is string path && !string.IsNullOrWhiteSpace(path))
                {
                    // Tạo Uri từ đường dẫn
                    var uri = new Uri(path, UriKind.RelativeOrAbsolute);

                    // Nếu là đường dẫn tương đối, chuyển thành tuyệt đối
                    if (!uri.IsAbsoluteUri)
                    {
                        var basePath = AppContext.BaseDirectory; // Thư mục gốc của ứng dụng
                        path = Path.Combine(basePath, path);
                    }

                    // Kiểm tra sự tồn tại của tệp
                    if (File.Exists(path))
                    {
                        return Visibility.Collapsed;
                    }
                    else return Visibility.Visible;
                }
            }
            catch
            {
                // Xử lý các trường hợp đường dẫn không hợp lệ
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
