using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website.DAL.Models
{

    public abstract class BaseModel
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; } = false;
        [Required]
        public string Name { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime? RestoreDate { get; set; }
        [Required]
        public string UserName { get; set; }
       
    }
}
