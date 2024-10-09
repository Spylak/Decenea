using Decenea.Domain.Aggregates.TestAggregate;
using Decenea.Infrastructure.Persistence.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistence.EntityConfigurations.Test;

public class TestUserConfiguration : LinkingTableConfiguration<TestUser>
{
    public override void Configure(EntityTypeBuilder<TestUser> builder)
    {
        base.Configure(builder);
        builder.ToTable(name: "TestUsers");

        builder.HasKey(tq => new { tq.TestId, tq.UserId });

        builder.Property(tq => tq.TestId).IsRequired();
        builder.Property(tq => tq.UserId).IsRequired();

        builder.HasOne(tq => tq.Test)
            .WithMany(t => t.TestUsers)
            .HasForeignKey(tq => tq.TestId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(tq => tq.User)
            .WithMany(u => u.TestUsers)
            .HasForeignKey(tq => tq.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}