using CommunityToolkit.Mvvm.Input;
using Cosmetics_Shop.Models;
using Cosmetics_Shop.Models.DataService;
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
    public class AdminViewModel : INotifyPropertyChanged
    {
        private readonly INavigationService _navigationService = null;
        private readonly UserSession        _userSession = null;
        private readonly IEventAggregator   _eventAggregator = null;


        public ICommand AccountManagerCommand   { get; set; }
        public ICommand OrderManagerCommand     { get; set; }
        public ICommand ProductManagerComamnd   { get; set; }
        public ICommand LogoutCommand           { get; set; }


        public AdminViewModel(INavigationService navigationService,
                              IEventAggregator   eventAggregator,
                              UserSession        userSession)
        {
            _navigationService  = navigationService;
            _eventAggregator    = eventAggregator;
            _userSession        = userSession;

            _userSession.GetId();

            _navigationService.NavigateTo<AccountManagerPage>();

            AccountManagerCommand = new RelayCommand(() =>
            {
                _navigationService.NavigateTo<AccountManagerPage>();
            });

            OrderManagerCommand = new RelayCommand(() =>
            {
                _navigationService.NavigateTo<OrderManagerPage>();
            });

            ProductManagerComamnd = new RelayCommand(() =>
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
