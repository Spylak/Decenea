using Decenea.Domain.Aggregates.ApplicationUserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistance.EntityConfigurations;

public class ApplicationUserTokenConfiguration : IEntityTypeConfiguration<ApplicationUserToken>
{
        public void Configure(EntityTypeBuilder<ApplicationUserToken> builder)
        {
                builder.ToTable(name: "UserTokens");
        }
}