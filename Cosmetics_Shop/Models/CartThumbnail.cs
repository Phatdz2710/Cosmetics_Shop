﻿using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models
{
    /// <summary>
    /// Store Cart Thumbnail Information
    /// </summary>
    public class CartThumbnail : Cart, INotifyPropertyChanged
    {
        public CartThumbnail(int id, string productImage, string productName,
            int price, int amount, int totalPrice)
            : base(id, productImage, productName, price, amount, totalPrice)
        { }

        public CartThumbnail(int userId, int productId, int quantity)
            : base(userId, productId, quantity)
        { }

    }
}
