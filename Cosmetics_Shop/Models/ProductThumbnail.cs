using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models
{
    public class ProductThumbnail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageThumbnalPath { get; set; }
        public int Price { get; set; }
        public string Brand { get; set; }
        public double Rating { get; set; }

        public ProductThumbnail(int id, string name, string imageThumbnailPath, int price, string brand, double rating = 0)
        {
            Id = id;
            Name = name;
            ImageThumbnalPath = imageThumbnailPath;
            Price = price;
            Brand = brand;
            Rating = rating;
        }
    }
}
