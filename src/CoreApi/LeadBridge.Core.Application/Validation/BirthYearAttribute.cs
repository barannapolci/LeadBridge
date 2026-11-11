using System.ComponentModel.DataAnnotations;
using LeadBridge.Core.Domain.Entities;

namespace LeadBridge.Core.Application.Validation
{
    public sealed class BirthYearAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is int year)
            {
                var currentYear = DateTime.UtcNow.Year;

                if (year < PersonalInfo.MinimalYear || year > currentYear)
                {
                    return new ValidationResult($"year must be between {PersonalInfo.MinimalYear} and {currentYear}");
                }
            }

            return ValidationResult.Success;
        }
    }
}
