<<<<<<< HEAD
﻿using CommunityToolkit.Mvvm.Input;
using Cosmetics_Shop.Models;
using Cosmetics_Shop.Services;
using Cosmetics_Shop.Views.Objects;
using Cosmetics_Shop.Views.Pages;
=======
﻿using Cosmetics_Shop.Models;
using Cosmetics_Shop.Views.Objects;
>>>>>>> ad4311b5a9c311e96d94838537af8decb763063a
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
<<<<<<< HEAD
using System.Windows.Input;
=======
>>>>>>> ad4311b5a9c311e96d94838537af8decb763063a

namespace Cosmetics_Shop.ViewModels
{
    public class CartThumbnailViewModel : INotifyPropertyChanged
    {
<<<<<<< HEAD
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
=======
        public event PropertyChangedEventHandler PropertyChanged;
        public CartThumbnail CartThumbnail { get; set; }

        public CartThumbnailViewModel(CartThumbnail cartThumbnail)
        {
            this.CartThumbnail = cartThumbnail;
>>>>>>> ad4311b5a9c311e96d94838537af8decb763063a
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
<<<<<<< HEAD

        public event PropertyChangedEventHandler PropertyChanged;
=======
>>>>>>> ad4311b5a9c311e96d94838537af8decb763063a
    }
}
