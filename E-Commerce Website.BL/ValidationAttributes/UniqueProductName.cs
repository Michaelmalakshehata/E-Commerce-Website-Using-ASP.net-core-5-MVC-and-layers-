using E_Commerce_Website.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website.BL.ValidationAttributes
{
    public class UniqueProductName: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            EntityContext context = (EntityContext)validationContext.GetService(typeof(EntityContext));
            if (value is null)
            {
                return null;
            }
            string newname = value.ToString();
            if (context.Menus.FirstOrDefault(s => s.Name == newname) is not null)
            {
                return new ValidationResult("Name Already Exist in Product or restore it from deleted");
            }
            return ValidationResult.Success;
        }
    }
}
