using Decenea.Infrastructure.Persistence.EntityConfigurations.Common;
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
        builder.ApplyConfiguration(new TestConfiguration());
    }
}