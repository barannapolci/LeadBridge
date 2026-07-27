using LeadBridge.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeadBridge.Core.Infrastructure.Postgres.Configurations
{
    public class EmailConfirmationConfiguration : IEntityTypeConfiguration<EmailConfirmation>
    {
        public void Configure(EntityTypeBuilder<EmailConfirmation> builder)
        {
            ArgumentNullException.ThrowIfNull(builder);

            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.User)
                .WithOne()
                .HasForeignKey<EmailConfirmation>(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}