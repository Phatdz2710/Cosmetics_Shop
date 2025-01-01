using CommunityToolkit.Mvvm.Input;
using Cosmetics_Shop.Models;
using Cosmetics_Shop.Services;
using Cosmetics_Shop.ViewModels.UserControlViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Cosmetics_Shop.Views.Pages;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Cosmetics_Shop.DataAccessObject.Interfaces;
using Windows.Web.AtomPub;

namespace Cosmetics_Shop.ViewModels.PageViewModels
{
    /// <summary>
    /// View model for ReviewPage
    /// </summary
    public class ReviewPageViewModel : INotifyPropertyChanged
    {
        // Data access object
        private readonly UserSession _userSession;
        private readonly INavigationService _navigationService;
        private readonly IServiceProvider _serviceProvider;
        private IDao _dao = null;
        public event Action<string> ShowDialogRequested;

        #region Command
        public ICommand ReviewCommand { get; set; }
        public ICommand CancelReviewCommand { get; set; }
        #endregion

        #region Fields
        private string _name;
        private string _nameDisplay;
        private string _phone;
        private string _address;
        private int _productID;

        #endregion

        #region Properties Binding
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string NameDisplay
        {
            get { return _nameDisplay; }
            set
            {
                _nameDisplay = value;
                OnPropertyChanged(nameof(NameDisplay));
            }
        }

        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }
        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }
        public int ProductID
        {
            get { return _productID; }
            set
            {
                _productID = value;
                OnPropertyChanged(nameof(ProductID));
            }
        }
        #endregion

        public ObservableCollection<ReviewPageThumbnailViewModel> ReviewThumbnails { get; set; }

        public ReviewPageViewModel(INavigationService navigationService, 
                                   IDao dao,
                                   UserSession userSession,
                                   IServiceProvider serviceProvider,
                                   ObservableCollection<OrderItemDisplay> products)
        {
            _dao = dao;
            _navigationService = navigationService;
            _userSession = userSession;
            _serviceProvider = serviceProvider;

            ReviewThumbnails = new ObservableCollection<ReviewPageThumbnailViewModel>();
            if (products != null)
            {
                foreach (var product in products)
                {
                    var reviewThumbnailViewModel = _serviceProvider.GetService<ReviewPageThumbnailViewModel>();
                    reviewThumbnailViewModel.Product = product;
                    ReviewThumbnails.Add(reviewThumbnailViewModel);
                }
            }

            loadUserInformation();

            ReviewCommand = new RelayCommand(async () => await ExecuteReviewCommand());
            CancelReviewCommand = new RelayCommand(() =>
            {
                _navigationService.GoBack();
            });

        }

        #region Execute command
        /// <summary>
        /// Command to add a rating to database
        /// </summary>
        public async Task ExecuteReviewCommand()
        {
            foreach (var reviews in ReviewThumbnails)
            {
                if (reviews.StarNumber > 0)
                {
                    //var review = new ReviewThumbnail(_userSession.GetId(), reviews.ProductID, reviewViewModel.StarNumber, DateTime.Now);
                    var review = await _dao.AddReviewAsync(reviews.Product.ProductId, reviews.StarNumber);
                    var average = await _dao.RecalculateRatingAverage(reviews.Product.ProductId, reviews.StarNumber);
                    if (review && average)
                    {

                    }
                }
            }

            // Navigate to a confirmation page or show a success message
            _navigationService.NavigateTo<OrderPage>();
        }

        #endregion

        #region User
        /// <summary>
        /// Load user information
        /// </summary>
        private async void loadUserInformation()
        {
            var userDetail = await _dao.GetUserDetailAsync(_userSession.GetId());

            Name = userDetail.Name;
            NameDisplay = userDetail.Name;
            Phone = userDetail.Phone;
            Address = userDetail.Address;
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
