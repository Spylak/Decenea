using Decenea.Application.Abstractions.Persistance;
using Decenea.Domain.Common;
using Decenea.Infrastructure.DataSeed;
using Decenea.Infrastructure.Outbox;
using Decenea.Infrastructure.Persistance.Converters;
using Decenea.Infrastructure.Persistance.EntityConfigurations;
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
        builder.ApplyConfigurations();
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
        {
            ProcessAuditableEntities(CreatedBy);
            return await base.SaveChangesAsync(cancellationToken);
        }

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

            await SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
            return 1;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            await AddDomainEventsAsOutboxMessages(domainEvents, userId, ex);
            Log.Error("Something went wrong while publishing events: {message} . Adding them to the outbox.",ex.Message);
            return await SaveChangesAsync(cancellationToken);
        }
    }

    private Queue<IDomainEvent> GetDomainEvents()
    {
        return new Queue<IDomainEvent>(ChangeTracker
            .Entries<AggregateRoot>()
            .Select(entry => entry.Entity)
            .SelectMany(ar => { return ar.PopDomainEvents(); }));
    }

    private async Task AddDomainEventsAsOutboxMessages(Queue<IDomainEvent> domainEvents, string userId, Exception exception)
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

        await AddRangeAsync(outboxMessages);
    }

    private void ProcessAuditableEntities(string createdBy)
    {
        var updatedEntityList = ChangeTracker.Entries()
            .Where(x => x.Entity is AuditableEntity && x.State == EntityState.Modified);

        var addedEntityList = ChangeTracker.Entries()
            .Where(x => x.Entity is AuditableEntity && x.State == EntityState.Added);

        foreach (var entity in updatedEntityList)
        {

            ((AuditableEntity)entity.Entity).LastModifiedByTimestampUtc = DateTime.UtcNow;
            ((AuditableEntity)entity.Entity).LastModifiedBy = createdBy;
        }
        foreach (var entity in addedEntityList)
        {
            ((AuditableEntity)entity.Entity).LastModifiedByTimestampUtc = DateTime.UtcNow;
            ((AuditableEntity)entity.Entity).CreatedByTimestampUtc = DateTime.UtcNow;
            ((AuditableEntity)entity.Entity).LastModifiedBy = createdBy;
            ((AuditableEntity)entity.Entity).CreatedBy = createdBy;
        }
    }
}