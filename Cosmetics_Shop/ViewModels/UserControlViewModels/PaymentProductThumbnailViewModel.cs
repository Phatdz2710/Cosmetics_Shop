using CommunityToolkit.Mvvm.Input;
using Cosmetics_Shop.Models;
using Cosmetics_Shop.Services;
using Cosmetics_Shop.ViewModels.PageViewModels;
using Cosmetics_Shop.Views.Controls;
using Cosmetics_Shop.Views.Pages;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cosmetics_Shop.ViewModels.UserControlViewModels
{
    /// <summary>
    /// View model for payment product thumbnail
    /// </summary>
    public class PaymentProductThumbnailViewModel : INotifyPropertyChanged
    {
        // Navigation service
        private readonly INavigationService _navigationService;
        /// <summary>
        /// Gets or sets the thumbnail information for a payment product.
        /// </summary>
        public PaymentProductThumbnail PaymentProductThumbnail { get; set; }

        /// <summary>
        /// Gets the total price for the payment product.
        /// It is calculated by multiplying the amount and price of the product.
        /// </summary>
        public int TotalPrice
        {
            get
            {
                return PaymentProductThumbnail.Amount * PaymentProductThumbnail.Price;
            }
            set
            {
                // No setter logic, as TotalPrice is a calculated property.
            }
        }

        // Constructor
        public PaymentProductThumbnailViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }


        // Implement INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
