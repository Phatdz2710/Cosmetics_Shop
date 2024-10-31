using CommunityToolkit.Mvvm.Input;
using Cosmetics_Shop.Models;
using Cosmetics_Shop.Models.DataService;
using Cosmetics_Shop.Services;
using Cosmetics_Shop.Views.Objects;
using Cosmetics_Shop.Views.Pages;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cosmetics_Shop.ViewModels
{
    public class ProductThumbnailViewModel : INotifyPropertyChanged
    {
        // Navigation service
        private readonly INavigationService _navigationService;

        // Main properties
        public ProductThumbnail ProductThumbnail { get; set; }   
        
        // Buy button command
        public ICommand BuyButtonCommand { get; set; }

        // Constructor
        public ProductThumbnailViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            BuyButtonCommand = new RelayCommand(() =>
            {
                _navigationService.NavigateTo<ProductDetailPage>();
            });
        }

        // For INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
