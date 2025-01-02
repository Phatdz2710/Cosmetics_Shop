using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models
{
    public class CartThumbnail : Cart, INotifyPropertyChanged
    {
        public CartThumbnail(int id, int productId, string productImage, string productName,
            int price, int amount, int totalPrice)
            : base(id, productId, productImage, productName, price, amount, totalPrice)
        { }

        public CartThumbnail(int userId, int productId, int quantity)
            : base(userId, productId, quantity)
        { }

    }
}
