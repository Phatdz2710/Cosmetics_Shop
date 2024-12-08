using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Services.EventAggregatorMessages
{
    /// <summary>
    /// Search Event Message
    /// </summary>
    public class SearchEvent
    {
        public string Keyword { get; set; }
    }
}
