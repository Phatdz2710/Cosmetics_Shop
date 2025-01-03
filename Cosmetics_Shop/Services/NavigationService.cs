using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Services
{
    /// <summary>
    /// Provides navigation functionality for navigating between pages in the application.
    /// </summary>
    public class NavigationService : INavigationService
    {
        /// <summary>
        /// Frame to navigate.
        /// </summary>
        private Frame _frame;

        /// <summary>
        /// Initializes the navigation service with the specified <see cref="Frame"/> instance.
        /// </summary>
        /// <param name="frame">
        /// The <see cref="Frame"/> control used for navigation.
        /// This frame will host the pages that the service navigates to.
        /// </param>
        public void Initialize(Frame frame)
        {
            _frame = frame;
        }

        /// <summary>
        /// Navigates to the specified page type.
        /// </summary>
        /// <typeparam name="TPage">
        /// The type of the page to navigate to.
        /// This type must derive from <see cref="Page"/>.
        /// </typeparam>
        public void NavigateTo<TPage>()
        {
            if (_frame == null)
            {
                throw new InvalidOperationException("Frame is not initialized");
            }
            _frame.Navigate(typeof(TPage));
        }

        /// <summary>
        /// Navigates to the specified page type with the specified parameter.
        /// </summary>
        /// <typeparam name="TPage">
        /// The type of the page to navigate to.
        /// This type must derive from <see cref="Page"/>.
        /// </typeparam>
        /// <param name="parameter">
        /// The parameter to pass to the target page.
        /// This parameter can be used to pass data to the target page.
        /// </param>
        public void NavigateTo<TPage>(object parameter)
        {
            if (_frame == null)
            {
                throw new InvalidOperationException("Frame is not initialized");
            }
            _frame.Navigate(typeof(TPage), parameter);
        }

        /// <summary>
        /// Navigates back to the previous page in the navigation stack, if one exists.
        /// </summary>
        public void GoBack()
        {
            if (_frame == null || !_frame.CanGoBack)
            {
                return;
            }
            _frame.GoBack();
        }
    }
}