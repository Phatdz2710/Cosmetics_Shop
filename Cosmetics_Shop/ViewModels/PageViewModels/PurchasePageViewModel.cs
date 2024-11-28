using CommunityToolkit.Mvvm.Input;
using Cosmetics_Shop.Models;
using Cosmetics_Shop.Models.DataService;
using Cosmetics_Shop.Models.Enums;
using Cosmetics_Shop.Services;
using Cosmetics_Shop.Services.EventAggregatorMessages;
using Cosmetics_Shop.Services.Interfaces;
using Cosmetics_Shop.ViewModels.UserControlViewModels;
using Cosmetics_Shop.Views.Objects;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
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
        public ObservableCollection<FilterCheckbox> Brands { get; set; }
        public ObservableCollection<FilterCheckbox> Categories { get; set; }

        private ProductThumbnailViewModel lazyLoading = null;

        #region Fields
        private string _keyword = "";
        private int _totalProducts = 0;
        private bool _filterBrand = false;
        private bool _filterCategory = false;
        private int pageIndex = 1;
        private int productPerPage = 30;
        private int totalPages = 1;
        private bool visiPrevious = true;
        private bool visiNext = true;
        private string filterBrand = "";
        private string filterCategory = "";
        private string minPrice = "";
        private string maxPrice = "";
        private int selectedIndexSort = 0;
        private bool isLoading = false;
        #endregion

        #region Binding Properties
        public string Keyword
        {
            get => _keyword;
            set
            {
                _keyword = value;
                OnPropertyChanged(nameof(Keyword));
            }
        }
        public int TotalProducts
        {
            get => _totalProducts;
            set
            {
                _totalProducts = value;
                OnPropertyChanged(nameof(TotalProducts));
            }
        }
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
            get => minPrice;
            set
            {
                minPrice = value;
                OnPropertyChanged(nameof(MinPrice));
            }
        }
        public string MaxPrice
        {
            get => maxPrice;
            set
            {
                maxPrice = value;
                OnPropertyChanged(nameof(MaxPrice));
            }
        }
        public bool VisiPrevious
        {
            get => visiPrevious;
            set
            {
                visiPrevious = value;
                OnPropertyChanged(nameof(VisiPrevious));
            }
        }
        public bool VisiNext
        {
            get => visiNext;
            set
            {
                visiNext = value;
                OnPropertyChanged(nameof(VisiNext));
            }
        }
        public int SelectedIndexSort
        {
            get => selectedIndexSort;
            set
            {
                selectedIndexSort = value;
                SearchProduct(searchNewEntire: false);
                OnPropertyChanged(nameof(SelectedIndexSort));
            }
        }
        public bool IsLoading
        {
            get => isLoading;
            set
            {
                isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        #endregion

        // Commands
        public ICommand NextPageCommand { get; }
        public ICommand PreviousPageCommand { get; }
        public ICommand CheckboxBrandCheckedCommand { get; }
        public ICommand CheckboxCategoryCheckedCommand { get; }
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
            Brands = new ObservableCollection<FilterCheckbox>();
            Categories = new ObservableCollection<FilterCheckbox>();

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
                if (Brands[index].IsChecked)
                {
                    filterBrand = "";
                    _filterBrand = false;
                }
                else
                {
                    filterBrand = Brands[index].Name;
                    _filterBrand = true;
                }

                PageIndex = 1;
                if (_filterCategory)
                {
                    await GetProductThumbnails(updateCategory: false);
                }
                else
                {
                    await GetProductThumbnails();
                }
            });

            CheckboxCategoryCheckedCommand = new RelayCommand<int>(async (index) =>
            {
                if (Categories[index].IsChecked)
                {
                    // Clear filter
                    filterCategory = "";
                    _filterCategory = false;
                }
                else
                {
                    filterCategory = Categories[index].Name;
                    _filterCategory = true;
                }

                PageIndex = 1;
                if (_filterBrand)
                {
                    await GetProductThumbnails(updateBrand: false);
                }
                else
                {
                    await GetProductThumbnails();
                }
            });

            FilterPriceCommand = new RelayCommand(() =>
            {
                SearchProduct(searchNewEntire: false);
            });

            SearchProduct();
        }

        private async Task GetProductThumbnails(bool updateBrand = true, bool updateCategory = true)
        {
            int minPc, maxPc;
            try
            {
                minPc = string.IsNullOrEmpty(minPrice) ? 0 : int.Parse(minPrice);
                maxPc = string.IsNullOrEmpty(maxPrice) ? int.MaxValue : int.Parse(maxPrice);

                // Check valid minPrice
                if (minPc < 0 || minPc > int.MaxValue)
                {
                    ShowDialog("Giá thấp nhất không hợp lệ");
                    MinPrice = "";
                    MaxPrice = "";
                    minPc = 0;
                    maxPc = int.MaxValue;
                }

                // Check valid maxPrice
                if (maxPc < 0 || maxPc > int.MaxValue)
                {
                    ShowDialog("Giá cao nhất không hợp lệ");
                    MinPrice = "";
                    MaxPrice = "";
                    minPc = 0;
                    maxPc = int.MaxValue;
                }

                // Check if minPc is bigger than maxPc
                if (minPc > maxPc)
                {
                    ShowDialog("Giá thấp nhất lớn hơn giá cao nhất");
                    MinPrice = "";
                    MaxPrice = "";
                    minPc = 0;
                    maxPc = int.MaxValue;
                }
            }
            // If minPrice or maxPrice is less than int.MinValue or bigger than int.MaxValue
            catch (OverflowException)
            {
                ShowDialog("Giá trị không hợp lệ");
                MinPrice = "";
                MaxPrice = "";
                minPc = 0;
                maxPc = int.MaxValue;
            }

            // Update group of Product thumbnails
            ProductThumbnails?.Clear();
            IsLoading = true;

            // Get list of product thumbnails with keyword, page index, products per page, filter brand, min price, max price
            SearchResult productQueryResult = await _dao.GetListProductThumbnailAsync(
                        keyword: Keyword,
                        pageIndex: PageIndex,
                        productsPerPage: this.productPerPage,
                        sortProduct: GetSortProductChoice(),
                        filterBrand: filterBrand,
                        filterCategory: filterCategory,
                        minPrice: minPc,
                        maxPrice: maxPc);

            // Update total pages
            TotalPages = productQueryResult.TotalPages;
            TotalProducts = productQueryResult.TotalProducts;

            // Check if total products equal zero (cannot find any product)
            if (TotalProducts == 0)
            {
                ShowDialog("Không tìm thấy bất kỳ sản phẩm nào");
            }

            // Show or hide button next/previous page
            VisiNext = PageIndex != totalPages;
            VisiPrevious = PageIndex != 1;

            for (int i = 0; i < productQueryResult.Products.Count; i++)
            {
                var productThumbnailViewModel = App.ServiceProvider.GetService(typeof(ProductThumbnailViewModel));
                productThumbnailViewModel.GetType().GetProperty("ProductThumbnail").SetValue(productThumbnailViewModel, productQueryResult.Products[i]);
                ProductThumbnails.Add(productThumbnailViewModel as ProductThumbnailViewModel);
            }

            if (updateBrand)
            {
                // Update list brand for filter
                Brands?.Clear();
                for (int i = 0; i < productQueryResult.Brands.Count; i++)
                {
                    Brands.Add(new FilterCheckbox()
                    {
                        Name = productQueryResult.Brands[i],
                        Index = i,
                        CheckedCommand = CheckboxBrandCheckedCommand,
                        IsChecked = _filterBrand
                    });
                }
            }

            if (updateCategory)
            {
                // Update list category for filter
                Categories?.Clear();
                for (int i = 0; i < productQueryResult.Categories.Count; i++)
                {
                    Categories.Add(new FilterCheckbox()
                    {
                        Name = productQueryResult.Categories[i],
                        Index = i,
                        CheckedCommand = CheckboxCategoryCheckedCommand,
                        IsChecked = _filterCategory
                    });
                }
            }

            IsLoading = false;
        }

        // Sort product change
        public SortProduct GetSortProductChoice()
        {
            switch (selectedIndexSort)
            {
                case 0:
                    return SortProduct.DateAscending;
                case 1:
                    return SortProduct.PriceDescending;
                case 2:
                    return SortProduct.PriceAscending;
                case 3:
                    return SortProduct.NameAscending;
                default:
                    return SortProduct.DateAscending;
            }
        }

        // Search product
        public async void SearchProduct(bool searchNewEntire = true)
        {
            if (searchNewEntire)
            {
                PageIndex = 1;
                filterBrand = "";
                filterCategory = "";
                _filterCategory = false;
                _filterBrand = false;
            }
            else
            {
                PageIndex = 1;
            }

            await GetProductThumbnails();

        }


        // For INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Require View layer show a ContentDialog with message
        private void ShowDialog(string message)
        {
            var _message = new InvalidPriceMinMaxMessageBox()
            {
                Message = message
            };

            _eventAggregator.Publish(_message);
        }
    }
}
