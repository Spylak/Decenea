using Decenea.Domain.Aggregates.ApplicationUserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistance.EntityConfigurations;

public class ApplicationUserClaimConfiguration : IEntityTypeConfiguration<ApplicationUserClaim>
{
        public void Configure(EntityTypeBuilder<ApplicationUserClaim> builder)
        {
                builder.ToTable(name: "UserClaims");
        }
}