using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Backend.DataAnnotations
{
    public class UserNameAttribute : ValidationAttribute
    {
        // Usernames on ERSMS consits of only numbers with maximum length of 8

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Username cannot be null");
            }

            string username = value.ToString();

            if (username.Length > 8)
            {
                return new ValidationResult("Username cannot be longer than 8 characters");
            }

            if (username.Any(c => !char.IsDigit(c)))
            {
                return new ValidationResult("Username can only contain numbers");
            }

            return ValidationResult.Success;
        }
    }
}
