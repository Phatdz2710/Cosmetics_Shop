using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Name { get; set; }
        Image UserImage { get; set; }
        public int StarNumber { get; set; }

        public Review(int id, string name, Image userImage, int starNumber)
        {
            Id = id;
            Name = name;
            UserImage = userImage;
            StarNumber = starNumber;
        }
    }
}
