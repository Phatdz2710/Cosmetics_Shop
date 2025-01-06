using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Services.EventAggregatorMessages
{
    /// <summary>
    /// Represents a message used to notify changes in the user's username or avatar.
    /// </summary>
    public class ChangeUsernameOrAvatarMessage
    {
        /// <summary>
        /// Gets or sets the file path of the new avatar image.
        /// </summary>
        public string AvatarPath { get; set; }

        /// <summary>
        /// Gets or sets the new username of the user.
        /// </summary>
        public string Name { get; set; }
    }
}
