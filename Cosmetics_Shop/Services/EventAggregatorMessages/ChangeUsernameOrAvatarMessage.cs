using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Services.EventAggregatorMessages
{
    public class ChangeUsernameOrAvatarMessage
    {
        public string AvatarPath { get; set; }
        public string Name { get; set; }
    }
}
