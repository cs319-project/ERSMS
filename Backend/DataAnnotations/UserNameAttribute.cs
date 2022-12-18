using System.ComponentModel.DataAnnotations;

namespace Backend.DataAnnotations
{
    /// <summary>A custom attribute for validating usernames.</summary>
    public class UserNameAttribute : ValidationAttribute
    {
        // Usernames on ERSMS consits of only numbers with maximum length of 8

        /// <summary>Validates that a username is valid.</summary>
        /// <param name="value">The username to validate.</param>
        /// <param name="validationContext">The context in which the validation is taking place.</param>
        /// <returns>A ValidationResult object.</returns>
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
