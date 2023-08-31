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
        public double Price { get; set; }
        [Required]
        public string imgpath { get; set; }
        [Required]
        public string imgpath2 { get; set; }
        [Required]
        public string imgpath3 { get; set; }
        [Required]
        public string Detailes { get; set; }
        public int CategoryId { get; set; }
        public virtual Categories Categories { get; set; }
    }
}
