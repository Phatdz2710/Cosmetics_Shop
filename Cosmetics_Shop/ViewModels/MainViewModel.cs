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
        //private int _selectedPageNumber;

        private Page purchasePage = new PurchasePage();
        private Page cartPage = new CartPage();
        private Page adminPage = new AdminPage();
        private Page accountPage = new AccountPage();
        private Page dashboardPage = new DashboardPage();

        public Page CurrentPage
        {
            get => currentPage;
            set
            {
                currentPage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentPage)));
            }
        }

        // Command (gán Event cho button qua binding)
        public ICommand PurchaseButtonCommand { get; set; }
        public ICommand CartButtonCommand { get; set; }
        public ICommand AdminButtonCommand { get; set; }
        public ICommand AccountButtonCommand { get; set; }
        public ICommand DashboardButtonCommand {  get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel()
        {
            CurrentPage = dashboardPage;
            //SelectedPageNumber = 1;

            DashboardButtonCommand = new RelayCommand(() =>
            {
                CurrentPage = dashboardPage;
            });

            PurchaseButtonCommand = new RelayCommand(() => {
                CurrentPage = purchasePage;
                //SelectedPageNumber = 1;
            });

            CartButtonCommand = new RelayCommand(() => {
                CurrentPage = cartPage;
                //SelectedPageNumber = 2;
            });

            AdminButtonCommand = new RelayCommand(() => {
                CurrentPage = adminPage;
                //SelectedPageNumber = 3;
            });

            AccountButtonCommand = new RelayCommand(() => {
                CurrentPage = accountPage;
                //SelectedPageNumber = 4;
            });
        }
    }
}
