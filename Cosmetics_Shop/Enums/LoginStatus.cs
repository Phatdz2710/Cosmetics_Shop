using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models.Enums
{
 
    /// <summary>
    /// Enum representing the status of a login attempt.
    /// Success: The login was successful.
    /// InvalidUsername: The username is invalid.
    /// InvalidPassword: The password is invalid.
    /// ConnectServerFailed: The connection to the server failed.
    /// </summary>
    public enum LoginStatus
    {
        Success         = 0, // The login was successful.
        InvalidUsername = 1, // The username is invalid.
        InvalidPassword = 2, // The password is invalid.
        ConnectServerFailed = 3 // The connection to the server failed.
    }
}
