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

        public double Price { get; set; }
        [Required]
        public int Quantity { get; set; }

        public string UserId { get; set; }
        public int MenuId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

    }
}
