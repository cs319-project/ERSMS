using System.ComponentModel.DataAnnotations;

namespace Backend.DataAnnotations
{
    /// <summary>A custom attribute for validating Bilkent University email addresses.</summary>
    public class BilkentMailAttribute : ValidationAttribute
    {
        // Mail addresses on ERSMS must be valid and end with bilkent.edu.tr

        /// <summary>Validates whether the given value is a valid Bilkent mail.</summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context in which the validation is performed.</param>
        /// <returns>The validation result.</returns>
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
