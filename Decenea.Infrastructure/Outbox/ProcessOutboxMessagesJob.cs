﻿using Decenea.Application.Abstractions.Persistance;
using Decenea.Domain.Common;
using Decenea.Infrastructure.Persistence;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Quartz;
using Serilog;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Decenea.Infrastructure.Outbox;

[DisallowConcurrentExecution]
internal sealed class ProcessOutboxMessagesJob : IJob
{
    private static readonly JsonSerializerSettings JsonSerializerSettings = new ()
    {
        TypeNameHandling = TypeNameHandling.All
    };
    
    private readonly OutboxOptions _outboxOptions;
    private readonly IDeceneaDbContext _dbContext;

    public ProcessOutboxMessagesJob(IOptions<OutboxOptions> outboxOptions, IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
        _outboxOptions = outboxOptions.Value;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        Log.Information("Beginning to process outbox messages");
        
        var outboxMessages = await _dbContext
            .Set<OutboxMessage>()
            .Where(i => i.ProcessedOnUtc == null)
            .ToListAsync();
        
        await using var transaction = await _dbContext
            .Database
            .BeginTransactionAsync();

        foreach (var outboxMessageGroup in outboxMessages.GroupBy(i => i.CreatedBy))
        {
            var error = string.Empty;
            await transaction.CreateSavepointAsync("OuterLoop");
            try
            {
                foreach (var outboxMessage in outboxMessageGroup)
                {
                    var domainEvent = JsonSerializer.Deserialize<IDomainEvent>(outboxMessage.DomainEvent)!;
                    await domainEvent.PublishAsync(Mode.WaitForAll, context.CancellationToken);
                }
            }
            catch (Exception ex)
            {
                await transaction.RollbackToSavepointAsync("OuterLoop");
                Log.Error(
                    ex,
                    "Exception while batch processing outbox messages with trace Id {traceId}",
                    outboxMessageGroup.Key);
                error = ex.Message;
            }

            foreach (var outboxMessage in outboxMessageGroup)
            {
                outboxMessage.ProcessedOnUtc = DateTime.UtcNow;
                outboxMessage.Error = error;
            }
        }

        _dbContext.Set<OutboxMessage>().UpdateRange(outboxMessages);
        await transaction.CommitAsync();
        Log.Information("Completed processing outbox messages");
    }
}