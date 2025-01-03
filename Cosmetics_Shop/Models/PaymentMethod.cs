using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models
{
    /// <summary>
    /// Represents a payment method in the Cosmetics Shop.
    /// </summary>
    public class PaymentMethod : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the unique identifier for the payment method.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the payment method.
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentMethod"/> class with specified id and method name.
        /// </summary>
        /// <param name="id">The unique identifier for the payment method.</param>
        /// <param name="methodname">The name of the payment method.</param>
        public PaymentMethod(int id, string methodname)
        {
            Id = id;
            MethodName = methodname;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentMethod"/> class with default values.
        /// </summary>
        public PaymentMethod()
        {
            Id = 1;
            MethodName = "Default Payment Method";
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
