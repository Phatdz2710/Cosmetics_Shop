using CommunityToolkit.Mvvm.Input;
using Cosmetics_Shop.Models;
using Cosmetics_Shop.Models.DataService;
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


namespace Cosmetics_Shop.ViewModels.PageViewModels
{

    public class ProductDetailViewModel : INotifyPropertyChanged
    {
        private ProductDetail _productDetail;
        private int _shippingFee;
        private ShippingMethod _currentShippingMethod;
        public ObservableCollection<ReviewThumbnailViewModel> reviewThumbnail { get; set; }
        public ObservableCollection<CartThumbnailViewModel> Cart { get; set; }

        private IDao _dao = null;

        //Command
        public ICommand PaidButtonCommand { get; set; }
        public ICommand GoBackCommand { get; set; }

        // Navigation service
        private readonly INavigationService _navigationService;
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
        public ProductDetailViewModel(INavigationService navigationService, IDao dao)
        {
            _dao = new MockDao();

            _navigationService = navigationService;
            PaidButtonCommand = new RelayCommand(() =>
            {

                _navigationService.NavigateTo<PaymentPage>();
            });
            GoBackCommand = new RelayCommand(() =>
            {
                _navigationService.GoBack();
            });
        }

        public void LoadInitialReviews(int idProduct)
        {
            reviewThumbnail = new ObservableCollection<ReviewThumbnailViewModel>();
            var review = _dao.GetListReviewThumbnailByIDProduct(idProduct);
            foreach (var item in review)
            {
                var reviewThumbnailViewModel = App.ServiceProvider.GetService(typeof(ReviewThumbnailViewModel)) as ReviewThumbnailViewModel;
                reviewThumbnailViewModel.ReviewThumbnail = item; // Assuming ReviewThumbnail is a property
                reviewThumbnail.Add(reviewThumbnailViewModel);
            }
        }

        public void ShowAllReviews()
        {
            // Fetch all reviews for the product
            var allReviews = _dao.GetListReviewThumbnailByIDProduct(ProductDetail.Id);

            // Update the reviewThumbnail collection and notify the change
            reviewThumbnail.Clear(); // Clear existing reviews
            foreach (var review in allReviews)
            {
                var reviewThumbnailViewModel = App.ServiceProvider.GetService(typeof(ReviewThumbnailViewModel)) as ReviewThumbnailViewModel;
                reviewThumbnailViewModel.ReviewThumbnail = review;
                reviewThumbnail.Add(reviewThumbnailViewModel); // Add all reviews
            }
            OnPropertyChanged(nameof(reviewThumbnail)); // Notify that the reviewThumbnail has changed
        }

        public void FilterReviewsByStarNumber(int starNumber)
        {
            // Fetch all reviews for the product
            var allReviews = _dao.GetListReviewThumbnailByIDProduct(ProductDetail.Id);

            // Filter reviews based on the selected star number
            var filteredReviews = allReviews
                .Where(review => review.StarNumber == starNumber)
                .Select(review =>
                {
                    var reviewThumbnailViewModel = App.ServiceProvider.GetService(typeof(ReviewThumbnailViewModel)) as ReviewThumbnailViewModel;
                    reviewThumbnailViewModel.ReviewThumbnail = review;
                    return reviewThumbnailViewModel;
                }).ToList(); // Convert to list to evaluate count

            // Update the reviewThumbnail collection and notify the change
            reviewThumbnail.Clear(); // Clear existing reviews
            foreach (var review in filteredReviews)
            {
                reviewThumbnail.Add(review); // Add filtered reviews
            }

            OnPropertyChanged(nameof(reviewThumbnail)); // Notify that the reviewThumbnail has changed
        }

        public void LoadProductDetail(int id)
        {
            //dao = new MockDao();
            ProductDetail = _dao.GetProductDetail(id);
        }
        public List<ShippingMethod> GetShippingMethods()
        {
            return _dao.GetShippingMethods(); // Assuming _dao is initialized correctly in the constructor
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void LoadListCart()
        {
            Cart = new ObservableCollection<CartThumbnailViewModel>();

            var cartProduct = _dao.GetListCartProduct();

            for (int i = 0; i < cartProduct.Count; i++)
            {
                var cartThumbnailViewModel = App.ServiceProvider.GetService(typeof(CartThumbnailViewModel));
                cartThumbnailViewModel.GetType().GetProperty("CartThumbnail").SetValue(cartThumbnailViewModel, cartProduct[i]);
                Cart.Add(cartThumbnailViewModel as CartThumbnailViewModel);
            }
        }

        public void AddNewProductToCart(CartThumbnail cart)
        {
            var cartThumbnailViewModel = App.ServiceProvider.GetService(typeof(CartThumbnailViewModel));
            cartThumbnailViewModel.GetType().GetProperty("CartThumbnail").SetValue(cartThumbnailViewModel, cart);
            Cart.Add(cartThumbnailViewModel as CartThumbnailViewModel);
        }



    }
}
