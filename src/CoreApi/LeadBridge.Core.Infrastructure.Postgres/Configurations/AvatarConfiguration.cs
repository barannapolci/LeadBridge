using LeadBridge.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeadBridge.Core.Infrastructure.Postgres.Configurations
{
    public class AvatarConfiguration : IEntityTypeConfiguration<Avatar>
    {
        public void Configure(EntityTypeBuilder<Avatar> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.AvatarLink).IsRequired().HasMaxLength(2200);
        }
    }
}
