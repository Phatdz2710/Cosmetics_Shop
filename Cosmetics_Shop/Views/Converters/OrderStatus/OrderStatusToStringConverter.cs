
using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Views.Converters.OrderStatus
{
    /// <summary>  
    /// Converts order status to a corresponding descriptive string.  
    /// </summary>  
    /// <remarks>  
    /// - 0: "Đang chờ duyệt" (Pending Approval)  
    /// - 1: "Đã duyệt" (Approved)  
    /// - 2: "Hoàn tất" (Completed)  
    /// - 3: "Đã hủy" (Canceled)  
    /// - Default: "Không xác định" (Undefined or invalid status)  
    /// </remarks>  
    public class OrderStatusToStringConverter : IValueConverter
    {
        /// <summary>  
        /// Converts an order status integer to a descriptive string.  
        /// </summary>  
        /// <param name="value">The order status integer to convert.</param>  
        /// <param name="targetType">The type of the target property. This parameter is not used.</param>  
        /// <param name="parameter">An optional parameter to be used in the converter logic. This parameter is not used.</param>  
        /// <param name="language">The language of the conversion. This parameter is not used.</param>  
        /// <returns>A string representing the descriptive text based on the order status.</returns>  
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is int status)
            {
                return status switch
                {
                    0 => "Đang chờ duyệt",       // Trạng thái 0  
                    1 => "Đã duyệt",             // Trạng thái 1  
                    2 => "Hoàn tất", // Trạng thái 2  
                    3 => "Đã hủy",               // Trạng thái 3  
                    _ => "Không xác định"        // Trạng thái mặc định  
                };
            }
            return "Không xác định"; // Trả về "Không xác định" nếu giá trị không hợp lệ  
        }

        
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}