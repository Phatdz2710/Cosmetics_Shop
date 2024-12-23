using CommunityToolkit.Mvvm.Input;
using Cosmetics_Shop.DataAccessObject.Interfaces;
using Cosmetics_Shop.Models;
using Cosmetics_Shop.Services;
using Cosmetics_Shop.Services.Interfaces;
using Cosmetics_Shop.ViewModels.UserControlViewModels;
using Cosmetics_Shop.Views.Pages;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Input;

namespace Cosmetics_Shop.ViewModels.AdminPageViewModels
{
    public class OrderManagerViewModel : INotifyPropertyChanged
    {
        #region Singletons
        private readonly IDao _dao = null;
        private readonly INavigationService _navigationService = null;
        private readonly IEventAggregator _eventAggregator = null;
        private readonly UserSession _userSession = null;

        #endregion

        #region Fields
        // 
        #endregion

        #region Properties for binding
        
        #endregion



        #region Properties for Binding
        //
        #endregion

        public OrderManagerViewModel(IDao dao,
                                     INavigationService navigationService,
                                     IEventAggregator eventAggregator,
                                     UserSession userSession)
        {
            _dao = dao;
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;
            _userSession = userSession;

            // CODE HERE

        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
