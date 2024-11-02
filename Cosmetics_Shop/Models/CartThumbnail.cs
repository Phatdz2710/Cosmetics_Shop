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
<<<<<<< HEAD
        public CartThumbnail(int id, Image productImage, string productName, string classification,
            int price, int amount, int totalPrice) 
            : base(id, productImage, productName, classification, price, amount, totalPrice)
=======
        public CartThumbnail(string shopName, int id, Image productImage, string productName, string classification,
            int price, int amount, int totalPrice) 
            : base(shopName, id, productImage, productName, classification, price, amount, totalPrice)
>>>>>>> ad4311b5a9c311e96d94838537af8decb763063a
        { }
    }
}
