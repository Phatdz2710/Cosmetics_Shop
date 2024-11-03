using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models
{
    public class User : UserBase
    {
        public User(int id, string name, string token) 
            : base(id, name, token)
        { }

        public override string DisplayRole()
        {
            return "User";
        }
    }
}
