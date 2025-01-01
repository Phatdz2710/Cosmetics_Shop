using CommunityToolkit.Mvvm.Input;
using Cosmetics_Shop.Models;
using Cosmetics_Shop.Services;
using Cosmetics_Shop.Views.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cosmetics_Shop.ViewModels
{
    /// <summary>
    /// View model for review area in product detail page
    /// </summary>
    public class ReviewThumbnailViewModel : INotifyPropertyChanged
    {
        // Navigation service
        private readonly INavigationService _navigationService;

        // Main properties
        //public ReviewThumbnail ReviewThumbnail { get; set; }

        // Constructor
        public ReviewThumbnailViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private ReviewThumbnail _reviewThumbnail;

        public ReviewThumbnail ReviewThumbnail
        {
            get => _reviewThumbnail;
            set
            {
                _reviewThumbnail = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(StarNumber)); // Notify that StarNumber has changed
                OnPropertyChanged(nameof(StarList)); // Notify that StarList has changed
            }
        }

        public int StarNumber => _reviewThumbnail?.StarNumber ?? 0; // Expose the StarNumber

        // Expose a collection of stars based on StarNumber
        public IEnumerable<int> StarList => Enumerable.Range(1, StarNumber);


        // Implement INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
