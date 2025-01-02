using CommunityToolkit.Mvvm.Input;
using Cosmetics_Shop.DataAccessObject.Interfaces;
using Cosmetics_Shop.Models;
using Cosmetics_Shop.Services;
using Cosmetics_Shop.ViewModels.PageViewModels;
using Cosmetics_Shop.Views.Controls;
using Cosmetics_Shop.Views.Pages;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cosmetics_Shop.ViewModels.UserControlViewModels
{
    /// <summary>
    /// View model for cart thumbnail
    /// </summary>
    public class CartThumbnailViewModel : INotifyPropertyChanged
    {
        // Navigation service
        private readonly INavigationService _navigationService;
        public readonly CartPageViewModel _cartPageViewModel;

        // Main properties
        public CartThumbnail CartThumbnail { get; set; }
        public ICommand DeleteCommand { get; }

        private bool _isChecked;

        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                    OnPropertyChanged();

                    // Call RecalculateTotalPay on the parent ViewModel
                    _cartPageViewModel.RecalculateTotalPay();
                }
            }
        }

        public CartThumbnailViewModel(INavigationService navigationService, CartPageViewModel cartPageViewModel)
        {
            _navigationService = navigationService;
            _cartPageViewModel = cartPageViewModel; // Pass the parent ViewModel

            DeleteCommand = new RelayCommand(async () => await DeleteItem());
        }

        /// <summary>
        /// Delete an item to cart in database
        /// </summary>
        private async Task DeleteItem()
        {
            // Call the DeleteFromCartAsync method in the parent ViewModel
            bool result = await _cartPageViewModel.DeleteFromCartAsync(CartThumbnail.Id);

        }


        // For INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
