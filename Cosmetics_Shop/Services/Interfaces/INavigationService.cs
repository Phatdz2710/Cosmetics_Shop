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
    public interface INavigationService
    {
        /// <summary>
        /// Initializes the navigation service with the specified <see cref="Frame"/> instance.
        /// </summary>
        /// <param name="frame">
        /// The <see cref="Frame"/> control used for navigation.
        /// This frame will host the pages that the service navigates to.
        /// </param>
        void Initialize(Frame frame);

        /// <summary>
        /// Navigates to the specified page type.
        /// </summary>
        /// <typeparam name="TPage">
        /// The type of the page to navigate to.
        /// This type must derive from <see cref="Page"/>.
        /// </typeparam>
        void NavigateTo<TPage>();

        /// <summary>
        /// Navigates to the specified page type with the specified parameter.
        /// </summary>
        /// <typeparam name="TPage"></typeparam>
        /// <param name="parameter"></param>
        void NavigateTo<TPage>(object parameter);

        /// <summary>
        /// Navigates back to the previous page in the navigation stack, if one exists.
        /// </summary>
        void GoBack();
    }

}
