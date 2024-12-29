using CommunityToolkit.Mvvm.Input;
using Cosmetics_Shop.DataAccessObject.Interfaces;
using Cosmetics_Shop.DBModels;
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
        private string _formTitle = "";
        

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

        //public Models.Order SelectedOrder
        //{
        //    get => _selectedOrder;
        //    set
        //    {
        //        _selectedOrder = value;
        //        OnPropertyChanged(nameof(SelectedOrder));
        //    }
        //}
        #endregion

        #region Commands
        public ICommand NextPageCommand => new RelayCommand(nextPageCommand);
        public ICommand PreviousPageCommand => new RelayCommand(previousPageCommand);
        public ICommand ReloadCommand => new RelayCommand(reloadCommand);

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
                        })));
            }
           
        }

        


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
