using Catalog.Domain.Abstractions.EventBus;
using MassTransit;

namespace Catalog.Infrastructure.MessageBroker;

public class EventBus : IEventBus
{
    private readonly IPublishEndpoint publishEndpoint;

    public EventBus(IPublishEndpoint publishEndpoint)
    {
        this.publishEndpoint = publishEndpoint;
    }

    public Task PublishAsync<T>(T message)
        where T : class
    {
        return publishEndpoint.Publish(message);
    }
}