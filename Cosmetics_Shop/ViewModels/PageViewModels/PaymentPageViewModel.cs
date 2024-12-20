using CommunityToolkit.Mvvm.Input;
using Cosmetics_Shop.Models;
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
using Cosmetics_Shop.DataAccessObject.Interfaces;
using Cosmetics_Shop.DBModels;

namespace Cosmetics_Shop.ViewModels.PageViewModels
{
    /// <summary>
    /// View model for PaymentPage
    /// </summary>
    public class PaymentPageViewModel : INotifyPropertyChanged
    {
        // Data access object
        private readonly UserSession _userSession;
        private IDao _dao = null;
        //Command
        public ICommand GoBackCommand {  get; set; }
        // Navigation service
        private readonly INavigationService _navigationService;
        // Service provider
        private readonly IServiceProvider _serviceProvider;

        #region Fields
        private int _voucherFee;
        private int _shippingFee;
        private int _finalFee;
        private DBModels.Voucher _currentVoucher;
        private DBModels.ShippingMethod _currentShippingMethod;

        private string _name;
        private string _nameDisplay;
        private string _phone;
        private string _address;
        #endregion

        #region Properties Binding
        public string NameDisplay
        {
            get { return _nameDisplay; }
            set
            {
                _nameDisplay = value;
                OnPropertyChanged(nameof(NameDisplay));
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }
        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

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

        #endregion

        // Observable Collection
        public ObservableCollection<PaymentProductThumbnailViewModel> PaymentProduct { get; set; }
        public PaymentPageViewModel(INavigationService      navigationService, 
                                    IDao                    dao, 
                                    IServiceProvider        serviceProvider,
                                    UserSession             userSession,
                                    List<PaymentProductThumbnail> products
                                    )
        {
            _dao                = dao;
            _serviceProvider    = serviceProvider;
            _userSession        = userSession;
            loadUserInformation();

            PaymentProduct = new ObservableCollection<PaymentProductThumbnailViewModel>();

            // Iterate through the list of products and add them to the ObservableCollection
            if (products != null)
            {
                foreach (var product in products)
                {
                    var paymentProductThumbnailViewModel = _serviceProvider.GetService<PaymentProductThumbnailViewModel>();
                    paymentProductThumbnailViewModel.PaymentProductThumbnail = product; // Set the product
                    PaymentProduct.Add(paymentProductThumbnailViewModel); // Add to the collection
                }
            }

            recalculateFinalFee();
            _navigationService = navigationService;
            GoBackCommand = new RelayCommand(() =>
            {
                _navigationService.GoBack();
            });
        }

        #region Voucher
        public async Task<List<DBModels.Voucher>> GetAllVouchersAsync()
        {
            return await _dao.GetAllVouchersAsync();
        }

        public void calculateVoucher()
        {
            if (_currentVoucher != null)
            {
                // Assuming selectedVoucher.Discount is a decimal representing a percentage (e.g., 10 for 10%)
                decimal discountAmount = _currentVoucher.DiscountAmount;
                decimal percent = _currentVoucher.PercentageDiscount;

                // Calculate the discount amount based on the current TotalPay
                if (discountAmount != 0)
                {
                    VoucherFee = (int)discountAmount;
                }
                else
                {
                    VoucherFee = (int)(TotalPay * percent); // Calculate percentage discount
                }
            }
            else
            {
                VoucherFee = 0; // Reset if no voucher is applied
            }
            recalculateFinalFee();
        }
        public void ApplyVoucher(DBModels.Voucher selectedVoucher)
        {
            // Remove the previous voucher's effect if a new one is being applied
            if (selectedVoucher != _currentVoucher)
            {
                _currentVoucher = selectedVoucher; // Update the current voucher
                recalculateFinalFee();
            }
            calculateVoucher();
        }
        #endregion

        #region Shipping
        public async Task<List<DBModels.ShippingMethod>> GetShippingMethodsAsync()
        {
            return await _dao.GetShippingMethodsAsync(); 
        }

        public void ApplyShipping(DBModels.ShippingMethod selectedShippingMethod)
        {
            // Remove the previous voucher's effect if a new one is being applied
            if (selectedShippingMethod != _currentShippingMethod)
            {
                _currentShippingMethod = selectedShippingMethod; // Update the current voucher
                ShippingFee = (int)_currentShippingMethod.ShippingCost;
                recalculateFinalFee();
            }
        }
        #endregion

        #region Calculation
        public void recalculateFinalFee()
        {
            FinalFee = TotalPay + ShippingFee - VoucherFee;
        }
        #endregion

        #region User
        private async void loadUserInformation()
        {
            var userDetail = await _dao.GetUserDetailAsync(_userSession.GetId());

            Name = userDetail.Name;
            NameDisplay = userDetail.Name;
            Phone = userDetail.Phone;
            Address = userDetail.Address;
        }
        #endregion


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
