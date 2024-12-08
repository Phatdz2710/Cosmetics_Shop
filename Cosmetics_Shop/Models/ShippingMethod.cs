using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models
{
    public class ShippingMethod 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

        public ShippingMethod(int id, string name, int price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }
}
