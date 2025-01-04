using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models
{
    /// <summary>
    /// This class is used to create a review object
    /// </summary>
    public class Review
    {
        public int Id { get; set; }
        public int ProductID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public string UserImage { get; set; }
        public int StarNumber { get; set; }
        public DateTime RatingDate { get; set; }

        public Review(int id, int userID, string name, string userImage, int starNumber, DateTime ratingDate)
        {
            ProductID = id;
            UserID = userID;
            Name = name;
            UserImage = userImage;
            StarNumber = starNumber;
            RatingDate = ratingDate;
        }


        public Review(int userID, int id, int starNumber, DateTime ratingDate)
        {
            UserID = userID;
            ProductID = id;
            StarNumber = starNumber;
            RatingDate = ratingDate;
        }
    }
}
