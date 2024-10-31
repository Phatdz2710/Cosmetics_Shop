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
    public class CartThumbnailViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public CartThumbnail CartThumbnail { get; set; }

        public CartThumbnailViewModel(CartThumbnail cartThumbnail)
        {
            this.CartThumbnail = cartThumbnail;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
