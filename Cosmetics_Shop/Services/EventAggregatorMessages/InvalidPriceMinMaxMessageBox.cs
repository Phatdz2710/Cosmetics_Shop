using Cosmetics_Shop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Services.EventAggregatorMessages
{
    public class InvalidPriceMinMaxMessageBox
    {
        public string Message { get; set; }
    }
}
