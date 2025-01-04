using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models
{
    /// <summary>
    /// Stores information about a voucher
    /// </summary>
    public class Voucher
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal PercentageDiscount { get; set; }

        public string Description { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }

        public bool? IsActive { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        public Voucher(int id,
                        string code,
                        decimal discountAmount,
                        decimal percentageDiscount,
                        string description,
                        DateTime validFrom,
                        DateTime validTo,
                        bool? isActive
                        )
        {
            Id = id;
            Code = code;
            DiscountAmount = discountAmount;
            PercentageDiscount = percentageDiscount;
            Description = description;
            ValidFrom = validFrom;
            ValidTo = validTo;
            IsActive = isActive;
        }

        public Voucher()
        {
            Id = 1;
            Description = "Default Voucher";
        }
    }

}
