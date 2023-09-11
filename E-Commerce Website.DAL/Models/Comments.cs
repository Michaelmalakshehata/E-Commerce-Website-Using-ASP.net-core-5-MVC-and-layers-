using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website.DAL.Models
{
    //Hard Delete
    public class Comments
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Comment { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        [Required]
        public string UserName { get; set; }
        [Required]
        public int Rating { get; set; }
        public int MenuId { get; set; }
        public virtual Menus Menus { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
