using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website.BL.ViewModels
{
    public class SearchViewModel
    {

        [Required]
        [StringLength(30, MinimumLength = 1)]
        public string name { get; set; }
    }
}
