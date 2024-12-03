using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
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
        public int      Id      { get; set; }
        public string   Name    { get; set; }
        public string   ImagePath   { get; set; }
        public int      Price       { get; set; }
        public string   Brand       { get; set; }
        public double   Rating      { get; set; }
        public int      Stock       { get; set; }
        public int      Sold        { get; set; }

        public ProductThumbnail(int id, 
            string  name, 
            string  imagePath, 
            int     price, 
            string  brand, 
            double  rating  = 0, 
            int     stock   = 0, 
            int     sold    = 0)
        {
            Id          = id;
            Name        = name;
            ImagePath   = imagePath;
            Price       = price;
            Brand       = brand;
            Rating      = rating;
            Stock       = stock;
            Sold        = sold;
        }

        public ProductThumbnail() 
        { }
    }
}
