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
        public PaymentProductThumbnail(int id, Image image, string productName,
            string classification, int price, int amount)
            : base(id, image, productName, classification, price, amount)
        {
        }

    }
}
