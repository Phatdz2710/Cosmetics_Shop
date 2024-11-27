using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models
{
    public class UserDetail
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string AvatarPath { get; set; } = "ms-appx:///Assets/avatar_temp.png";
    }
}
