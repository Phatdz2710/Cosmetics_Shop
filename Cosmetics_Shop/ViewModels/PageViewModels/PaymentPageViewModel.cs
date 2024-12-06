using CommunityToolkit.Mvvm.Input;
using Cosmetics_Shop.Models;
using Cosmetics_Shop.Models.DataService;
using Cosmetics_Shop.Services;
using Cosmetics_Shop.ViewModels.UserControlViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Cosmetics_Shop.Views.Pages;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cosmetics_Shop.ViewModels.PageViewModels
{
    public class PaymentPageViewModel : INotifyPropertyChanged
    {
        // Data access object
        private IDao _dao = null;
        private int _voucherFee;
        private int _shippingFee;
        private int _finalFee;
        private Voucher _currentVoucher;
        private ShippingMethod _currentShippingMethod;

        //Command
        public ICommand GoBackCommand {  get; set; }

        // Navigation service
        private readonly INavigationService _navigationService;

        // Service provider
        private readonly IServiceProvider _serviceProvider;

        public int TotalPay
        {
            get => PaymentProduct.Sum(item => item.TotalPrice);
            set
            {
            }
        }

        public int ShippingFee
        {
            get => _shippingFee;
            set
            {
                if (_shippingFee != value)
                {
                    _shippingFee = value;
                    OnPropertyChanged(); // Notify UI of change
                }
            }
        }

        public int VoucherFee 
        {
            get => _voucherFee;
            set
            {
                if (_voucherFee != value)
                {
                    _voucherFee = value;
                    OnPropertyChanged(); // Notify UI of change
                }
            }
        }

        public int FinalFee
        {
            get => _finalFee;
            set
            {
                if (_finalFee != value)
                {
                    _finalFee = value;
                    OnPropertyChanged(); // Notify UI of change
                }
            }
        }

        // Observable Collection
        public ObservableCollection<PaymentProductThumbnailViewModel> PaymentProduct { get; set; }
        public PaymentPageViewModel(INavigationService navigationService, 
                                    IDao dao, 
                                    IServiceProvider serviceProvider)
        {
            _dao = dao;
            _serviceProvider = serviceProvider;

            PaymentProduct = new ObservableCollection<PaymentProductThumbnailViewModel>();

            var paymentProduct = _dao.GetAllPaymentProducts();

            for (int i = 0; i < paymentProduct.Count; i++)
            {
                var paymentProductThumbnailViewModel = _serviceProvider.GetService(typeof(PaymentProductThumbnailViewModel));
                paymentProductThumbnailViewModel.GetType().GetProperty("PaymentProductThumbnail")
                    .SetValue(paymentProductThumbnailViewModel, paymentProduct[i]);
                PaymentProduct.Add(paymentProductThumbnailViewModel as PaymentProductThumbnailViewModel);
            }

            //PaymentProduct = paymentProducts;
            recalculateFinalFee();
            _navigationService = navigationService;
            GoBackCommand = new RelayCommand(() =>
            {
                _navigationService.GoBack();
            });
        }

        public List<Voucher> GetAllVouchers()
        {
            return _dao.GetAllVouchers(); // Assuming _dao is initialized correctly in the constructor
        }
        public List<ShippingMethod> GetShippingMethods()
        {
            return _dao.GetShippingMethods(); // Assuming _dao is initialized correctly in the constructor
        }

        public void calculateVoucher()
        {
            if (_currentVoucher != null)
            {
                // Assuming selectedVoucher.Discount is a decimal representing a percentage (e.g., 10 for 10%)
                int discountPercentage = _currentVoucher.Discount; // This should be a value between 0 and 100

                // Calculate the discount amount based on the current TotalPay
                VoucherFee = (int)(TotalPay * (discountPercentage / 100.0)); // Calculate percentage discount
            }
            else
            {
                VoucherFee = 0; // Reset if no voucher is applied
            }
            recalculateFinalFee();
        }
        public void ApplyVoucher(Voucher selectedVoucher)
        {
            // Remove the previous voucher's effect if a new one is being applied
            if (selectedVoucher != _currentVoucher)
            {
                _currentVoucher = selectedVoucher; // Update the current voucher
                recalculateFinalFee();
            }
            calculateVoucher();
        }
        public void ApplyShipping(ShippingMethod selectedShippingMethod)
        {
            // Remove the previous voucher's effect if a new one is being applied
            if (selectedShippingMethod != _currentShippingMethod)
            {
                _currentShippingMethod = selectedShippingMethod; // Update the current voucher
                ShippingFee = _currentShippingMethod.Price;
                recalculateFinalFee();
            }
        }

        public void recalculateFinalFee()
        {
            FinalFee = TotalPay + ShippingFee - VoucherFee;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
