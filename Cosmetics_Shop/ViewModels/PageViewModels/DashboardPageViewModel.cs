using CommunityToolkit.Mvvm.Input;
using Cosmetics_Shop.DataAccessObject.Interfaces;
using Cosmetics_Shop.Models;
using Cosmetics_Shop.Services;
using Cosmetics_Shop.Services.EventAggregatorMessages;
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
    /// <summary>
    /// View model for DashboardPage
    /// </summary>
    public class DashboardPageViewModel
    {
        #region Singleton
        // Data access object
        private IDao _dao = null;

        // Event Aggregator
        private IEventAggregator _eventAggregator = null;

        // Service provider
        private IServiceProvider _serviceProvider = null;
        #endregion

        #region Properties for binding
        // Observable Collection
        public ObservableCollection<ProductThumbnailViewModel> BestSeller   { get; set; } = new ObservableCollection<ProductThumbnailViewModel>();
        public ObservableCollection<ProductThumbnailViewModel> NewProducts  { get; set; } = new ObservableCollection<ProductThumbnailViewModel>();
        public ObservableCollection<ProductThumbnailViewModel> RecentlyView { get; set; } = new ObservableCollection<ProductThumbnailViewModel>();
        #endregion

        // Constructor
        public DashboardPageViewModel(IEventAggregator      eventAggregator,
                                      IDao                  dao,
                                      IServiceProvider      serviceProvider)
        {
            _dao                = dao;
            _eventAggregator    = eventAggregator;
            _serviceProvider    = serviceProvider;

            _eventAggregator.Subscribe<ReloadDashboardRequire>((searchEvent) =>
            {
                LoadDashboardData();
            });

            LoadDashboardData();
        }


        /// <summary>
        /// Load data for dashboard from database
        /// </summary>
        private async void LoadDashboardData()
        {
            var bestSeller      = await _dao.GetListBestSellerAsync();
            var newProducts     = await _dao.GetListNewProductAsync();
            var recentlyView    = await _dao.GetListRecentlyViewAsync();

            BestSeller.Clear();
            NewProducts.Clear();
            RecentlyView.Clear();

            for (int i = 0; i < bestSeller.Count; i++)
            {
                var productThumbnailViewModel = _serviceProvider.GetService(typeof(ProductThumbnailViewModel));
                productThumbnailViewModel.GetType().GetProperty("ProductThumbnail").SetValue(productThumbnailViewModel, bestSeller[i]);
                BestSeller.Add(productThumbnailViewModel as ProductThumbnailViewModel);
            }

            for (int i = 0; i < newProducts.Count; i++)
            {
                var productThumbnailViewModel = _serviceProvider.GetService(typeof(ProductThumbnailViewModel));
                productThumbnailViewModel.GetType().GetProperty("ProductThumbnail").SetValue(productThumbnailViewModel, newProducts[i]);
                NewProducts.Add(productThumbnailViewModel as ProductThumbnailViewModel);
            }

            for (int i = 0; i < recentlyView.Count; i++)
            {
                var productThumbnailViewModel = _serviceProvider.GetService(typeof(ProductThumbnailViewModel));
                productThumbnailViewModel.GetType().GetProperty("ProductThumbnail").SetValue(productThumbnailViewModel, recentlyView[i]);
                RecentlyView.Add(productThumbnailViewModel as ProductThumbnailViewModel);
            }
        }
    }
}
