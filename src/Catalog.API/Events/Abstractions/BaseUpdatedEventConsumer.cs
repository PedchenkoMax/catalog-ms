using Catalog.Domain.Abstractions;
using Catalog.Infrastructure.Database;
using MassTransit;

namespace Catalog.API.Events.Abstractions;

public abstract class BaseUpdatedEventConsumer<TEvent, TEntity> : IConsumer<TEvent>
    where TEvent : class, IEvent<TEntity>
    where TEntity : Entity
{
    private readonly CatalogContext ctx;
    private readonly ILogger<BaseUpdatedEventConsumer<TEvent, TEntity>> logger;

    protected BaseUpdatedEventConsumer(ILogger<BaseUpdatedEventConsumer<TEvent, TEntity>> logger, CatalogContext ctx)
    {
        this.logger = logger;
        this.ctx = ctx;
    }

    public async Task Consume(ConsumeContext<TEvent> context)
    {
        logger.LogInformation("Event received: {EventName}", context.Message.GetType().Name);
        logger.LogDebug("Event fields: {EventFields}", context.Message);

        var entity = context.Message.ToEntity();

        ctx.Set<TEntity>().Update(entity);

        try
        {
            await ctx.SaveChangesAsync();
            logger.LogInformation("Successfully updated");
        }
        catch (Exception e)
        {
            logger.LogCritical("Error while saving changes. Event fields: {EventFields}. Error: {error}",
                context.Message, e.Message);
        }
    }
}