using LeadBridge.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeadBridge.Core.Infrastructure.Postgres.Configurations;   
using Microsoft.EntityFrameworkCore;

public class RequestConfiguration : IEntityTypeConfiguration<Request>
{
    public void Configure(EntityTypeBuilder<Request> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Version).IsConcurrencyToken();
        builder.HasIndex(r => r.OwnerId);
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(r => r.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.OwnsMany(r => r.Items, items => 
        {
            items.ToJson();
            items.OwnsMany(i => i.Conditions);
        });
    }
}