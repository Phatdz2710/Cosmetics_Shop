using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models
{
    // Use this class to store user information when you get data from Database
    public class UserDetail
    {
        public string   Name    { get; set; }
        public string   Email   { get; set; }
        public string   Phone   { get; set; }
        public string   Address { get; set; }
        public string   AvatarPath      { get; set; }
        public int      TotalMoneySpent { get; set; } = 0;
        public int      TotalBills      { get; set; } = 0;
        public int      TotalProducts   { get; set; } = 0;
        
    }
}
