using Decenea.Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistance.EntityConfigurations.User;

public class UserSourceEventConfiguration : IEntityTypeConfiguration<UserSourceEvent>
{
        public void Configure(EntityTypeBuilder<UserSourceEvent> builder)
        {
                builder.ToTable(name: "UserSourceEvents");

                builder.HasOne(i => i.UserSourceEventStream)
                        .WithMany(i => i.UserSourceEvents)
                        .HasForeignKey(i => i.StreamId);
        }
}