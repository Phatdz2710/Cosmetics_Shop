using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Models
{
    /// <summary>
    /// Represents a voucher that can be applied to an order.
    /// </summary>
    public class Voucher
    {
        /// <summary>
        /// Gets or sets the unique identifier for the voucher.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the code of the voucher.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the discount amount of the voucher.
        /// </summary>
        public decimal DiscountAmount { get; set; }

        /// <summary>
        /// Gets or sets the percentage discount of the voucher.
        /// </summary>
        public decimal PercentageDiscount { get; set; }

        /// <summary>
        /// Gets or sets the description of the voucher.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the start date from which the voucher is valid.
        /// </summary>
        public DateTime ValidFrom { get; set; }

        /// <summary>
        /// Gets or sets the end date until which the voucher is valid.
        /// </summary>
        public DateTime ValidTo { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the voucher is active.
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        /// Gets or sets the collection of orders associated with the voucher.
        /// </summary>
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Voucher"/> class with specified values.
        /// </summary>
        /// <param name="id">The unique identifier for the voucher.</param>
        /// <param name="code">The code of the voucher.</param>
        /// <param name="discountAmount">The discount amount of the voucher.</param>
        /// <param name="percentageDiscount">The percentage discount of the voucher.</param>
        /// <param name="description">The description of the voucher.</param>
        /// <param name="validFrom">The start date from which the voucher is valid.</param>
        /// <param name="validTo">The end date until which the voucher is valid.</param>
        /// <param name="isActive">A value indicating whether the voucher is active.</param>
        public Voucher(int id,
                        string code,
                        decimal discountAmount,
                        decimal percentageDiscount,
                        string description,
                        DateTime validFrom,
                        DateTime validTo,
                        bool? isActive)
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

        /// <summary>
        /// Initializes a new instance of the <see cref="Voucher"/> class with default values.
        /// </summary>
        public Voucher()
        {
            Id = 1;
            Description = "Default Voucher";
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Voucher other)
            {
                return this.Id == other.Id; // Compare by Id
            }
            return false;
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode(); // Or use another property
        }
    }
}