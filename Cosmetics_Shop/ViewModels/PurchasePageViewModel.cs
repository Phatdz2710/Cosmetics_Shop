using Cosmetics_Shop.Models;
using Cosmetics_Shop.Models.DataService;
using Cosmetics_Shop.Views.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.ViewModels
{
    public class PurchasePageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<ProductThumbnailViewModel> ProductThumbnails { get; set; }

        private IDao dao = null;

        public string Keyword { get; set; } = "";

        public PurchasePageViewModel()
        {
            dao = new MockDao();
            ProductThumbnails = new ObservableCollection<ProductThumbnailViewModel>();
            GetAllProductThumbnail();
        }

        private void GetAllProductThumbnail()
        {
            var (productThumbnails, totalItems) = dao.GetListProductThumbnail(Keyword);
            ProductThumbnails?.Clear();
            
            for (int i = 0; i < productThumbnails.Count; i++)
            {
                ProductThumbnails.Add(new ProductThumbnailViewModel(productThumbnails[i]));
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SearchProduct()
        {
            GetAllProductThumbnail();
        }
    }
}
