using CommunityToolkit.Mvvm.Input;
using Cosmetics_Shop.DataAccessObject.Interfaces;
using Cosmetics_Shop.DBModels;
using Cosmetics_Shop.Enums;
using Cosmetics_Shop.Models;
using Cosmetics_Shop.Services;
using Cosmetics_Shop.Services.Interfaces;
using Cosmetics_Shop.ViewModels.UserControlViewModels;
using Cosmetics_Shop.Views;
using Cosmetics_Shop.Views.Pages;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
//using System.Data;
using System.Linq;
//using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Input;

namespace Cosmetics_Shop.ViewModels.AdminPageViewModels
{
    public class OrderManagerViewModel : INotifyPropertyChanged
    {
        #region Singletons
        private readonly IDao _dao;
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;
        private readonly UserSession _userSession;
        #endregion

        #region Fields
        private bool _visiPrevious = false;
        private bool _visiNext = true;
        private int _currentPage = 1;
        private int _totalPage = 1;
        private int _totalOrders = 0;
        private bool _showForm = false;       
        private int _orderId = 0;        
        private int _orderStatus = 0;
        private int _totalPrice = 0;        
        private string _message = "";

        //private Models.Order _selectedOrder;
        

        #endregion

        #region Properties for binding
        public ObservableCollection<OrderCellViewModel> ListOrders { get; set; }
        public ObservableCollection<OrderItemDisplay> ListView { get; set; }


        public bool VisiPrevious
        {
            get { return _visiPrevious; }
            set
            {
                _visiPrevious = value;
                OnPropertyChanged(nameof(VisiPrevious));
            }
        }

        public bool VisiNext
        {
            get { return _visiNext; }
            set
            {
                _visiNext = value;
                OnPropertyChanged(nameof(VisiNext));
            }
        }

        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }

        public int TotalPage
        {
            get { return _totalPage; }
            set
            {
                _totalPage = value;
                OnPropertyChanged(nameof(TotalPage));
            }
        }

        public int TotalOrders
        {
            get { return _totalOrders; }
            set
            {
                _totalOrders = value;
                OnPropertyChanged(nameof(TotalOrders));
            }
        }

        public bool ShowForm
        {
            get { return _showForm; }
            set
            {
                _showForm = value;
                OnPropertyChanged(nameof(ShowForm));
            }
        }

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
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

        public int OrderStatus
        {
            get { return _orderStatus; }
            set
            {
                _orderStatus = value;
                OnPropertyChanged(nameof(OrderStatus));

                
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
        public ICommand NextPageCommand => new RelayCommand(nextPageCommand);
        public ICommand PreviousPageCommand => new RelayCommand(previousPageCommand);
        public ICommand ReloadCommand { get; set; }

        private ICommand _acceptFormCommand;
        public ICommand AcceptFormCommand
        {
            get => _acceptFormCommand;
            set
            {
                _acceptFormCommand = value;
                OnPropertyChanged(nameof(AcceptFormCommand));
            }
        }

        private ICommand _cancelFormCommand;

        public ICommand CancelFormCommand
        {
            get => _cancelFormCommand;
            set
            {
                _cancelFormCommand = value;
                OnPropertyChanged(nameof(CancelFormCommand));
            }
        }

        private ICommand _showHideItemCommand { get; set; }
        public ICommand ShowHideItemCommand
        {
            get { return _showHideItemCommand; }
            set
            {
                _showHideItemCommand = value;
                OnPropertyChanged(nameof(ShowHideItemCommand));
            }
        }
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

            ListOrders = new ObservableCollection<OrderCellViewModel>();

            ListView = new ObservableCollection<OrderItemDisplay>();                     

           
            ReloadCommand = new RelayCommand(reloadCommand);

            CancelFormCommand = new RelayCommand(cancelFormCommand);
            // Load orders when the ViewModel is created
            LoadData();
            
        }


        /// <summary>
        /// Command for go to next page
        /// </summary>
        private void nextPageCommand()
        {
            CurrentPage++;
            LoadData();
        }

        /// <summary>
        /// Command for go to previous page
        /// </summary>
        private void previousPageCommand()
        {
            CurrentPage--;
            LoadData();
        }

        private void reloadCommand()
        {
            CurrentPage = 1;
            LoadData();
        }


        private async void LoadData()
        {
            var result = await _dao.GetListAllOrdersAsync(CurrentPage, 10);
            TotalPage = result.TotalPages;
            TotalOrders = result.TotalOrders;
            var sum = 0;

            // Show or hide button next/previous page
            VisiNext = CurrentPage != TotalPage;
            VisiPrevious = CurrentPage != 1;

            ListOrders.Clear();
            foreach (var order in result.ListOrders)
            {
                ListOrders.Add(new OrderCellViewModel(order.Id, 
                        order.UserId, 
                        order.OrderDate, 
                        order.ShippingAddress,
                        order.OrderStatus,
                        new RelayCommand(async () =>
                        {
                            ShowForm = true;
                            ///<summary>
                            ///
                            /// </summary>

                            //ShowHideItemCommand = new RelayCommand (async() =>
                            //{


                            //});

                            ListView.Clear();
                            var orderItems = await _dao.GetListOrderItemAsync(order.UserId);

                            foreach (var orderItem in orderItems)
                            {
                                var product = await _dao.GetProductDetailAsync(orderItem.ProductId);
                                var totalPrice = orderItem.Quantity * product.Price;
                                var orderItemDisplay = new OrderItemDisplay(orderItem.ProductId, product.Name, orderItem.Quantity, product.ThumbnailImage, product.Price, totalPrice);


                                sum += totalPrice;

                                ListView.Add(orderItemDisplay);
                                AcceptFormCommand = new RelayCommand(async () =>
                                {
                                    ShowForm = false;
                                    var result = await _dao.ChangeOrderStatusAsync();

                                    if (result)
                                    {
                                        Message = "Đơn hàng đã được duyệt!";
                                        order.OrderStatus = 1;
                                        ListView.Add(orderItemDisplay);
                                        LoadData();
                                    }
                                    else
                                    {
                                        Message = "Đơn hàng đang chờ duyệt!";
                                        cancelFormCommand();
                                    }
                                });
                            }
                            TotalPrice = sum;
                            sum = 0;

                            

                        }))
                            
                            
                        
                );

            }
        }             
       
               

        
        /// <summary>
        /// Cancel form command
        /// </summary>
        private void cancelFormCommand()
        {
            OrderStatus = 0;            
            ShowForm = false;
        }

               

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
