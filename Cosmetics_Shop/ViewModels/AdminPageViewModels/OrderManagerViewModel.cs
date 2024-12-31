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
        private bool _showFormShowMore = false;
        private string _formTitle = "";   
        private string _productName = "";
        private int _productId = 0;
        private int _quantity = 0;
        private decimal _price = 0;
        private decimal _totalPrice = 0;
        private string _message = "";

        private Models.Order _selectedOrder;
        

        #endregion

        #region Properties for binding
        public ObservableCollection<OrderCellViewModel> ListOrders { get; set; }
        

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

        public string FormTitle
        {
            get { return _formTitle; }
            set
            {
                _formTitle = value;
                OnPropertyChanged(nameof(FormTitle));
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
        
        public string ProductName
        {
            get { return _productName; }
            set
            {
                _productName = value;
                OnPropertyChanged(nameof(ProductName));
            }
        }

        

        public int ProductId
        {
            get { return _productId; }
            set
            {
                _productId = value;
                OnPropertyChanged(nameof(ProductId));
            }
        }

        

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        public decimal Price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
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

        public bool ShowFormShowMore
        {
            get { return _showFormShowMore; }
            set
            {
                _showFormShowMore = value;
                OnPropertyChanged(nameof(ShowFormShowMore));
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

        private ICommand _showMore;

        public ICommand ShowMore
        {
            get { return _showMore; }
            set
            {
                _showMore = value;
                OnPropertyChanged(nameof(ShowMore));
            }
        }
        public object OrderStatusEnum { get; private set; }
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

            CancelFormCommand = new RelayCommand(cancelFormCommand);

            ShowMore = new RelayCommand<int>(execute: ShowMoreCommand);
            ReloadCommand = new RelayCommand(reloadCommand);
            // Load orders when the ViewModel is created
            LoadData();
            //
            
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
                        new RelayCommand(() =>
                        {
                            ShowForm = true;
                        }))
                );

            }
        }

        

        /// <summary>
        /// Show form to show more information 
        /// </summary>
        /// <param name="productId"></param>
        private async void ShowMoreCommand(int productId)
        {
            var orderInfo = await _dao.GetOrderItemDisplayAsync(productId);
            AcceptFormCommand = new RelayCommand(acceptOrderCommand);
            AcceptFormCommand = new RelayCommand(CloseShowMoreForm);
            ShowFormShowMore = true;
            FormTitle = "Xem thêm thông tin";
            ProductId = orderInfo.ProductId;
            ProductName = orderInfo.ProductName;
            Quantity = orderInfo.Quantity;
            Price = orderInfo.Price;
            TotalPrice = orderInfo.TotalPrice;
        }

        /// <summary>
        /// Close show more form command
        /// </summary>
        private void CloseShowMoreForm()
        {
            ShowFormShowMore = false;
        }

        /// <summary>
        /// Accept create account command
        /// </summary>
        private async void acceptOrderCommand()
        {
            ShowFormShowMore = false;
            var result = await _dao.ChangeOrderStatusAsync();

            if (result)
            {
                Message = "Đơn hàng đã được duyệt!";
                LoadData();
            }
            else
            {
                Message = "Đơn hàng đang chờ duyệt!";
            }
        }

        /// <summary>
        /// Cancel form command
        /// </summary>
        private void cancelFormCommand()
        {
            ShowForm = false;
        }





        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
