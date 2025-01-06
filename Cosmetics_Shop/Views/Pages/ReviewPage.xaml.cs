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
using Cosmetics_Shop.ViewModels.PageViewModels;
using Cosmetics_Shop.Models;
using Cosmetics_Shop.DataAccessObject.Interfaces;
using Cosmetics_Shop.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Cosmetics_Shop.Views.Pages
{
    /// <summary>
    /// Review page for user
    /// </summary>
    public sealed partial class ReviewPage : Page
    {
        public ReviewPageViewModel ViewModel { get; set; }
        public ReviewPage()
        {
            this.InitializeComponent();
            //ViewModel = App.ServiceProvider.GetService(typeof(ReviewPageViewModel)) as ReviewPageViewModel;
        }

        /// <summary>
        /// Handles navigation to the ReviewPage and initializes the ViewModel with data and required services.
        /// </summary>
        /// <param name="e">Navigation event arguments containing parameters passed to the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is ObservableCollection<OrderItemDisplay> products)
            {
                var navigationService = App.ServiceProvider.GetRequiredService<INavigationService>();
                var dao = App.ServiceProvider.GetRequiredService<IDao>();
                var serviceProvider = App.ServiceProvider; // Use the service provider
                var userSession = App.ServiceProvider.GetRequiredService<UserSession>();

                // Initialize the ViewModel with the products
                ViewModel = new ReviewPageViewModel(navigationService, dao, userSession, serviceProvider, products);
                DataContext = ViewModel; // Set the DataContext for binding
            }
        }


    }
}