using E_Commerce_Website.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website.BL.ValidationAttributes
{
    public class UniqueEmail : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            EntityContext context = (EntityContext)validationContext.GetService(typeof(EntityContext));
            if (value is null)
                return null;
            string newemail = value.ToString();
            var Email = context.Users.FirstOrDefault(s => s.Email == newemail);
            if (Email is not null)
            {
                return new ValidationResult("Email Already Exist");
            }
            return ValidationResult.Success;
        }
    }
}
