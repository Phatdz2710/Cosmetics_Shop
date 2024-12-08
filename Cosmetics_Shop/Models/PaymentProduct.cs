using Cosmetics_Shop.DBModels;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models
{
    public class PaymentProduct : INotifyPropertyChanged
    {
        public int      Id              { get; set; }
        public string   ProductImage    { get; set; }
        public string   ProductName     { get; set; }
        public int      Price           { get; set; }
        private int     _amount         { get; set; }
        //public Voucher Voucher {  get; set; }
        public int Amount
        {
            get => _amount;
            set
            {
                if (_amount != value)
                {
                    _amount = value;
                    OnPropertyChanged(nameof(Amount));
                }
            }
        }

        public PaymentProduct(
                    int    id, 
                    string image, 
                    string productName, 
                    int    price, 
                    int    amount)
        {
            Id              = id;
            ProductImage    = image;
            ProductName     = productName;
            Price           = price;
            Amount          = amount;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
