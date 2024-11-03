using CommunityToolkit.Mvvm.Input;
using Cosmetics_Shop.Models;
using Cosmetics_Shop.Models.DataService;
using Cosmetics_Shop.Services;
using Cosmetics_Shop.Views.Objects;
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


namespace Cosmetics_Shop.ViewModels.PageViewModels
{

    public class ProductDetailViewModel : INotifyPropertyChanged
    {
        private ProductDetail _productDetail;
        public ObservableCollection<ReviewThumbnailViewModel> reviewThumbnail { get; set; }
        private IDao dao = null;
        public ProductDetail ProductDetail
        {
            get => _productDetail;
            set
            {
                _productDetail = value;
                OnPropertyChanged(nameof(ProductDetail));
            }
        }

        public ProductDetailViewModel()
        {
            dao = new MockDao();
        }

        public void LoadInitialReviews(int idProduct)
        {
            reviewThumbnail = new ObservableCollection<ReviewThumbnailViewModel>();
            //int idProduct = 1; // This could be parameterized
            var review = dao.GetListReviewThumbnailByIDProduct(idProduct);
            foreach (var item in review)
            {
                var reviewThumbnailViewModel = App.ServiceProvider.GetService(typeof(ReviewThumbnailViewModel)) as ReviewThumbnailViewModel;
                reviewThumbnailViewModel.ReviewThumbnail = item; // Assuming ReviewThumbnail is a property
                reviewThumbnail.Add(reviewThumbnailViewModel);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void LoadProductDetail(int id)
        {
            //dao = new MockDao();
            ProductDetail = dao.GetProductDetail(id);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
