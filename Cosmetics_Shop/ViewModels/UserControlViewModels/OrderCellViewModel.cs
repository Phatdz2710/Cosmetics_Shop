using CommunityToolkit.Mvvm.Input;
using Cosmetics_Shop.DataAccessObject.Interfaces;
using Cosmetics_Shop.DBModels;
using Cosmetics_Shop.Models;
using Cosmetics_Shop.ViewModels.AdminPageViewModels;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cosmetics_Shop.ViewModels.UserControlViewModels
{
    /// <summary>
    /// View model for order cell
    /// </summary>
    public class OrderCellViewModel : INotifyPropertyChanged
    {
        private readonly IDao _dao = null;
        private readonly OrderManagerViewModel _viewModel;

        private int _orderId;
        private int _userId;
        private DateTime _orderDate;
        private int _orderShippingMethod;
        private string _orderAddress;
        private int _orderStatus;
        private int _orderTotalPrice;

        public int OrderId
        {
            get { return _orderId; }
            set
            {
                _orderId = value;
                OnPropertyChanged(nameof(OrderId));
            }
        }
        public int UserId
        {
            get { return _userId; }
            set
            {
                _userId = value;
                OnPropertyChanged(nameof(UserId));
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
        public string OrderAddress
        {
            get { return _orderAddress; }
            set
            {
                _orderAddress = value;
                OnPropertyChanged(nameof(OrderAddress));
            }
        }
        public int OrderStatus
        {
            get { return _orderStatus; }
            set
            {
                _orderStatus = value;
                OnPropertyChanged(nameof(OrderStatus));
            }
        }
        public int OrderTotalPrice 
        {
            get { return _orderTotalPrice; }
            set
            {
                _orderTotalPrice = value;
                OnPropertyChanged(nameof(OrderTotalPrice));
            }
        }
        public ICommand ActionCommand { get; set; }
        public OrderCellViewModel() { }

        public OrderCellViewModel(int orderId, 
            int userId, 
            DateTime orderDate, 
            string orderAddress, 
            int orderStatus, 
            int orderTotalPrice,
            OrderManagerViewModel viewModel,
            IDao dao)
        {
            _dao = dao;
            OrderId = orderId;
            UserId = userId;
            OrderDate = orderDate;
            OrderAddress = orderAddress;
            OrderStatus = orderStatus;
            OrderTotalPrice = orderTotalPrice;
            _viewModel = viewModel;


            ActionCommand = new RelayCommand(ExecuteActionCommand);
        }

        private async void ExecuteActionCommand()
        {
            _viewModel.ShowForm = true;
            _viewModel.VisiActionButton = OrderStatus == 0;
            _viewModel.TotalPrice = OrderTotalPrice;
            _viewModel.ListView.Clear();

            var orderItems = await _dao.GetListOrderItemAsync(OrderId);

            foreach (var items in orderItems)
            {
                _viewModel.ListView.Add(items);
            }

            _viewModel.AcceptFormCommand = new RelayCommand(async () =>
            {
                await _dao.ChangeOrderStatusAsync(OrderId, 1);
                _viewModel.ShowForm = false;
                _viewModel.Message = "Duyệt đơn hàng thành công";
                OrderStatus = 1;
            });

            _viewModel.CancelFormCommand = new RelayCommand(async () =>
            {
                await _dao.ChangeOrderStatusAsync(OrderId, 3);
                _viewModel.ShowForm = false;
                _viewModel.Message = "Hủy đơn hàng thành công";
                OrderStatus = 3;
            });

        }

        // For INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
