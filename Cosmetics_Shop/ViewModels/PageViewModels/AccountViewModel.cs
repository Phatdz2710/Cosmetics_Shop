using CommunityToolkit.Mvvm.Input;
using Cosmetics_Shop.DataAccessObject.Data;
using Cosmetics_Shop.DataAccessObject.Interfaces;
using Cosmetics_Shop.Enums;
using Cosmetics_Shop.Models;
using Cosmetics_Shop.Services;
using Cosmetics_Shop.Services.EventAggregatorMessages;
using Cosmetics_Shop.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.IdentityModel.Tokens;
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
    /// View model for AccountPage
    /// </summary>
    public class AccountViewModel : INotifyPropertyChanged
    {
        #region Singletons   
        private readonly UserSession        _userSession;
        private readonly IDao               _dao;
        private readonly IEventAggregator   _eventAggregator;
        private readonly IFilePickerService _filePickerService;
        private readonly INavigationService _navigationService;
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
        /// <summary>
        /// Display name of the user.
        /// </summary>
        public string NameDisplay
        {
            get { return _nameDisplay; }
            set
            {
                _nameDisplay = value;
                OnPropertyChanged(nameof(NameDisplay));
            }
        }
        /// <summary>
        /// Name of the user.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        /// <summary>
        /// User's email address.
        /// </summary>
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        /// <summary>
        /// User's phone number.
        /// </summary>
        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }
        /// <summary>
        /// User's address.
        /// </summary>
        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }
        /// <summary>
        /// Total number of products purchased by the user.
        /// </summary>
        public int TotalProducts
        {
            get { return totalProducts; }
            set
            {
                totalProducts = value;
                OnPropertyChanged(nameof(TotalProducts));
            }
        }
        /// <summary>
        /// Total number of bills the user has.
        /// </summary>
        public int TotalBills
        {
            get { return totalBills; }
            set
            {
                totalBills = value;
                OnPropertyChanged(nameof(TotalBills));
            }
        }
        /// <summary>
        /// Total money spent by the user.
        /// </summary>
        public int TotalMoneySpent
        {
            get { return totalMoneySpent; }
            set
            {
                totalMoneySpent = value;
                OnPropertyChanged(nameof(TotalMoneySpent));
            }
        }
        /// <summary>
        /// Path to the user's avatar image.
        /// </summary>
        public string AvatarPath
        {

            get => _avatarPath;
            set
            {
                _avatarPath = value;
                OnPropertyChanged(nameof(AvatarPath));
            }
        }
        /// <summary>
        /// User's account creation time.
        /// </summary>
        public string CreateTime
        {
            get => _createTime;
            set
            {
                _createTime = value;
                OnPropertyChanged(nameof(CreateTime));
            }
        }
        /// <summary>
        /// Boolean indicating if the user is in change information mode.
        /// </summary>
        public bool ChangeInforMode
        {
            get => _changeInforMode;
            set
            {
                _changeInforMode = value; 
                OnPropertyChanged(nameof(ChangeInforMode));
            }
        }
        /// <summary>
        /// Boolean indicating if the password change dialog is displayed.
        /// </summary>
        public bool ShowDialogChangePassword
        {
            get => _showDialogChangePassword;
            set
            {
                _showDialogChangePassword = value;
                OnPropertyChanged(nameof(ShowDialogChangePassword));
            }
        }
        /// <summary>
        /// User's current password.
        /// </summary>
        public string PasswordCurrent
        {
            get => _passwordCurrent;
            set
            {
                _passwordCurrent = value;
                OnPropertyChanged(nameof(PasswordCurrent));
            }
        }
        /// <summary>
        /// User's new password.
        /// </summary>
        public string PasswordNew
        {
            get => _passwordNew;
            set
            {
                _passwordNew = value;
                OnPropertyChanged(nameof(PasswordNew));
            }
        }
        /// <summary>
        /// Confirmation of the user's new password.
        /// </summary>
        public string ConfirmPasswordNew
        {
            get => _confirmPasswordNew;
            set
            {
                _confirmPasswordNew = value;
                OnPropertyChanged(nameof(ConfirmPasswordNew));
            }
        }
        /// <summary>
        /// Message related to password change status.
        /// </summary>
        public string ChangePasswordMessage
        {
            get => _changePasswordMessage;
            set
            {
                _changePasswordMessage = value;
                OnPropertyChanged(nameof(ChangePasswordMessage));
            }
        }
        /// <summary>
        /// Message related to information change status.
        /// </summary>
        public string ChangeInfoMessage
        {
            get => _changeInfoMessage;
            set
            {
                _changeInfoMessage = value;
                OnPropertyChanged(nameof(ChangeInfoMessage));
            }
        }
        /// <summary>
        /// Content shown when the user is in change mode (for info or password).
        /// </summary>
        public string ChangeModeContent
        {
            get => _changeModeContent;
            set
            {
                _changeModeContent = value;
                OnPropertyChanged(nameof(ChangeModeContent));
            }
        }
        /// <summary>
        /// User's level
        /// </summary>
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
        /// <summary>
        /// Command to change user information.
        /// </summary>
        public ICommand ChangeInfoModeCommand { get; set; }

        /// <summary>
        /// Command to save user information changes.
        /// </summary>
        public ICommand SaveInfoCommand { get; set; }

        /// <summary>
        /// Command to change the user's avatar.
        /// </summary>
        public ICommand ChangeAvatarCommand { get; set; }

        /// <summary>
        /// Command to change the user's password.
        /// </summary>
        public ICommand ChangePasswordCommand { get; set; }

        /// <summary>
        /// Command to log out the user.
        /// </summary>
        public ICommand LogoutCommand { get; set; }

        /// <summary>
        /// Command to accept the password change request.
        /// </summary>
        public ICommand AcceptChangePasswordCommand { get; set; }

        /// <summary>
        /// Command to refuse the password change request.
        /// </summary>
        public ICommand RefuseChangePasswordCommand { get; set; }

        /// <summary>
        /// Command to go back to the previous page or state.
        /// </summary>
        public ICommand GoBackCommand { get; set; }
        #endregion


        // Constructor
        public AccountViewModel(IDao                dao, 
                                IEventAggregator    eventAggregator, 
                                IFilePickerService  filePickerService,
                                INavigationService  navigationService,
                                UserSession         userSession)
        {
            this._userSession       = userSession;
            this._eventAggregator   = eventAggregator;
            this._filePickerService = filePickerService;
            this._dao               = dao;
            this._navigationService = navigationService;

            loadUserInformation();
            ChangeInforMode = false;

            ChangeInfoModeCommand   = new RelayCommand(changeInfoModeCommand);
            SaveInfoCommand         = new RelayCommand(saveInfoCommand);
            LogoutCommand           = new RelayCommand(logoutCommand);
            ChangeAvatarCommand     = new RelayCommand(changeAvatarCommand);

            AcceptChangePasswordCommand = new RelayCommand(acceptChangePasswordCommand);
            RefuseChangePasswordCommand = new RelayCommand(cancelChangePasswordCommand);

            ChangePasswordCommand = new RelayCommand(changePasswordCommand);

            GoBackCommand = new RelayCommand(() =>
            {
                _navigationService.GoBack();
            });
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
            if (!Email.IsValidEmail() && !Email.IsNullOrEmpty())
            {
                Name = _nameRestore;
                Email = _emailRestore;
                Phone = _phoneRestore;
                Address = _addressRestore;
                changeInfoModeCommand();
                ChangeInfoMessage = "Email không đúng định dạng! Vui lòng nhập đúng định dạng example@mail.com!";
                return;
            }

            if (!Phone.IsValidPhoneNumber() && !Phone.IsNullOrEmpty())
            {
                Name = _nameRestore;
                Email = _emailRestore;
                Phone = _phoneRestore;
                Address = _addressRestore;
                changeInfoModeCommand();
                ChangeInfoMessage = "Số điện thoại không đúng định dạng! Yêu cầu có đúng 10 chữ số!";
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
