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
    public class CartThumbnailViewModel : INotifyPropertyChanged
    {
        // Navigation service
        private readonly INavigationService _navigationService;

        // Main properties
        public CartThumbnail CartThumbnail { get; set; }
        public ICommand PayButtonCommand { get; set; }

        public CartThumbnailViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            PayButtonCommand = new RelayCommand(() =>
            {
                //chuyen sang page thanh toan
                //_navigationService.NavigateTo<>();
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
