using Cosmetics_Shop.Models;
<<<<<<< HEAD
using Cosmetics_Shop.Models.DataService;
using Cosmetics_Shop.Services;
=======
>>>>>>> ad4311b5a9c311e96d94838537af8decb763063a
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
<<<<<<< HEAD
        // Data access object
        private IDao _dao = null;

        // Observable Collection
        public ObservableCollection<CartThumbnailViewModel> Cart { get; set; }

        // Constructor
        public CartPageViewModel(INavigationService navigationService,
                                        IDao dao)
        {
            _dao = dao;

            Cart = new ObservableCollection<CartThumbnailViewModel>();

            var cartProduct = _dao.GetListCartProduct();

            for (int i = 0; i < cartProduct.Count; i++)
            {
                var cartThumbnailViewModel = App.ServiceProvider.GetService(typeof(CartThumbnailViewModel));
                cartThumbnailViewModel.GetType().GetProperty("CartThumbnail").SetValue(cartThumbnailViewModel, cartProduct[i]);
                Cart.Add(cartThumbnailViewModel as CartThumbnailViewModel);
            }
=======
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

>>>>>>> ad4311b5a9c311e96d94838537af8decb763063a
        }
    }
}