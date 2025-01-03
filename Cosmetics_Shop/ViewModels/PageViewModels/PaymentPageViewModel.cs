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
using System.Windows;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using Windows.UI.Popups;

namespace Cosmetics_Shop.ViewModels.PageViewModels
{
    /// <summary>
    /// View model for PaymentPage
    /// </summary>
    public class PaymentPageViewModel : INotifyPropertyChanged
    {
        // Data access object
        private readonly UserSession _userSession;
        private readonly INavigationService _navigationService;
        private readonly IServiceProvider _serviceProvider;
        private IDao _dao = null;
        public event Action<string> ShowDialogRequested;

        public CartPageViewModel Carts { get; set; }

        //Command
        public ICommand GoBackCommand {  get; set; }
        public ICommand OrderCommand { get; set; }


        #region Fields
        private int _voucherFee;
        private int _shippingFee;
        private int _finalFee;
        private Models.Voucher _currentVoucher;
        private Models.ShippingMethod _currentShippingMethod;
        private Models.PaymentMethod _selectedPaymentMethod;

        private string _name;
        private string _nameDisplay;
        private string _phone;
        private string _address;
        private string _shippingAddress;

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

        public Models.PaymentMethod SelectedPaymentMethod
        {
            get => _selectedPaymentMethod;
            set
            {
                _selectedPaymentMethod = value;
                OnPropertyChanged(); // Thông báo thay đổi nếu cần
            }
        }
        public string ShippingAddress
        {
            get => _shippingAddress;
            set
            {
                if (_shippingAddress != value)
                {
                    _shippingAddress = value;
                    OnPropertyChanged(); // Notify UI of change
                }
            }
        }
        public Models.Voucher CurrentVoucher
        {
            get => _currentVoucher;
            set
            {
                if (_currentVoucher != value)
                {
                    _currentVoucher = value;
                    OnPropertyChanged(nameof(CurrentVoucher)); // Notify UI of change
                }
            }
        }
        public Models.ShippingMethod CurrentShippingMethod
        {
            get => _currentShippingMethod;
            set
            {
                if (_currentShippingMethod != value)
                {
                    _currentShippingMethod = value;
                    OnPropertyChanged(nameof(CurrentShippingMethod)); // Notify UI of change
                }
            }
        }

        #endregion

        // Observable Collection
        public ObservableCollection<PaymentProductThumbnailViewModel> PaymentProduct { get; set; }
        public ObservableCollection<Models.PaymentMethod> PaymentMethods { get; set; }
        public PaymentPageViewModel(INavigationService navigationService,
                                    IDao dao,
                                    IServiceProvider serviceProvider,
                                    UserSession userSession,
                                    PaymentNavigationData data
                                    )
        {
            _dao                = dao;
            _serviceProvider    = serviceProvider;
            _userSession        = userSession;
            _navigationService  = navigationService;
            CurrentVoucher = data.CurrentVoucher;
            CurrentShippingMethod = data.CurrentShippingMethod;

            PaymentProduct = new ObservableCollection<PaymentProductThumbnailViewModel>();
            PaymentMethods = new ObservableCollection<Models.PaymentMethod>();

            loadUserInformation();
            LoadPaymentMethods();

            // Iterate through the list of products and add them to the ObservableCollection
            if (data.Products != null)
            {
                foreach (var product in data.Products)
                {
                    var paymentProductThumbnailViewModel = _serviceProvider.GetService<PaymentProductThumbnailViewModel>();
                    paymentProductThumbnailViewModel.PaymentProductThumbnail = product; // Set the product
                    PaymentProduct.Add(paymentProductThumbnailViewModel); // Add to the collection
                }
            }

            recalculateFinalFee();
            GoBackCommand = new RelayCommand(() =>
            {
                _navigationService.GoBack();
            });

            // Initialize the OrderCommand
            OrderCommand = new RelayCommand(async () => await ExecuteOrderCommand());
        }

        #region Voucher
        /// <summary>
        /// Load the vouchers from database
        /// </summary>
        /// <returns>A list of vouchers</returns>
        public async Task<List<Models.Voucher>> GetAllVouchersAsync()
        {
            return await _dao.GetAllVouchersAsync();
        }

        /// <summary>
        /// Calculate the voucher to the total payment
        /// </summary>
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

        /// <summary>
        /// Recalculate the total payment after apply voucher fee
        /// </summary>
        /// <param name="selectedVoucher">Voucher was selected.</param>
        public void ApplyVoucher(Models.Voucher selectedVoucher)
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
        /// <summary>
        /// Load the shipping methods from database
        /// </summary>
        /// <returns>A list of shipping methods</returns>
        public async Task<List<Models.ShippingMethod>> GetShippingMethodsAsync()
        {
            return await _dao.GetShippingMethodsAsync(); 
        }

