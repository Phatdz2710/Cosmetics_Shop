using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public string UserImage { get; set; }
        public int StarNumber { get; set; }
        public DateTime RatingDate { get; set; }

        public Review(int id, int userID, string name, string userImage, int starNumber, DateTime ratingDate)
        {
            Id = id;
            UserID = userID;
            Name = name;
            UserImage = userImage;
            StarNumber = starNumber;
            RatingDate = ratingDate;
        }
    }
}
