using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;

namespace Cosmetics_Shop.Models
{
    /// <summary>
    /// Represents the details of a product in the cosmetics shop.
    /// </summary>
    public class ProductDetail
    {
        /// <summary>
        /// Gets or sets the unique identifier of the product.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the URL or path of the thumbnail image of the product.
        /// </summary>
        public string ThumbnailImage { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Gets or sets the average review rating of the product.
        /// </summary>
        public double review { get; set; }

        /// <summary>
        /// Gets or sets the number of units sold for the product.
        /// </summary>
        public int sold { get; set; }

        /// <summary>
        /// Gets or sets the available stock amount for the product.
        /// </summary>
        public int availableAmount { get; set; }

        /// <summary>
        /// Gets or sets the category of the product.
        /// </summary>
        public string danhMuc { get; set; }

        /// <summary>
        /// Gets or sets the storage location or warehouse of the product.
        /// </summary>
        public string kho { get; set; }

        /// <summary>
        /// Gets or sets the brand of the product.
        /// </summary>
        public string thuongHieu { get; set; }

        /// <summary>
        /// Gets or sets the shipping origin of the product.
        /// </summary>
        public string guiTu { get; set; }

        /// <summary>
        /// Gets or sets the description of the product.
        /// </summary>
        public string moTa { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDetail"/> class with default values.
        /// </summary>
        public ProductDetail()
        {
            Id = 0;
            Name = "";
            ThumbnailImage = null;
            Price = 0;
            review = 0;
            sold = 0;
            availableAmount = 0;
            danhMuc = "";
            kho = "";
            thuongHieu = "";
            guiTu = "";
            moTa = "";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDetail"/> class with specified values.
        /// </summary>
        /// <param name="id">The unique identifier of the product.</param>
        /// <param name="name">The name of the product.</param>
        /// <param name="thumbnailImage">The thumbnail image URL or path.</param>
        /// <param name="price">The price of the product.</param>
        /// <param name="revie">The average review rating.</param>
        /// <param name="sol">The number of units sold.</param>
        /// <param name="available">The available stock amount.</param>
        /// <param name="danhMu">The category of the product.</param>
        /// <param name="kh">The storage location or warehouse.</param>
        /// <param name="thuongHie">The brand of the product.</param>
        /// <param name="guiT">The shipping origin of the product.</param>
        /// <param name="moT">The description of the product.</param>
        public ProductDetail(int id, string name, string thumbnailImage, int price, double revie, int sol,
            int available, string danhMu, string kh, string thuongHie, string guiT, string moT)
        {
            Id = id;
            Name = name;
            ThumbnailImage = thumbnailImage;
            Price = price;
            review = revie;
            sold = sol;
            availableAmount = available;
            danhMuc = danhMu;
            kho = kh;
            thuongHieu = thuongHie;
            guiTu = guiT;
            moTa = moT;
        }
    }
}
