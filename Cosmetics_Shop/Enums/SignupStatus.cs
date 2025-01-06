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
        Success         = 0, // The signup was successful.
        EmptyUsername   = 1, // The username is empty.
        EmptyPassword   = 2, // The password is empty.
        ConfirmPasswordWrong    = 3, // The confirm password is wrong.
        UsernameAlreadyExists   = 4, // The username already exists.
        ConnectServerFailed     = 5, // The connection to the server failed.
        InvalidEmail            = 6 // The email is invalid.
    }
}
