using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models
{
    public class ProductThumbnail : Product
    {
        public ProductThumbnail(int id, 
                                string name, 
                                Image thumbnailImage, 
                                int price, 
                                string brand)
            : base(id, name, thumbnailImage, price, brand)
        { }
    }
}
