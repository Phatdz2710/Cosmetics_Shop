using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.DataAccessObject.Data
{
    /// <summary>
    /// User Detail (Information of user
    /// </summary>
    public class UserDetail
    {
        public string   Name    { get; set; } // Name of user
        public string   Email   { get; set; } // Email of user
        public string   Phone   { get; set; } // Phone number of user
        public string   Address { get; set; } // Address of user
        public DateTime CreateTime { get; set; } // Create time of user
        public string   AvatarPath      { get; set; } // Avatar path of user
        public int      TotalMoneySpent { get; set; } = 0; // Total money spent
        public int      TotalBills      { get; set; } = 0; // Total bills
        public int      TotalProducts   { get; set; } = 0; // Total products
        
    }
}
