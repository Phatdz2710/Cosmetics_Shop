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

        public string MethodName { get; set; }

        public decimal ShippingCost { get; set; }
        public ShippingMethod(int id, string name, decimal price)
        {
            Id = id;
            MethodName = name;
            ShippingCost = price;
        }

        public ShippingMethod()
        {
            Id = 2;
            MethodName = "Default Shipping Method";
        }

        public override bool Equals(object obj)
        {
            if (obj is ShippingMethod other)
            {
                return this.Id == other.Id; // So sánh theo Id
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode(); // Hoặc sử dụng một thuộc tính khác
        }
    }
}
