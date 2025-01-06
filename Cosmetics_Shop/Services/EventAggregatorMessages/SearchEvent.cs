using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Services.EventAggregatorMessages
{
    /// <summary>
    /// Request to search for a keyword.
    /// </summary>
    public class SearchEvent
    {
        /// <summary>
        /// Gets or sets the keyword to search for.
        /// </summary>
        public string Keyword { get; set; }
    }
}