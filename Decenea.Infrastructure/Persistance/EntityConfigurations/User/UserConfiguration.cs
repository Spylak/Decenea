using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistance.EntityConfigurations.User;

public class UserConfiguration : IEntityTypeConfiguration<Domain.Aggregates.UserAggregate.User>
{
        public void Configure(EntityTypeBuilder<Domain.Aggregates.UserAggregate.User> builder)
        {
                builder.ToTable(name: "Users");
        }
}