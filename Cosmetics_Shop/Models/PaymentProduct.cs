using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models
{
    public class PaymentProduct
    {
        public int Id { get; set; }
        string ProductImage { get; set; }
        public string ProductName { get; set; }
        public string Classification { get; set; }
        public int Price { get; set; }
        public int Amount {  get; set; }
        //public Voucher Voucher {  get; set; }

        public PaymentProduct(int id, string image, string productName, 
            string classification, int price, int amount)
        {
            Id = id;
            ProductImage = image;
            ProductName = productName;
            Classification = classification;
            Price = price;
            Amount = amount;
        }
    }
}
