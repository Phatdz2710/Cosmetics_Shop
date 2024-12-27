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
        private string _customerName;
        private DateTime _orderDate;
        private decimal _totalAmount;
        private decimal _totalPrice;
        //private string _status;

        public int OrderId
        {
            get { return _orderId; }
            set
            {
                _orderId = value;
                OnPropertyChanged(nameof(OrderId));
            }
        }
        public string CustomerName
        {
            get { return _customerName; }
            set
            {
                _customerName = value;
                OnPropertyChanged(nameof(CustomerName));
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
        public decimal TotalAmount
        {
            get { return _totalAmount; }
            set
            {
                _totalAmount = value;
                OnPropertyChanged(nameof(TotalAmount));
            }
        }

        public decimal TotalPrice
        {
            get { return _totalPrice; }
            set
            {
                _totalPrice = value;
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        //public string Status
        //{
        //    get { return _status; }
        //    set
        //    {
        //        _status = value;
        //        OnPropertyChanged(nameof(Status));
        //    }
        //}

        public ICommand DetailCommand { get; set; }

        public ICommand DeleteCommand { get; set; }

        public OrderCellViewModel() { }

        public OrderCellViewModel(int orderId,
                                  string customerName,
                                  DateTime orderDate,
                                  decimal totalAmount,
                                  decimal totalPrice,
                                  ICommand detailCommand,
                                  ICommand deleteCommand)
        {
            OrderId = orderId;
            CustomerName = customerName;
            OrderDate = orderDate;
            TotalAmount = totalAmount;
            TotalPrice = totalPrice;
            //Status = status;
            DetailCommand = detailCommand;    
            DeleteCommand = deleteCommand;
        }

        // For INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
