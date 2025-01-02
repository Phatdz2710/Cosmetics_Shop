using CommunityToolkit.Mvvm.Input;
using Cosmetics_Shop.Services;
using Cosmetics_Shop.Views.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cosmetics_Shop.Models
{
    public class PaymentProduct : INotifyPropertyChanged
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public string   ProductImage    { get; set; }
        public string   ProductName     { get; set; }
        public int      Price           { get; set; }
        private int     _amount         { get; set; }
        //public Voucher Voucher {  get; set; }

        public ICommand OpenProductDetailCommand { get; set; }

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
                    int cartId,
                    int productId,
                    string image,
                    string productName,
                    int price,
                    int amount)
        {
            CartId = cartId;
            ProductId = productId;
            ProductImage = image;
            ProductName = productName;
            Price = price;
            Amount = amount;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
