using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;

namespace Cosmetics_Shop.Models
{
    public class ProductDetail
    {
        public int Id { get; set; }
        public int review { get; set; }
        public int sold { get; set; }
        public int availableAmount { get; set; }
        public string danhMuc { get; set; }
        public string kho { get; set; }
        public string thuongHieu {  get; set; }
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
            review = revie;
            sold = sol;
            availableAmount = available;
            danhMuc = danhMu;
            kho = kh;
            thuongHieu = thuongHie;
            chatLieu = chatLie;
            guiTu = guiT;
            moTa = moT;
            cuaHang = cuaHan;
            anhCuaHang = anhCuaHan;
        }
    }
}
