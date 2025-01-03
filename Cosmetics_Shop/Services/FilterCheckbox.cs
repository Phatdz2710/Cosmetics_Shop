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
    /// <summary>
    /// Represents a filter checkbox with an index, name, command, and checked state.
    /// </summary>
    public struct FilterCheckbox
    {
        /// <summary>
        /// Gets or sets the index of the filter checkbox.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets the name of the filter checkbox.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the command to execute when the checkbox is checked.
        /// </summary>
        public ICommand CheckedCommand { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the checkbox is checked.
        /// </summary>
        public bool IsChecked { get; set; }
    }
}