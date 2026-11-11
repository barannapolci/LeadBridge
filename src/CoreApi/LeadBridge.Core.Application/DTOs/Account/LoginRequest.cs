using System.ComponentModel.DataAnnotations;

namespace LeadBridge.Core.Application.DTOs.Account
{
    public class LoginRequest
    {
        [Required, EmailAddress, MaxLength(500)]
        public required string Email { get; init; }

        [Required, MinLength(8), MaxLength(100)]
        public required string Password { get; init; }
    }
}
