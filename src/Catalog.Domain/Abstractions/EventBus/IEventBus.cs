namespace Catalog.Domain.Abstractions.EventBus;

public interface IEventBus
{
    Task PublishAsync<T>(T message) where T : class;
}