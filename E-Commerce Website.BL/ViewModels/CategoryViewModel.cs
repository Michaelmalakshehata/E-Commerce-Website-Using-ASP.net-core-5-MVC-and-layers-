using E_Commerce_Website.BL.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website.BL.ViewModels
{
    public class CategoryViewModel
    {
        [Required]
        [UniqueCategoryName]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "Name length between 5 and 25 Letters")]
        public string Name { get; set; }

    }
}
