using Cosmetics_Shop.DBModels;
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
        private int _orderId;
        private int _userId;
        private DateTime _orderDate;
        private int _orderShippingMethod;
        private string _orderAddress;
        private int _orderStatus;

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

        public ICommand ActionCommand { get; set; }

        public OrderCellViewModel() { }

        public OrderCellViewModel(int orderId, int userId, DateTime orderDate, string orderAddress, int orderStatus, ICommand actionCommand)
        {
            OrderId = orderId;
            UserId = userId;
            OrderDate = orderDate;
            OrderAddress = orderAddress;
            OrderStatus = orderStatus;
            ActionCommand = actionCommand;
        }

        // For INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
