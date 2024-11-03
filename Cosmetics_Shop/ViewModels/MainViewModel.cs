﻿using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cosmetics_Shop.Views.Pages;
using Windows.Networking.Sockets;
using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Cosmetics_Shop.Services;
using Cosmetics_Shop.Services.Interfaces;
using Windows.ApplicationModel.Activation;

namespace Cosmetics_Shop.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        // Navigation service
        private readonly INavigationService _navigationService;
        // Event aggregator for publish and subscribe
        private readonly IEventAggregator _eventAggregator;

        // Command (gán Event cho button qua binding)

        #region Commands for Switch page
        public ICommand PurchaseButtonCommand { get; }
        public ICommand CartButtonCommand { get; }
        public ICommand AdminButtonCommand { get; }
        public ICommand AccountButtonCommand { get; }
        public ICommand DashboardButtonCommand { get; }
        public ICommand ProductDetailButtonCommand { get; }
        #endregion

        // Search button command
        public ICommand SearchButtonCommand { get; }
        
        // Từ khóa tìm kiếm (Binding)
        public string Keyword { get; set; }

        // Constructor
        public MainViewModel(IEventAggregator eventAggregator,
                                INavigationService navigationService)
        {
            _eventAggregator = eventAggregator;
            _navigationService = navigationService;

            // Switch to Purchase page
            PurchaseButtonCommand = new RelayCommand(() =>
            {
                _navigationService.NavigateTo<PurchasePage>();
            });

            // Switch to Cart page
            CartButtonCommand = new RelayCommand(() =>
            {
                _navigationService.NavigateTo<CartPage>();
            });

            // Switch to Admin page
            AdminButtonCommand = new RelayCommand(() =>
            {
                _navigationService.NavigateTo<AdminPage>();
            });

            // Switch to Account page
            AccountButtonCommand = new RelayCommand(() =>
            {
                _navigationService.NavigateTo<AccountPage>();
            });

            // Switch to Dashboard page
            DashboardButtonCommand = new RelayCommand(() =>
            {
                _navigationService.NavigateTo<DashboardPage>();
            });

            // Search button click event
            SearchButtonCommand = new RelayCommand(() =>
            {
                SearchEvent searchEvent = new SearchEvent();
                searchEvent.Keyword = Keyword;

                // Publish event (send event to all subscribers)
                _eventAggregator.Publish(searchEvent);
                _navigationService.NavigateTo<PurchasePage>();
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
