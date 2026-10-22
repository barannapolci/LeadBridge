using System.ComponentModel.DataAnnotations;
using LeadBridge.Core.Application.Validation;

namespace LeadBridge.Core.Application.DTOs.Account
{
    public class RegisterRequest
    {
        [Required, EmailAddress, MaxLength(500)]
        public required string Email { get; init; }

        [Required, MaxLength(100)]
        public required string Password { get; init; }

        [Required, MaxLength(70)]
        public required string FirstName { get; init; }

        [Required, MaxLength(70)]
        public required string LastName { get; init; }

        [Required, BirthYear]
        public required int Year { get; init;  }

        [Required, MaxLength(250)]
        public required string Country { get; init; }

        [Required, MaxLength(250)]
        public required string Region { get; init; } 

        [Required, MaxLength(250)]
        public required string City { get; init; }
        public Uri? AvatarURL { get; init; }
    }
}
