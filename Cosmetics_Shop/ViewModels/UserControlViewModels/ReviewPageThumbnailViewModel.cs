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
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Cosmetics_Shop.ViewModels.UserControlViewModels
{
    /// <summary>
    /// View model for review page thumbnail
    /// </summary>
    public class ReviewPageThumbnailViewModel : INotifyPropertyChanged
    {
        // Navigation service
        private readonly INavigationService _navigationService;

        #region Fields
        private int _starNumber;
        public bool Star1 { get; set; }
        public bool Star2 { get; set; }
        public bool Star3 { get; set; }
        public bool Star4 { get; set; }
        public bool Star5 { get; set; }
        public OrderItemDisplay Product { get; set; }

        #endregion

        #region Properties Binding
        public int StarNumber
        {
            get { return _starNumber; }
            set
            {
                if (_starNumber != value)
                {
                    _starNumber = value;
                    OnPropertyChanged();
                    UpdateStarStates(value);
                }
            }
        }
        public int TotalPrice
        {
            get
            {
                return Product.Quantity * Product.Price;
            }
            set
            {
            }
        }
        #endregion

        public ReviewPageThumbnailViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            StarNumber = 0;

            // Khởi tạo các sao
            Star1 = false;
            Star2 = false;
            Star3 = false;
            Star4 = false;
            Star5 = false;

        }

        /// <summary>
        /// Update state of star number
        /// </summary>
        public void UpdateStarStates(int selectedStar)
        {
            Star1 = selectedStar >= 1;
            Star2 = selectedStar >= 2;
            Star3 = selectedStar >= 3;
            Star4 = selectedStar >= 4;
            Star5 = selectedStar >= 5;

            // Thông báo UI rằng các sao đã thay đổi
            OnPropertyChanged(nameof(Star1));
            OnPropertyChanged(nameof(Star2));
            OnPropertyChanged(nameof(Star3));
            OnPropertyChanged(nameof(Star4));
            OnPropertyChanged(nameof(Star5));
        }


        // For INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
