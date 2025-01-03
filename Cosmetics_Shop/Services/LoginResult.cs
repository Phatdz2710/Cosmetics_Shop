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
    /// Result of a login attempt.
    /// </summary>
    public struct LoginResult
    {
        /// <summary>
        /// Gets or sets the status of the login attempt.
        /// </summary>
        public LoginStatus LoginStatus { get; set; }

        /// <summary>
        /// Gets or sets the user information if the login was successful.
        /// </summary>
        public User UserInfo { get; set; }
    }
}