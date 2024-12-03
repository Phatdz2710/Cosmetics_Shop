using Cosmetics_Shop.Models;
using Cosmetics_Shop.Services;
using Cosmetics_Shop.Views;
using Cosmetics_Shop.Views.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly UserSession        _userSession;
        private readonly INavigationService _navigationService;
        public MainViewModel(INavigationService navigationService, 
                             UserSession        userSession)
        {
            _userSession        = userSession;
            _navigationService  = navigationService;

            if (_userSession.GetRole() == "Admin")
            {
                _navigationService.NavigateTo<AdminPage>();
            }
            else
            {
                _navigationService.NavigateTo <UserPage>();
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
