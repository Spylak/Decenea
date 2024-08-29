using System.Text.Json;
using System.Text.Json.Serialization;
using Decenea.Application.Abstractions.Persistance;
using Decenea.Common.Common;
using Decenea.Domain.Common;
using Decenea.Domain.Common.Enums;
using Decenea.Domain.Helpers;
using Decenea.Infrastructure.Outbox;
using Decenea.Infrastructure.Persistence.Converters;
using Decenea.Infrastructure.Persistence.DataSeed;
using Decenea.Infrastructure.Persistence.EntityConfigurations;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Serilog;


namespace Decenea.Infrastructure.Persistence;

internal class DeceneaDbContext : DbContext, IDeceneaDbContext
{
    public string? ModifiedBy { get; set; }
    private Queue<IDomainEvent>? DomainEvents { get; set; }

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

    public new DbSet<T> Set<T>() where T : Versioned
    {
        return base.Set<T>();
    }

    public new async Task<Result<object, Exception>> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await SaveChangesAsync(null, cancellationToken);
    }

    public async Task<Result<object, Exception>> SaveChangesAsync(string? modifiedBy = null,
        CancellationToken cancellationToken = default)
    {
        modifiedBy ??= ModifiedBy;
        if (modifiedBy is null)
            return Result<object, Exception>.Anticipated(null, ["Unable to save changes."]);

        DomainEvents ??= GetDomainEvents();

        if (DomainEvents.Count == 0)
        {
            await ProcessAuditableEntities(modifiedBy);
            try
            {
                await base.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                //Handle concurrency exception
                return Result<object, Exception>.Anticipated(null, ["Unable to save changes due to invalid data."]);
            }
        }

        return await HandleDomainEvents(DomainEvents, modifiedBy, cancellationToken);
    }

    private async Task<Result<object, Exception>> HandleDomainEvents(Queue<IDomainEvent> domainEvents, string userId,
        CancellationToken cancellationToken = default)
    {
        await using var transaction = await Database.BeginTransactionAsync(cancellationToken);
        try
        {
            while (domainEvents.TryDequeue(out var nextEvent))
            {
                await nextEvent.PublishAsync(Mode.WaitForAll, cancellationToken);
            }

            await base.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
            return Result<object, Exception>.Anticipated(null, ["Successful process."], true);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            await AddDomainEventsAsOutboxMessages(domainEvents, userId, ex);
            Log.Error("Something went wrong while publishing events: {message} . Adding them to the outbox.",
                ex.Message);
            await base.SaveChangesAsync(cancellationToken);
            return Result<object, Exception>.Excepted(null, ["Unable to Handle DomainEvents in DbContext."]);
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
            Log.Error("There was something wrong while AddDomainEventsAsOutboxMessages : {ex}", ex);
        }
    }

    private async Task ProcessAuditableEntities(string createdBy)
    {
        try
        {
            var entityEntryList = ChangeTracker.Entries()
                .Where(x => x.State is EntityState.Modified or EntityState.Added or EntityState.Deleted)
                .ToList();

            var dateTimeUtcNow = DateTime.UtcNow;

            foreach (var entityEntry in entityEntryList)
            {
                if (entityEntry.Entity is IAuditableEntity auditable)
                {
                    if (entityEntry.State == EntityState.Added)
                    {
                        auditable.CreatedBy = createdBy;
                        auditable.CreatedByTimestampUtc = dateTimeUtcNow;
                    }

                    auditable.LastModifiedBy = createdBy;
                    auditable.LastModifiedByTimestampUtc = dateTimeUtcNow;
                    
                    if (entityEntry.State == EntityState.Modified)
                    {
                        auditable.Version = RandomStringGenerator.RandomString(8);
                    }
                }

                var entityId = GetEntityKeyString(entityEntry);
                if (!string.IsNullOrWhiteSpace(entityId))
                {
                    var auditLog = new AuditLog()
                    {
                        EntityId = entityId,
                        EntityType = entityEntry.Entity.GetType().ToString(),
                        ExecutedOperation = (ExecutedOperation)entityEntry.State,
                        OperationExecutedAt = dateTimeUtcNow,
                        DataAfterExecutedOperation = JsonSerializer.Serialize(entityEntry.Entity, new JsonSerializerOptions
                        {
                            ReferenceHandler = ReferenceHandler.Preserve,
                            MaxDepth = 64
                        })
                    };

                    await AddAsync(auditLog);
                }
                else
                {
                    Log.Error("The entityId for: {entityEntry} was not found", entityEntry);
                }
                
            }
        }
        catch (Exception ex)
        {
            Log.Error("There was something wrong while ProcessAuditableEntities : {ex}", ex);
        }
    }
    
    private string GetEntityKeyString(EntityEntry entityEntry)
    {
        var keyProperties = entityEntry
            .Metadata
            .FindPrimaryKey()?
            .Properties;
        
        if (keyProperties == null || !keyProperties.Any())
            return string.Empty;

        var keyValues = new List<object>();
        foreach (var property in keyProperties)
        {
            var value = entityEntry.Property(property.Name).CurrentValue 
                        ?? entityEntry.Property(property.Name).OriginalValue;
        
            // For new entities, use the local value if available
            if (entityEntry.State == EntityState.Added)
            {
                value = entityEntry.Property(property.Name).CurrentValue;
            }
        
            keyValues.Add(value ?? "");
        }

        if (keyValues.Count == 1)
            return keyValues[0].ToString() ?? string.Empty;

        return JsonSerializer.Serialize(keyValues);
    }
}