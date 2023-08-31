﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace E_Commerce_Website.BL.ValidationAttributes
{
    public class PatternPhone : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Regex reg = new Regex("^(01)(0|1|2|5)[0-9]{8}$");

            if (value is null)
                return null;
            string phone = value.ToString();
            if (reg.IsMatch(phone) is false)
            {
                return new ValidationResult("Invalid Phone Number");
            }
            return ValidationResult.Success;
        }
    }
}
