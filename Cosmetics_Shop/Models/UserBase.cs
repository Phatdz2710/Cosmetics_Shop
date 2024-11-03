
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Appointments;

namespace Cosmetics_Shop.Models
{
    public abstract class UserBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }

        public UserBase(int id, string name, string token)
        {
            Id = id;
            Name = name;
            Token = token;
        }

        public abstract string DisplayRole();
    }
}
