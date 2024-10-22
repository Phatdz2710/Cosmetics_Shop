using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models
{
    public class User : UserBase
    {
        public User(int id, string name) : base(id, name)
        { }

        public override string DisplayRole()
        {
            return "User";
        }
    }
}
