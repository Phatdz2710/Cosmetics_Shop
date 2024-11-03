using Cosmetics_Shop.Models;
using Cosmetics_Shop.Models.DataService;
using Cosmetics_Shop.Services;
using Cosmetics_Shop.ViewModels.UserControlViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.ViewModels.PageViewModels
{
    public class CartPageViewModel
    {
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
        }
    }
}