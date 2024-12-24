using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Enums
{
    /// <summary>
    /// Enum representing the status of a signup attempt.
    /// Success: The signup was successful.
    /// EmptyUsername: The username is empty.
    /// EmptyPassword: The password is empty.
    /// ConfirmPasswordWrong: The confirm password is wrong.
    /// UsernameAlreadyExists: The username already exists.
    /// ConnectServerFailed: The connection to the server failed.
    /// </summary>
    public enum SignupStatus
    {
        Success         = 0,
        EmptyUsername   = 1,
        EmptyPassword   = 2,
        ConfirmPasswordWrong    = 3,
        UsernameAlreadyExists   = 4,
        ConnectServerFailed     = 5,
        InvalidEmail           = 6
    }
}
