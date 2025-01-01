using CommunityToolkit.Mvvm.Input;
using Cosmetics_Shop.DataAccessObject.Interfaces;
using Cosmetics_Shop.Models;
using Cosmetics_Shop.Services;
using Cosmetics_Shop.Views.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cosmetics_Shop.ViewModels.UserControlViewModels
{
    public class UserOrderCellViewModel : INotifyPropertyChanged
    {
        #region Singleton
        private readonly IDao _dao = null;
        private readonly INavigationService _navigationService;

        #endregion

        #region Fields
        private int _orderId = 0;
        private DateTime _orderDate = DateTime.MinValue;
        private int _orderStatus = 0;
        private int _totalPrice = 0;
        private bool _isShowItems = false;
        private bool _alreadyLoadItems = false;
        private bool _isApproved = false;
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

                IsApproved = _orderStatus == 1;
            }
        }

        public bool IsApproved
        {
            get { return _isApproved; }
            set
            {
                _isApproved = value;
                OnPropertyChanged(nameof(IsApproved));
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
        public ICommand ReviewCommand { get; set; }

        #endregion

        public UserOrderCellViewModel(IDao dao, INavigationService navigationService)
        {
            _dao = dao;
            _navigationService = navigationService;

            OrderItemsDisplay = new ObservableCollection<OrderItemDisplay>();

            ShowHideItemCommand = new RelayCommand(ShowHideItems);

            ReviewCommand = new RelayCommand(async () =>
            {
                await LoadOrderItems();
                _navigationService.NavigateTo<ReviewPage>(OrderItemsDisplay);
            });
        }

        public async Task LoadOrderItems()
        {
            OrderItemsDisplay.Clear();
            var orderItems = await _dao.GetListOrderItemAsync(OrderId);

            foreach (var orderItem in orderItems)
            {
                var product = await _dao.GetProductDetailAsync(orderItem.ProductId);
                var orderItemDisplay = new OrderItemDisplay(orderItem.ProductId, product.Name, orderItem.Quantity, product.ThumbnailImage, product.Price);
                OrderItemsDisplay.Add(orderItemDisplay);
            }
        }


        private void ShowHideItems()
        {
            IsShowItems = !IsShowItems;

            if (_alreadyLoadItems == false)
            {
                _ = LoadOrderItems();
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
