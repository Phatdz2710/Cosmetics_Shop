using CommunityToolkit.Mvvm.Input;
using Cosmetics_Shop.DataAccessObject.Interfaces;
using Cosmetics_Shop.Enums;
using Cosmetics_Shop.Models;
using Cosmetics_Shop.Services;
using Cosmetics_Shop.ViewModels.UserControlViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cosmetics_Shop.ViewModels.PageViewModels
{
    public class OrderPageViewModel : INotifyPropertyChanged
    {
        #region Singleton
        private readonly IDao _dao = null;
        private readonly UserSession _userSession = null;
        private readonly INavigationService _navigationService;
        #endregion

        #region Fields
        private bool _isZeroOrder = false;
        #endregion

        #region Properties for binding
        public ObservableCollection<UserOrderCellViewModel> UserOrders { get; set; }

        public bool IsZeroOrder
        {
            get { return _isZeroOrder; }
            set
            {
                _isZeroOrder = value;
                OnPropertyChanged(nameof(IsZeroOrder));
            }
        }

        #endregion

        #region Commands
        public ICommand LoadOrdersInProcessCommand { get; set; }
        public ICommand LoadOrdersSuccessCommand { get; set; }
        public ICommand LoadOrdersFailedCommand { get; set; }

        #endregion

        public OrderPageViewModel(IDao dao, 
                                  UserSession userSession,
                                  INavigationService navigationService)
        {
            _dao = dao;
            _userSession = userSession;
            _navigationService = navigationService;

            UserOrders = new ObservableCollection<UserOrderCellViewModel>();

            LoadOrdersInProcessCommand = new RelayCommand(loadOrdersInProcess);
            LoadOrdersSuccessCommand = new RelayCommand(loadOrdersSuccess);
            LoadOrdersFailedCommand = new RelayCommand(loadOrdersFailed);

            _ = LoadListOrder(OrderStatus.InProcess);
        }

        private async Task LoadListOrder(OrderStatus status)
        {
            UserOrders.Clear();
            var orders = await _dao.GetListOrderAsync(_userSession.GetId(), status);

            if (orders.Count == 0)
            {
                IsZeroOrder = true;
                return;
            }
            else IsZeroOrder = false;

            foreach (var order in orders)
            {
                var userOrderCellViewModel = new UserOrderCellViewModel(_dao, _navigationService, this) 
                { 
                    OrderId = order.Id,
                    OrderDate = order.OrderDate,
                    OrderStatus = order.OrderStatus,
                    TotalPrice = order.TotalPrice
                };
                   
                UserOrders.Add(userOrderCellViewModel);
            }
        }

        private async void loadOrdersInProcess()
        {
            await LoadListOrder(OrderStatus.InProcess);
        }

        private async void loadOrdersSuccess()
        {
            await LoadListOrder(OrderStatus.Success);
        }

        private async void loadOrdersFailed()
        {
            await LoadListOrder(OrderStatus.Failed);
        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
