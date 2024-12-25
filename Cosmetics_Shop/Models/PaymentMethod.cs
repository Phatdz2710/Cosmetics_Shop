using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models
{
    public class PaymentMethod: INotifyPropertyChanged
    {
        public int      Id          { get; set; }

        public string   MethodName  { get; set; }

        public PaymentMethod(int    id, 
                             string methodname)
        {
            Id          = id;
            MethodName  = methodname;
        }
        public PaymentMethod()
        {
            Id = 1;
            MethodName = "Default Payment Method";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
