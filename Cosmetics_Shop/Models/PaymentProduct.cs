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
    /// <summary>
    /// Stores the payment product information
    /// </summary>
    public class PaymentProduct : INotifyPropertyChanged
    {
        public int      Id              { get; set; }
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

            OpenProductDetailCommand = new RelayCommand(OpenProductDetail);
        }
        private void OpenProductDetail()
        {
            var navigationService = App.ServiceProvider.GetService(typeof(INavigationService)) as INavigationService;
            navigationService.NavigateTo<ProductDetailPage>(Id);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
