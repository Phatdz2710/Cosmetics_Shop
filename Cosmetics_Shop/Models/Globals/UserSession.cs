using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models
{
    /// <summary>
    /// Stores user information in the session.
    /// </summary>
    public class UserSession
    {
        /// <summary>
        /// Gets or sets the user information.
        /// </summary>
        private User UserInfo { get; set; }

        /// <summary>
        /// Sets the user information.
        /// </summary>
        /// <param name="userInfo">The user information to set.</param>
        public void SetUserInfo(User userInfo)
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// Gets the token of the user.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> that represents the token of the user.
        /// </returns>
        public string GetToken()
        {
            return UserInfo.GetToken();
        }

        /// <summary>
        /// Gets the role of the user.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> that represents the role of the user.
        /// </returns>
        public string GetRole()
        {
            return UserInfo.GetRole();
        }

        /// <summary>
        /// Gets the ID of the user.
        /// </summary>
        /// <returns>
        /// An <see cref="int"/> that represents the ID of the user.
        /// </returns>
        public int GetId()
        {
            return UserInfo.GetId();
        }

        /// <summary>
        /// Clears the user information when the user logs out.
        /// </summary>
        public void Logout()
        {
            UserInfo = null;
        }
    }
}