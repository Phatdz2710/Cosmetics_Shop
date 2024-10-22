using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models
{
    public class ProductThumbnail : Product
    {
        public ProductThumbnail(int id, string name, Image thumbnailImage, int price)
            : base(id, name, thumbnailImage, price)
        { }
    }
}
