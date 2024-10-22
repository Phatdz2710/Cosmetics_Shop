using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
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

        public event PropertyChangedEventHandler PropertyChanged;

        public LoginViewModel()
        {
            WrongMessage = "";
            MessageVisibility = Visibility.Collapsed;
            LoginSignupState = "Signup";
        }
    }
}
