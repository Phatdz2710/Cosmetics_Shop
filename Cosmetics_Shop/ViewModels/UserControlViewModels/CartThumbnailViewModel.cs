using CommunityToolkit.Mvvm.Input;
using Cosmetics_Shop.Models;
using Cosmetics_Shop.Models.DataService;
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
    public class CartThumbnailViewModel : INotifyPropertyChanged
    {
        // Navigation service
        private readonly INavigationService _navigationService;
        private readonly CartPageViewModel _cartPageViewModel;

        // Main properties
        public CartThumbnail CartThumbnail { get; set; }
        public ICommand PayButtonCommand { get; set; }
        //public ICommand DeleteCommand { get; }

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
            PayButtonCommand = new RelayCommand(() =>
            {
                //chuyen sang page thanh toan
                //_navigationService.NavigateTo<>();
            });

            //DeleteCommand = new RelayCommand(DeleteItem);
        }

        //private void DeleteItem()
        //{
        //    // Logic to delete this item from the cart
        //    // You might need to access the parent ViewModel to remove this item from the collection
        //    _parentViewModel.Cart.Remove(this);
        //}

        // For INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
