﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website.DAL.Models
{
    //Hard Delete
    public class WishList
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Ordername { get; set; }
        [Required]
        public string imgpath { get; set; }

        [Required]
        public double Price { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
