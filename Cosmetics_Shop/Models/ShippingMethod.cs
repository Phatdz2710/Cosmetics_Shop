using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models
{
    /// <summary>
    /// stores the shipping method information
    /// </summary>
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
    }
}
