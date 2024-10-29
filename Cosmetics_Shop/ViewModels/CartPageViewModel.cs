using Cosmetics_Shop.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.ViewModels
{
    public class CartPageViewModel
    {
        public ObservableCollection<CartThumbnailViewModel> Cart { get; set; } = new ObservableCollection<CartThumbnailViewModel>()
        {
            new CartThumbnailViewModel(new CartThumbnail("Loreal Official Store", 1, null, "Tẩy trang loreal", "Tươi mát", 150000, 2, 300000)),
            new CartThumbnailViewModel(new CartThumbnail("Loreal Official Store", 1, null, "Tẩy trang loreal", "Sạch sâu", 150000, 2, 300000)),
            new CartThumbnailViewModel(new CartThumbnail("Bioderma Official Store", 1, null, "Tẩy trang Bioderma", "Tươi mát", 150000, 1, 150000)),
            new CartThumbnailViewModel(new CartThumbnail("Bioderma Official Store", 1, null, "Tẩy trang Bioderma", "Sạch sâu", 150000, 2, 300000)),
            new CartThumbnailViewModel(new CartThumbnail("Ganier Official Store", 1, null, "Tẩy trang Ganier", "BHA", 130000, 1, 130000)),
        };

        public CartPageViewModel()
        {

        }
    }
}