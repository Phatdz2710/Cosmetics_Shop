using CommunityToolkit.Mvvm.Input;
using Cosmetics_Shop.Models;
using Cosmetics_Shop.Models.DataService;
using Cosmetics_Shop.Services;
using Cosmetics_Shop.Services.Interfaces;
using Cosmetics_Shop.ViewModels.UserControlViewModels;
using Cosmetics_Shop.Views.Objects;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Graphics.Printing;

namespace Cosmetics_Shop.ViewModels.PageViewModels
{
    /// <summary>
    /// View model for PurchasePage
    /// Singleton pattern
    /// </summary>
    public class PurchasePageViewModel : INotifyPropertyChanged
    {
        // Event aggregator for publish and subscribe
        private readonly IEventAggregator _eventAggregator;

        // Navigation service
        private readonly INavigationService _navigationService;

        // Data access object
        private IDao _dao = null;



        // Observable Collection
        public ObservableCollection<ProductThumbnailViewModel> ProductThumbnails { get; set; }
        public ObservableCollection<CheckboxBrandContent> Brands { get; set; }

        #region Fields
        private int pageIndex = 1;
        private int productPerPage = 15;
        private int totalPages = 0;
        private Visibility visiPrevious = Visibility.Visible;
        private Visibility visiNext = Visibility.Visible;
        private int brandSelectedIndex = 0;
        private string filterBrand = "";
        private string minPrice = "";
        private string maxPrice = "";
        #endregion

        public string Keyword { get; set; } = "";

        #region Binding Properties
        public int PageIndex
        {
            get => pageIndex;
            set
            {
                pageIndex = value;
                OnPropertyChanged(nameof(PageIndex));
            }
        }
        public int ProductsPerPage
        {
            get => productPerPage;
            set
            {
                productPerPage = value;
                OnPropertyChanged(nameof(ProductsPerPage));
            }
        }
        public int TotalPages
        {
            get => totalPages;
            set
            {
                totalPages = value;
                OnPropertyChanged(nameof(TotalPages));
            }
        }
        public string MinPrice
        {
            get
            {
                if (minPrice == string.Empty)
                {
                    return "0";
                }
                return minPrice;
            }
            set
            {
                minPrice = value;
                OnPropertyChanged(nameof(MinPrice));
            }
        }
        public string MaxPrice
        {
            get
            {
                if (maxPrice == string.Empty)
                {
                    return "99999999";
                }
                return maxPrice;
            }
            set
            {
                maxPrice = value;
                OnPropertyChanged(nameof(MaxPrice));
            }
        }
        public Visibility VisiPrevious
        {
            get => visiPrevious;
            set
            {
                visiPrevious = value;
                OnPropertyChanged(nameof(VisiPrevious));
            }
        }
        public Visibility VisiNext
        {
            get => visiNext;
            set
            {
                visiNext = value;
                OnPropertyChanged(nameof(VisiNext));
            }
        }
        #endregion

        // Commands
        public ICommand NextPageCommand { get; }
        public ICommand PreviousPageCommand { get; }
        public ICommand CheckboxBrandCheckedCommand { get; }
        public ICommand FilterPriceCommand { get; }


        // Constructor
        public PurchasePageViewModel(INavigationService navigationService,
                                        IEventAggregator eventAggregator,
                                        IDao dao)
        {
            _eventAggregator = eventAggregator;
            _navigationService = navigationService;
            _dao = dao;

            // Register event to listen from MainViewModel
            _eventAggregator.Subscribe<SearchEvent>((searchEvent) =>
            {
                Keyword = searchEvent.Keyword;
                SearchProduct();
            });

            // Initialize list of product thumbnails
            ProductThumbnails = new ObservableCollection<ProductThumbnailViewModel>();

            // Initialize list of brands
            Brands = new ObservableCollection<CheckboxBrandContent>();

            NextPageCommand = new RelayCommand(async () =>
            {
                PageIndex++;
                await GetProductThumbnails();
            });

            PreviousPageCommand = new RelayCommand(async () =>
            {
                PageIndex--;
                await GetProductThumbnails();
            });

            CheckboxBrandCheckedCommand = new RelayCommand<int>(async (index) =>
            {
                filterBrand = Brands[index].Name;
                PageIndex = 1;
                if (Brands[index].IsChecked)
                {
                    filterBrand = string.Empty;
                    await GetProductThumbnails();
                }
                else
                {
                    await GetProductThumbnails(isBrandFilter: true);
                }
            });

            FilterPriceCommand = new RelayCommand(async () =>
            {
                PageIndex = 1;
                await GetProductThumbnails();
            });

            FirstLoadProductThumbnails();
        }

        private async void FirstLoadProductThumbnails()
        {
            await GetProductThumbnails();
        }

        private async Task GetProductThumbnails(bool isBrandFilter = false)
        {
            ProductQueryResult productQueryResult = await _dao.GetListProductThumbnailAsync(
                keyword: Keyword,
                pageIndex: PageIndex,
                productsPerPage: ProductsPerPage,
                filterBrand: filterBrand,
                minPrice: int.Parse(MinPrice),
                maxPrice: int.Parse(MaxPrice));

            TotalPages = productQueryResult.TotalPages;

            VisiNext = PageIndex == totalPages ? Visibility.Collapsed : Visibility.Visible;
            VisiPrevious = PageIndex == 1 ? Visibility.Collapsed : Visibility.Visible;

            ProductThumbnails?.Clear();

            for (int i = 0; i < productQueryResult.Products.Count; i++)
            {
                var productThumbnailViewModel = App.ServiceProvider.GetService(typeof(ProductThumbnailViewModel));
                productThumbnailViewModel.GetType().GetProperty("ProductThumbnail").SetValue(productThumbnailViewModel, productQueryResult.Products[i]);
                ProductThumbnails.Add(productThumbnailViewModel as ProductThumbnailViewModel);
            }

            Brands?.Clear();
            for (int i = 0; i < productQueryResult.Brands.Count; i++)
            {
                Brands.Add(new CheckboxBrandContent()
                {
                    Name = productQueryResult.Brands[i],
                    Index = i,
                    CheckedCommand = CheckboxBrandCheckedCommand,
                    IsChecked = isBrandFilter
                });
            }
        }


        public void SearchProduct()
        {
            PageIndex = 1;
            filterBrand = "";
            GetProductThumbnails();
        }


        // For INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
