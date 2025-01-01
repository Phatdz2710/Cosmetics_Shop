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
        public ReviewThumbnail(int productID, int userID, string name, string userImage, int starNumber, DateTime ratingDate)
                : base(productID, userID, name, userImage, starNumber, ratingDate)
        { }

        public ReviewThumbnail(int userID, int productID, int starNumber, DateTime ratingDate)
                : base(userID, productID, starNumber, ratingDate)
        { }

    }

}
