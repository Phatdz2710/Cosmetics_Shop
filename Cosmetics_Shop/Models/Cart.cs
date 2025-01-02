using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models
{
    public class Cart : INotifyPropertyChanged
    {
        public int      Id              { get; set; }
        public int      UserId          { get; set; }
        public int      ProductId       { get; set; }
        public string   ProductImage    { get; set; }
        public string   ProductName     { get; set; }
        public int      Price   { get; set; }
        private int     _amount;
        public int Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));
                OnPropertyChanged(nameof(TotalPrice)); // Notify that TotalPrice has changed
            }
        }
        public int TotalPrice
        {
            get
            {
                return Amount * Price;
            }
            set
            {
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Cart(int     id, 
            int productId,
                    string  productImage, 
                    string  productName,
                    int     price, 
                    int     amount, 
                    int     totalPrice)
        {
            Id = id;
            ProductId       = productId;
            ProductImage    = productImage;
            ProductName     = productName;
            Price           = price;
            Amount          = amount;
            TotalPrice      = totalPrice;
        }

        public Cart(int userId, 
                    int productId, 
                    int quantity)
        {
            UserId      = userId;
            ProductId   = productId;
            Amount      = quantity;
        }

    }
}
