using Decenea.Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistance.EntityConfigurations;

public class ApplicationUserTokenConfiguration : IEntityTypeConfiguration<UserToken>
{
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
                builder.ToTable(name: "UserTokens");
        }
}