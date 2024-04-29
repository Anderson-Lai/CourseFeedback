using System.ComponentModel.DataAnnotations;

namespace CourseFeedback.CustomValidation
{
    public class GraduatingYearValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
            {
                return new ValidationResult("Please enter your graduating year.");
            }
            else
            {
                string inputValue = value.ToString();
                if(inputValue.Length == 4)
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult("Please enter a valid year (e.g. 2026).");
            }
        }
    }
}
