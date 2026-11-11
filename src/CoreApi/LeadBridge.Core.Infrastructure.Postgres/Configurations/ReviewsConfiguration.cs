using LeadBridge.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeadBridge.Core.Infrastructure.Postgres.Configurations;
using Microsoft.EntityFrameworkCore;

public class ReviewsConfiguration : IEntityTypeConfiguration<Reviews>
{
    public void Configure(EntityTypeBuilder<Reviews> builder)
    {
        builder.HasKey(r => r.Id);

        builder.HasOne(r => r.Sender)
            .WithMany(u => u.GivenReviews)
            .HasForeignKey(r => r.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Recipient)
            .WithMany(u => u.ReceivedReviews)
            .HasForeignKey(r => r.RecipientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(r => new { r.SenderId, r.RecipientId }).IsUnique();
    }
}