using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models
{
    public class Cart
    {
<<<<<<< HEAD
=======
        public string ShopName { get; set; }
>>>>>>> ad4311b5a9c311e96d94838537af8decb763063a
        public int Id { get; set; }
        Image ProductImage { get; set; }
        public string ProductName { get; set; }
        public string Classification { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
        public int TotalPrice { get; set; }

<<<<<<< HEAD
        public Cart(int id, Image productImage, string productName, string classification, 
            int price, int amount, int totalPrice)
        {
=======
        public Cart(string shopName, int id, Image productImage, string productName, string classification, 
            int price, int amount, int totalPrice)
        {
            this.ShopName = shopName;
>>>>>>> ad4311b5a9c311e96d94838537af8decb763063a
            Id = id;
            ProductImage = productImage;
            ProductName = productName;
            Classification = classification;
            Price = price;
            Amount = amount;
            TotalPrice = totalPrice;
        }

    }
}
