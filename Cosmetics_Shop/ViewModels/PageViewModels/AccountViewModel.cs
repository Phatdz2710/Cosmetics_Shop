using CommunityToolkit.Mvvm.Input;
using Cosmetics_Shop.Models;
using Cosmetics_Shop.Models.DataService;
using Cosmetics_Shop.Services;
using Cosmetics_Shop.Services.EventAggregatorMessages;
using Cosmetics_Shop.Services.Interfaces;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cosmetics_Shop.ViewModels.PageViewModels
{
    public class AccountViewModel : INotifyPropertyChanged
    {
        private UserSession _userSession;
        private readonly IDao _dao;
        private readonly IEventAggregator _eventAggregator;

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

        public bool ChangeInforMode
        {
            get => _changeInforMode;
            set
            {
                _changeInforMode = value; 
                OnPropertyChanged(nameof(ChangeInforMode));
            }
        }

        public ICommand ChangeInfoModeCommand { get; set; }
        public ICommand ChangeAvatarCommand { get; set; }
        public ICommand LogoutCommand { get; set; }

        public AccountViewModel(UserSession userSession, IDao dao, IEventAggregator eventAggregator)
        {
            this._userSession = userSession;
            this._eventAggregator = eventAggregator;
            this._dao = dao;

            loadUserInformation();
            ChangeInforMode = false;

            ChangeInfoModeCommand = new RelayCommand(() =>
            {
                ChangeInforMode = !ChangeInforMode;
            });

            LogoutCommand = new RelayCommand(() =>
            {
                _userSession.Logout();
                _eventAggregator.Publish(new LogoutMessage());
            });
        }

        private async void loadUserInformation()
        {
            var userDetail = await _dao.GetUserDetailAsync(_userSession.GetId());
            Name = userDetail.Name;
            NameDisplay = userDetail.Name;
            Email = userDetail.Email;
            Phone = userDetail.Phone;
            Address = userDetail.Address;
            AvatarPath = userDetail.AvatarPath;

            TotalProducts = 100;
            TotalBills = 10;
            TotalMoneySpent = 7890000;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
