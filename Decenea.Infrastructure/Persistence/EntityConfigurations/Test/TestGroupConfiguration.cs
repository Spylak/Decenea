using Decenea.Domain.Aggregates.TestAggregate;
using Decenea.Infrastructure.Persistence.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistence.EntityConfigurations.Test;

public class TestGroupConfiguration : LinkingTableConfiguration<TestGroup>
{
    public override void Configure(EntityTypeBuilder<TestGroup> builder)
    {
        base.Configure(builder);
        builder.ToTable(name: "TestGroups");

        builder.HasKey(tq => new { tq.TestId, tq.GroupId });

        builder.Property(tq => tq.TestId).IsRequired();
        builder.Property(tq => tq.GroupId).IsRequired();

        builder.HasOne(tq => tq.Test)
            .WithMany(t => t.TestGroups)
            .HasForeignKey(tq => tq.TestId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(tq => tq.Group)
            .WithMany(u => u.TestGroups)
            .HasForeignKey(tq => tq.GroupId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}