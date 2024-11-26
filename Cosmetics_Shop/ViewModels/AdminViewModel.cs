using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cosmetics_Shop.Models;
using System.ComponentModel;

namespace Cosmetics_Shop.ViewModels
{
    public class AdminViewModel : INotifyPropertyChanged
    {
        private Admin _admin;

        public AdminViewModel(Admin admin)
        {
            _admin = admin;
        }

        public string AdminRole => _admin.DisplayRole();

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
