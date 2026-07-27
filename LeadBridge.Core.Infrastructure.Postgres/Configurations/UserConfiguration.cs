using System.Runtime.CompilerServices;
using LeadBridge.Core.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeadBridge.Core.Infrastructure.Postgres.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Email).IsRequired().HasMaxLength(120);

        builder.HasOne(u => u.PersonalInfo)
            .WithOne()
            .HasForeignKey<User>(u => u.PersonalInfoId);

        builder.HasOne(u => u.Avatar)
            .WithOne()
            .HasForeignKey<User>(u => u.AvatarId);
    }
}