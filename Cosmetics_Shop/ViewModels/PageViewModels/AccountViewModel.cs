using CommunityToolkit.Mvvm.Input;
using Cosmetics_Shop.DataAccessObject.Data;
using Cosmetics_Shop.DataAccessObject.Interfaces;
using Cosmetics_Shop.Enums;
using Cosmetics_Shop.Models;
using Cosmetics_Shop.Services;
using Cosmetics_Shop.Services.EventAggregatorMessages;
using Cosmetics_Shop.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage.Pickers;
using WinRT.Interop;

namespace Cosmetics_Shop.ViewModels.PageViewModels
{
    /// <summary>
    /// View Model for Account Page
    /// </summary>
    public class AccountViewModel : INotifyPropertyChanged
    {
        #region Dependency   
        private readonly UserSession        _userSession;
        private readonly IDao               _dao;
        private readonly IEventAggregator   _eventAggregator;
        private readonly IFilePickerService _filePickerService;
        #endregion

        #region Fields
        private string _name;
        private string _nameDisplay;
        private string _email;
        private string _phone;
        private string _address;
        private int totalProducts;
        private int totalBills;
        private int totalMoneySpent;
        private bool _changeInforMode = false;
        private string _avatarPath;
        private string _changeModeContent = "Thay đổi thông tin";
        private bool _showDialogChangePassword = false;
        private string _changePasswordMessage = "";
        private string _changeInfoMessage = "";
        private string _passwordCurrent = "";
        private string _passwordNew = "";
        private string _confirmPasswordNew = "";
        private string _nameRestore;
        private string _emailRestore;
        private string _phoneRestore;
        private string _addressRestore;
        private string _userLevel = "";
        private string _createTime;
        #endregion

