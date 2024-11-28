
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Appointments;

namespace Cosmetics_Shop.Models
{
    public class User
    {
        private int id { get; set; }
        private string role { get; set; }
        private string token { get; set; }

        public User(int id, string token, string role)
        {
            this.id = id;
            this.token = token;
            this.role = role;
        }

        public string GetRole()
        {
            return role;
        }
        public string GetToken()
        {
            return role;
        }

        public int GetId()
        {
            return id;
        }

    }
}
