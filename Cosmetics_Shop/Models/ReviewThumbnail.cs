using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;

namespace Cosmetics_Shop.Models
{
    /// <summary>
    /// Represents a thumbnail view of a review, inheriting from the Review class.
    /// </summary>
    public class ReviewThumbnail : Review
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReviewThumbnail"/> class with specified values.
        /// </summary>
        /// <param name="productID">The unique identifier for the product.</param>
        /// <param name="userID">The unique identifier for the user.</param>
        /// <param name="name">The name of the user.</param>
        /// <param name="userImage">The image of the user.</param>
        /// <param name="starNumber">The star rating given by the user.</param>
        /// <param name="ratingDate">The date when the rating was given.</param>
        public ReviewThumbnail(int productID, int userID, string name, string userImage, int starNumber, DateTime ratingDate)
                : base(productID, userID, name, userImage, starNumber, ratingDate)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReviewThumbnail"/> class with specified values.
        /// </summary>
        /// <param name="userID">The unique identifier for the user.</param>
        /// <param name="productID">The unique identifier for the product.</param>
        /// <param name="starNumber">The star rating given by the user.</param>
        /// <param name="ratingDate">The date when the rating was given.</param>
        public ReviewThumbnail(int userID, int productID, int starNumber, DateTime ratingDate)
                : base(userID, productID, starNumber, ratingDate)
        { }

    }

}