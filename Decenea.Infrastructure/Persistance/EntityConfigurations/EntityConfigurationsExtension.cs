using Decenea.Infrastructure.Persistance.EntityConfigurations.Common;
using Decenea.Infrastructure.Persistance.EntityConfigurations.Location;
using Decenea.Infrastructure.Persistance.EntityConfigurations.User;
using Microsoft.EntityFrameworkCore;

namespace Decenea.Infrastructure.Persistance.EntityConfigurations;

public static class EntityConfigurationsExtension
{
    public static void ApplyConfigurations(this ModelBuilder builder)
    {
        builder.ApplyConfiguration(new AuditLogConfiguration());
        builder.ApplyConfiguration(new OutboxMessageConfiguration());
        
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new UserClaimConfiguration());
        builder.ApplyConfiguration(new UserTokenConfiguration());
        
        builder.ApplyConfiguration(new CityConfiguration());
        builder.ApplyConfiguration(new CountryConfiguration());
        builder.ApplyConfiguration(new RegionConfiguration());
        builder.ApplyConfiguration(new MunicipalityConfiguration());
        builder.ApplyConfiguration(new MunicipalUnitConfiguration());
        builder.ApplyConfiguration(new RegionalUnitConfiguration());
        builder.ApplyConfiguration(new CommunityConfiguration());
        
        builder.ApplyConfiguration(new MicroAdConfiguration());
    }
}