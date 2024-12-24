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

        #region Fields
        private bool _isAllChecked;
        private int _totalPay;
        private Models.Voucher _currentVoucher;
        #endregion

        // Observable Collection
        public ObservableCollection<CartThumbnailViewModel> Cart { get; set; }

        //Command
        public ICommand PaidButtonCommand { get; set; }
        public ICommand GoBackCommand { get; set; }

        // Navigation service
        private readonly INavigationService _navigationService;

        #region Properties Binding
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
        #endregion

        public CartPageViewModel(INavigationService navigationService, IDao dao)
        {
            _dao = dao;
            _navigationService = navigationService;
            Cart = new ObservableCollection<CartThumbnailViewModel>();

            // Load cart data asynchronously
            _ = LoadCartProductsAsync();


            PaidButtonCommand = new RelayCommand(() =>
            {
                var selectedProducts = Cart
                .Where(item => item.IsChecked) // Get checked items
                .Select(item => new PaymentProductThumbnail(
                    item.CartThumbnail.Id, 
                    null, // image
                    item.CartThumbnail.ProductName,
                    item.CartThumbnail.Price,
                    item.CartThumbnail.Amount
                ))
                .ToList();

                if (selectedProducts.Any())
                {
                    _navigationService.NavigateTo<PaymentPage>(selectedProducts); // Pass the list of selected products
                }
                //_navigationService.NavigateTo<PaymentPage>();
            });
            GoBackCommand = new RelayCommand(() =>
            {
                _navigationService.GoBack();
            });
        }

        #region Load Cart
        public async Task LoadCartProductsAsync()
        {
            Cart = new ObservableCollection<CartThumbnailViewModel>();
            try
            {
                var cartProduct = await _dao.GetListCartProductAsync();

                foreach (var product in cartProduct)
                {
                    var cartThumbnailViewModel = new CartThumbnailViewModel(_navigationService, this);
                    cartThumbnailViewModel.CartThumbnail = product;

                    // Subscribe to the PropertyChanged event
                    cartThumbnailViewModel.CartThumbnail.PropertyChanged += (s, e) =>
                    {
                        if (e.PropertyName == nameof(CartThumbnail.Amount))
                        {
                            RecalculateTotalPay();
                        }
                    };

                    Cart.Add(cartThumbnailViewModel);
                }
            }
            catch (Exception ex)
            {
                // Log or handle the error
                Console.WriteLine($"Error loading cart products: {ex.Message}");
            }
        }

        public async Task<bool> DeleteFromCartAsync(int cartId)
        {
            // Call the method to delete from the database
            bool result = await _dao.DeleteFromCartAsync(cartId);

            if (result)
            {
                // If deletion is successful, remove the item from the Cart collection
                var itemToRemove = Cart.FirstOrDefault(item => item.CartThumbnail.Id == cartId);
                if (itemToRemove != null)
                {
                    Cart.Remove(itemToRemove);
                }
                return true; // Deletion successful
            }
            else
            {
                // Handle the error if deletion fails
                return false; // Deletion failed
            }
        }
        public async Task<bool> UpdateCartAsync(int cartId, int quantity)
        {
            // Call the method to delete from the database
            bool result = await _dao.UpdateCartAsync(cartId, quantity);
            //_ = LoadCartProductsAsync();
            return result;
        }
        public async Task<bool> DeleteFromCartByProductIDAsync(int productId)
        {
            // Call the method to delete from the database
            bool result = await _dao.DeleteFromCartByProductIDAsync(productId);

            if (result)
            {
                // If deletion is successful, remove the item from the Cart collection
                var itemToRemove = Cart.FirstOrDefault(item => item.CartThumbnail.ProductId == productId);
                if (itemToRemove != null)
                {
                    Cart.Remove(itemToRemove);
                }
                return true; // Deletion successful
            }
            else
            {
                // Handle the error if deletion fails
                return false; // Deletion failed
            }
        }
        #endregion

        #region Voucher
        public async Task<List<Models.Voucher>> GetAllVouchersAsync()
        {
            return await _dao.GetAllVouchersAsync(); 
        }

        public void ApplyVoucher(Models.Voucher selectedVoucher)
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
                decimal discountAmount = _currentVoucher.DiscountAmount;
                decimal percent = _currentVoucher.PercentageDiscount;

                // Calculate the discount amount based on the current TotalPay
                if (discountAmount != 0)
                {
                    TotalPay = (int)Math.Max(TotalPay - discountAmount, 0);
                }
                else
                {
                    TotalPay = TotalPay - (int)(TotalPay * percent); // Calculate percentage discount
                }
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}