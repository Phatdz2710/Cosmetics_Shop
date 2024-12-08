using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Services
{
    public class NavigationService : INavigationService
    {
        private Frame _frame;

        public void Initialize(Frame frame)
        {
            _frame = frame;
        }

        public void NavigateTo<TPage>()
        {
            if (_frame == null)
            {
                throw new InvalidOperationException("Frame is not initialized");
            }
            _frame.Navigate(typeof(TPage));
        }
        public void NavigateTo<TPage>(object parameter)
        {
            if (_frame == null)
            {
                throw new InvalidOperationException("Frame is not initialized");
            }
            _frame.Navigate(typeof(TPage), parameter);
        }

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
