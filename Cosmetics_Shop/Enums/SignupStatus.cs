using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Enums
{
    public enum SignupStatus
    {
        Success = 0,
        EmptyUsername = 1,
        EmptyPassword = 2,
        ConfirmPasswordWrong = 3,
        UsernameAlreadyExists = 4,
        ConnectServerFailed = 5
    }
}
