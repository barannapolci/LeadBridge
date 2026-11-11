using LeadBridge.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeadBridge.Core.Infrastructure.Postgres.Configurations
{
    public class BusinessTypeConfiguration : IEntityTypeConfiguration<BusinessType>
    {
        public void Configure(EntityTypeBuilder<BusinessType> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Description).HasMaxLength(2000);
        }
    }
}
