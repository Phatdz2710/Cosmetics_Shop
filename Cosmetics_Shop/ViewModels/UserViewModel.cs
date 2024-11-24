using Microsoft.UI.Xaml.Controls;
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
using Cosmetics_Shop.Models;
using System.Collections.ObjectModel;
using Cosmetics_Shop.Models.DataService;
using System.Runtime.CompilerServices;

namespace Cosmetics_Shop.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        // Navigation service
        private readonly INavigationService _navigationService;
        // Event aggregator for publish and subscribe
        private readonly IEventAggregator _eventAggregator;
        // Data access object
        private readonly IDao _dao;

        private readonly UserSession _userSession;
        // Command (gán Event cho button qua binding)
        private ObservableCollection<string> _suggestions;
        private string _keyword;
        public ObservableCollection<string> Suggestions
        {
            get => _suggestions;
            set
            {
                _suggestions = value;
                OnPropertyChanged(nameof(Suggestions));
            }
        }

        // Từ khóa tìm kiếm (Binding)
        public string Keyword
        {
            get => _keyword;
            set
            {
                _keyword = value;
                OnPropertyChanged(nameof(_keyword));
                UpdateSuggestions();
            }
        }

        #region Commands for Switch page
        public ICommand PurchaseButtonCommand { get; }
        public ICommand CartButtonCommand { get; }
        public ICommand AdminButtonCommand { get; }
        public ICommand AccountButtonCommand { get; }
        public ICommand DashboardButtonCommand { get; }
        #endregion

        // Search button command
        public ICommand SearchButtonCommand { get; }

        // Display Username
        public string Username
        {
            get
            {
                return _userSession.GetName();
            }
        }


        // Constructor
        public UserViewModel(   IEventAggregator    eventAggregator,
                                INavigationService  navigationService,
                                IDao                dao,
                                UserSession         userSession)
        {
            _eventAggregator    = eventAggregator;
            _navigationService  = navigationService;
            _userSession        = userSession;
            _dao                = dao;

            Suggestions = new ObservableCollection<string>();

            if (_userSession.GetRole() == "Admin")
            {
                // Switch to Admin page
                _navigationService.NavigateTo<AdminPage>();
            }
            else
            {
                // Switch to Dashboard page
                _navigationService.NavigateTo<DashboardPage>();
            }

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
                _navigationService.NavigateTo<PurchasePage>();
                SearchEvent searchEvent = new SearchEvent();
                searchEvent.Keyword = Keyword;

                // Publish event (send event to all subscribers)
                _eventAggregator.Publish(searchEvent);
            });
        }


        // Update Search's suggest
        private async void UpdateSuggestions()
        {
            Suggestions.Clear();
            if (!string.IsNullOrWhiteSpace(Keyword))
            {
                var suggestions = await _dao.GetSuggestionsAsync(Keyword);

                foreach (var suggestion in suggestions)
                {
                    Suggestions.Add(suggestion);
                }
            }
        }


        // For INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
