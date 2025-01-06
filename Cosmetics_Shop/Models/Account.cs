using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models
{
    /// <summary>
    /// Stores account information.
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Gets or sets the unique identifier for the account.
        /// </summary>
        public int ID { get; set; } // Id of account

        /// <summary>
        /// Gets or sets the username of the account.
        /// </summary>
        public string Username { get; set; } // Username of account 

        /// <summary>
        /// Gets or sets the password of the account.
        /// </summary>
        public string Password { get; set; } // Password of account

        /// <summary>
        /// Gets or sets the role of the account.
        /// </summary>
        public string Role { get; set; } // Role of account

        /// <summary>
        /// Gets or sets the unique identifier for the user associated with the account.
        /// </summary>
        public int UserID { get; set; } // Id of user

        /// <summary>
        /// Initializes a new instance of the <see cref="Account"/> class with default values.
        /// </summary>
        public Account()
        {
            ID = 0;
            Username = "";
            Password = "";
            Role = "";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Account"/> class with specified values.
        /// </summary>
        /// <param name="id">The unique identifier for the account.</param>
        /// <param name="username">The username of the account.</param>
        /// <param name="password">The password of the account.</param>
        /// <param name="role">The role of the account.</param>
        /// <param name="userID">The unique identifier for the user associated with the account.</param>
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
