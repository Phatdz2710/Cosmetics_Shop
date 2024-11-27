using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models
{
    public class UserSession
    {
        private UserBase UserInfo { get; set; }

        public void SetUserInfo(UserBase userInfo)
        {
            UserInfo = userInfo;
        }

        public string GetToken()
        {
            return UserInfo.Token;
        }

        public string GetRole()
        {
            return UserInfo.DisplayRole();
        }

        public string GetName()
        {
            return UserInfo.Name;
        }

        public int GetId()
        {
            return UserInfo.Id;
        }

        public void Logout()
        {
            UserInfo = null;
        }
    }
}
