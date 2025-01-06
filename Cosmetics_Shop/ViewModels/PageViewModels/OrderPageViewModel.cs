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
    /// <summary>
    /// View model for OrderPage
    /// </summary>
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
        /// <summary>
        /// Collection of user order cells.
        /// </summary>
        public ObservableCollection<UserOrderCellViewModel> UserOrders { get; set; }

        /// <summary>
        /// Flag indicating whether the user has no orders.
        /// </summary>
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
        /// <summary>
        /// Command to load orders in process.
        /// </summary>
        public ICommand LoadOrdersInProcessCommand { get; set; }

        /// <summary>
        /// Command to load successful orders.
        /// </summary>
        public ICommand LoadOrdersSuccessCommand { get; set; }

        /// <summary>
        /// Command to load failed orders.
        /// </summary>
        public ICommand LoadOrdersFailedCommand { get; set; }

        /// <summary>
        /// Command to navigate back.
        /// </summary>
        public ICommand GoBackCommand { get; set; }


        #endregion

        // Constructor
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

            GoBackCommand = new RelayCommand(() =>
            {
                _navigationService.GoBack();
            });

            _ = LoadListOrder(OrderStatus.InProcess);
        }

        /// <summary>
        /// Load list order by status
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Load orders in process
        /// </summary>
        private async void loadOrdersInProcess()
        {
            await LoadListOrder(OrderStatus.InProcess);
        }

        /// <summary>
        /// Load orders success
        /// </summary>
        private async void loadOrdersSuccess()
        {
            await LoadListOrder(OrderStatus.Success);
        }

        /// <summary>
        /// Load orders failed
        /// </summary>
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
