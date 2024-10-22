
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models
{
    public abstract class UserBase
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public UserBase(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public abstract string DisplayRole();
    }
}
