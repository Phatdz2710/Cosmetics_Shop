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
        public ObservableCollection<string> Brands { get; set; }

        private IDao dao = null;
        
        private string keyword;
        private int pageIndex = 1;
        private int productPerPage = 15;
        private int totalPages = 0;
        private Visibility visiPrevious = Visibility.Visible;
        private Visibility visiNext = Visibility.Visible;
        private int brandSelectedIndex = 0;


        public string Keyword
        {
            get => keyword;
            set
            {
                keyword = value;
                OnPropertyChanged(nameof(Keyword));
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
        public int BrandSelectedIndex
        {
            get => brandSelectedIndex;
            set
            {
                brandSelectedIndex = value;
                OnPropertyChanged(nameof(BrandSelectedIndex));
            }
        }


        public ICommand NextPageCommand { get; }
        public ICommand PreviousPageCommand { get; }

        public PurchasePageViewModel()
        {
            dao = new MockDao();
            ProductThumbnails = new ObservableCollection<ProductThumbnailViewModel>();
            Brands = new ObservableCollection<string>();
            GetProductThumbnails();

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
        }

        private void GetProductThumbnails(bool isLoadNew = false)
        {
            ProductQueryResult productQueryResult = dao.GetListProductThumbnail(Keyword, PageIndex, ProductsPerPage);
            TotalPages = productQueryResult.TotalPages;

            VisiNext = PageIndex == totalPages ? Visibility.Collapsed : Visibility.Visible;
            VisiPrevious = PageIndex == 1 ? Visibility.Collapsed : Visibility.Visible;

            ProductThumbnails?.Clear();
            
            
            for (int i = 0; i < productQueryResult.Products.Count; i++)
            {
                ProductThumbnails.Add(new ProductThumbnailViewModel(productQueryResult.Products[i]));
            }

            if (isLoadNew)
            {
                Brands?.Clear();
                for (int i = 0; i < productQueryResult.Brands.Count; i++)
                {
                    Brands.Add(productQueryResult.Brands[i]);
                }
            } 
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SearchProduct()
        {
            PageIndex = 1;
            GetProductThumbnails(isLoadNew: true);
            BrandSelectedIndex = 0;
        }
    }
}
