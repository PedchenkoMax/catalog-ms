using Catalog.Domain.Abstractions;
using Catalog.Infrastructure.Database;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Events.Abstractions;

public abstract class BaseUpdatedEventConsumer<TEvent, TEntity> : IConsumer<TEvent>
    where TEvent : class, IEvent<TEntity>
    where TEntity : Entity
{
    private readonly ILogger<BaseUpdatedEventConsumer<TEvent, TEntity>> logger;
    private readonly CatalogContext ctx;
    private readonly DbSet<TEntity> dbSet;

    protected BaseUpdatedEventConsumer(ILogger<BaseUpdatedEventConsumer<TEvent, TEntity>> logger, CatalogContext ctx)
    {
        this.logger = logger;
        this.ctx = ctx;
        dbSet = ctx.Set<TEntity>();
    }

    public async Task Consume(ConsumeContext<TEvent> context)
    {
        logger.LogDebug($"Received event of type {typeof(TEvent).Name} with fields: {context.Message}");

        var entity = context.Message.ToEntity();

        var isEntityExists = dbSet.Any(x => x.Id == entity.Id);

        if (isEntityExists)
        {
            dbSet.Update(entity);

            var res = await ctx.SaveChangesAsync();

            logger.LogDebug($"Changes saved successfully: {res > 0}");
        }
        else
        {
            logger.LogDebug($"Doesn't exist");
        }
    }
}