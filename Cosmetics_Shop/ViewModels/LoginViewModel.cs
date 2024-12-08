using CommunityToolkit.Mvvm.Input;
using Cosmetics_Shop.DataAccessObject.Interfaces;
using Cosmetics_Shop.Enums;
using Cosmetics_Shop.Models;
using Cosmetics_Shop.Models.Enums;
using Cosmetics_Shop.Services;
using Cosmetics_Shop.Services.EventAggregatorMessages;
using Cosmetics_Shop.Services.Interfaces;
using Cosmetics_Shop.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using Microsoft.UI.Xaml;
using Microsoft.Windows.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cosmetics_Shop.ViewModels
{
    /// <summary>
    /// View model for login window
    /// </summary>
    public class LoginViewModel : INotifyPropertyChanged
    {
        #region Singleton
        // Data access object
        private readonly IDao _dao = null;
        // Event Aggregator
        private readonly IEventAggregator _eventAggregator;
        // Service provider
        private readonly IServiceProvider _serviceProvider;
        #endregion

        #region Fields
        private string      wrongMessage;
        private string      successMessage;
        private Visibility  messageVisibility;
        private string      loginSignupState;
        private bool        isEnabled = true; // Logged in
        #endregion

        #region Properties for binding
        // Message about login failed
        public string WrongMessage
        {
            get => wrongMessage;
            set
            {
                wrongMessage = value;
                this.MessageVisibility = Visibility.Visible;
                OnPropertyChanged(nameof(WrongMessage));
            }
        }
        // Message about login success
        public string SuccessMessage
        {
            get => successMessage;
            set
            {
                successMessage = value;
                this.MessageVisibility = Visibility.Visible;
                OnPropertyChanged(nameof(SuccessMessage));
            }
        }

        // Show message
        public Visibility MessageVisibility
        {
            get => messageVisibility;
            set
            {
                messageVisibility = value;
                OnPropertyChanged(nameof(MessageVisibility));
            }
        }

        // Switch between login and signup mode
        public string LoginSignupState
        {
            get => loginSignupState;
            set
            {
                loginSignupState = value;
                OnPropertyChanged(nameof(LoginSignupState));
            }
        }

        // Enable or disable login button
        public bool IsEnabled
        {
            get => isEnabled;
            set
            {
                isEnabled = value;
                OnPropertyChanged(nameof(IsEnabled));
            }
        }

        // For binding two-way
        public string Username { get; set; }
        public string Password { get; set; }
        public string UsernameSignup { get; set; }
        public string PasswordSignup { get; set; }
        public string ConfirmPasswordSingup { get; set; }
        public string Email { get; set; }
        public bool RememberMe { get; set; } = true;

        #endregion

        #region Commands
        // Commands
        public ICommand SwitchLoginCommand  { get; set; }
        public ICommand SwitchSignupCommand { get; set; }
        public ICommand LoginCommand        { get; set; }
        public ICommand SignupCommand       { get; set; }
        #endregion

        // Constructor
        public LoginViewModel(IEventAggregator  eventAggregator,
                              IDao              dao,
                              IServiceProvider  serviceProvider)
        {
            _dao                = dao;
            _eventAggregator    = eventAggregator;
            _serviceProvider    = serviceProvider;

            WrongMessage        = "";
            SuccessMessage      = "";
            MessageVisibility   = Visibility.Collapsed;
            LoginSignupState    = "Login";

            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            Username    = localSettings.Values["Username"] as string;
            Password    = localSettings.Values["Password"] as string;
            RememberMe  = localSettings.Values["RememberMe"] as string != "";

            SwitchLoginCommand = new RelayCommand(() =>
            {
                LoginSignupState    = "Login";
                MessageVisibility   = Visibility.Collapsed;
            });

            SwitchSignupCommand = new RelayCommand(() =>
            {
                LoginSignupState    = "Signup";
                MessageVisibility   = Visibility.Collapsed;
            });

            LoginCommand = new RelayCommand(() =>
            {
                CheckLogin();
            });

            SignupCommand = new RelayCommand(() =>
            {
                DoSignup();
            });
        }

        /// <summary>
        /// Do login with username and password
        /// </summary>
        public async void CheckLogin()
        {
            IsEnabled = false;
            LoginResult loginResult = await _dao.CheckLoginAsync(Username, Password);
            IsEnabled = true;

            switch (loginResult.LoginStatus)
            {
                case LoginStatus.Success:
                    // Local store username and password'
                    var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                    if (RememberMe)
                    {
                        localSettings.Values["Username"]    = Username;
                        localSettings.Values["Password"]    = Password;
                        localSettings.Values["RememberMe"]  = "Remember";
                    }
                    else
                    {
                        localSettings.Values["Username"]    = "";
                        localSettings.Values["Password"]    = "";
                        localSettings.Values["RememberMe"]  = "";
                    }

                    // Set Info to UserSession
                    var userSession = _serviceProvider.GetService(typeof(UserSession)) as UserSession;

                    // Set user info
                    userSession.SetUserInfo(new User(loginResult.UserInfo.GetId(),
                                                     loginResult.UserInfo.GetToken(),
                                                     loginResult.UserInfo.GetRole()));

                    // After lged in, switch to main window
                    var mainWindow = new MainWindow();
                    mainWindow.Activate();

                    // Request to close login window
                    _eventAggregator.Publish(new CloseLoginWindowMessage());
                    break;

                case LoginStatus.InvalidUsername:
                    WrongMessage = "Tên đăng nhập không tồn tại !";
                    break;

                case LoginStatus.InvalidPassword:
                    WrongMessage = "Sai mật khẩu !";
                    break;

                case LoginStatus.ConnectServerFailed:
                    WrongMessage = "Vui lòng kiểm tra lại kết nối của bạn !";
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Do signup
        /// </summary>
        public async void DoSignup()
        {
            SuccessMessage      = "";
            WrongMessage        = "";
            MessageVisibility   = Visibility.Collapsed;
            IsEnabled       = false;
            var loginStatus = await _dao.DoSignupAsync(UsernameSignup, PasswordSignup, ConfirmPasswordSingup, Email);
            IsEnabled = true;

            switch(loginStatus)
            {
                case SignupStatus.Success:
                    SuccessMessage = "Tạo tài khoản thành công !";
                    break;
                case SignupStatus.UsernameAlreadyExists:
                    WrongMessage = "Tên đăng nhập đã tồn tại !";
                    break;
                case SignupStatus.EmptyUsername:
                    WrongMessage = "Tên đăng nhập không được bỏ trống !";
                    break;
                case SignupStatus.EmptyPassword:
                    WrongMessage = "Mật khẩu không được bỏ trống !";
                    break;
                case SignupStatus.ConfirmPasswordWrong:
                    WrongMessage = "Mật khẩu không trùng khớp !";
                    break;
                case SignupStatus.ConnectServerFailed:
                    WrongMessage = "Vui lòng kiểm tra lại kết nối của bạn !";
                    break;
                default:
                    break;
                
            }
        }


        // For INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
