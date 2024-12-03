using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;

namespace Cosmetics_Shop.Models
{
    public struct FilterCheckbox
    { 
        public int      Index           { get; set; }
        public string   Name            { get; set; }
        public ICommand CheckedCommand  { get; set; }
        public bool     IsChecked       { get; set; }
    }
}
