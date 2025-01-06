using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.DataAccessObject.Data
{
    /// <summary>
    /// Represents detailed information of a user.
    /// </summary>
    public class UserDetail
    {
        /// <summary>
        /// The name of the user.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The email of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The phone number of the user.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// The address of the user.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// The account creation time of the user.
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// The file path to the user's avatar image.
        /// </summary>
        public string AvatarPath { get; set; }

        /// <summary>
        /// The total amount of money the user has spent.
        /// Default value is 0.
        /// </summary>
        public int TotalMoneySpent { get; set; } = 0;

        /// <summary>
        /// The total number of bills the user has made.
        /// Default value is 0.
        /// </summary>
        public int TotalBills { get; set; } = 0;

        /// <summary>
        /// The total number of products the user has purchased.
        /// Default value is 0.
        /// </summary>
        public int TotalProducts { get; set; } = 0;
    }

}
