
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Appointments;

namespace Cosmetics_Shop.Models
{
    /// <summary>
    /// User information
    /// </summary>
    public class User
    {
        private int     id      { get; set; }
        private string  role    { get; set; }
        private string  token   { get; set; }

        // Constructor
        public User(int id, string token, string role)
        {
            this.id     = id;
            this.token  = token;
            this.role   = role;
        }

        /// <summary>
        /// Get role of user
        /// </summary>
        /// <returns></returns>
        public string GetRole()
        {
            return role;
        }

        /// <summary>
        /// Get token of user
        /// </summary>
        /// <returns></returns>
        public string GetToken()
        {
            return role;
        }


        /// <summary>
        /// Get id of user
        /// </summary>
        /// <returns></returns>
        public int GetId()
        {
            return id;
        }

    }
}
