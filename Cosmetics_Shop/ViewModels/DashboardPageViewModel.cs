using Cosmetics_Shop.Models;
using Cosmetics_Shop.Models.DataService;
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
        public ObservableCollection<ProductThumbnailViewModel> BestSeller { get; set; } = new ObservableCollection<ProductThumbnailViewModel>();
        public ObservableCollection<ProductThumbnailViewModel> NewProducts { get; set; } = new ObservableCollection<ProductThumbnailViewModel>();

        private IDao dao = null;
        public DashboardPageViewModel()
        {
            dao = new MockDao();

            var bestSeller = dao.GetListBestSeller();
            var newProducts = dao.GetListNewProduct();

            for (int i = 0; i < bestSeller.Count; i++)
            {
                BestSeller.Add(new ProductThumbnailViewModel(bestSeller[i]));
            }

            for (int i = 0; i < newProducts.Count; i++)
            {
                NewProducts.Add(new ProductThumbnailViewModel(newProducts[i]));
            }
        }
    }
}
