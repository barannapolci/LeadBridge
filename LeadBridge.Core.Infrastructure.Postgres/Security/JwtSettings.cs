using System.ComponentModel.DataAnnotations;

namespace LeadBridge.Core.Infrastructure.Postgres.Security
{
    public sealed class JwtSettings
    {
        public const string SectionName = "Jwt";

        [Required]
        public string Issuer { get; set; } = string.Empty;

        [Required]
        public string Audience { get; set; } = string.Empty;

        [Required, MinLength(32)]
        public string SecretKey { get; set; } = string.Empty;

        [Range(1, 1440)]
        public int AccessTokenLifetimeMinutes { get; set; } = 15;
    }
}