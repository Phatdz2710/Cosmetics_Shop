using Cosmetics_Shop.Models;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Cosmetics_Shop.Models.DataService;

namespace Cosmetics_Shop.ViewModels
{
    public class ProductDetailViewModel : INotifyPropertyChanged
    {
        private ProductDetail _productDetail;
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void LoadProductDetail(int id)
        {
            ProductDetail = dao.GetProductDetail(id);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
