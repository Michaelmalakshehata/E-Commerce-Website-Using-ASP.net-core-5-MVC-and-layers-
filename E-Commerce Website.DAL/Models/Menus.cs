using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website.DAL.Models
{
    //Soft delete
    public class Menus : BaseModel
    {
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string imgpath { get; set; }
        [Required]
        public string VideoPath { get; set; }
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
        public decimal Discount { get; set; }
        [Required]
        public string Detailes { get; set; }
        public int CategoryId { get; set; }
        public virtual Categories Categories { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }

    }
}