        /// <summary>
        /// Recalculate the total payment after apply shipping fee
        /// </summary>
        /// <param name="selectedShippingMethod">Shipping method was selected.</param>
        public void ApplyShipping(Models.ShippingMethod selectedShippingMethod)
        {
            // Remove the previous voucher's effect if a new one is being applied
            if (selectedShippingMethod != _currentShippingMethod)
            {
                _currentShippingMethod = selectedShippingMethod; // Update the current voucher
                ShippingFee = (int)_currentShippingMethod.ShippingCost;
                recalculateFinalFee();
            }
        }

        /// <summary>
        /// Update shipping address to order
        /// </summary>
        /// <param name="address">Address was changed.</param>
        public bool LoadShippingAddress(string address)
        {
            if (string.IsNullOrEmpty(address) || !address.IsValidAddress())
            {
                ShowDialogRequested?.Invoke("Địa chỉ đặt hàng không hợp lệ !");
                return false;
            }
            ShippingAddress = address;
            return true;
        }

        #endregion

        #region Calculation
        /// <summary>
        /// Calculate the final fee
        /// </summary>
        public void recalculateFinalFee()
        {
            FinalFee = TotalPay + ShippingFee - VoucherFee;
        }
        #endregion

        #region User
        /// <summary>
        /// Load user information
        /// </summary>
        private async void loadUserInformation()
        {
            var userDetail = await _dao.GetUserDetailAsync(_userSession.GetId());

            Name = userDetail.Name;
            NameDisplay = userDetail.Name;
            Phone = userDetail.Phone;
            Address = userDetail.Address;
            ShippingAddress = Address; // initial shipping address is user address
        }
        #endregion

        #region Payment Method
        /// <summary>
        /// Load payment methods from database
        /// </summary>
        /// <returns>A list of payment methods</returns>
        public async Task<List<Models.PaymentMethod>> GetPaymentMethodsAsync()
        {
            return await _dao.GetPaymentMethodsAsync();
        }

        /// <summary>
        /// Load payment methods from database
        /// </summary>
        private async void LoadPaymentMethods()
        {
            var methods = await GetPaymentMethodsAsync();
            foreach (var method in methods)
            {
                PaymentMethods.Add(method);
            }
        }

        #endregion

        #region Order
        /// <summary>
        /// Command to add a new order to database
        /// </summary>
        private async Task ExecuteOrderCommand()
        {
            // Validate user input
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Phone) || string.IsNullOrWhiteSpace(Address))
            {
                return; 
            }

            // Validate that a payment method is selected
            if (SelectedPaymentMethod == null)
            {
                SelectedPaymentMethod = new Models.PaymentMethod();
            }

            // Validate that a shipping method is selected
            if (_currentShippingMethod == null)
            {
                _currentShippingMethod = new Models.ShippingMethod();
            }

            // Validate that a shipping method is selected
            if (_currentVoucher == null)
            {
                _currentVoucher = new Models.Voucher();
            }

            if (Address == null || !Address.IsValidAddress()) 
            {
                ShowDialogRequested?.Invoke("Vui lòng nhập địa chỉ nhận hàng hợp lệ !");
                return;
            }

            // Gather necessary information for the order
            var products = PaymentProduct.Select(p => p.PaymentProductThumbnail).ToList();
            var paymentMethod = SelectedPaymentMethod?.Id; // Assuming SelectedPaymentMethod has an Id property
            var shippingMethod = _currentShippingMethod?.Id; // Assuming _currentShippingMethod has an Id property
            var voucherId = _currentVoucher?.Id; // Assuming _currentVoucher has an Id property

            // Call the method to add the order
            var order = await _dao.AddToOrderAsync(products, (int)paymentMethod, (int)shippingMethod, 
                            (int)voucherId, ShippingAddress, FinalFee);

            foreach (var product in PaymentProduct)
            {
                bool isDeleted = await _dao.DeleteFromCartAsync(product.PaymentProductThumbnail.CartId);
                if (isDeleted)
                {
                    //
                }
                else
                {
                    //
                }
            }
            // await Carts.LoadCartProductsAsync();

            if (order != null)
            {
                ShowDialogRequested?.Invoke("Cảm ơn bạn đã đặt hàng !");
                _navigationService.NavigateTo<DashboardPage>();
                //_navigationService.NavigateTo<ReviewPage>(products);
            }
            else
            {
            }
        }
        #endregion

        // Method to show messages to the user

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
