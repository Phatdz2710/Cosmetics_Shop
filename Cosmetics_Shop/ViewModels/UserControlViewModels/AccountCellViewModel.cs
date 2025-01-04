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

        /// <summary>
        /// Gets or sets the unique identifier for the entity.
        /// Notifies the UI when the value changes.
        /// </summary>
        public int ID
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(ID));
            }
        }

        /// <summary>
        /// Gets or sets the user identifier.
        /// Notifies the UI when the value changes.
        /// </summary>
        public int UserID
        {
            get => _userId;
            set
            {
                _userId = value;
                OnPropertyChanged(nameof(UserID));
            }
        }

        /// <summary>
        /// Gets or sets the username associated with the user.
        /// Notifies the UI when the value changes.
        /// </summary>
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        /// <summary>
        /// Gets or sets the role of the user.
        /// Notifies the UI when the value changes.
        /// </summary>
        public string Role
        {
            get => _role;
            set
            {
                _role = value;
                OnPropertyChanged(nameof(Role));
            }
        }

        /// <summary>
        /// Gets or sets the user level associated with the user.
        /// Notifies the UI when the value changes.
        /// </summary>
        public string UserLevel
        {
            get => _userLevel;
            set
            {
                _userLevel = value;
                OnPropertyChanged(nameof(UserLevel));
            }
        }

        #region Commands

        /// <summary>
        /// Command to show more details related to the entity (likely the user).
        /// </summary>
        public ICommand ShowMoreCommand { get; set; }

        /// <summary>
        /// Command to delete the entity (likely the user).
        /// </summary>
        public ICommand DeleteCommand { get; set; }

        #endregion


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
