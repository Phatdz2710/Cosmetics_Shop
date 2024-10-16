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
        private int _selectedPageNumber;

        private Page buyPage = new BuyPage();
        private Page cartPage = new CartPage();
        private Page adminPage = new AdminPage();
        private Page accountPage = new AccountPage();

        public int SelectedPageNumber
        {
            get => _selectedPageNumber;
            set
            {
                _selectedPageNumber = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedPageNumber)));
            }
        }

        public Page CurrentPage
        {
            get => currentPage;
            set
            {
                currentPage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentPage)));
            }
        }

        public ICommand BuyButton { get; set; }
        public ICommand CartButton { get; set; }
        public ICommand AdminButton { get; set; }
        public ICommand AccountButton { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel()
        {
            CurrentPage = buyPage;
            SelectedPageNumber = 1;

            BuyButton = new RelayCommand(() => {
                CurrentPage = buyPage;
                SelectedPageNumber = 1;
            });

            CartButton = new RelayCommand(() => {
                CurrentPage = cartPage;
                SelectedPageNumber = 2;
            });

            AdminButton = new RelayCommand(() => {
                CurrentPage = adminPage;
                SelectedPageNumber = 3;
            });

            AccountButton = new RelayCommand(() => {
                CurrentPage = accountPage;
                SelectedPageNumber = 4;
            });
        }
    }
}
