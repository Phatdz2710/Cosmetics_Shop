using CommunityToolkit.Mvvm.Input;
using Cosmetics_Shop.DataAccessObject.Data;
using Cosmetics_Shop.DataAccessObject.Interfaces;
using Cosmetics_Shop.Models;
using Cosmetics_Shop.Models.Enums;
using Cosmetics_Shop.Services;
using Cosmetics_Shop.Services.EventAggregatorMessages;
using Cosmetics_Shop.Services.Interfaces;
using Cosmetics_Shop.ViewModels.UserControlViewModels;
using Cosmetics_Shop.Views.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Graphics.Printing;

namespace Cosmetics_Shop.ViewModels.PageViewModels
{
    /// <summary>
    /// View model for PurchasePage
    /// </summary>
    public class PurchasePageViewModel : INotifyPropertyChanged
    {
        #region Singleton
        // Event aggregator for publish and subscribe
        private readonly IEventAggregator   _eventAggregator    = null;

        // Navigation service
        private readonly INavigationService _navigationService  = null;

        // Data access object
        private readonly IDao _dao = null;

        // Service provider
        private readonly IServiceProvider _serviceProvider = null;
        #endregion

        #region ObservableCollections

        /// <summary>
        /// Observable collection to hold product thumbnails for display.
        /// </summary>
        public ObservableCollection<ProductThumbnailViewModel> ProductThumbnails { get; set; }

        /// <summary>
        /// Observable collection for storing filter checkboxes for brand options.
        /// </summary>
        public ObservableCollection<FilterCheckbox> Brands { get; set; }

        /// <summary>
        /// Observable collection for storing filter checkboxes for category options.
        /// </summary>
        public ObservableCollection<FilterCheckbox> Categories { get; set; }

        /// <summary>
        /// Lazy loading product thumbnail for asynchronous data fetching.
        /// </summary>
        private ProductThumbnailViewModel lazyLoading = null;

        #endregion

        #region Fields
        private string  _keyword         = "";
        private string  filterBrand      = "";
        private string  filterCategory   = "";
        private string  _minPrice        = "";
        private string  _maxPrice        = "";
        private bool    _filterBrand     = false;
        private bool    _filterCategory  = false;
        private bool    _isLoading       = false;
        private bool    _visiPrevious    = true;
        private bool    _visiNext        = true;
        private int     _pageIndex       = 1;
        private int     _productPerPage  = 30;
        private int     _totalPages      = 1;
        private int     _totalProducts   = 0;
        private int     _selectedIndexSort = 0;

        // Semaphore for thread synchronization
        private SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

        #endregion

        #region Binding Properties

        /// <summary>
        /// Gets or sets the search keyword for filtering products.
        /// Notifies the UI when the value changes.
        /// </summary>
        public string Keyword
        {
            get => _keyword;
            set
            {
                _keyword = value;
                OnPropertyChanged(nameof(Keyword));
            }
        }

        /// <summary>
        /// Gets or sets the total number of products.
        /// Notifies the UI when the value changes.
        /// </summary>
        public int TotalProducts
        {
            get => _totalProducts;
            set
            {
                _totalProducts = value;
                OnPropertyChanged(nameof(TotalProducts));
            }
        }

        /// <summary>
        /// Gets or sets the current page index for pagination.
        /// Notifies the UI when the value changes.
        /// </summary>
        public int PageIndex
        {
            get => _pageIndex;
            set
            {
                _pageIndex = value;
                OnPropertyChanged(nameof(PageIndex));
            }
        }

        /// <summary>
        /// Gets or sets the number of products displayed per page.
        /// Notifies the UI when the value changes.
        /// </summary>
        public int ProductsPerPage
        {
            get => _productPerPage;
            set
            {
                _productPerPage = value;
                OnPropertyChanged(nameof(ProductsPerPage));
            }
        }

        /// <summary>
        /// Gets or sets the total number of pages for pagination.
        /// Notifies the UI when the value changes.
        /// </summary>
        public int TotalPages
        {
            get => _totalPages;
            set
            {
                _totalPages = value;
                OnPropertyChanged(nameof(TotalPages));
            }
        }

        /// <summary>
        /// Gets or sets the minimum price for filtering products.
        /// Notifies the UI when the value changes.
        /// </summary>
        public string MinPrice
        {
            get => _minPrice;
            set
            {
                _minPrice = value;
                OnPropertyChanged(nameof(MinPrice));
            }
        }

        /// <summary>
        /// Gets or sets the maximum price for filtering products.
        /// Notifies the UI when the value changes.
        /// </summary>
        public string MaxPrice
        {
            get => _maxPrice;
            set
            {
                _maxPrice = value;
                OnPropertyChanged(nameof(MaxPrice));
            }
        }

        /// <summary>
        /// Gets or sets the visibility of the "Previous" button for pagination.
        /// Notifies the UI when the value changes.
        /// </summary>
        public bool VisiPrevious
        {
            get => _visiPrevious;
            set
            {
                _visiPrevious = value;
                OnPropertyChanged(nameof(VisiPrevious));
            }
        }

        /// <summary>
        /// Gets or sets the visibility of the "Next" button for pagination.
        /// Notifies the UI when the value changes.
        /// </summary>
        public bool VisiNext
        {
            get => _visiNext;
            set
            {
                _visiNext = value;
                OnPropertyChanged(nameof(VisiNext));
            }
        }

        /// <summary>
        /// Gets or sets the selected index for sorting the products.
        /// Notifies the UI when the value changes and triggers a product search.
        /// </summary>
        public int SelectedIndexSort
        {
            get => _selectedIndexSort;
            set
            {
                _selectedIndexSort = value;
                SearchProduct(searchNewEntire: false);
                OnPropertyChanged(nameof(SelectedIndexSort));
            }
        }

