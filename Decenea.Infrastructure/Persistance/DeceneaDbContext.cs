using System.Text.Json;
using Decenea.Domain.Aggregates.ApplicationUserAggregate;
using Decenea.Domain.Common;
using Decenea.Infrastructure.DataSeed;
using Decenea.Infrastructure.Outbox;
using Decenea.Infrastructure.Persistance.Converters;
using Decenea.Infrastructure.Persistance.EntityConfigurations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Decenea.Infrastructure.Persistance;

public class DeceneaDbContext : DbContext
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
        ApplicationUserSeed.Seed(builder);
        
        builder.ApplyConfiguration(new ApplicationUserConfiguration());
        builder.ApplyConfiguration(new ApplicationRoleConfiguration());
        builder.ApplyConfiguration(new ApplicationUserClaimConfiguration());
        builder.ApplyConfiguration(new ApplicationUserTokenConfiguration());
        
        builder.ApplyConfiguration(new CityConfiguration());
        builder.ApplyConfiguration(new CountryConfiguration());
        builder.ApplyConfiguration(new RegionConfiguration());
        builder.ApplyConfiguration(new MunicipalityConfiguration());
        builder.ApplyConfiguration(new MunicipalUnitConfiguration());
        builder.ApplyConfiguration(new RegionalUnitConfiguration());
        builder.ApplyConfiguration(new CommunityConfiguration());
        builder.ApplyConfiguration(new MicroAdConfiguration());
    }

    public override int SaveChanges()
    {
        return 0;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return 0;
    }

    public async Task<int> SaveChangesAsync(string userId, CancellationToken cancellationToken = default)
    {
        try
        {
            AddDomainEventsAsOutboxMessages(userId);

            var result = await base.SaveChangesAsync(cancellationToken);
            
            return await base.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new Exception("Concurrency exception occurred.", ex);
        }
    }
    
    private void AddDomainEventsAsOutboxMessages(string userId)
    {
        var dateTime = DateTime.UtcNow;
        var outboxMessages = ChangeTracker
            .Entries<AggregateRoot>()
            .Select(entry => entry.Entity)
            .SelectMany(ar =>
            {
                return ar.PopDomainEvents();
            })
            .Select(domainEvent => new OutboxMessage(
                Ulid.NewUlid(),
                dateTime,
                domainEvent.GetType().Name,
                JsonSerializer.Serialize(domainEvent),
                userId))
            .ToList();

        AddRange(outboxMessages);
    }
}