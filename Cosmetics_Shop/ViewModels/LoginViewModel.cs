using CommunityToolkit.Mvvm.Input;
using Cosmetics_Shop.Models.DataService;
using Cosmetics_Shop.Services;
using Cosmetics_Shop.Services.Interfaces;
using Cosmetics_Shop.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.Windows.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
        private Visibility messageVisibility;
        private string loginSignupState;


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
        public Visibility MessageVisibility
        {
            get => messageVisibility;
            set
            {
                messageVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MessageVisibility)));
            }
        }
        public string LoginSignupState
        {
            get => loginSignupState;
            set
            {
                loginSignupState = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LoginSignupState)));
            }
        }
        public string Username { get; set; }
        public string Password { get; set; }
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
            MessageVisibility = Visibility.Collapsed;
            LoginSignupState = "Login";

            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            Username = localSettings.Values["Username"] as string;
            Password = localSettings.Values["Password"] as string;
            RememberMe = localSettings.Values["Username"] as string != "";

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
                WrongMessage = "Signup successfully!";
            });
        }

        public void CheckLogin()
        {
            var loginResult = _dao.CheckLogin(Username, Password);
            switch ((int)loginResult)
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

                    var mainWindow = new MainWindow();
                    mainWindow.Activate();
                    _eventAggregator.Publish(new CloseWindowMessage());
                    break;
                case 1:
                    WrongMessage = "Username does not exist!";
                    break;
                case 2:
                    WrongMessage = "Wrong password!";
                    break;
                default:
                    break;
            }
        }
    }
}
