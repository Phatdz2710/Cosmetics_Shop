using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Cosmetics_Shop.ViewModels.UserControlViewModels;
using Cosmetics_Shop.ViewModels;
using System.ComponentModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Cosmetics_Shop.Views.Controls
{
    /// <summary>
    /// Review page thumbnail control in review page 
    /// </summary>
    public sealed partial class ReviewPageThumbnailControl : UserControl
    {
        public ReviewPageThumbnailViewModel ViewModel
        {
            get { return (ReviewPageThumbnailViewModel)GetValue(ViewModelProperty); }
            set
            {
                SetValue(ViewModelProperty, value);
                DataContext = value;
            }
        }

        // Sử dụng DependencyProperty để hỗ trợ binding
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel",
                                        typeof(ReviewPageThumbnailViewModel),
                                        typeof(ReviewPageThumbnailControl),
                                        new PropertyMetadata(null));
        public ReviewPageThumbnailControl()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Handles the click event for a star rating button.
        /// Updates the `StarNumber` in the ViewModel and triggers an update of star states based on the selected rating.
        /// </summary>
        /// <remarks>
        /// - The method checks if the sender is a `Button` and if the `Tag` property is a valid string representing a star number.
        /// - It then parses the tag to an integer (`starNumber`) and updates the `StarNumber` property in the ViewModel.
        /// - Calls the `UpdateStarStates` method in the ViewModel to visually update the stars based on the rating.
        /// </remarks>
        private void StarButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string tag && int.TryParse(tag.ToString(), out int starNumber))
            {
                // Gọi phương thức trong ViewModel để cập nhật StarNumber
                ViewModel.StarNumber = starNumber;
                ViewModel.UpdateStarStates(starNumber);
            }
        }
    }
}
