using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models
{
    /// <summary>
    /// Store Product Thumbnail Information
    /// </summary>
    public class ProductThumbnail
    {
        /// <summary>
        /// Gets or sets the unique identifier for the product.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the image path of the product.
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Gets or sets the brand of the product.
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Gets or sets the category of the product.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the rating of the product.
        /// </summary>
        public double Rating { get; set; }

        /// <summary>
        /// Gets or sets the stock of the product.
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// Gets or sets the number of units sold.
        /// </summary>
        public int Sold { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductThumbnail"/> class with specified values.
        /// </summary>
        /// <param name="id">The unique identifier for the product.</param>
        /// <param name="name">The name of the product.</param>
        /// <param name="imagePath">The image path of the product.</param>
        /// <param name="price">The price of the product.</param>
        /// <param name="brand">The brand of the product.</param>
        /// <param name="category">The category of the product.</param>
        /// <param name="rating">The rating of the product.</param>
        /// <param name="stock">The stock of the product.</param>
        /// <param name="sold">The number of units sold.</param>
        public ProductThumbnail(int id,
            string name,
            string imagePath,
            int price,
            string brand,
            string category,
            double rating = 0,
            int stock = 0,
            int sold = 0)
        {
            Id = id;
            Name = name;
            ImagePath = imagePath;
            Price = price;
            Brand = brand;
            Category = category;
            Rating = rating;
            Stock = stock;
            Sold = sold;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductThumbnail"/> class with default values.
        /// </summary>
        public ProductThumbnail()
        { }
    }
}