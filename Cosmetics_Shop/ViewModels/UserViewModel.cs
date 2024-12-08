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
using System.Runtime.CompilerServices;
using Cosmetics_Shop.Services.EventAggregatorMessages;
using Cosmetics_Shop.Enums;
using Cosmetics_Shop.DataAccessObject.Interfaces;

namespace Cosmetics_Shop.ViewModels
{
    /// <summary>
    /// View model for user window
    /// </summary>
    public class UserViewModel : INotifyPropertyChanged
    {
        #region Singleton
        // Navigation service
        private readonly INavigationService _navigationService;
        // Event aggregator for publish and subscribe
        private readonly IEventAggregator _eventAggregator;
        // Data access object
        private readonly IDao _dao;
        // User session
        private readonly UserSession _userSession;
        #endregion

        #region Fields
        private string avatarPath;
        private string name;
        
        // Command (gán Event cho button qua binding)
        private ObservableCollection<string> _suggestions;
        private string _keyword;
        #endregion

        #region Properties for binding
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

        // Display Username
        public string Username
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string AvatarPath
        {
            get => avatarPath;
            set
            {
                avatarPath = value;
                OnPropertyChanged(nameof(AvatarPath));
            }
        }

        #endregion

        #region Commands
        public ICommand PurchaseButtonCommand { get; }
        public ICommand CartButtonCommand { get; }
        public ICommand AdminButtonCommand { get; }
        public ICommand AccountButtonCommand { get; }
        public ICommand DashboardButtonCommand { get; }

        // Search button command
        public ICommand SearchButtonCommand { get; }

        #endregion

        // Constructor
        public UserViewModel(IEventAggregator   eventAggregator,
                            INavigationService  navigationService,
                            IDao                dao,
                            UserSession         userSession)
        {
            _eventAggregator    = eventAggregator;
            _navigationService  = navigationService;
            _userSession        = userSession;
            _dao                = dao;

            Suggestions = new ObservableCollection<string>();

            _navigationService.NavigateTo<DashboardPage>();

            // Switch to Purchase page
            PurchaseButtonCommand   = new RelayCommand(ExecutePurchaseButtonCommand);

            // Switch to Cart page
            CartButtonCommand       = new RelayCommand(ExecuteCartButtonCommand);

            // Switch to Admin page
            AdminButtonCommand      = new RelayCommand(ExecuteAdminButtonCommand);

            // Switch to Account page
            AccountButtonCommand    = new RelayCommand(ExecuteAccountButtonCommand);

            // Switch to Dashboard page
            DashboardButtonCommand  = new RelayCommand(ExecuteDashboardButtonCommand);

            // Search button click event
            SearchButtonCommand     = new RelayCommand(ExecuteSearchButtonCommand);

            LoadUserButton();

            _eventAggregator.Subscribe<ChangeUsernameOrAvatarMessage>((param) =>
            {
                Username = param.Name;
                AvatarPath = param.AvatarPath;
            });
        }

        /// <summary>
        /// Logic for the Purchase button command.
        /// </summary>
        private void ExecutePurchaseButtonCommand()
        {
            _navigationService.NavigateTo<PurchasePage>();
        }

        /// <summary>
        /// Logic for the Cart button command.
        /// </summary>
        private void ExecuteCartButtonCommand()
        {
            _navigationService.NavigateTo<CartPage>();
        }

        /// <summary>
        /// Logic for the Admin button command.
        /// </summary>
        private void ExecuteAdminButtonCommand()
        {
            _navigationService.NavigateTo<AdminPage>();
        }

        /// <summary>
        /// Logic for the Account button command.
        /// </summary>
        private void ExecuteAccountButtonCommand()
        {
            _navigationService.NavigateTo<AccountPage>();
        }

        /// <summary>
        /// Logic for the Dashboard button command.
        /// </summary>
        private void ExecuteDashboardButtonCommand()
        {
            _navigationService.NavigateTo<DashboardPage>();
            var reloadEvent = new ReloadDashboardRequire();

            // Publish event (send event to all subscribers)
            _eventAggregator.Publish(reloadEvent);
        }

        /// <summary>
        /// Logic for the Search button command.
        /// </summary>
        private void ExecuteSearchButtonCommand()
        {
            // Navigate to Purchase page
            _navigationService.NavigateTo<PurchasePage>();

            // Publish event (send event to all subscribers)
            var searchEvent = new SearchEvent
            {
                Keyword = Keyword
            };
            _eventAggregator.Publish(searchEvent);
        }


        /// <summary>
        /// Load user's information
        /// </summary>
        private async void LoadUserButton()
        {
            var userName    = await _dao.GetInformationAsync(_userSession.GetId(), UserInformationType.Name);
            var avatarPath  = await _dao.GetInformationAsync(_userSession.GetId(), UserInformationType.AvatarPath);

            if (avatarPath == null)
            {
                avatarPath = "ms-appx:///Assets/avatar_temp.png";
            }

            Username    = (string)userName;
            AvatarPath  = (string)avatarPath;
        }


        /// <summary>
        /// Update suggestions for search box
        /// </summary>
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
