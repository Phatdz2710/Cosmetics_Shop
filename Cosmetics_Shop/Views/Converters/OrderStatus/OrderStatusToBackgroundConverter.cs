using Microsoft.UI;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace Cosmetics_Shop.Views.Converters.OrderStatus
{
    /// <summary>
    /// Converts order status to a corresponding background color.
    /// </summary>
    /// <remarks>
    /// - 0: LightYellow (Pending Approval)
    /// - 1: LightGreen (Approved)
    /// - 2: LightBlue (Order Received)
    /// - 3: Tomato (Order Canceled)
    /// - Default: Transparent (Invalid or undefined status)
    /// </remarks>
    public class OrderStatusToBackgroundConverter : IValueConverter
    {
        /// <summary>
        /// Converts an order status integer to a SolidColorBrush representing the background color.
        /// </summary>
        /// <param name="value">The order status integer to convert.</param>
        /// <param name="targetType">The type of the target property. This parameter is not used.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic. This parameter is not used.</param>
        /// <param name="language">The language of the conversion. This parameter is not used.</param>
        /// <returns>A <see cref="SolidColorBrush"/> representing the background color based on the order status.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is int status)
            {
                return status switch
                {
                    0 => new SolidColorBrush(Colors.LightYellow), // Đang chờ duyệt
                    1 => new SolidColorBrush(Colors.LightGreen),  // Đã duyệt
                    2 => new SolidColorBrush(Colors.LightBlue),   // Đã xác nhận nhận hàng
                    3 => new SolidColorBrush(Colors.Tomato),   // Đã hủy đơn hàng
                    _ => new SolidColorBrush(Colors.Transparent), // Mặc định
                };
            }
            return new SolidColorBrush(Colors.Transparent);
        }

        
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}