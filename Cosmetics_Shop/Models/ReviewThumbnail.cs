using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;

namespace Cosmetics_Shop.Models
{
    public class ReviewThumbnail : Review
    {
        public ReviewThumbnail(int id, int userID, string name, string userImage, int starNumber, DateTime ratingDate)
                : base(id, userID, name, userImage, starNumber, ratingDate)
        { }
    }

}
