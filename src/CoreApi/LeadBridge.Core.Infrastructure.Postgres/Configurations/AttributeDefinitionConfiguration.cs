using LeadBridge.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeadBridge.Core.Infrastructure.Postgres.Configurations;

public class AttributeDefinitionConfiguration : IEntityTypeConfiguration<AttributeDefinition>
{
    public void Configure(EntityTypeBuilder<AttributeDefinition> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Key).IsRequired().HasMaxLength(AttributeDefinition.MaxLengthOfKey);
        builder.Property(a => a.Name).IsRequired().HasMaxLength(AttributeDefinition.MaxLengthOfName);
        builder.Property(a => a.Unit).HasMaxLength(AttributeDefinition.MaxLengthOfUnit);

        builder.HasIndex(a => new { a.CategoryId, a.Key }).IsUnique();
    }
}