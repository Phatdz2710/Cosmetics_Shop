using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models
{
    /// <summary>
    /// Represents a review for a product.
    /// </summary>
    public class Review
    {
        /// <summary>
        /// Gets or sets the unique identifier for the review.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the product.
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the image of the user.
        /// </summary>
        public string UserImage { get; set; }

        /// <summary>
        /// Gets or sets the star rating given by the user.
        /// </summary>
        public int StarNumber { get; set; }

        /// <summary>
        /// Gets or sets the date when the rating was given.
        /// </summary>
        public DateTime RatingDate { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Review"/> class with specified values.
        /// </summary>
        /// <param name="id">The unique identifier for the review.</param>
        /// <param name="userID">The unique identifier for the user.</param>
        /// <param name="name">The name of the user.</param>
        /// <param name="userImage">The image of the user.</param>
        /// <param name="starNumber">The star rating given by the user.</param>
        /// <param name="ratingDate">The date when the rating was given.</param>
        public Review(int id, int userID, string name, string userImage, int starNumber, DateTime ratingDate)
        {
            ProductID = id;
            UserID = userID;
            Name = name;
            UserImage = userImage;
            StarNumber = starNumber;
            RatingDate = ratingDate;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Review"/> class with specified values.
        /// </summary>
        /// <param name="userID">The unique identifier for the user.</param>
        /// <param name="id">The unique identifier for the product.</param>
        /// <param name="starNumber">The star rating given by the user.</param>
        /// <param name="ratingDate">The date when the rating was given.</param>
        public Review(int userID, int id, int starNumber, DateTime ratingDate)
        {
            UserID = userID;
            ProductID = id;
            StarNumber = starNumber;
            RatingDate = ratingDate;
        }
    }
}