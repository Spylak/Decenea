using Decenea.Application.Mappers;
using Decenea.Domain.Aggregates.GroupAggregate;
using Decenea.Domain.Aggregates.TestAggregate;
using Decenea.Infrastructure.Persistence.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistence.EntityConfigurations.Group;

public class GroupMemberConfiguration : LinkingTableConfiguration<GroupMember>
{
    public override void Configure(EntityTypeBuilder<GroupMember> builder)
    {
        base.Configure(builder);
        builder.ToTable(name: "GroupMembers");

        builder.HasKey(tq => new { UserEmail = tq.GroupUserEmail, tq.GroupId });

        builder.Property(tq => tq.GroupUserEmail).IsRequired();
        builder.Property(tq => tq.GroupId).IsRequired();

        builder.HasOne(tq => tq.Group)
            .WithMany(t => t.GroupMembers)
            .HasForeignKey(tq => tq.GroupId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}