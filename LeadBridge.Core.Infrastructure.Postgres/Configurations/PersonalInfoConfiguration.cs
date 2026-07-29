using LeadBridge.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeadBridge.Core.Infrastructure.Postgres.Configurations
{
    public class PersonalInfoConfiguration : IEntityTypeConfiguration<PersonalInfo>
    {
        public void Configure(EntityTypeBuilder<PersonalInfo> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasMany(p => p.BusinessTypes)
                .WithMany(b => b.PersonalInfos);

            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(70);

            builder.Property(p => p.LastName)
                .IsRequired().HasMaxLength(70);

            builder.Property(p => p.Country).IsRequired().HasMaxLength(250);

            builder.Property(p => p.Region).HasMaxLength(250);

            builder.Property(p => p.City).HasMaxLength(250);

            builder.Property(p => p.Year).IsRequired();
        }
    }
}