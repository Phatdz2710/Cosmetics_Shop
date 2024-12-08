using Cosmetics_Shop.Models;
using Cosmetics_Shop.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Services
{
    /// <summary>
    /// Login Result
    /// </summary>
    public struct LoginResult
    {
        public LoginStatus  LoginStatus { get; set; }
        public User         UserInfo    { get; set; }
    }
}
