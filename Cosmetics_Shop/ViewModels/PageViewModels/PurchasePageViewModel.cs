using CommunityToolkit.Mvvm.Input;
using Cosmetics_Shop.Models;
using Cosmetics_Shop.Models.DataService;
using Cosmetics_Shop.Models.Enums;
using Cosmetics_Shop.Services;
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

        private string _keyword = "";
        private int _totalProducts = 0;

        // Observable Collection
        public ObservableCollection<ProductThumbnailViewModel> ProductThumbnails { get; set; }
        public ObservableCollection<CheckboxBrandContent> Brands { get; set; }

        #region Fields
        private int pageIndex = 1;
        private int productPerPage = 20;
        private int totalPages = 0;
        private Visibility visiPrevious = Visibility.Visible;
        private Visibility visiNext = Visibility.Visible;
        private int brandSelectedIndex = 0;
        private string filterBrand = "";
        private string minPrice = "";
        private string maxPrice = "";
        private int selectedIndexSort = 0;
        #endregion

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
        public int SelectedIndexSort
        {
            get => selectedIndexSort;
            set
            {
                selectedIndexSort = value;
                OnPropertyChanged(nameof(SelectedIndexSort));
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

            // Get list of product thumbnails with keyword, page index, products per page, filter brand, min price, max price
            ProductQueryResult productQueryResult = await _dao.GetListProductThumbnailAsync(
                keyword: Keyword,
                pageIndex: PageIndex,
                productsPerPage: this.productPerPage,
                sortProduct: GetSortProductChoice(),
                filterBrand: filterBrand,
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
            VisiNext = PageIndex == totalPages ? Visibility.Collapsed : Visibility.Visible;
            VisiPrevious = PageIndex == 1 ? Visibility.Collapsed : Visibility.Visible;

            // Update group of Product thumbnails
            ProductThumbnails?.Clear();

            for (int i = 0; i < productQueryResult.Products.Count; i++)
            {
                var productThumbnailViewModel = App.ServiceProvider.GetService(typeof(ProductThumbnailViewModel));
                productThumbnailViewModel.GetType().GetProperty("ProductThumbnail").SetValue(productThumbnailViewModel, productQueryResult.Products[i]);
                ProductThumbnails.Add(productThumbnailViewModel as ProductThumbnailViewModel);
            }

            // Update list brand for filter
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
        public async void SearchProduct()
        {
            PageIndex = 1;
            filterBrand = "";
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
