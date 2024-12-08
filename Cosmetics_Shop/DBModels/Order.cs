﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Cosmetics_Shop.DBModels;

public partial class Order
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public bool? IsApproved { get; set; }

    public string OrderStatus { get; set; }

    public DateTime OrderDate { get; set; }

    public int PaymentMethod { get; set; }

    public int ShippingMethod { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual User User { get; set; }
}