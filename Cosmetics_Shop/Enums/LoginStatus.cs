using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models.Enums
{
 
    /// <summary>
    /// Login Status
    /// </summary>
    public enum LoginStatus
    {
        Success         = 0,
        InvalidUsername = 1,
        InvalidPassword = 2,
        ConnectServerFailed = 3
    }
}
