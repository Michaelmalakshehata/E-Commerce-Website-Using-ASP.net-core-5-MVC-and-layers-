using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website.DAL.Models
{

    //Hard Delete
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Ordername { get; set; }
        [Required]
        public string imgpath { get; set; }

        [Required]
        public decimal Price { get; set; }
        public decimal Discount { get; set; }

        [Required]
        public int Quantity { get; set; }
        public decimal TotalPrice
        {

            get
            {
                if (Discount > 0)
                {
                    decimal priceDiscount = Price - (Price * (Discount / 100));
                    return (Quantity * priceDiscount);
                }
                return Quantity * Price;
            }
        }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
