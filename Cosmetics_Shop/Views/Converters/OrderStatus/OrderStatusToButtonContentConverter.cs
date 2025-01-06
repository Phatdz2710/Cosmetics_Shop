using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Views.Converters.OrderStatus
{
    /// <summary>
    /// Converts order status to the appropriate button content text.
    /// </summary>
    /// <remarks>
    /// - 1: "Đã nhận hàng" (Order Received)
    /// - 2: "Đánh giá" (Review Order)
    /// - Default: "Lỗi" (Error)
    /// - Returns "Không xác định" (Undefined) if the input value is invalid.
    /// </remarks>
    public class OrderStatusToButtonContentConverter : IValueConverter
    {
        /// <summary>
        /// Converts an order status integer to a string representing the button content.
        /// </summary>
        /// <param name="value">The order status integer to convert.</param>
        /// <param name="targetType">The type of the target property. This parameter is not used.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic. This parameter is not used.</param>
        /// <param name="language">The language of the conversion. This parameter is not used.</param>
        /// <returns>A string representing the button content based on the order status.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is int status)
            {
                return status switch
                {
                    1 => "Đã nhận hàng",       // Trạng thái 1
                    2 => "Đánh giá",           // Trạng thái 2
                    _ => "Lỗi"
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