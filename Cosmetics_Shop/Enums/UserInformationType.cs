using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Enums
{
    /// <summary>
    /// Enum representing the type of user information.
    /// Name: The name of the user.
    /// Email: The email of the user.
    /// Phone: The phone number of the user.
    /// Address: The address of the user.
    /// AvatarPath: The path to the avatar of the user.
    /// TotalMoneySpent: The total money spent by the user.
    /// TotalBills: The total number of bills of the user.
    /// TotalProducts: The total number of products bought by the user.
    /// </summary>
    public enum UserInformationType
    {
        Name, // The name of the user.
        Email, // The email of the user.
        Phone, // The phone number of the user.
        Address, // The address of the user.
        AvatarPath, // The path to the avatar of the user.
        TotalMoneySpent, // The total money spent by the user.
        TotalBills, // The total number of bills of the user.
        TotalProducts // The total number of products bought by the user.
    }
}
