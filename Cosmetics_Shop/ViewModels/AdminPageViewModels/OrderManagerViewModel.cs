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
        private ObservableCollection<Order> _listorders= new ObservableCollection<Order>();

        private bool _visiPrevious = true;
        private bool _visiNext = true;
        private int _currentPage = 1;
        private int _totalPage = 1;
        private bool _showForm = false;

        private Order _selectedOrder;
        #endregion

        #region Properties for binding
        public ObservableCollection<Order> ListOrders
        {
            get{ return _listorders; }
            set
            {
                _listorders = value;
                OnPropertyChanged(nameof(ListOrders));
            }
        }

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

        public bool ShowForm
        {
            get { return _showForm; }
            set
            {
                _showForm = value;
                OnPropertyChanged(nameof(ShowForm));
            }
        }


        public Order SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
                OnPropertyChanged(nameof(SelectedOrder));
            }
        }
        #endregion

        #region Commands
        public ICommand LoadOrdersCommand { get; }
        public ICommand EditOrderCommand { get; }

        public ICommand CancelFormCommand { get; set; }
        public ICommand NextPageCommand { get; }
        public ICommand PreviousPageCommand { get; }

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

            ListOrders = new ObservableCollection<Order>();

            LoadOrdersCommand = new AsyncRelayCommand(LoadData);
            //EditOrderCommand = new RelayCommand(EditOrder);

            // Load orders when the ViewModel is created
            LoadData();

            CancelFormCommand = new RelayCommand(cancelFormCommand);
            //SelectImagePathCommand = new RelayCommand(executeSelectImagePathAsyncCommand);
            NextPageCommand = new RelayCommand(nextPageCommand);
            PreviousPageCommand = new RelayCommand(previousPageCommand);
        }

        /// <summary>
        /// Cancels the form by hiding it.
        /// </summary>
        private void cancelFormCommand()
        {
            ShowForm = false;
        }

        /// <summary>
        /// Allows the user to select an image file path and updates the product image path.
        /// </summary>
        //private async void executeSelectImagePathAsyncCommand()
        //{
        //    var path = await _filePickerService.PickFileAsync(new List<string>
        //    {
        //        ".jpg", ".jpeg", ".png"
        //    });

        //    if (path == null) return;
        //    ProductImagePath = path;
        //}

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

        private async Task LoadData()
        {
            try
            {
                var result = await _dao.GetListOrderAsync(_userSession.GetId());

                //TotalPage = result.TotalPages;

                // Show or hide button next/previous page
                VisiNext = CurrentPage != _totalPage;
                VisiPrevious = CurrentPage != 1;

                ListOrders.Clear();
                foreach (var order in result)
                {
                    ListOrders.Add(order);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error, show a message to the user)
            }
        }

        


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
