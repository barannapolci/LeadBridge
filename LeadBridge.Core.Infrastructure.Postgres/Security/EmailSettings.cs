using System.ComponentModel.DataAnnotations;

namespace LeadBridge.Core.Infrastructure.Postgres.Email
{
    public sealed class EmailSettings
    {
        public const string SectionName = "Email";

        [Required]
        public Uri? ConfirmationBaseUrl { get; set; }
    }
}