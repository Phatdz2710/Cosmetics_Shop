using CommunityToolkit.Mvvm.Input;
using Cosmetics_Shop.Models;
using Cosmetics_Shop.Models.DataService;
using Cosmetics_Shop.Services;
using Cosmetics_Shop.Services.Interfaces;
using Cosmetics_Shop.ViewModels.UserControlViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cosmetics_Shop.ViewModels.PageViewModels
{
    public class DashboardPageViewModel
    {
        // Data access object
        private IDao _dao = null;

        // Event Aggregator
        private IEventAggregator _eventAggregator = null;

        // Observable Collection
        public ObservableCollection<ProductThumbnailViewModel> BestSeller { get; set; } = new ObservableCollection<ProductThumbnailViewModel>();
        public ObservableCollection<ProductThumbnailViewModel> NewProducts { get; set; } = new ObservableCollection<ProductThumbnailViewModel>();
        public ObservableCollection<ProductThumbnailViewModel> RecentlyView { get; set; } = new ObservableCollection<ProductThumbnailViewModel>();

        // Constructor
        public DashboardPageViewModel(INavigationService navigationService,
                                        IEventAggregator eventAggregator,
                                        IDao dao)
        {
            _dao = dao;
            _eventAggregator = eventAggregator;

            _eventAggregator.Subscribe<ReloadDashboardRequire>((searchEvent) =>
            {
                LoadDashboardData();
            });

            LoadDashboardData();
        }

        private async void LoadDashboardData()
        {
            var bestSeller = await _dao.GetListBestSellerAsync();
            var newProducts = await _dao.GetListNewProductAsync();
            var recentlyView = await _dao.GetListRecentlyViewAsync();

            BestSeller.Clear();
            NewProducts.Clear();
            RecentlyView.Clear();

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

            for (int i = 0; i < recentlyView.Count; i++)
            {
                var productThumbnailViewModel = App.ServiceProvider.GetService(typeof(ProductThumbnailViewModel));
                productThumbnailViewModel.GetType().GetProperty("ProductThumbnail").SetValue(productThumbnailViewModel, recentlyView[i]);
                RecentlyView.Add(productThumbnailViewModel as ProductThumbnailViewModel);
            }
        }
    }
}
