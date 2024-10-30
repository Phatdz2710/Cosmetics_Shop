using CommunityToolkit.Mvvm.Input;
using Cosmetics_Shop.Models;
using Cosmetics_Shop.Models.DataService;
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

namespace Cosmetics_Shop.ViewModels
{
    public class PurchasePageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<ProductThumbnailViewModel> ProductThumbnails { get; set; }
        public ObservableCollection<CheckboxBrandContent> Brands { get; set; }


        private IDao dao = null;
        private int pageIndex = 1;
        private int productPerPage = 15;
        private int totalPages = 0;
        private Visibility visiPrevious = Visibility.Visible;
        private Visibility visiNext = Visibility.Visible;
        private int brandSelectedIndex = 0;
        private string filterBrand = "";
        private string minPrice = "";
        private string maxPrice = "";


        public string Keyword { get; set; } = "";
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
        public ICommand NextPageCommand { get; }
        public ICommand PreviousPageCommand { get; }
        public ICommand CheckboxBrandCheckedCommand { get; }
        public ICommand FilterPriceCommand { get; }

        public PurchasePageViewModel()
        {
            dao = new MockDao();
            ProductThumbnails = new ObservableCollection<ProductThumbnailViewModel>();
            Brands = new ObservableCollection<CheckboxBrandContent>();

            NextPageCommand = new RelayCommand(() =>
            {
                PageIndex++;
                GetProductThumbnails();
            });

            PreviousPageCommand = new RelayCommand(() =>
            {
                PageIndex--;
                GetProductThumbnails();
            });

            CheckboxBrandCheckedCommand = new RelayCommand<int>((index) =>
            {
                this.filterBrand = this.Brands[index].Name;
                PageIndex = 1;
                if (this.Brands[index].IsChecked)
                {
                    this.filterBrand = string.Empty;
                    GetProductThumbnails();
                }
                else
                {
                    GetProductThumbnails(isBrandFilter: true);
                }
            });

            FilterPriceCommand = new RelayCommand(() =>
            {
                PageIndex = 1;
                GetProductThumbnails();
            });

            GetProductThumbnails();
        }


        private void GetProductThumbnails(bool isBrandFilter = false)
        {
            ProductQueryResult productQueryResult = dao.GetListProductThumbnail(
                keyword:Keyword, 
                pageIndex:PageIndex, 
                productsPerPage:ProductsPerPage, 
                filterBrand: this.filterBrand,
                minPrice: int.Parse(MinPrice),
                maxPrice: int.Parse(MaxPrice));

            TotalPages = productQueryResult.TotalPages;

            VisiNext = PageIndex == totalPages ? Visibility.Collapsed : Visibility.Visible;
            VisiPrevious = PageIndex == 1 ? Visibility.Collapsed : Visibility.Visible;

            ProductThumbnails?.Clear();
            
            for (int i = 0; i < productQueryResult.Products.Count; i++)
            {
                ProductThumbnails.Add(new ProductThumbnailViewModel(productQueryResult.Products[i]));
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

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SearchProduct()
        {
            PageIndex = 1;
            this.filterBrand = "";
            GetProductThumbnails();
        }
    }
}
