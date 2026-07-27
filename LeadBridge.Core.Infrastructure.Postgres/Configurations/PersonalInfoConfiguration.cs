using LeadBridge.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeadBridge.Core.Infrastructure.Postgres.Configurations
{
    public class PersonalInfoConfiguration : IEntityTypeConfiguration<PersonalInfo>
    {
        public void Configure(EntityTypeBuilder<PersonalInfo> builder)
        {
            ArgumentNullException.ThrowIfNull(builder);

            builder.HasKey(p => p.Id);

            builder.HasMany(p => p.BusinessTypes)
                .WithMany(b => b.PersonalInfos);
        }
    }
}