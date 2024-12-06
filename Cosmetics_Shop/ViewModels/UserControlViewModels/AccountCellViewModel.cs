using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Core;

namespace Cosmetics_Shop.ViewModels.UserControlViewModels
{
    /// <summary>
    /// View model for account cell
    /// </summary>
    public class AccountCellViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;
        private string _role;
        private int _id;

        public int ID
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(ID));
            }
        }

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string Role
        {
            get => _role;
            set
            {
                _role = value;
                OnPropertyChanged(nameof(Role));
            }
        }

        public ICommand EditCommand     { get; set; }
        public ICommand DeleteCommand   { get; set; }


        public AccountCellViewModel(int id, 
                                    string username, 
                                    string password, 
                                    string role, 
                                    ICommand editCommand, 
                                    ICommand deleteCommand)
        {
            _id             = id;
            _username       = username;
            _password       = password;
            _role           = role;
            EditCommand     = editCommand;
            DeleteCommand   = deleteCommand;
        }

        /// <summary>
        /// Get ID of account
        /// </summary>
        /// <returns>ID of account</returns>
        public int GetID()
        {
            return _id;
        }


        // For INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
