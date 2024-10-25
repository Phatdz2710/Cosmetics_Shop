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

        private PurchasePage purchasePage;
        private CartPage cartPage;
        private AdminPage adminPage;
        private AccountPage accountPage;
        private DashboardPage dashboardPage;

        private PurchasePageViewModel purchasePageViewModel;
        public string Keyword
        {
            get => purchasePageViewModel.Keyword;
            set
            {
                purchasePageViewModel.Keyword = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Keyword)));
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

        // Command (gán Event cho button qua binding)
        public ICommand PurchaseButtonCommand { get; }
        public ICommand CartButtonCommand { get; }
        public ICommand AdminButtonCommand { get; }
        public ICommand AccountButtonCommand { get; }
        public ICommand DashboardButtonCommand { get; }


        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel()
        {
            purchasePageViewModel = new PurchasePageViewModel();
            purchasePage = new PurchasePage(purchasePageViewModel);
            cartPage = new CartPage();
            adminPage = new AdminPage();
            accountPage = new AccountPage();
            dashboardPage = new DashboardPage();


            CurrentPage = dashboardPage;;

            DashboardButtonCommand = new RelayCommand(() =>
            {
                CurrentPage = dashboardPage;
            });

            PurchaseButtonCommand = new RelayCommand(() =>
            {
                CurrentPage = purchasePage;
                purchasePage.ViewModel.SearchProduct();
            });

            CartButtonCommand = new RelayCommand(() =>
            {
                CurrentPage = cartPage;
            });

            AdminButtonCommand = new RelayCommand(() =>
            {
                CurrentPage = adminPage;
            });

            AccountButtonCommand = new RelayCommand(() =>
            {
                CurrentPage = accountPage;
            });
        }
    }
}
