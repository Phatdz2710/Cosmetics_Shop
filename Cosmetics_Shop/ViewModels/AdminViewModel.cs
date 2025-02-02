﻿using CommunityToolkit.Mvvm.Input;
using Cosmetics_Shop.Models;
using Cosmetics_Shop.Services;
using Cosmetics_Shop.Services.EventAggregatorMessages;
using Cosmetics_Shop.Services.Interfaces;
using Cosmetics_Shop.Views.AdminPages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cosmetics_Shop.ViewModels
{
    /// <summary>
    /// View Model for Admin Mode
    /// </summary>
    public class AdminViewModel : INotifyPropertyChanged
    {
        #region Singleton
        // Navigation Service
        private readonly INavigationService _navigationService = null;
        // User Session
        private readonly UserSession        _userSession = null;
        // Event Aggregator
        private readonly IEventAggregator   _eventAggregator = null;
        #endregion

        #region Commands
        /// <summary>
        /// Command to navigate to the account management section.
        /// </summary>
        public ICommand AccountManagerCommand { get; set; }

        /// <summary>
        /// Command to navigate to the order management section.
        /// </summary>
        public ICommand OrderManagerCommand { get; set; }

        /// <summary>
        /// Command to navigate to the product management section.
        /// </summary>
        public ICommand ProductManagerCommand { get; set; }

        /// <summary>
        /// Command to log out of the system.
        /// </summary>
        public ICommand LogoutCommand { get; set; }
        #endregion


        public AdminViewModel(INavigationService navigationService,
                              IEventAggregator   eventAggregator,
                              UserSession        userSession)
        {
            _navigationService  = navigationService;
            _eventAggregator    = eventAggregator;
            _userSession        = userSession;

            _navigationService.NavigateTo<AccountManagerPage>();

            AccountManagerCommand = new RelayCommand(() =>
            {
                _navigationService.NavigateTo<AccountManagerPage>();
            });

            OrderManagerCommand = new RelayCommand(() =>
            {
                _navigationService.NavigateTo<OrderManagerPage>();
            });

            ProductManagerCommand = new RelayCommand(() =>
            {
                _navigationService.NavigateTo<ProductManagerPage>();
            });

            LogoutCommand = new RelayCommand(() =>
            {
                _userSession.Logout();

                // Publish message to notify other view models (Logout)
                _eventAggregator.Publish(new LogoutMessage());
            });
        }


        // INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
