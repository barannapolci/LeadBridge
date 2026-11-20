using LeadBridge.Matching.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeadBridge.Matching.Infrastructure.Postgres.Configurations;
public class IndexedItemsConfiguration : IEntityTypeConfiguration<IndexedItem>
{
    public void Configure(EntityTypeBuilder<IndexedItem> builder)
    {
        builder.HasIndex(i => i.Id);
        builder.Property(i => i.CategoryKey).IsRequired().HasMaxLength(IndexedItem.MaxLengthOfCategoryKey);

        builder.HasIndex(i => i.RequestId);

        builder.HasIndex(i => new { i.Side, i.CategoryKey });

        builder.OwnsMany(i => i.Conditions, conditions =>
        {
            conditions.ToJson();
        });
    }
}