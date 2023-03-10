using Catalog.API.Events.Abstractions;
using Catalog.Domain.Abstractions;
using Catalog.Infrastructure.Database;
using Divergic.Logging.Xunit;
using MassTransit;
using MassTransit.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Catalog.Tests.IntegrationTests.Events.Common;

public abstract class BaseEventTest<TCreatedEventConsumer, TUpdatedEventConsumer, TDeletedEventConsumer>
    : IAsyncDisposable
    where TCreatedEventConsumer : class, IConsumer
    where TUpdatedEventConsumer : class, IConsumer
    where TDeletedEventConsumer : class, IConsumer
{
    protected readonly AutoResetEvent ResetEvent = new(false); // Look up 'TestCatalogContext' for the explanation.
    protected readonly ServiceProvider Provider;

    protected readonly ICacheLogger<TCreatedEventConsumer> CreatedLogger;
    protected readonly ICacheLogger<TUpdatedEventConsumer> UpdatedLogger;
    protected readonly ICacheLogger<TDeletedEventConsumer> DeletedLogger;

    protected BaseEventTest(EventFixture fixture, ITestOutputHelper output)
    {
        CreatedLogger = output.BuildLoggerFor<TCreatedEventConsumer>(LogLevel.Information);
        UpdatedLogger = output.BuildLoggerFor<TUpdatedEventConsumer>(LogLevel.Information);
        DeletedLogger = output.BuildLoggerFor<TDeletedEventConsumer>(LogLevel.Information);

        Provider = new ServiceCollection()
            .AddSingleton<ILogger<TCreatedEventConsumer>>(CreatedLogger)
            .AddSingleton<ILogger<TUpdatedEventConsumer>>(UpdatedLogger)
            .AddSingleton<ILogger<TDeletedEventConsumer>>(DeletedLogger)
            .AddMassTransitTestHarness(cfg =>
            {
                cfg.SetKebabCaseEndpointNameFormatter();
                cfg.AddConsumer<TCreatedEventConsumer>();
                cfg.AddConsumer<TUpdatedEventConsumer>();
                cfg.AddConsumer<TDeletedEventConsumer>();
            })
            .AddTransient<CatalogContext>(_ => new TestCatalogContext(fixture.Options, ResetEvent))
            .BuildServiceProvider(true);

        var harness = Provider.GetTestHarness();
        harness.Start();
    }

    public async ValueTask DisposeAsync()
    {
        var harness = Provider.GetTestHarness();
        await harness.Stop();

        await Provider.DisposeAsync();
    }

    protected async Task Publish<T>(T @event)
        where T : IEvent<Entity>
    {
        await Provider.GetTestHarness().Bus.Publish(@event);

        ResetEvent.WaitOne();
    }

    protected async Task AddEntity<T>(T entity)
        where T : Entity
    {
        var ctx = Provider.CreateAsyncScope().ServiceProvider.GetRequiredService<CatalogContext>();

        ctx.Set<T>().Add(entity);

        await ctx.SaveChangesAsync();
    }

    protected async Task AddEntityRange<T>(params T[] entity)
        where T : Entity
    {
        var ctx = Provider.CreateAsyncScope().ServiceProvider.GetRequiredService<CatalogContext>();

        ctx.Set<T>().AddRange(entity);

        await ctx.SaveChangesAsync();
    }

    protected async Task<T?> FirstOrDefault<T>(Guid id)
        where T : Entity
    {
        var ctx = Provider.CreateAsyncScope().ServiceProvider.GetRequiredService<CatalogContext>();

        return await ctx.Set<T>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }
}