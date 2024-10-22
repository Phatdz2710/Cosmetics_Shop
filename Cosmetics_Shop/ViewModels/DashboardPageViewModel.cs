using Cosmetics_Shop.Models;
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
        public ObservableCollection<ProductThumbnailViewModel> BestSeller { get; set; } = new ObservableCollection<ProductThumbnailViewModel>()
        {
            new ProductThumbnailViewModel(new ProductThumbnail(1, "Sieu cap san pham my pham provip cua fat", null, 10000000)),
                new ProductThumbnailViewModel(new ProductThumbnail(2, "Product 2", null, 2000000)),
                new ProductThumbnailViewModel(new ProductThumbnail(3, "Product 3", null, 3000000)),
                new ProductThumbnailViewModel(new ProductThumbnail(1, "Sieu cap san pham my pham provip cua fat", null, 10000000)),
                new ProductThumbnailViewModel(new ProductThumbnail(2, "Product 2", null, 2000000)),
                new ProductThumbnailViewModel(new ProductThumbnail(3, "Product 3", null, 3000000)),
                new ProductThumbnailViewModel(new ProductThumbnail(1, "Sieu cap san pham my pham provip cua fat", null, 10000000)),
                new ProductThumbnailViewModel(new ProductThumbnail(2, "Product 2", null, 2000000)),
                new ProductThumbnailViewModel(new ProductThumbnail(3, "Product 3", null, 3000000)),
                new ProductThumbnailViewModel(new ProductThumbnail(1, "Sieu cap san pham my pham provip cua fat", null, 10000000)),
                new ProductThumbnailViewModel(new ProductThumbnail(2, "Product 2", null, 2000000)),
                new ProductThumbnailViewModel(new ProductThumbnail(3, "Product 3", null, 3000000)),
                new ProductThumbnailViewModel(new ProductThumbnail(1, "Sieu cap san pham my pham provip cua fat", null, 10000000)),
                new ProductThumbnailViewModel(new ProductThumbnail(2, "Product 2", null, 2000000)),
                new ProductThumbnailViewModel(new ProductThumbnail(3, "Product 3", null, 3000000)),
        };

        public ObservableCollection<ProductThumbnailViewModel> NewProducts { get; set; } = new ObservableCollection<ProductThumbnailViewModel>() 
        {
            new ProductThumbnailViewModel(new ProductThumbnail(1, "Sieu cap san pham my pham provip cua fat", null, 10000000)),
                new ProductThumbnailViewModel(new ProductThumbnail(2, "Product 2", null, 2000000)),
                new ProductThumbnailViewModel(new ProductThumbnail(3, "Product 3", null, 3000000)),
                new ProductThumbnailViewModel(new ProductThumbnail(1, "Sieu cap san pham my pham provip cua fat", null, 10000000)),
                new ProductThumbnailViewModel(new ProductThumbnail(2, "Product 2", null, 2000000)),
                new ProductThumbnailViewModel(new ProductThumbnail(3, "Product 3", null, 3000000)),
                new ProductThumbnailViewModel(new ProductThumbnail(1, "Sieu cap san pham my pham provip cua fat", null, 10000000)),
                new ProductThumbnailViewModel(new ProductThumbnail(2, "Product 2", null, 2000000)),
                new ProductThumbnailViewModel(new ProductThumbnail(3, "Product 3", null, 3000000)),
                new ProductThumbnailViewModel(new ProductThumbnail(1, "Sieu cap san pham my pham provip cua fat", null, 10000000)),
                new ProductThumbnailViewModel(new ProductThumbnail(2, "Product 2", null, 2000000)),
                new ProductThumbnailViewModel(new ProductThumbnail(3, "Product 3", null, 3000000)),
                new ProductThumbnailViewModel(new ProductThumbnail(1, "Sieu cap san pham my pham provip cua fat", null, 10000000)),
                new ProductThumbnailViewModel(new ProductThumbnail(2, "Product 2", null, 2000000)),
                new ProductThumbnailViewModel(new ProductThumbnail(3, "Product 3", null, 3000000)),
        };
        public DashboardPageViewModel()
        {

        }
    }
}
