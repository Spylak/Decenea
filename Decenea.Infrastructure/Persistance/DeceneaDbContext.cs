using Decenea.Application.Abstractions.Persistance;
using Decenea.Domain.Common;
using Decenea.Infrastructure.DataSeed;
using Decenea.Infrastructure.Outbox;
using Decenea.Infrastructure.Persistance.Converters;
using Decenea.Infrastructure.Persistance.EntityConfigurations;
using Decenea.Infrastructure.Persistance.EntityConfigurations.User;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Serilog;


namespace Decenea.Infrastructure.Persistance;

public class DeceneaDbContext : DbContext, IDeceneaDbContext
{
    private readonly IPublisher _publisher;
    public string? CreatedBy { get; set; }
    public Queue<IDomainEvent>? DomainEvents { get; set; }

    public DeceneaDbContext(DbContextOptions<DeceneaDbContext> options,
        IPublisher publisher) : base(options)
    {
        _publisher = publisher;
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
        ApplicationUserSeed.Seed(builder);

        builder.ApplyConfiguration(new UserSourceEventConfiguration());
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

    public new DbSet<T> Set<T>() where T : class
    {
        return base.Set<T>();
    }
    
    public override int SaveChanges()
    {
        return 0;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        if (CreatedBy is null)
            return 1;

        DomainEvents ??= GetDomainEvents();

        if (DomainEvents.Count == 0)
            return await base.SaveChangesAsync(cancellationToken);

        return await HandleDomainEvents(DomainEvents, CreatedBy, cancellationToken);
    }

    private async Task<int> HandleDomainEvents(Queue<IDomainEvent> domainEvents, string userId,
        CancellationToken cancellationToken = default)
    {
        await using var transaction = await Database.BeginTransactionAsync(cancellationToken);
        try
        {
            while (domainEvents.TryDequeue(out var nextEvent))
            {
                await _publisher.Publish(nextEvent, cancellationToken);
            }

            await base.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            AddDomainEventsAsOutboxMessages(domainEvents, userId, ex);
            await base.SaveChangesAsync(cancellationToken);
            Log.Error("Something went wrong while publishing events: {message} . Adding them to the outbox.",ex.Message);
        }

        return 1;
    }

    private Queue<IDomainEvent> GetDomainEvents()
    {
        return new Queue<IDomainEvent>(ChangeTracker
            .Entries<AggregateRoot>()
            .Select(entry => entry.Entity)
            .SelectMany(ar => { return ar.PopDomainEvents(); }));
    }

    private void AddDomainEventsAsOutboxMessages(Queue<IDomainEvent> domainEvents, string userId, Exception exception)
    {
        var dateTime = DateTime.UtcNow;
        var outboxMessages = domainEvents
            .Select(domainEvent => new OutboxMessage(
                Ulid.NewUlid(),
                dateTime,
                domainEvent.GetType().Name,
                domainEvent,
                userId,
                exception.Message))
            .ToList();

        AddRange(outboxMessages);
    }
}