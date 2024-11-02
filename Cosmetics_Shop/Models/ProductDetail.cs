using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.ComponentModel;
=======
>>>>>>> ad4311b5a9c311e96d94838537af8decb763063a
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;

namespace Cosmetics_Shop.Models
{
    public class ProductDetail
    {
        public int Id { get; set; }
<<<<<<< HEAD
        public string Name { get; set; }
        public Image ThumbnailImage { get; set; }
        public int Price { get; set; }
=======
>>>>>>> ad4311b5a9c311e96d94838537af8decb763063a
        public int review { get; set; }
        public int sold { get; set; }
        public int availableAmount { get; set; }
        public string danhMuc { get; set; }
        public string kho { get; set; }
        public string thuongHieu {  get; set; }
<<<<<<< HEAD
        public string guiTu {  get; set; }
        public string moTa { get; set; }

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
        public ProductDetail(int id, string name, Image thumbnailImage, int price,int revie, int sol, 
            int available, string danhMu, string kh, string thuongHie, string guiT, string moT)
        {
            Id = id;
            Name = name;
            ThumbnailImage = thumbnailImage;
            Price = price;
=======
        public string chatLieu { get; set; }
        public string guiTu {  get; set; }
        public string moTa { get; set; }
        public string cuaHang { get; set; }
        Image anhCuaHang { get; set; }

        public ProductDetail(int id, int revie, int sol, 
            int available, string danhMu, string kh, string thuongHie,
            string chatLie, string guiT, string moT, string cuaHan, Image anhCuaHan, int soLgMua)
        {
            Id = id;
>>>>>>> ad4311b5a9c311e96d94838537af8decb763063a
            review = revie;
            sold = sol;
            availableAmount = available;
            danhMuc = danhMu;
            kho = kh;
            thuongHieu = thuongHie;
<<<<<<< HEAD
            guiTu = guiT;
            moTa = moT;
=======
            chatLieu = chatLie;
            guiTu = guiT;
            moTa = moT;
            cuaHang = cuaHan;
            anhCuaHang = anhCuaHan;
>>>>>>> ad4311b5a9c311e96d94838537af8decb763063a
        }
    }
}
