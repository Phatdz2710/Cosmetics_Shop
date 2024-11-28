using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models
{
    public class UserSession
    {
        private User UserInfo { get; set; }

        public void SetUserInfo(User userInfo)
        {
            UserInfo = userInfo;
        }

        public string GetToken()
        {
            return UserInfo.GetToken();
        }

        public string GetRole()
        {
            return UserInfo.GetRole();
        }

        public int GetId()
        {
            return UserInfo.GetId();
        }

        public void Logout()
        {
            UserInfo = null;
        }
    }
}
