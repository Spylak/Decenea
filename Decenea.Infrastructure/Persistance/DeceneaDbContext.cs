using System.Text.Json;
using Decenea.Application.Abstractions.Persistance;
using Decenea.Common.Common;
using Decenea.Domain.Common;
using Decenea.Domain.Common.Enums;
using Decenea.Domain.Helpers;
using Decenea.Infrastructure.DataSeed;
using Decenea.Infrastructure.Outbox;
using Decenea.Infrastructure.Persistance.Converters;
using Decenea.Infrastructure.Persistance.EntityConfigurations;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Serilog;


namespace Decenea.Infrastructure.Persistance;

internal class DeceneaDbContext : DbContext, IDeceneaDbContext
{
    public string? CreatedBy { get; set; }
    public Queue<IDomainEvent>? DomainEvents { get; set; }

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
        ApplicationUserSeed.Seed(builder);
        builder.ApplyConfigurations();
    }

    public new DbSet<T> Set<T>() where T : Entity
    {
        return base.Set<T>();
    }

    public new async Task<Result<object, Exception>> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await SaveChangesAsync(null, cancellationToken);
    }

    public async Task<Result<object,Exception>> SaveChangesAsync(string? createdBy = null, CancellationToken cancellationToken = default)
    {
        CreatedBy ??= createdBy;
        if (CreatedBy is null)
            return Result<object,Exception>.Anticipated(null,"Unable to save changes.");

        DomainEvents ??= GetDomainEvents();

        if (DomainEvents.Count == 0)
        {
            await ProcessAuditableEntities(CreatedBy);
            try
            {
                await base.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                //Handle concurrency exception
                return Result<object,Exception>.Anticipated(null,"Unable to save changes due to invalid data.");
            }
        }

        return await HandleDomainEvents(DomainEvents, CreatedBy, cancellationToken);
    }

    private async Task<Result<object,Exception>> HandleDomainEvents(Queue<IDomainEvent> domainEvents, string userId,
        CancellationToken cancellationToken = default)
    {
        await using var transaction = await Database.BeginTransactionAsync(cancellationToken);
        try
        {
            while (domainEvents.TryDequeue(out var nextEvent))
            {
                await nextEvent.PublishAsync(Mode.WaitForAll,cancellationToken);
            }

            await base.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
            return Result<object,Exception>.Anticipated(null,"Successful process.", true);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            await AddDomainEventsAsOutboxMessages(domainEvents, userId, ex);
            Log.Error("Something went wrong while publishing events: {message} . Adding them to the outbox.",
                ex.Message);
            await base.SaveChangesAsync(cancellationToken);
            return Result<object,Exception>.Excepted(null,"Unable to Handle DomainEvents in DbContext.");
        }
    }

    private Queue<IDomainEvent> GetDomainEvents()
    {
        return new Queue<IDomainEvent>(ChangeTracker
            .Entries<AggregateRoot>()
            .Select(entry => entry.Entity)
            .SelectMany(ar => { return ar.PopDomainEvents(); }));
    }

    private async Task AddDomainEventsAsOutboxMessages(Queue<IDomainEvent> domainEvents, string userId,
        Exception exception)
    {
        try
        {
            var dateTime = DateTime.UtcNow;
            var outboxMessages = domainEvents
                .Select(domainEvent => new OutboxMessage()
                {
                    Id = Ulid.NewUlid().ToString()!,
                    OccurredOnUtc = dateTime,
                    Type = domainEvent.GetType().Name,
                    DomainEvent = JsonSerializer.Serialize(domainEvent),
                    CreatedBy = userId,
                    Error = exception.Message
                })
                .ToList();

            await AddRangeAsync(outboxMessages);
        }
        catch (Exception ex)
        {
            Log.Error("There was something wrong while AddDomainEventsAsOutboxMessages : {ex}",ex);
        }
    }

    private async Task ProcessAuditableEntities(string createdBy)
    {
        try
        {
            var entityList = ChangeTracker.Entries()
                .Where(x => x.Entity is AuditableEntity
                            || x.State == EntityState.Modified
                            || x.State == EntityState.Added
                            || x.State == EntityState.Deleted);

            var dateTimeUtcNow = DateTime.UtcNow;

            foreach (var entity in entityList)
            {
                if (entity.State == EntityState.Added)
                {
                    ((AuditableEntity)entity.Entity).CreatedBy = createdBy;
                    ((AuditableEntity)entity.Entity).CreatedByTimestampUtc = dateTimeUtcNow;
                }

                if (entity.State == EntityState.Added || entity.State == EntityState.Modified)
                {
                    ((Entity)entity.Entity).Version = RandomStringGenerator.RandomString(8);
                }

                ((AuditableEntity)entity.Entity).LastModifiedBy = createdBy;
                ((AuditableEntity)entity.Entity).LastModifiedByTimestampUtc = dateTimeUtcNow;

                var auditLog = new AuditLog()
                {
                    EntityId = (string)entity.OriginalValues["Id"]!,
                    EntityType = entity.Entity.GetType().ToString(),
                    ExecutedOperation = (ExecutedOperation)entity.State,
                    OperationExecutedAt = dateTimeUtcNow,
                    DataAfterExecutedOperation = JsonSerializer.Serialize(entity.Entity)
                };

                await AddAsync(auditLog);
            }
        }
        catch (Exception ex)
        {
            Log.Error("There was something wrong while ProcessAuditableEntities : {ex}",ex);
        }
    }
}