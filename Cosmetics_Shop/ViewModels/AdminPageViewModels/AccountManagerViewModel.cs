using CommunityToolkit.Mvvm.Input;
using Cosmetics_Shop.DataAccessObject.Interfaces;
using Cosmetics_Shop.ViewModels.UserControlViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
        private bool    _showForm = false;
        private string  _username = "";
        private string  _password = "";
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
        public bool ShowForm
        {
            get { return _showForm; }
            set
            {
                _showForm = value;
                OnPropertyChanged(nameof(ShowForm));
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
        /// Reload data
        /// </summary>
        private void reloadCommand()
        {
            LoadData();
        }

        /// <summary>
        /// Command to create account
        /// </summary>
        private void createAccountCommand()
        {
            AcceptFormCommand = new RelayCommand(acceptCreateAccountCommand);
            ShowForm = true;
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
                ListAccounts.Add(new AccountCellViewModel(account.ID, account.Username, account.Password, account.Role,

                    // Edit command for each cell
                    editCommand: new RelayCommand(() =>
                    {
                        AcceptFormCommand = new RelayCommand(acceptChangeAccountInformationCommand);
                        ShowForm = true;
                        FormTitle = "Chỉnh sửa tài khoản";
                        Username = account.Username;
                        Password = account.Password;
                        Role = account.Role == "User" ? 0 : 1;
                        _id = account.ID;
                    }),

                    // Delete command for each cell
                    deleteCommand: new RelayCommand(async () =>
                    {
                        await _dao.DeleteAccount(account.ID);
                        LoadData();
                    })));
            }
        }

        /// <summary>
        /// Command to accept change account information
        /// </summary>
        private async void acceptChangeAccountInformationCommand()
        {
            ShowForm = false;
            var result = await _dao.ChangeAccountInfoAsync(_id, Username, Password, Role == 0 ? "User" : "Admin");

            if (result)
            {
                LoadData();
            }
        }

        /// <summary>
        /// Command to accept create account
        /// </summary>
        private async void acceptCreateAccountCommand()
        {
            ShowForm = false;
            var result = await _dao.CreateAccountAsync(Username, Password, Role == 0 ? "User" : "Admin");

            if (result)
            {
                LoadData();
            }
        }

        /// <summary>
        /// Command to cancel form
        /// </summary>
        private void cancelFormCommand()
        {
            ShowForm = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
