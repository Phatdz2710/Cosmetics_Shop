using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models
{
    /// <summary>
    /// Store User Information in Session
    /// </summary>
    public class UserSession
    {
        private User UserInfo { get; set; }

        public void SetUserInfo(User userInfo)
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// Get Token of User
        /// </summary>
        /// <returns></returns>
        public string GetToken()
        {
            return UserInfo.GetToken();
        }

        /// <summary>
        /// Get Role of User
        /// </summary>
        /// <returns></returns>
        public string GetRole()
        {
            return UserInfo.GetRole();
        }

        /// <summary>
        /// Get Id of User
        /// </summary>
        /// <returns></returns>
        public int GetId()
        {
            return UserInfo.GetId();
        }

        /// <summary>
        /// Clear user information when user logout
        /// </summary>
        public void Logout()
        {
            UserInfo = null;
        }
    }
}
