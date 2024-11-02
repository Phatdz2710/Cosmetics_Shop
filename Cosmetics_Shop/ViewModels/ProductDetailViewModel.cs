using Cosmetics_Shop.Models;
<<<<<<< HEAD
using Cosmetics_Shop.Models.DataService;
using Cosmetics_Shop.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
=======
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
>>>>>>> ad4311b5a9c311e96d94838537af8decb763063a
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.ViewModels
{
<<<<<<< HEAD
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

            //int idProduct = 1;
            //reviewThumbnail = new ObservableCollection<ReviewThumbnailViewModel>();
            //var review = dao.GetListReviewThumbnailByIDProduct(idProduct);
            //for (int i = 0; i < review.Count; i++)
            //{
            //    var reviewThumbnailViewModel = App.ServiceProvider.GetService(typeof(ReviewThumbnailViewModel));
            //    reviewThumbnailViewModel.GetType().GetProperty("ReviewThumbnail").SetValue(reviewThumbnailViewModel, review[i]);
            //    reviewThumbnail.Add(reviewThumbnailViewModel as ReviewThumbnailViewModel);
            //}
            //reviewThumbnail = new ObservableCollection<ReviewThumbnailViewModel>();
            //LoadInitialReviews();
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
=======
    public class ProductDetailViewModel
    {

>>>>>>> ad4311b5a9c311e96d94838537af8decb763063a
    }
}
