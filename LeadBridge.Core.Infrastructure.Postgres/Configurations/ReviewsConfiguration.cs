using LeadBridge.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeadBridge.Core.Infrastructure.Postgres.Configurations;
using Microsoft.EntityFrameworkCore;

public class ReviewsConfiguration : IEntityTypeConfiguration<Reviews>
{
    public void Configure(EntityTypeBuilder<Reviews> builder)
    {
        throw new NotImplementedException();
    }
}