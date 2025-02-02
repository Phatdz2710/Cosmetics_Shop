﻿using CommunityToolkit.Mvvm.Input;
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
using Microsoft.IdentityModel.Tokens;


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
        private Models.ShippingMethod _currentShippingMethod;
        private Models.Voucher _currentVoucher = new Models.Voucher();
        private List<PaymentProductThumbnail> _productsToPayment;
        private bool _isNavigating;
        private int _amount = 1;
        #endregion

        /// <summary>
        /// Gets or sets the collection of review thumbnails for the product.
        /// </summary>
        public ObservableCollection<ReviewThumbnailViewModel> reviewThumbnail { get; set; }

        /// <summary>
        /// Gets or sets the collection of cart thumbnails for the cart items.
        /// </summary>
        public ObservableCollection<CartThumbnailViewModel> Cart { get; set; }

        #region Singleton
        private readonly IDao _dao = null;
        private readonly IServiceProvider _serviceProvider = null;
        private readonly INavigationService _navigationService;
        #endregion

        #region Command
        /// <summary>
        /// Command for handling the paid button action.
        /// </summary>
        public ICommand PaidButtonCommand { get; set; }

        /// <summary>
        /// Command to navigate back to the previous page.
        /// </summary>
        public ICommand GoBackCommand { get; set; }
        #endregion

        #region Properties Binding

        /// <summary>
        /// Gets or sets the product details.
        /// Notifies the UI when the value changes.
        /// </summary>
        public ProductDetail ProductDetail
        {
            get => _productDetail;
            set
            {
                _productDetail = value;
                OnPropertyChanged(nameof(ProductDetail));
            }
        }

        /// <summary>
        /// Gets or sets the shipping fee.
        /// Notifies the UI when the value changes.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the list of products to be paid.
        /// Notifies the UI when the value changes.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the navigation state.
        /// Notifies the UI when the value changes.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the amount.
        /// Notifies the UI when the value changes.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the current shipping method.
        /// Notifies the UI when the value changes.
        /// </summary>
        public Models.ShippingMethod CurrentShippingMethod
        {
            get => _currentShippingMethod;
            set
            {
                if (_currentShippingMethod != value)
                {
                    _currentShippingMethod = value;
                    OnPropertyChanged(nameof(CurrentShippingMethod));
                }
            }
        }

        #endregion

        // Constructor
        public ProductDetailViewModel(INavigationService navigationService,
                                      IDao dao,
                                      IServiceProvider serviceProvider)
        {
            _dao = dao;
            _serviceProvider = serviceProvider;
            _navigationService = navigationService;
            //ProductToPayment = new PaymentProductThumbnail();
            ProductsToPayment = new List<PaymentProductThumbnail>();
            reviewThumbnail = new ObservableCollection<ReviewThumbnailViewModel>();

            PaidButtonCommand = new RelayCommand(ExecutePaidButtonCommand);
            GoBackCommand = new RelayCommand(() =>
            {
                _navigationService.GoBack();
            });
        }

        #region Reviews
        /// <summary>
        /// Load product ratings from database
        /// </summary>
        /// <param name="productID">ID of the product need to load ratings.</param>
        public async Task LoadInitialReviews(int productID)
        {
            reviewThumbnail.Clear();
            var review = await _dao.GetListReviewThumbnailByIDProductAsync(productID);
   
            foreach (var item in review)
            {
                var reviewThumbnailViewModel = _serviceProvider.GetService(typeof(ReviewThumbnailViewModel)) as ReviewThumbnailViewModel;
                reviewThumbnailViewModel.ReviewThumbnail = item; // Assuming ReviewThumbnail is a property
                reviewThumbnail.Add(reviewThumbnailViewModel);
            }
        }

        /// <summary>
        /// Show all product ratings from database
        /// </summary>
        public async void ShowAllReviews()
        {
            // Fetch all reviews for the product
            var allReviews = await _dao.GetListReviewThumbnailByIDProductAsync(_productDetail.Id);

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

        /// <summary>
        /// Category the product ratings by star number
        /// </summary>
        /// <param name="starNumber">The number of star need to category.</param>
        public async void FilterReviewsByStarNumber(int starNumber)
        {
            // Fetch all reviews for the product
            var allReviews = await _dao.GetListReviewThumbnailByIDProductAsync(_productDetail.Id);

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
        /// <summary>
        /// Load the product detail by ID of product from database
        /// </summary>
        /// <param name="id">ID of product need to load detail.</param>
        public async Task LoadProductDetailAsync(int id)
        {
            ProductDetail = await _dao.GetProductDetailAsync(id);
        }
        #endregion

        #region Shipping
        /// <summary>
        /// Load shipping methods from database
        /// </summary>
        /// <returns>A list of shipping methods</returns>
        public async Task<List<Models.ShippingMethod>> GetShippingMethodsAsync()
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
        /// <summary>
        /// Command to order product
        /// </summary>
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

                //_navigationService.NavigateTo<PaymentPage>(ProductsToPayment);
                var navigationData = new PaymentNavigationData
                (
                    ProductsToPayment,
                    CurrentShippingMethod,
                    null
                );

                // Navigate to the PaymentPage với đối tượng chứa thông tin
                _navigationService.NavigateTo<PaymentPage>(navigationData);
                IsNavigating = false;
            }
        }

        /// <summary>
        /// Load the product detail and quantity to order
        /// </summary>
        /// <param name="p">Product was selected to order.</param>
        public void SetInfo(PaymentProductThumbnail p)
        {
            // Add the product to the list instead of setting a single product
            ProductsToPayment.Add(p);
            Amount = p.Amount; // Set the amount from the product
        }

        #endregion

        #region Cart
        /// <summary>
        /// Add a product to cart in database
        /// </summary>
        /// <param name="productId">Product was selected to add the cart.</param>
        /// <param name="quantity">Quantity of product was added to cart.</param>
        /// <returns>Successful adding or not</returns>
        public async Task<bool> AddToCartAsync(int productId, int quantity)
        {
            // Gọi phương thức AddToCartAsync để thêm sản phẩm vào giỏ hàng
            bool result = await _dao.AddToCartAsync(productId, quantity);
            return result;
        }
        #endregion


    }
}
