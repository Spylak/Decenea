using Decenea.Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistance.EntityConfigurations;

public class ApplicationUserClaimConfiguration : IEntityTypeConfiguration<UserClaim>
{
        public void Configure(EntityTypeBuilder<UserClaim> builder)
        {
                builder.ToTable(name: "UserClaims");
        }
}