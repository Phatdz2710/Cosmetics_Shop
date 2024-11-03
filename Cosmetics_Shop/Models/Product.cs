using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models
{
    public abstract class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Image ThumbnailImage { get; set; }
        public int Price { get; set; }
        public string Brand { get; set; }

        public Product(int id, string name, Image thumbnailImage, int price, string brand)
        {
            Id = id;
            Name = name;
            ThumbnailImage = thumbnailImage;
            Price = price;
            Brand = brand;
        }
    }
}
