using CommunityToolkit.Mvvm.Input;
using Cosmetics_Shop.DataAccessObject.Interfaces;
using Cosmetics_Shop.Services.Interfaces;
using Cosmetics_Shop.ViewModels.UserControlViewModels;
using Cosmetics_Shop.Views.Pages;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Input;

namespace Cosmetics_Shop.ViewModels.AdminPageViewModels
{
    public class ProductManagerViewModel : INotifyPropertyChanged
    {
        #region Singleton
        // Data access object
        private readonly IDao _dao = null;
        // File picker service
        private readonly IFilePickerService _filePickerService = null;
        #endregion

        #region Fields
        private ObservableCollection<ProductCellViewModel> listProducts = new ObservableCollection<ProductCellViewModel>();

        private bool    _visiPrevious = true;
        private bool    _visiNext = true;
        private int     _currentPage = 1;
        private int     _totalPage = 1;
        private bool    _showForm = false;
        private string  _formTitle = "";
        private string  _productImagePath = "";
        private string  _productName = "";
        private string  _productBrand = "";
        private string  _productCategory = "";
        private int     _productPrice = 0;
        private int     _productInventory = 0;
        private int     _productSold = 0;
        private string _productDescription = "";
        private string  _message = "";
        #endregion

        #region Properties for binding
        public ObservableCollection<ProductCellViewModel> ListProducts
        {
            get { return listProducts; }
            set
            {
                listProducts = value;
                OnPropertyChanged(nameof(ListProducts));
            }
        }

        public bool VisiPrevious
        {
            get { return _visiPrevious; }
            set
            {
                _visiPrevious = value;
                OnPropertyChanged(nameof(VisiPrevious));
            }
        }

        public bool VisiNext
        {
            get { return _visiNext; }
            set
            {
                _visiNext = value;
                OnPropertyChanged(nameof(VisiNext));
            }
        }

        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }

        public int TotalPage
        {
            get { return _totalPage; }
            set
            {
                _totalPage = value;
                OnPropertyChanged(nameof(TotalPage));
            }
        }

        public bool ShowForm
        {
            get { return _showForm; }
            set
            {
                _showForm = value;
                OnPropertyChanged(nameof(ShowForm));
            }
        }
        public string ProductName
        {
            get { return _productName; }
            set
            {
                _productName = value;
                OnPropertyChanged(nameof(ProductName));
            }
        }
        public string ProductBrand
        {
            get { return _productBrand; }
            set
            {
                _productBrand = value;
                OnPropertyChanged(nameof(ProductBrand));
            }
        }
        public string ProductCategory
        {
            get { return _productCategory; }
            set
            {
                _productCategory = value;
                OnPropertyChanged(nameof(ProductCategory));
            }
        }
        public int ProductPrice
        {
            get { return _productPrice; }
            set
            {
                _productPrice = value;
                OnPropertyChanged(nameof(ProductPrice));
            }
        }
        public int ProductInventory
        {
            get { return _productInventory; }
            set
            {
                _productInventory = value;
                OnPropertyChanged(nameof(ProductInventory));
            }
        }
        public int ProductSold
        {
            get { return _productSold; }
            set
            {
                _productSold = value;
                OnPropertyChanged(nameof(ProductSold));
            }
        }

        public string ProductDescription
        {
            get { return _productDescription; }
            set
            {
                _productDescription = value;
                OnPropertyChanged(nameof(ProductDescription));
            }
        }
        public string FormTitle
        {
            get { return _formTitle; }
            set
            {
                _formTitle = value;
                OnPropertyChanged(nameof(FormTitle));
            }
        }
        public string ProductImagePath
        {
            get { return _productImagePath; }
            set
            {
                _productImagePath = value;
                OnPropertyChanged(nameof(ProductImagePath));
            }
        }

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        #endregion

        #region Commands
        private ICommand _acceptFormCommand;
        public  ICommand AcceptFormCommand
        {
            get => _acceptFormCommand;
            set
            {
                _acceptFormCommand = value;
                OnPropertyChanged(nameof(AcceptFormCommand));
            }
        }
        public  ICommand CancelFormCommand      { get; set; }
        public  ICommand CreateProductCommand   { get; set; }
        public  ICommand SelectImagePathCommand { get; set; }
        public  ICommand NextPageCommand        { get; }
        public  ICommand PreviousPageCommand    { get; }

