using Catalog.Domain.Abstractions;
using Catalog.Infrastructure.Database;
using MassTransit;

namespace Catalog.API.Events.Abstractions;

public abstract class BaseDeletedEventConsumer<TEvent, TEntity> : IConsumer<TEvent>
    where TEvent : class, IEvent<TEntity>
    where TEntity : Entity
{
    private readonly CatalogContext ctx;
    private readonly ILogger<BaseDeletedEventConsumer<TEvent, TEntity>> logger;

    protected BaseDeletedEventConsumer(ILogger<BaseDeletedEventConsumer<TEvent, TEntity>> logger, CatalogContext ctx)
    {
        this.logger = logger;
        this.ctx = ctx;
    }

    public async Task Consume(ConsumeContext<TEvent> context)
    {
        logger.LogInformation("Event received: {EventName}", context.Message.GetType().Name);
        logger.LogDebug("Event fields: {EventFields}", context.Message);

        var entity = context.Message.ToEntity();

        ctx.Set<TEntity>().Remove(entity);

        try
        {
            await ctx.SaveChangesAsync();
            logger.LogInformation("Successfully deleted");
        }
        catch (Exception e)
        {
            logger.LogCritical("Error while saving changes. Event fields: {EventFields}. Error: {error}",
                context.Message, e.Message);
        }
    }
}