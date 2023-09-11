using E_Commerce_Website.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website.BL.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }    
        [Required]
        public int ProductId { get; set; }
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name length between 3 and 100 Letters")]
        [Required]
        public string Comments { get; set; }
        [Required]
        public int Rating { get; set; }
        public string UserName { set; get; }
        public DateTime Date { set; get; }
    }
}
