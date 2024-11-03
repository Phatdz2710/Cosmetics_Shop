using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models
{
    public class CartThumbnail : Cart
    {
        public CartThumbnail(int id, Image productImage, string productName, string classification,
            int price, int amount, int totalPrice) 
            : base(id, productImage, productName, classification, price, amount, totalPrice)
        { }
    }
}
