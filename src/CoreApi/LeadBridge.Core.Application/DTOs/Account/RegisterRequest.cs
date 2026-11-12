using System.ComponentModel.DataAnnotations;
using LeadBridge.Core.Application.Validation;
using LeadBridge.Core.Domain.Entities;
using LeadBridge.Core.Domain.Enums;
namespace LeadBridge.Core.Application.DTOs.Account
{
    public class RegisterRequest
    {
        [Required, EmailAddress, MaxLength(500)]
        public required string Email { get; init; }

        [Required, MaxLength(100)]
        public required string Password { get; init; }

        [Required, MaxLength(PersonalInfo.GlobalMaxLength)]
        public required string FirstName { get; init; }

        [Required, MaxLength(PersonalInfo.GlobalMaxLength)]
        public required string LastName { get; init; }

        [Required, BirthYear]
        public required int Year { get; init;  }

        [Required, MaxLength(PersonalInfo.GlobalMaxLength)]
        public required string Country { get; init; }

        [Required, MaxLength(PersonalInfo.GlobalMaxLength)]
        public required string Region { get; init; } 

        [Required, MaxLength(PersonalInfo.GlobalMaxLength)]
        public required string City { get; init; }

        [EnumDataType(typeof(UserRole))]
        public required UserRole Role { get; init;  }
    }
}
