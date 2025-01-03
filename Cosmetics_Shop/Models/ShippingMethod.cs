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
    /// Represents a shipping method.
    /// </summary>
    public class ShippingMethod
    {
        /// <summary>
        /// Gets or sets the unique identifier for the shipping method.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the shipping method.
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// Gets or sets the cost of the shipping method.
        /// </summary>
        public decimal ShippingCost { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShippingMethod"/> class with specified values.
        /// </summary>
        /// <param name="id">The unique identifier for the shipping method.</param>
        /// <param name="name">The name of the shipping method.</param>
        /// <param name="price">The cost of the shipping method.</param>
        public ShippingMethod(int id, string name, decimal price)
        {
            Id = id;
            MethodName = name;
            ShippingCost = price;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShippingMethod"/> class with default values.
        /// </summary>
        public ShippingMethod()
        {
            Id = 2;
            MethodName = "Default Shipping Method";
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is ShippingMethod other)
            {
                return this.Id == other.Id; // Compare by Id
            }
            return false;
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode(); // Or use another property
        }
    }
}