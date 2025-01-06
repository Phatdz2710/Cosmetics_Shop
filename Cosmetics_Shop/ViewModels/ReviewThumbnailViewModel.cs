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

        /// <summary>
        /// Gets or sets the review thumbnail object, which includes the star rating and review details.
        /// Changes to this property will update the star number and the list of stars displayed.
        /// </summary>
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

        /// <summary>
        /// Gets the number of stars for the review.
        /// Returns 0 if the review thumbnail is null.
        /// </summary>
        public int StarNumber => _reviewThumbnail?.StarNumber ?? 0; // Expose the StarNumber

        /// <summary>
        /// Gets a collection of star numbers (1 to StarNumber) for display purposes.
        /// This is used to render a list of stars in the UI based on the star rating.
        /// </summary>
        public IEnumerable<int> StarList => Enumerable.Range(1, StarNumber);


        // Implement INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
