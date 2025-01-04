using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;

namespace Cosmetics_Shop.Models
{
    /// <summary>
    /// Store product information
    /// </summary>
    public class ProductDetail
    {
        public int      Id              { get; set; }
        public string   Name            { get; set; }
        public string   ThumbnailImage  { get; set; }
        public int      Price           { get; set; }
        public double   review          { get; set; }
        public int      sold            { get; set; }
        public int      availableAmount { get; set; }
        public string   danhMuc         { get; set; }
        public string   kho             { get; set; }
        public string   thuongHieu      { get; set; }
        public string   guiTu           { get; set; }
        public string   moTa            { get; set; }

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
        public ProductDetail(int id, string name, string thumbnailImage, int price,double revie, int sol, 
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
