using CommunityToolkit.Mvvm.Input;
using Cosmetics_Shop.Models;
using Cosmetics_Shop.Services;
using Cosmetics_Shop.Views.Controls;
using Cosmetics_Shop.Views.Pages;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Cosmetics_Shop.ViewModels.UserControlViewModels;
using System.Runtime.CompilerServices;
using Cosmetics_Shop.DataAccessObject;
using Cosmetics_Shop.DataAccessObject.Interfaces;


namespace Cosmetics_Shop.ViewModels.PageViewModels
{
    /// <summary>
    /// View model for Product Detail Page
    /// </summary>
    public class ProductDetailViewModel : INotifyPropertyChanged
    {
        #region Fields
        private ProductDetail _productDetail;
        private int _shippingFee;
        private DBModels.ShippingMethod _currentShippingMethod;
        //private PaymentProductThumbnail _productToPayment;
        private List<PaymentProductThumbnail> _productsToPayment;
        private bool _isNavigating;
        private int _amount = 1;
        #endregion

        public ObservableCollection<ReviewThumbnailViewModel> reviewThumbnail { get; set; }
        public ObservableCollection<CartThumbnailViewModel> Cart { get; set; }

        #region Singleton
        private readonly IDao _dao = null;
        private readonly IServiceProvider _serviceProvider = null;
        private readonly INavigationService _navigationService;
        #endregion

        #region Command
        public ICommand PaidButtonCommand { get; set; }
        public ICommand GoBackCommand { get; set; }
        #endregion

        #region Properties Binding
        public ProductDetail ProductDetail
        {
            get => _productDetail;
            set
            {
                _productDetail = value;
                OnPropertyChanged(nameof(ProductDetail));
            }
        }
        public int ShippingFee
        {
            get => _shippingFee;
            set
            {
                if (_shippingFee != value)
                {
                    _shippingFee = value;
                    OnPropertyChanged(); // Notify UI of change
                }
            }
        }
        //public PaymentProductThumbnail ProductToPayment
        //{
        //    get => _productToPayment;
        //    set
        //    {
        //        if (_productToPayment != value)
        //        {
        //            _productToPayment = value;
        //            OnPropertyChanged(); // Notify UI of change
        //        }
        //    }
        //}
        public List<PaymentProductThumbnail> ProductsToPayment // Changed to List
        {
            get => _productsToPayment;
            set
            {
                if (_productsToPayment != value)
                {
                    _productsToPayment = value;
                    OnPropertyChanged(); // Notify UI of change
                }
            }
        }
        public bool IsNavigating
        {
            get => _isNavigating;
            set
            {
                if (_isNavigating != value)
                {
                    _isNavigating = value;
                    OnPropertyChanged(); // Notify UI of change
                }
            }
        }
        public int Amount
        {
            get => _amount;
            set
            {
                if (_amount != value)
                {
                    _amount = value;
                    OnPropertyChanged(nameof(Amount));
                }
            }
        }
        #endregion

        public ProductDetailViewModel(INavigationService    navigationService, 
                                      IDao                  dao, 
                                      IServiceProvider      serviceProvider)
        {
            _dao = dao;
            _serviceProvider = serviceProvider;
            _navigationService = navigationService;
            //ProductToPayment = new PaymentProductThumbnail();
            ProductsToPayment = new List<PaymentProductThumbnail>();

            PaidButtonCommand = new RelayCommand(ExecutePaidButtonCommand);
            GoBackCommand = new RelayCommand(() =>
            {
                _navigationService.GoBack();
            });
        }

        #region Reviews
        public void LoadInitialReviews(int idProduct)
        {
            reviewThumbnail = new ObservableCollection<ReviewThumbnailViewModel>();
            var review = _dao.GetListReviewThumbnailByIDProduct(idProduct);
            foreach (var item in review)
            {
                var reviewThumbnailViewModel = _serviceProvider.GetService(typeof(ReviewThumbnailViewModel)) as ReviewThumbnailViewModel;
                reviewThumbnailViewModel.ReviewThumbnail = item; // Assuming ReviewThumbnail is a property
                reviewThumbnail.Add(reviewThumbnailViewModel);
            }
        }

