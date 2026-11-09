using System.ComponentModel.DataAnnotations;
using LeadBridge.Core.Application.Validation;
using LeadBridge.Core.Domain.Entities;

namespace LeadBridge.Core.Application.DTOs.Profile
{
    public class ProfileForm
    {
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
    }
}
