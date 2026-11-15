using LeadBridge.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeadBridge.Core.Infrastructure.Postgres.Configurations;   
using Microsoft.EntityFrameworkCore;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Name).IsRequired().HasMaxLength(Category.MaxLengthOfCategoryName);
        builder.Property(c => c.Key).IsRequired().HasMaxLength(Category.MaxLengthOfCategoryKey);
        
        builder.HasIndex(c => c.Key).IsUnique();
        builder.HasMany(c => c.Attributes)
            .WithOne()
            .HasForeignKey(a => a.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}