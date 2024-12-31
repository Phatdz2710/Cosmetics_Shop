using CommunityToolkit.Mvvm.Input;
using Cosmetics_Shop.DataAccessObject.Interfaces;
using Cosmetics_Shop.Models;
using Cosmetics_Shop.Services.Interfaces;    
using Cosmetics_Shop.ViewModels.UserControlViewModels;
using Cosmetics_Shop.Views.Pages;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Input;

namespace Cosmetics_Shop.ViewModels.AdminPageViewModels
{
    /// <summary>
    /// View model for AccountManager page
    /// </summary>
    public class AccountManagerViewModel : INotifyPropertyChanged
    {
        #region Singleton
        // Data access object
        private readonly IDao _dao = null;
        #endregion

        #region Fields
        private ObservableCollection<AccountCellViewModel> _listAccounts;

        private int     _currentPage = 1;
        private int     _totalPages;
        private int     _accountsPerPage = 10;
        private int     _totalAccounts;
        private bool    _showFormAddAccount = false;
        private bool    _showFormShowMore = false;
        private string  _username = "";
        private string  _password = "";
        private string  _name = "";
        private string  _numberPhone = "";
        private string  _address = "";
        private string  _email = "";
        private string  _message = "";

        private int     _role = 0;
        private int     _id = 0;
        private string  _formTitle = "";

        #endregion

        #region Properties for binding
        public ObservableCollection<AccountCellViewModel> ListAccounts
        {
            get { return _listAccounts; }
            set
            {
                _listAccounts = value;
                OnPropertyChanged(nameof(ListAccounts));
            }
        }
        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }


        public int TotalPages
        {
            get { return _totalPages; }
            set
            {
                _totalPages = value;
                OnPropertyChanged(nameof(TotalPages));
            }
        }
        public int TotalAccounts
        {
            get { return _totalAccounts; }
            set
            {
                _totalAccounts = value;
                OnPropertyChanged(nameof(TotalAccounts));
            }
        }
        public bool ShowFormAddAccount
        {
            get { return _showFormAddAccount; }
            set
            {
                _showFormAddAccount = value;
                OnPropertyChanged(nameof(ShowFormAddAccount));
            }
        }
        public bool ShowFormShowMore
        {
            get { return _showFormShowMore; }
            set
            {
                _showFormShowMore = value;
                OnPropertyChanged(nameof(ShowFormShowMore));
            }
        }

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string NumberPhone
        {
            get { return _numberPhone; }
            set
            {
                _numberPhone = value;
                OnPropertyChanged(nameof(NumberPhone));
            }
        }
        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public int Role
        {
            get { return _role; }
            set
            {
                _role = value;
                OnPropertyChanged(nameof(Role));
            }
        }
        public string FormTitle
        {
            get { return _formTitle; }
            set
            {
                _formTitle = value;
                OnPropertyChanged(nameof(FormTitle));
            }
        }

        #endregion

        #region Commands
        private ICommand _acceptFormCommand;
        public ICommand AcceptFormCommand
        {
            get => _acceptFormCommand;
            set
            {
                _acceptFormCommand = value;
                OnPropertyChanged(nameof(AcceptFormCommand));
            }
        }
        public ICommand CancelFormCommand       { get; set; }
        public ICommand CreateAccountCommand    { get; set; }
        public ICommand ReloadCommand           { get; set; }
        #endregion

        public AccountManagerViewModel(IDao dao)
        {
            _dao = dao;

            ListAccounts = new ObservableCollection<AccountCellViewModel>();

            CancelFormCommand = new RelayCommand(cancelFormCommand);

            CreateAccountCommand    = new RelayCommand(createAccountCommand);
            ReloadCommand           = new RelayCommand(reloadCommand);

            LoadData();
        }

        /// <summary>
        /// Reload data command
        /// </summary>
        private void reloadCommand()
        {
            LoadData();
        }

        /// <summary>
        /// Show form to create account command
        /// </summary>
        private void createAccountCommand()
        {
            AcceptFormCommand = new RelayCommand(acceptCreateAccountCommand);
            ShowFormAddAccount = true;
            FormTitle = "Tạo tài khoản";
            Username = "";
            Password = "";
            Role = 0;
        }

        /// <summary>
        /// Load data from database
        /// </summary>
        public async void LoadData()
        {
            var accountSearchResult = await _dao.GetListAccountAsync();

            TotalPages = accountSearchResult.TotalPages;
            TotalAccounts = accountSearchResult.TotalAccounts;

            ListAccounts.Clear();

            foreach (var account in accountSearchResult.ListAccounts)
            {
                var userLevel = await _dao.GetUserLevelAsync(account.UserID);

                ListAccounts.Add(new AccountCellViewModel(account.ID, account.Username, account.Role, userLevel, account.UserID,

                    // Edit command for each cell
                    showMoreCommand: new RelayCommand<int>(ShowMoreCommand),

                    // Delete command for each cell
                    deleteCommand: new RelayCommand(async () =>
                    {
                        await _dao.DeleteAccount(account.ID);
                        Message = "Xóa tài khoản thành công!";
                        LoadData();
                    })));
            }
        }

        /// <summary>
        /// Show form to show more information command
        /// </summary>
        /// <param name="userId"></param>
        private async void ShowMoreCommand(int userId)
        {
            var userInfo = await _dao.GetUserDetailAsync(userId);
            AcceptFormCommand = new RelayCommand(CloseShowMoreForm);
            ShowFormShowMore = true;
            FormTitle = "Xem thêm thông tin";
            Name = userInfo.Name;
            Email = userInfo.Email;
            NumberPhone = userInfo.Phone;
            Address = userInfo.Address;
        }

        /// <summary>
        /// Close show more form command
        /// </summary>
        private void CloseShowMoreForm()
        {
            ShowFormShowMore = false;
        }

        /// <summary>
        /// Accept create account command
        /// </summary>
        private async void acceptCreateAccountCommand()
        {
            ShowFormAddAccount = false;
            var result = await _dao.CreateAccountAsync(Username, Password, Role == 0 ? "User" : "Admin");

            if (result)
            {
                Message = "Thêm tài khoản thành công!";
                LoadData();
            }
            else
            {
                Message = "Thêm tài khoản thất bại!";
            }
        }

        /// <summary>
        /// Cancel form command
        /// </summary>
        private void cancelFormCommand()
        {
            ShowFormAddAccount = false;
        }



        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
