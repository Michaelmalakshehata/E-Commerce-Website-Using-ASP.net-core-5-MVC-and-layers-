using E_Commerce_Website.BL.ValidationAttributes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website.BL.ViewModels
{
    public class ProductViewModel
    {
        [Required]
        [UniqueProductName]
        [StringLength(25, MinimumLength = 5, ErrorMessage = "Name length between 5 and 25 Letters")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Detailes length between 5 and 100 Letters")]
        public string Detailes { get; set; }
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public IFormFile File2 { get; set; }
        [Required]
        public IFormFile File3 { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
