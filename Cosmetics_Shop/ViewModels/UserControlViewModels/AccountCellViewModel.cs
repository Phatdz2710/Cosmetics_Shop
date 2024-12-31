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
        private string _role;
        private string _userLevel;
        private int _id;
        private int _userId;

        public int ID
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(ID));
            }
        }

        public int UserID
        {
            get => _userId;
            set
            {
                _userId = value;
                OnPropertyChanged(nameof(UserID));
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
        public string Role
        {
            get => _role;
            set
            {
                _role = value;
                OnPropertyChanged(nameof(Role));
            }
        }

        public string UserLevel
        {
            get => _userLevel;
            set
            {
                _userLevel = value;
                OnPropertyChanged(nameof(UserLevel));
            }
        }

        public ICommand ShowMoreCommand     { get; set; }
        public ICommand DeleteCommand   { get; set; }


        public AccountCellViewModel(int id, 
                                    string username,
                                    string role, 
                                    string userLevel,
                                    int userId,
                                    ICommand showMoreCommand, 
                                    ICommand deleteCommand)
        {
            _id             = id;
            _userId         = userId;
            _username       = username;
            _role           = role;
            _userLevel      = userLevel;
            ShowMoreCommand = showMoreCommand;
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
