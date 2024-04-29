using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;

namespace CourseFeedback.CustomValidation
{
    public class CustomEmailValidation: ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if(value is null)
            {
                return new ValidationResult("Please enter your school email.");
            }
            else
            {
                const string correctEnding = "ycdsbk12.ca";
                string toValidate = value.ToString();

                string[] parts = toValidate.Split('@');

                return parts[1] == correctEnding ? ValidationResult.Success :
                    new ValidationResult("Please use your ycdsbk12.ca email.");
            }
        }
    }
}