        /// <summary>
        /// Gets or sets the loading state.
        /// Notifies the UI when the value changes.
        /// </summary>
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Command to navigate to the next page of products.
        /// </summary>
        public ICommand NextPageCommand { get; }

        /// <summary>
        /// Command to navigate to the previous page of products.
        /// </summary>
        public ICommand PreviousPageCommand { get; }

        /// <summary>
        /// Command to handle the checkbox for brand selection.
        /// </summary>
        public ICommand CheckboxBrandCheckedCommand { get; }

        /// <summary>
        /// Command to handle the checkbox for category selection.
        /// </summary>
        public ICommand CheckboxCategoryCheckedCommand { get; }

        /// <summary>
        /// Command to apply the price filter.
        /// </summary>
        public ICommand FilterPriceCommand { get; }

        #endregion


        // Constructor
        public PurchasePageViewModel(INavigationService navigationService,
                                     IEventAggregator   eventAggregator,
                                     IDao               dao,
                                     IServiceProvider   serviceProvider)
        {
            _eventAggregator    = eventAggregator;
            _navigationService  = navigationService;
            _dao                = dao;
            _serviceProvider    = serviceProvider;

            SearchProduct();

            // Register event to listen from MainViewModel
            _eventAggregator.Subscribe<SearchEvent>((searchEvent) =>
            {
                Keyword = searchEvent.Keyword;
                SearchProduct();
            });

            // Initialize list of product thumbnails
            ProductThumbnails = new ObservableCollection<ProductThumbnailViewModel>();

            // Initialize list of brands and categories
            Brands      = new ObservableCollection<FilterCheckbox>();
            Categories  = new ObservableCollection<FilterCheckbox>();

            NextPageCommand     = new RelayCommand(nextPageCommand);
            PreviousPageCommand = new RelayCommand(previousPageCommand);

            CheckboxBrandCheckedCommand     = new RelayCommand<int>(checkboxBrandCheckedCommand);
            CheckboxCategoryCheckedCommand  = new RelayCommand<int>(checkboxCategoryCheckedCommand);
            FilterPriceCommand              = new RelayCommand(filterPriceCommand);
        }

        /// <summary>
        /// Moves to the next page and refreshes the product thumbnails.
        /// </summary>
        private async void nextPageCommand()
        {
            PageIndex++;
            await GetProductThumbnails();
        }


        /// <summary>
        /// Moves to the previous page and refreshes the product thumbnails.
        /// </summary>
        private async void previousPageCommand()
        {
            PageIndex--;
            await GetProductThumbnails();
        }


        /// <summary>
        /// Handles the checkbox change event for brands. Updates the filter and refreshes the product thumbnails.
        /// </summary>
        /// <param name="index">The index of the selected brand.</param>
        private async void checkboxBrandCheckedCommand(int index)
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
        }


        /// <summary>
        /// Handles the checkbox change event for categories. Updates the filter and refreshes the product thumbnails.
        /// </summary>
        /// <param name="index">The index of the selected category.</param>
        private async void checkboxCategoryCheckedCommand(int index)
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
        }


        /// <summary>
        /// Filter price command
        /// </summary>
        private void filterPriceCommand()
        {
            SearchProduct(searchNewEntire: false);
        }


        /// <summary>
        /// Get list of product thumbnails
        /// </summary>
        /// <param name="updateBrand"> Update new list of brands </param>
        /// <param name="updateCategory"> Update new list of categories </param>
        /// <returns></returns>
        private async Task GetProductThumbnails(bool updateBrand = true, bool updateCategory = true)
        {
            // Wait for other threads
            await semaphore.WaitAsync();
            try
            {
                int minPc, maxPc;
                try
                {
                    minPc = string.IsNullOrEmpty(_minPrice) ? 0 : int.Parse(_minPrice);
                    maxPc = string.IsNullOrEmpty(_maxPrice) ? int.MaxValue : int.Parse(_maxPrice);

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
                            productsPerPage: _productPerPage,
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
                VisiNext = PageIndex != _totalPages;
                VisiPrevious = PageIndex != 1;

                for (int i = 0; i < productQueryResult.Products.Count; i++)
                {
                    var productThumbnailViewModel = _serviceProvider.GetService(typeof(ProductThumbnailViewModel));
                    productThumbnailViewModel.GetType()
                        .GetProperty("ProductThumbnail")
                        .SetValue(productThumbnailViewModel, productQueryResult.Products[i]);

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
            finally
            {
                semaphore.Release();
            }
        }


        
        /// <summary>
        /// By selectedIndexSort to get type of SortProduct
        /// </summary>
        /// <returns>Type of SortProduct</returns>
        public SortProduct GetSortProductChoice()
        {
            switch (_selectedIndexSort)
            {
                case 0:
                    return SortProduct.DateAscending;
                case 1:
                    return SortProduct.PriceDescending;
                case 2:
                    return SortProduct.PriceAscending;
                case 3:
                    return SortProduct.NameAscending;
                case 4:
                    return SortProduct.NameDescending;
                default:
                    return SortProduct.DateAscending;
            }
        }



        /// <summary>
        /// For new search product with new keyword or new sorting 
        /// </summary>
        /// <param name="searchNewEntire">Refresh the whole</param>
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



        /// <summary>
        /// For show dialog when invalid price min max
        /// </summary>
        /// <param name="message">Message are send</param>
        private void ShowDialog(string message)
        {
            var _message = new InvalidPriceMinMaxMessageBox()
            {
                Message = message
            };

            _eventAggregator.Publish(_message);
        }




        // For INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
