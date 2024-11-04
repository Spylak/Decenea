using Decenea.Domain.Aggregates.TestAggregate;
using Decenea.Infrastructure.Persistence.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Decenea.Infrastructure.Persistence.EntityConfigurations.Test;

public class TestUserConfiguration : AuditableEntityConfiguration<TestUser>
{
    public override void Configure(EntityTypeBuilder<TestUser> builder)
    {
        base.Configure(builder);
        builder.ToTable(name: "TestUsers");
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
        
        builder.HasMany(tq => tq.TestAnswers)
            .WithOne(u => u.TestUser)
            .HasForeignKey(tq => tq.TestUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}