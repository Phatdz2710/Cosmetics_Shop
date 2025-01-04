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
        private bool _isChecked;

        /// <summary>
        /// A readonly field holding the instance of the CartPageViewModel.
        /// </summary>
        public readonly CartPageViewModel _cartPageViewModel;

        #region Main Properties

        /// <summary>
        /// Gets or sets the CartThumbnail for displaying a preview or information about the cart.
        /// </summary>
        public CartThumbnail CartThumbnail { get; set; }

        /// <summary>
        /// Command to delete the current cart item.
        /// </summary>
        public ICommand DeleteCommand { get; }

        #endregion

        #region IsChecked Property

        /// <summary>
        /// Gets or sets a value indicating whether the cart item is selected (checked).
        /// Notifies the UI when the value changes and triggers recalculation of the total payment in the parent ViewModel.
        /// </summary>
        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                    OnPropertyChanged();

                    // Calls RecalculateTotalPay on the parent ViewModel to update the total payment when the checked status changes
                    _cartPageViewModel.RecalculateTotalPay();
                }
            }
        }

        #endregion

        // Constructor
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
