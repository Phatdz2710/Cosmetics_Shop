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

namespace Cosmetics_Shop.ViewModels.PageViewModels
{
    /// <summary>
    /// View model for CartPage
    /// </summary>
    public class CartPageViewModel : INotifyPropertyChanged
    {
        // Data access object
        private IDao _dao = null;

        private bool _isAllChecked;
        private int _totalPay;
        private Voucher _currentVoucher;

        // Observable Collection
        public ObservableCollection<CartThumbnailViewModel> Cart { get; set; }

        //Command
        public ICommand PaidButtonCommand { get; set; }
        public ICommand GoBackCommand { get; set; }

        // Navigation service
        private readonly INavigationService _navigationService;

        // Expose the CartPageViewModel

        public bool IsAllChecked
        {
            get => _isAllChecked;
            set
            {
                if (_isAllChecked != value)
                {
                    _isAllChecked = value;
                    OnPropertyChanged();
                    // Update all items in the Cart collection
                    foreach (var item in Cart)
                    {
                        item.IsChecked = _isAllChecked; // Set the IsChecked property of each item
                    }
                    RecalculateTotalPay();
                }
            }
        }

        public int TotalPay
        {
            get => _totalPay;
            set
            {
                if (_totalPay != value)
                {
                    _totalPay = value;
                    OnPropertyChanged(); // Notify UI of change
                }
            }
        }
        public void RecalculateTotalPay()
        {
            // Check if any item is checked
            if (Cart.Any(item => item.IsChecked))
            {
                // Calculate total pay from checked items
                TotalPay = Cart.Where(item => item.IsChecked).Sum(item => item.CartThumbnail.TotalPrice);

            }
            else
            {
                // If no items are checked, set TotalPay to 0
                TotalPay = 0;
            }
        }

        public CartPageViewModel(INavigationService navigationService, IDao dao)
        {
            _dao = dao;
            Cart = new ObservableCollection<CartThumbnailViewModel>();

            var cartProduct = _dao.GetListCartProduct();

            foreach (var product in cartProduct)
            {
                var cartThumbnailViewModel = new CartThumbnailViewModel(navigationService, this);
                cartThumbnailViewModel.CartThumbnail = product; // Set the CartThumbnail property

                // Subscribe to the PropertyChanged event
                cartThumbnailViewModel.CartThumbnail.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == nameof(CartThumbnail.Amount))
                    {
                        RecalculateTotalPay(); // Recalculate when Amount changes
                    }
                };

                Cart.Add(cartThumbnailViewModel);
            }

            _navigationService = navigationService;
            PaidButtonCommand = new RelayCommand(() =>
            {

                _navigationService.NavigateTo<PaymentPage>();
            });
            GoBackCommand = new RelayCommand(() =>
            {
                _navigationService.GoBack();
            });
        }

        public List<Voucher> GetAllVouchers()
        {
            return _dao.GetAllVouchers(); // Assuming _dao is initialized correctly in the constructor
        }

        public void ApplyVoucher(Voucher selectedVoucher)
        {
            // Remove the previous voucher's effect if a new one is being applied
            if (selectedVoucher != _currentVoucher)
            {
                _currentVoucher = selectedVoucher; // Update the current voucher
                RecalculateTotalPay(); // Recalculate total pay before applying the new voucher
            }

            if (_currentVoucher != null)
            {
                // Assuming selectedVoucher.Discount is a decimal representing a percentage (e.g., 10 for 10%)
                double discountPercentage = _currentVoucher.Discount; // This should be a value between 0 and 100

                // Calculate the discount amount based on the current TotalPay
                double discountAmount = TotalPay * (discountPercentage / 100 ); // Calculate percentage discount

                // Apply the discount, ensuring TotalPay doesn't go below zero
                TotalPay = (int)Math.Max(TotalPay - discountAmount, 0);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}