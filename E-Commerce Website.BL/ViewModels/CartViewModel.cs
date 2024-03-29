﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website.BL.ViewModels
{
    public class CartViewModel
    {
        public string Ordername { get; set; }
        public string imgpath { get; set; }

        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal DiscountPrice
        {

            get
            {
                if (Discount > 0)
                {
                    return Price - (Price * (Discount / 100));
                }
                return 0;
            }
        }
        [Required]
        public int Quantity { get; set; }

        public string UserId { get; set; }
        public int MenuId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

    }
}
