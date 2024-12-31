using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models
{
    /// <summary>
    /// Store Account Information
    /// </summary>
    public class Account
    {
        public int      ID          { get; set; }
        public string   Username    { get; set; }
        public string   Password    { get; set; }
        public string   Role        { get; set; }
        public int      UserID      { get; set; }

        public Account()
        {
            ID = 0;
            Username = "";
            Password = "";
            Role = "";
        }

        public Account(int id, string username, string password, string role, int userID)
        {
            ID = id;
            Username = username;
            Password = password;
            Role = role;
            UserID = userID;
        }
    }
}
