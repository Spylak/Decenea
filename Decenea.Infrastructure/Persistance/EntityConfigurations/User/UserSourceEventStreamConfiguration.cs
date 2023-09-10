using Decenea.Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistance.EntityConfigurations.User;

public class UserSourceEventStreamConfiguration : IEntityTypeConfiguration<UserSourceEventStream>
{
        public void Configure(EntityTypeBuilder<UserSourceEventStream> builder)
        {
                builder.ToTable(name: "UserSourceEventStreams");

                builder.HasMany(i => i.UserSourceEvents)
                        .WithOne(i => i.UserSourceEventStream)
                        .HasForeignKey(i => i.Id);
        }
}