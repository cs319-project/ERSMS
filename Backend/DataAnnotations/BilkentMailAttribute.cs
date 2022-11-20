using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DataAnnotations
{
    public class BilkentMailAttribute : ValidationAttribute
    {
        // Mail addresses on ERSMS must be valid and end with bilkent.edu.tr

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string mail = (value as string).Trim().ToLower();

            if (new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(mail) && mail.EndsWith("bilkent.edu.tr"))
            {
                return ValidationResult.Success;
            }

            else
            {
                return new ValidationResult("A Bilkent mail must be used.");
            }
        }
    }
}
