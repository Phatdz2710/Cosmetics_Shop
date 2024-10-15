using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cosmetics_Shop.Views.Pages;
using Windows.Networking.Sockets;
using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace Cosmetics_Shop.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private Page currentPage;
        public Page CurrentPage
        {
            get => currentPage;
            set
            {
                currentPage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentPage)));
            }
        }

        private Page buyPage = new BuyPage();
        private Page cartPage = new CartPage();
        private Page adminPage = new AdminPage();
        private Page accountPage = new AccountPage();

        public ICommand BuyButton { get; set; }
        public ICommand CartButton { get; set; }
        public ICommand AdminButton { get; set; }
        public ICommand AccountButton { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel()
        {
            CurrentPage = buyPage;
            BuyButton = new RelayCommand(() => CurrentPage = buyPage);
            CartButton = new RelayCommand(() => CurrentPage = cartPage);
            AdminButton = new RelayCommand(() => CurrentPage = adminPage);
            AccountButton = new RelayCommand(() => CurrentPage = accountPage);
        }
    }
}
