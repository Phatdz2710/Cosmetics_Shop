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
    /// <summary>
    /// View model for main window
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Singleton
        // User Session
        private readonly UserSession        _userSession;
        // Navigation Service
        private readonly INavigationService _navigationService;
        #endregion

        // Constructor
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

        // For INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
