using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Services
{
    public interface INavigationService
    {
        void Initialize(Frame frame);
        void NavigateTo<TPage>();
        void GoBack();
    }
}
