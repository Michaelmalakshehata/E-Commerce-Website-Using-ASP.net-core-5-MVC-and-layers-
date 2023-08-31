using E_Commerce_Website.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website.BL.ValidationAttributes
{
    public class UniqueUsername : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            EntityContext context = (EntityContext)validationContext.GetService(typeof(EntityContext));
            if (value is null)
                return null;
            string newname = value.ToString();
            var username = context.Users.FirstOrDefault(s => s.UserName == newname);
            if (username is not null)
            {
                return new ValidationResult("Username Already Exist");
            }
            return ValidationResult.Success;
        }
    }
}
