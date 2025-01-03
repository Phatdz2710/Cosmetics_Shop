using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Services
{
    /// <summary>
    /// Represents the status display with properties for border brush, foreground color, and text.
    /// </summary>
    public class StatusDisplay
    {
        /// <summary>
        /// Gets or sets the brush used to draw the border of the status display.
        /// </summary>
        public SolidColorBrush BorderBrush { get; set; }

        /// <summary>
        /// Gets or sets the brush used to draw the foreground of the status display.
        /// </summary>
        public SolidColorBrush Foreground { get; set; }

        /// <summary>
        /// Gets or sets the text displayed in the status display.
        /// </summary>
        public string Text { get; set; }
    }
}