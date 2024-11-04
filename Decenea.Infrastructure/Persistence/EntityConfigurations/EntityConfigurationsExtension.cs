using Decenea.Infrastructure.Persistence.EntityConfigurations.Common;
using Decenea.Infrastructure.Persistence.EntityConfigurations.Group;
using Decenea.Infrastructure.Persistence.EntityConfigurations.Question;
using Decenea.Infrastructure.Persistence.EntityConfigurations.Test;
using Decenea.Infrastructure.Persistence.EntityConfigurations.User;
using Microsoft.EntityFrameworkCore;

namespace Decenea.Infrastructure.Persistence.EntityConfigurations;

public static class EntityConfigurationsExtension
{
    public static void ApplyConfigurations(this ModelBuilder builder)
    {
        builder.ApplyConfiguration(new AuditLogConfiguration());
        builder.ApplyConfiguration(new OutboxMessageConfiguration());
        
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new UserClaimConfiguration());
        builder.ApplyConfiguration(new UserTokenConfiguration());
        
        builder.ApplyConfiguration(new QuestionConfiguration());
        builder.ApplyConfiguration(new QuestionAnswerConfiguration());
        builder.ApplyConfiguration(new TestAnswerConfiguration());
        
        builder.ApplyConfiguration(new TestConfiguration());
        builder.ApplyConfiguration(new TestQuestionConfiguration());
        builder.ApplyConfiguration(new TestUserConfiguration());
        builder.ApplyConfiguration(new TestGroupConfiguration());
        
        builder.ApplyConfiguration(new GroupConfiguration());
        builder.ApplyConfiguration(new GroupMemberConfiguration());
    }
}