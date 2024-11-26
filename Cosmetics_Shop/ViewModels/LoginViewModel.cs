using CommunityToolkit.Mvvm.Input;
using Cosmetics_Shop.Enums;
using Cosmetics_Shop.Models;
using Cosmetics_Shop.Models.DataService;
using Cosmetics_Shop.Services;
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
    public class LoginViewModel : INotifyPropertyChanged
    {
        // Data access object
        private readonly IDao _dao = null;
        // Event aggregator for publish and subscribe
        private readonly IEventAggregator _eventAggregator;

        private string wrongMessage;
        private string successMessage;
        private Visibility messageVisibility;
        private string loginSignupState;
        private bool isEnabled = true; // Logged in

        // Message about login failed
        public string WrongMessage
        {
            get => wrongMessage;
            set
            {
                wrongMessage = value;
                this.MessageVisibility = Visibility.Visible;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WrongMessage)));
            }
        }
        public string SuccessMessage
        {
            get => successMessage;
            set
            {
                successMessage = value;
                this.MessageVisibility = Visibility.Visible;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SuccessMessage)));
            }
        }

        // Show message
        public Visibility MessageVisibility
        {
            get => messageVisibility;
            set
            {
                messageVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MessageVisibility)));
            }
        }

        // Switch between login and signup mode
        public string LoginSignupState
        {
            get => loginSignupState;
            set
            {
                loginSignupState = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LoginSignupState)));
            }
        }

        public bool IsEnabled
        {
            get => isEnabled;
            set
            {
                isEnabled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsEnabled)));
            }
        }
        public string Username { get; set; }
        public string Password { get; set; }

        public string UsernameSignup { get; set; }
        public string PasswordSignup { get; set; }
        public string ConfirmPasswordSingup { get; set; }
        public string Email { get; set; }



        public bool RememberMe { get; set; } = true;

        // Commands
        public ICommand SwitchLoginCommand { get; set; }
        public ICommand SwitchSignupCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand SignupCommand { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        public LoginViewModel(IEventAggregator eventAggregator,
                                IDao dao)
        {
            _dao = dao;
            _eventAggregator = eventAggregator;

            WrongMessage = "";
            SuccessMessage = "";
            MessageVisibility = Visibility.Collapsed;
            LoginSignupState = "Login";

            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            Username = localSettings.Values["Username"] as string;
            Password = localSettings.Values["Password"] as string;
            RememberMe = localSettings.Values["RememberMe"] as string != "";

            SwitchLoginCommand = new RelayCommand(() =>
            {
                LoginSignupState = "Login";
                MessageVisibility = Visibility.Collapsed;
            });

            SwitchSignupCommand = new RelayCommand(() =>
            {
                LoginSignupState = "Signup";
                MessageVisibility = Visibility.Collapsed;
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

        public async void CheckLogin()
        {
            IsEnabled = false;
            LoginResult loginResult = await _dao.CheckLoginAsync(Username, Password);
            IsEnabled = true;

            switch ((int)loginResult.LoginStatus)
            {
                case 0:
                    // Local store username and password'
                    var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                    if (RememberMe)
                    {
                        localSettings.Values["Username"] = Username;
                        localSettings.Values["Password"] = Password;
                        localSettings.Values["RememberMe"] = "Remember";
                    }
                    else
                    {
                        localSettings.Values["Username"] = "";
                        localSettings.Values["Password"] = "";
                        localSettings.Values["RememberMe"] = "";
                    }

                    // Set Info to UserSession
                    var userSession = App.ServiceProvider.GetService(typeof(UserSession)) as UserSession;
                    
                    if (loginResult.UserInfo.DisplayRole() == "Admin")
                    {
                        userSession.SetUserInfo(new Admin(loginResult.UserInfo.Id, loginResult.UserInfo.Name, loginResult.UserInfo.Token));
                    }
                    else
                    {
                        userSession.SetUserInfo(new User(loginResult.UserInfo.Id, loginResult.UserInfo.Name, loginResult.UserInfo.Token));
                    }

                    var mainWindow = new MainWindow();
                    mainWindow.Activate();
                    _eventAggregator.Publish(new CloseWindowMessage());
                    break;
                case 1:
                    WrongMessage = "Tên đăng nhập không tồn tại !";
                    break;
                case 2:
                    WrongMessage = "Sai mật khẩu !";
                    break;
                case 3:
                    WrongMessage = "Vui lòng kiểm tra lại kết nối của bạn !";
                    break;
                default:
                    break;
            }
        }

        public async void DoSignup()
        {
            SuccessMessage = "";
            WrongMessage = "";
            MessageVisibility = Visibility.Collapsed;
            IsEnabled = false;
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
    }
}
