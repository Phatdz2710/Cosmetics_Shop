using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models
{
    public class Voucher : INotifyPropertyChanged
    {
        public int Id { get; set; } // Unique identifier for the voucher
        public string Code { get; set; } // The code for the voucher
        public int Discount { get; set; } // The discount amount or percentage
        public DateTime ExpiryDate { get; set; } // The expiry date of the voucher

        public Voucher(int id, string code, int discount, DateTime expiryDate)
        {
            Id = id;
            Code = code;
            Discount = discount;
            ExpiryDate = expiryDate;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