        #endregion

        public ProductManagerViewModel(IDao                 dao,
                                       IFilePickerService   filePickerService)
        {
            this._dao               = dao;
            this._filePickerService = filePickerService;

            LoadData();

            CreateProductCommand = new RelayCommand(() =>
            {
                ShowForm = true;
                FormTitle = "Thêm sản phẩm mới";
                ProductImagePath = "";
                ProductName = "";
                ProductBrand = "";
                ProductCategory = "";
                ProductPrice = 0;
                ProductInventory = 0;
                ProductSold = 0;
                ProductDescription = "";

                AcceptFormCommand = new RelayCommand(async () =>
                {
                    ShowForm = false;
                    var result = await _dao.CreateProductAsync(ProductName, ProductBrand, ProductCategory, ProductPrice, ProductInventory, ProductSold, ProductImagePath);

                    if (result)
                    {
                        Message = "Thêm sản phẩm thành công!";
                        LoadData();
                    }
                    else
                    {
                        Message = "Thêm sản phẩm thất bại!";
                    }
                });
            });

            CancelFormCommand       = new RelayCommand(cancelFormCommand);
            SelectImagePathCommand  = new RelayCommand(executeSelectImagePathAsyncCommand);
            NextPageCommand         = new RelayCommand(nextPageCommand);
            PreviousPageCommand     = new RelayCommand(previousPageCommand);
        }

        /// <summary>
        /// Cancels the form by hiding it command.
        /// </summary>
        private void cancelFormCommand()
        {
            ShowForm = false;
        }

        /// <summary>
        /// Select image path command
        /// </summary>
        private async void executeSelectImagePathAsyncCommand()
        {
            var path = await _filePickerService.PickFileAsync(new List<string>
            {
                ".jpg", ".jpeg", ".png"
            });

            if (path == null) return;
            ProductImagePath = path;
        }

        /// <summary>
        /// Go to next page command
        /// </summary>
        private void nextPageCommand()
        {
            CurrentPage++;
            LoadData();
        }

        /// <summary>
        /// Go to previous page command
        /// </summary>
        private void previousPageCommand()
        {
            CurrentPage--;
            LoadData();
        }

        /// <summary>
        /// Load data from database (All products)
        /// </summary>
        private async void LoadData()
        {
            var result = await _dao.GetListProductThumbnailAsync("", CurrentPage, 20, Models.Enums.SortProduct.DateAscending, "", "", 0, int.MaxValue);

            TotalPage = result.TotalPages;

            // Show or hide button next/previous page
            VisiNext = CurrentPage != _totalPage;
            VisiPrevious = CurrentPage != 1;

            ListProducts.Clear();
            foreach (var item in result.Products)
            {
                ListProducts.Add(new ProductCellViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price,
                    Brand = item.Brand,
                    Category = item.Category,
                    Inventory = item.Stock,
                    Sold = item.Sold,

                    // Edit command for each cell to show edit form
                    EditCommand = new RelayCommand(async () =>
                    {
                        FormTitle = "Chỉnh sửa sản phẩm";
                        ShowForm = true;
                        AcceptFormCommand = new RelayCommand(async () =>
                        {
                            ShowForm = false;
                            var result = await _dao.ChangeProductInfoAsync(item.Id, ProductName, ProductBrand, ProductCategory, ProductPrice, ProductInventory, ProductSold, ProductImagePath, ProductDescription);

                            if (result)
                            {
                                Message = "Sửa sản phẩm thành công!";
                                LoadData();
                            }
                            else
                            {
                                Message = "Sửa sản phẩm thất bại!";
                            }

                        });

                        ProductImagePath = item.ImagePath;
                        ProductName = item.Name;
                        ProductBrand = item.Brand;
                        ProductCategory = item.Category;
                        ProductPrice = item.Price;
                        ProductInventory = item.Stock;
                        ProductSold = item.Sold;
                        ProductDescription = await _dao.GetProductDescriptionAsync(item.Id);
                    })

                });
            }
        }
        


        // For INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
