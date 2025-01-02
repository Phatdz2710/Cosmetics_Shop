using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Services
{
    /// <summary>
    /// Check if the input is valid
    /// </summary>
    public static class CheckValidInput
    {
        /// <summary>
        /// Check if the input is a valid email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>
        /// True if the input is a valid email, otherwise false
        /// </returns>
        public static bool IsValidEmail(this string email)
        {
            if (email.IsNullOrEmpty()) return false;
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        /// <summary>
        /// Check if the input is a valid phone number
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns>
        /// True if the input is a valid phone number, otherwise false
        /// </returns>
        public static bool IsValidPhoneNumber(this string phoneNumber)
        {
            string pattern = @"^\d{10}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }
    }
}
