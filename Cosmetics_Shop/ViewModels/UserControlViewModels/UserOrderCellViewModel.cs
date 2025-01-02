using CommunityToolkit.Mvvm.Input;
using Cosmetics_Shop.DataAccessObject.Interfaces;
using Cosmetics_Shop.Models;
using Cosmetics_Shop.Services;
using Cosmetics_Shop.ViewModels.PageViewModels;
using Cosmetics_Shop.Views.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cosmetics_Shop.ViewModels.UserControlViewModels
{
    public class UserOrderCellViewModel : INotifyPropertyChanged
    {
        #region Singleton
        private readonly IDao _dao = null;
        private readonly INavigationService _navigationService;
        private readonly OrderPageViewModel _viewModel;

        #endregion

        #region Fields
        private int _orderId = 0;
        private DateTime _orderDate = DateTime.MinValue;
        private int _orderStatus = 0;
        private int _totalPrice = 0;
        private bool _isShowItems = false;
        private bool _alreadyLoadItems = false;
        private bool _isShowButton = false;
        private readonly SemaphoreSlim _loadItemsLock = new SemaphoreSlim(1, 1);
        #endregion

        #region Properties for binding
        public ObservableCollection<OrderItemDisplay> OrderItemsDisplay { get; set; }

        public bool IsShowItems
        {
            get { return _isShowItems; }
            set
            {
                _isShowItems = value;
                OnPropertyChanged(nameof(IsShowItems));
            }
        }

        public int OrderId
        {
            get { return _orderId; }
            set
            {
                _orderId = value;
                OnPropertyChanged(nameof(OrderId));
            }
        } 

        public DateTime OrderDate
        {
            get { return _orderDate; }
            set
            {
                _orderDate = value;
                OnPropertyChanged(nameof(OrderDate));
            }
        }

        public int OrderStatus
        {
            get { return _orderStatus; }
            set
            {
                _orderStatus = value;
                OnPropertyChanged(nameof(OrderStatus));

                IsShowButton = _orderStatus == 1 || _orderStatus == 2;

                if (OrderStatus == 1)
                {
                    ReceivedCommand = new RelayCommand(receivedCommand);
                }
                else
                {
                    ReceivedCommand = new RelayCommand(reviewCommand);
                }
            }
        }

        public bool IsShowButton
        {
            get { return _isShowButton; }
            set
            {
                _isShowButton = value;
                OnPropertyChanged(nameof(IsShowButton));
            }
        }

        public int TotalPrice
        {
            get { return _totalPrice; }
            set
            {
                _totalPrice = value;
                OnPropertyChanged(nameof(TotalPrice));
            }
        }
        #endregion

        #region Commands
        public ICommand ShowHideItemCommand { get; set; }
        public ICommand ReceivedCommand { get; set; }

        #endregion

        public UserOrderCellViewModel(IDao dao, INavigationService navigationService, OrderPageViewModel viewModel)
        {
            _dao = dao;
            _navigationService = navigationService;
            _viewModel = viewModel;

            OrderItemsDisplay = new ObservableCollection<OrderItemDisplay>();

            ShowHideItemCommand = new RelayCommand(ShowHideItems);
        }

        public async Task LoadOrderItems()
        {
            await _loadItemsLock.WaitAsync(); // Đảm bảo chỉ một luồng được truy cập

            try
            {
                OrderItemsDisplay.Clear();
                var orderItems = await _dao.GetListOrderItemAsync(OrderId);

                foreach (var orderItem in orderItems)
                {
                    var product = await _dao.GetProductDetailAsync(orderItem.ProductId);
                    var totalPrice = orderItem.Quantity * product.Price;
                    var orderItemDisplay = new OrderItemDisplay(orderItem.ProductId, product.Name, orderItem.Quantity, product.ThumbnailImage, product.Price, totalPrice);
                    OrderItemsDisplay.Add(orderItemDisplay);
                }
            }
            finally
            {
                _loadItemsLock.Release();
            }
        }

        private async void receivedCommand()
        {
            var result = await _dao.ChangeOrderStatusAsync(OrderId, 2);
            if (result)
            {
                OrderStatus = 1;
            }

            _viewModel.UserOrders.Remove(this);
        }

        private async void reviewCommand()
        {
            if (_alreadyLoadItems == false)
            {
                await LoadOrderItems();
                _alreadyLoadItems = true;
            }
            _navigationService.NavigateTo<ReviewPage>(OrderItemsDisplay);
        }


        private async void ShowHideItems()
        {
            IsShowItems = !IsShowItems;

            if (_alreadyLoadItems == false)
            {
                await LoadOrderItems();
                _alreadyLoadItems = true;
            }
        }



        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