        public void ShowAllReviews()
        {
            // Fetch all reviews for the product
            //var allReviews = _dao.GetListReviewThumbnailByIDProduct(ProductDetail.Id);
            var allReviews = _dao.GetListReviewThumbnailByIDProduct(1);

            // Update the reviewThumbnail collection and notify the change
            reviewThumbnail.Clear(); // Clear existing reviews
            foreach (var review in allReviews)
            {
                var reviewThumbnailViewModel = _serviceProvider.GetService(typeof(ReviewThumbnailViewModel)) as ReviewThumbnailViewModel;
                reviewThumbnailViewModel.ReviewThumbnail = review;
                reviewThumbnail.Add(reviewThumbnailViewModel); // Add all reviews
            }
            OnPropertyChanged(nameof(reviewThumbnail)); // Notify that the reviewThumbnail has changed
        }

        public void FilterReviewsByStarNumber(int starNumber)
        {
            // Fetch all reviews for the product
            //var allReviews = _dao.GetListReviewThumbnailByIDProduct(ProductDetail.Id);
            var allReviews = _dao.GetListReviewThumbnailByIDProduct(1);

            // Filter reviews based on the selected star number
            var filteredReviews = allReviews
                .Where(review => review.StarNumber == starNumber)
                .Select(review =>
                {
                    var reviewThumbnailViewModel = _serviceProvider.GetService(typeof(ReviewThumbnailViewModel)) as ReviewThumbnailViewModel;
                    //reviewThumbnailViewModel.ReviewThumbnail = review;
                    if (reviewThumbnailViewModel != null)
                    {
                        reviewThumbnailViewModel.ReviewThumbnail = review; // Set the ReviewThumbnail property
                    }
                    return reviewThumbnailViewModel;
                })
                .Where(viewModel => viewModel != null) // Ensure we only return valid view models
                .ToList(); // Convert to list to evaluate count

            // Update the reviewThumbnail collection and notify the change
            reviewThumbnail.Clear(); // Clear existing reviews
            foreach (var review in filteredReviews)
            {
                reviewThumbnail.Add(review); // Add filtered reviews
            }

            OnPropertyChanged(nameof(reviewThumbnail)); // Notify that the reviewThumbnail has changed
        }
        #endregion

        #region Product Detail
        public async Task LoadProductDetailAsync(int id)
        {
            ProductDetail = await _dao.GetProductDetailAsync(id);
        }
        #endregion

        #region Shipping
        public async Task<List<DBModels.ShippingMethod>> GetShippingMethodsAsync()
        {
            return await _dao.GetShippingMethodsAsync(); 
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Payment
        public void ExecutePaidButtonCommand()
        {
            if (ProductsToPayment != null && ProductsToPayment.Count > 0)
            {
                IsNavigating = true; // Disable navigation
                // Set the amount for each product in the list
                foreach (var product in ProductsToPayment)
                {
                    product.Amount = Amount; // Set the amount for each product
                }
                // Navigate to the PaymentPage with the list of products
                _navigationService.NavigateTo<PaymentPage>(ProductsToPayment);
                IsNavigating = false;
            }
        }

        public void SetInfo(PaymentProductThumbnail p)
        {
            // Add the product to the list instead of setting a single product
            ProductsToPayment.Add(p);
            Amount = p.Amount; // Set the amount from the product
        }

        #endregion

        #region Cart
        public void LoadListCart()
        {
            Cart = new ObservableCollection<CartThumbnailViewModel>();

            var cartProduct = _dao.GetListCartProduct();

            for (int i = 0; i < cartProduct.Count; i++)
            {
                var cartThumbnailViewModel = _serviceProvider.GetService(typeof(CartThumbnailViewModel));
                cartThumbnailViewModel.GetType().GetProperty("CartThumbnail").SetValue(cartThumbnailViewModel, cartProduct[i]);
                Cart.Add(cartThumbnailViewModel as CartThumbnailViewModel);
            }
        }

        public void AddNewProductToCart(CartThumbnail cart)
        {
            var cartThumbnailViewModel = _serviceProvider.GetService(typeof(CartThumbnailViewModel));
            cartThumbnailViewModel.GetType().GetProperty("CartThumbnail").SetValue(cartThumbnailViewModel, cart);
            Cart.Add(cartThumbnailViewModel as CartThumbnailViewModel);
        }
        #endregion


    }
}
