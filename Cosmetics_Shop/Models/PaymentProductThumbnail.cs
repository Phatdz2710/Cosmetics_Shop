using Microsoft.Identity.Client;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models
{
    public class PaymentProductThumbnail : PaymentProduct
    {
        public PaymentProductThumbnail(
                int     id, 
                string  image, 
                string  productName,
                int     price, 
                int     amount)
            : base(id, 
                  image, 
                  productName, 
                  price, 
                  amount)
        {
        }
        public PaymentProductThumbnail()
            :base(-1, null, null, 0, 0)
        { }

    }
}
