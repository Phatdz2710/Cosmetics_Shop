using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cosmetics_Shop.ViewModels.UserControlViewModels
{
    /// <summary>
    /// View model for product cell
    /// </summary>
    public class ProductCellViewModel : INotifyPropertyChanged
    {
        private int _id;
        private string _name;
        private int _price;
        private string _brand;
        private string _category;
        private int _inventory;
        private int _sold;
        /// <summary>
        /// Gets or sets the unique identifier for the product.
        /// Notifies the UI when the value changes.
        /// </summary>
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        /// <summary>
        /// Gets or sets the name of the product.
        /// Notifies the UI when the value changes.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        /// <summary>
        /// Gets or sets the price of the product.
        /// Notifies the UI when the value changes.
        /// </summary>
        public int Price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        /// <summary>
        /// Gets or sets the brand of the product.
        /// Notifies the UI when the value changes.
        /// </summary>
        public string Brand
        {
            get { return _brand; }
            set
            {
                _brand = value;
                OnPropertyChanged(nameof(Brand));
            }
        }

        /// <summary>
        /// Gets or sets the category of the product.
        /// Notifies the UI when the value changes.
        /// </summary>
        public string Category
        {
            get { return _category; }
            set
            {
                _category = value;
                OnPropertyChanged(nameof(Category));
            }
        }

        /// <summary>
        /// Gets or sets the inventory count for the product.
        /// Notifies the UI when the value changes.
        /// </summary>
        public int Inventory
        {
            get { return _inventory; }
            set
            {
                _inventory = value;
                OnPropertyChanged(nameof(Inventory));
            }
        }

        /// <summary>
        /// Gets or sets the number of products sold.
        /// Notifies the UI when the value changes.
        /// </summary>
        public int Sold
        {
            get { return _sold; }
            set
            {
                _sold = value;
                OnPropertyChanged(nameof(Sold));
            }
        }

        #region Command

        /// <summary>
        /// Command for editing the product details (e.g., price, name, etc.).
        /// </summary>
        public ICommand EditCommand { get; set; }

        #endregion

        public ProductCellViewModel() { }

        public ProductCellViewModel(string name, 
                                    int price, 
                                    string brand,
                                    string category,
                                    int inventory,
                                    int sold,
                                    ICommand editCommand)
        {
            Name        = name;
            Price       = price;
            Brand       = brand;
            Category    = category;
            Inventory   = inventory;
            Sold        = sold;
            EditCommand = editCommand;
        }


        // For INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