        #region Properties Binding
        public string NameDisplay
        {
            get { return _nameDisplay; }
            set
            {
                _nameDisplay = value;
                OnPropertyChanged(nameof(NameDisplay));
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
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
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
        public int TotalProducts
        {
            get { return totalProducts; }
            set
            {
                totalProducts = value;
                OnPropertyChanged(nameof(TotalProducts));
            }
        }
        public int TotalBills
        {
            get { return totalBills; }
            set
            {
                totalBills = value;
                OnPropertyChanged(nameof(TotalBills));
            }
        }
        public int TotalMoneySpent
        {
            get { return totalMoneySpent; }
            set
            {
                totalMoneySpent = value;
                OnPropertyChanged(nameof(TotalMoneySpent));
            }
        }
        public string AvatarPath
        {

            get => _avatarPath;
            set
            {
                _avatarPath = value;
                OnPropertyChanged(nameof(AvatarPath));
            }
        }

        public string CreateTime
        {
            get => _createTime;
            set
            {
                _createTime = value;
                OnPropertyChanged(nameof(CreateTime));
            }
        }
        public bool ChangeInforMode
        {
            get => _changeInforMode;
            set
            {
                _changeInforMode = value; 
                OnPropertyChanged(nameof(ChangeInforMode));
            }
        }

        
        public bool ShowDialogChangePassword
        {
            get => _showDialogChangePassword;
            set
            {
                _showDialogChangePassword = value;
                OnPropertyChanged(nameof(ShowDialogChangePassword));
            }
        }
        public string PasswordCurrent
        {
            get => _passwordCurrent;
            set
            {
                _passwordCurrent = value;
                OnPropertyChanged(nameof(PasswordCurrent));
            }
        }
        public string PasswordNew
        {
            get => _passwordNew;
            set
            {
                _passwordNew = value;
                OnPropertyChanged(nameof(PasswordNew));
            }
        }

        public string ConfirmPasswordNew
        {
            get => _confirmPasswordNew;
            set
            {
                _confirmPasswordNew = value;
                OnPropertyChanged(nameof(ConfirmPasswordNew));
            }
        }
        public string ChangePasswordMessage
        {
            get => _changePasswordMessage;
            set
            {
                _changePasswordMessage = value;
                OnPropertyChanged(nameof(ChangePasswordMessage));
            }
        }
        
        public string ChangeInfoMessage
        {
            get => _changeInfoMessage;
            set
            {
                _changeInfoMessage = value;
                OnPropertyChanged(nameof(ChangeInfoMessage));
            }
        }

        public string ChangeModeContent
        {
            get => _changeModeContent;
            set
            {
                _changeModeContent = value;
                OnPropertyChanged(nameof(ChangeModeContent));
            }
        }

        public string UserLevel
        {
            get
            {
                return _userLevel;
            }
            set
            {
                _userLevel = value;
                OnPropertyChanged(nameof(UserLevel));
            }
        }
        #endregion

        #region Commands
        public ICommand ChangeInfoModeCommand { get; set; }
        public ICommand SaveInfoCommand { get; set; }
        public ICommand ChangeAvatarCommand { get; set; }
        public ICommand ChangePasswordCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand AcceptChangePasswordCommand { get; set; }
        public ICommand RefuseChangePasswordCommand { get; set; }
        #endregion


        /// <summary>
        /// Constructor of AccountViewModel
        /// </summary>
        /// <param name="dao"></param>
        /// <param name="eventAggregator"></param>
        /// <param name="filePickerService"></param>
        /// <param name="userSession"></param>
        public AccountViewModel(IDao                dao, 
                                IEventAggregator    eventAggregator, 
                                IFilePickerService  filePickerService,
                                UserSession         userSession)
        {
            this._userSession       = userSession;
            this._eventAggregator   = eventAggregator;
            this._filePickerService = filePickerService;
            this._dao               = dao;

            loadUserInformation();
            ChangeInforMode = false;

            ChangeInfoModeCommand   = new RelayCommand(changeInfoModeCommand);
            SaveInfoCommand         = new RelayCommand(saveInfoCommand);
            LogoutCommand           = new RelayCommand(logoutCommand);
            ChangeAvatarCommand     = new RelayCommand(changeAvatarCommand);

            AcceptChangePasswordCommand = new RelayCommand(acceptChangePasswordCommand);
            RefuseChangePasswordCommand = new RelayCommand(cancelChangePasswordCommand);

            ChangePasswordCommand = new RelayCommand(changePasswordCommand);
        }

        /// <summary>
        /// Command for press change password
        /// </summary>
        public void changePasswordCommand() 
        {
            ChangePasswordMessage = string.Empty;
            PasswordCurrent = string.Empty;
            PasswordNew = string.Empty;
            ConfirmPasswordNew = string.Empty;
            ShowDialogChangePassword = true;
        }

        
        /// <summary>
        /// Command for press log out
        /// </summary>
        private void logoutCommand()
        {
            _userSession.Logout();

            // Publish message to notify other view models (Logout)
            _eventAggregator.Publish(new LogoutMessage());
        }


        /// <summary>
        /// Command to press saving new information 
        /// </summary>
        private async void saveInfoCommand()
        {
            if (!Email.IsValidEmail() || !Phone.IsValidPhoneNumber())
            {
                Name = _nameRestore;
                Email = _emailRestore;
                Phone = _phoneRestore;
                Address = _addressRestore;
                changeInfoModeCommand();
                ChangeInfoMessage = "Thông tin không hợp lệ!";
                return;
            }

            if (_nameRestore == Name && _emailRestore == Email && _phoneRestore == Phone && _addressRestore == Address)
            {
                changeInfoModeCommand();
                ChangeInfoMessage = "";
                return;
            }

                await _dao.ChangeAllUserInformationAsync(_userSession.GetId(), new UserDetail()
            {
                Name    = Name,
                Email   = Email,
                Phone   = Phone,
                Address = Address,
                AvatarPath = AvatarPath
            });

            
            _nameRestore    = Name;
            _emailRestore   = Email;
            _phoneRestore   = Phone;
            _addressRestore = Address;
            NameDisplay     = Name;

            changeInfoModeCommand();
            ChangeInfoMessage = "Thay đổi thông tin thành công!";

            _eventAggregator.Publish(new ChangeUsernameOrAvatarMessage()
            {
                Name        = Name,
                AvatarPath  = AvatarPath
            });
        }

        
        /// <summary>
        /// Command for press change information mode
        /// </summary>
        private void changeInfoModeCommand()
        {
            if (ChangeInforMode)
            {
                Name = _nameRestore;
                Email = _emailRestore;
                Phone = _phoneRestore;
                Address = _addressRestore;
            }

            ChangeInforMode = !ChangeInforMode;
            ChangeModeContent = ChangeInforMode ? "Hủy" : "Thay đổi thông tin";
        }

        
        /// <summary>
        /// Command for press change avatar mode
        /// </summary>
        private async void changeAvatarCommand()
        {

            // Thiết lập loại file được chọn
            List<string> filters = new List<string>()
            {
                ".jpg", ".jpeg", ".png"
            };

            var result = await _filePickerService.PickFileAsync(filters);

            var avatarPath = result;
            if (avatarPath != null)
            {
                AvatarPath = avatarPath;
                await _dao.ChangeUserInformationAsync(_userSession.GetId(), UserInformationType.AvatarPath, avatarPath);
                _eventAggregator.Publish(new ChangeUsernameOrAvatarMessage()
                {
                    Name = Name,
                    AvatarPath = AvatarPath
                });
            }
        }

        

        /// <summary>
        /// Load user information from database
        /// </summary>
        private async void loadUserInformation()
        {
            var userDetail = await _dao.GetUserDetailAsync(_userSession.GetId());

            Name            = userDetail.Name;
            NameDisplay     = userDetail.Name;
            Email           = userDetail.Email;
            Phone           = userDetail.Phone;
            Address         = userDetail.Address;
            AvatarPath      = userDetail.AvatarPath;
            TotalProducts   = userDetail.TotalProducts;
            TotalBills      = userDetail.TotalBills;
            TotalMoneySpent = userDetail.TotalMoneySpent;
            UserLevel       = await _dao.GetUserLevelAsync(_userSession.GetId());
            CreateTime      = userDetail.CreateTime.ToShortDateString();

            if (AvatarPath == null)
            {
                AvatarPath = "ms-appx:///Assets/avatar_temp.png";
            }

            _nameRestore    = Name;
            _emailRestore   = Email;
            _phoneRestore   = Phone;
            _addressRestore = Address;
        }

        
        /// <summary>
        /// Command for press accept change password
        /// </summary>
        private async void acceptChangePasswordCommand()
        {
            if (PasswordNew != ConfirmPasswordNew)
            {
                ChangePasswordMessage = "Mật khẩu mới không khớp !";
                return;
            }

            var result = await _dao.ChangePasswordAsync(_userSession.GetId(), PasswordCurrent, PasswordNew);
            if (result)
            {
                ShowDialogChangePassword = false;
            }
            else
            {
                ChangePasswordMessage = "Mật khẩu hiện tại không đúng !";
            }
        }

        
        /// <summary>
        /// Command for press cancel change password
        /// </summary>
        private void cancelChangePasswordCommand()
        {
            ShowDialogChangePassword = false;
        } 



        // For INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
