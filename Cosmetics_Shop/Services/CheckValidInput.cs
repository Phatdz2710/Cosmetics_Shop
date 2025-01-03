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
    /// Provides methods to check if the input is valid.
    /// </summary>
    public static class CheckValidInput
    {
        /// <summary>
        /// Checks if the input is a valid email.
        /// </summary>
        /// <param name="email">The email address to validate.</param>
        /// <returns>
        /// True if the input is a valid email, otherwise false.
        /// </returns>
        public static bool IsValidEmail(this string email)
        {
            if (email.IsNullOrEmpty()) return false;
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        /// <summary>
        /// Checks if the input is a valid phone number.
        /// </summary>
        /// <param name="phoneNumber">The phone number to validate.</param>
        /// <returns>
        /// True if the input is a valid phone number, otherwise false.
        /// </returns>
        public static bool IsValidPhoneNumber(this string phoneNumber)
        {
            if (phoneNumber.IsNullOrEmpty()) return false;
            string pattern = @"^\d{10}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }

        /// <summary>
        /// Checks if the input is a valid address.
        /// </summary>
        /// <param name="address">The address to validate.</param>
        /// <returns>
        /// True if the input is a valid address, otherwise false.
        /// </returns>
        public static bool IsValidAddress(this string address)
        {
            // Pattern requires at least one letter, allows numbers and slashes
            string pattern = @"^(?=.*[a-zA-Z])[a-zA-Z0-9/ ]+$";
            return !string.IsNullOrEmpty(address) && Regex.IsMatch(address, pattern);
        }
    }
}