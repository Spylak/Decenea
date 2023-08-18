using Decenea.Domain.Entities.ApplicationUser;
using Decenea.Domain.Entities.Common;
using Decenea.Domain.Entities.Location;
using Decenea.Infrastructure.Data.EntityConfigurations;
using Decenea.Infrastructure.DataSeed;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Decenea.Infrastructure.Data;

public class DeceneaDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, long,
    ApplicationUserClaim,
    ApplicationUserRole,
    ApplicationUserLogin,
    ApplicationRoleClaim,
    ApplicationUserToken
>
{
    public DeceneaDbContext(DbContextOptions<DeceneaDbContext> options) : base(options)
    {
    }
    protected sealed override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        configurationBuilder.Properties<DateTime>()
            .HaveConversion(typeof(DateTimeToDateTimeUtc));
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        ApplicationRoleSeed.Seed(builder);
        builder.ApplyConfiguration(new ApplicationUserConfiguration());
        builder.ApplyConfiguration(new ApplicationRoleConfiguration());
        builder.ApplyConfiguration(new ApplicationUserLoginConfiguration());
        builder.ApplyConfiguration(new ApplicationUserClaimConfiguration());
        builder.ApplyConfiguration(new ApplicationRoleClaimConfiguration());
        builder.ApplyConfiguration(new ApplicationUserRoleConfiguration());
        builder.ApplyConfiguration(new ApplicationUserTokenConfiguration());
        builder.ApplyConfiguration(new CityConfiguration());
        builder.ApplyConfiguration(new CountryConfiguration());
        builder.ApplyConfiguration(new RegionConfiguration());
        builder.ApplyConfiguration(new MunicipalityConfiguration());
        builder.ApplyConfiguration(new MunicipalUnitConfiguration());
        builder.ApplyConfiguration(new RegionalUnitConfiguration());
        builder.ApplyConfiguration(new CommunityConfiguration());
    }

    public override int SaveChanges()
    {
        return SaveChanges(null);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return SaveChangesAsync(null, cancellationToken);
    }

    public int SaveChanges(string userId)
    {
        var updatedEntityList = ChangeTracker.Entries()
            .Where(x => x.Entity is IAuditableEntity && x.State == EntityState.Modified);

        var addedEntityList = ChangeTracker.Entries()
            .Where(x => x.Entity is IAuditableEntity && x.State == EntityState.Added);

        foreach (var entity in updatedEntityList)
        {
            ((IAuditableEntity)entity.Entity).ModifiedAt = DateTime.UtcNow;
            ((IAuditableEntity)entity.Entity).ModifiedBy = userId;
        }

        foreach (var entity in addedEntityList)
        {
            ((IAuditableEntity)entity.Entity).ModifiedAt = DateTime.UtcNow;
            ((IAuditableEntity)entity.Entity).CreatedAt = DateTime.UtcNow;
            ((IAuditableEntity)entity.Entity).ModifiedBy = userId;
            ((IAuditableEntity)entity.Entity).CreatedBy = userId;
        }

        return base.SaveChanges();
    }

    public async Task<int> SaveChangesAsync(string userId, CancellationToken cancellationToken)
    {
        var updatedEntityList = ChangeTracker.Entries()
            .Where(x => x.Entity is IAuditableEntity && x.State == EntityState.Modified);

        var addedEntityList = ChangeTracker.Entries()
            .Where(x => x.Entity is IAuditableEntity && x.State == EntityState.Added);

        foreach (var entity in updatedEntityList)
        {
            ((IAuditableEntity)entity.Entity).ModifiedAt = DateTime.UtcNow;
            ((IAuditableEntity)entity.Entity).ModifiedBy = userId;
        }

        foreach (var entity in addedEntityList)
        {
            ((IAuditableEntity)entity.Entity).ModifiedAt = DateTime.UtcNow;
            ((IAuditableEntity)entity.Entity).CreatedAt = DateTime.UtcNow;
            ((IAuditableEntity)entity.Entity).ModifiedBy = userId;
            ((IAuditableEntity)entity.Entity).CreatedBy = userId;
        }

        return await base.SaveChangesAsync();
    }
}