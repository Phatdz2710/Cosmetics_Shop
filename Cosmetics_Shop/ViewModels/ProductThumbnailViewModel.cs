using Cosmetics_Shop.Models;
using Cosmetics_Shop.Views.Objects;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.ViewModels
{
    public class ProductThumbnailViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ProductThumbnail ProductThumbnail { get; set; }                                                                                                                                                                                                                                                                                                                                                                                                     

        public ProductThumbnailViewModel(ProductThumbnail productThumbnail)
        {
            this.ProductThumbnail = productThumbnail;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
