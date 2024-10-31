using Cosmetics_Shop.Models;
using Cosmetics_Shop.Models.DataService;
using Cosmetics_Shop.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.ViewModels
{
    public class DashboardPageViewModel
    {
        // Data access object
        private IDao _dao = null;

        // Observable Collection
        public ObservableCollection<ProductThumbnailViewModel> BestSeller { get; set; } 
        public ObservableCollection<ProductThumbnailViewModel> NewProducts { get; set; } 


        // Constructor
        public DashboardPageViewModel(INavigationService navigationService,
                                        IDao dao)
        {
            _dao = dao;

            BestSeller = new ObservableCollection<ProductThumbnailViewModel>();
            NewProducts = new ObservableCollection<ProductThumbnailViewModel>();

            var bestSeller = _dao.GetListBestSeller();
            var newProducts = _dao.GetListNewProduct();

            for (int i = 0; i < bestSeller.Count; i++)
            {
                var productThumbnailViewModel = App.ServiceProvider.GetService(typeof(ProductThumbnailViewModel));
                productThumbnailViewModel.GetType().GetProperty("ProductThumbnail").SetValue(productThumbnailViewModel, bestSeller[i]);
                BestSeller.Add(productThumbnailViewModel as ProductThumbnailViewModel);
            }

            for (int i = 0; i < newProducts.Count; i++)
            {
                var productThumbnailViewModel = App.ServiceProvider.GetService(typeof(ProductThumbnailViewModel));
                productThumbnailViewModel.GetType().GetProperty("ProductThumbnail").SetValue(productThumbnailViewModel, newProducts[i]);
                NewProducts.Add(productThumbnailViewModel as ProductThumbnailViewModel);
            }
        }
    }
}
