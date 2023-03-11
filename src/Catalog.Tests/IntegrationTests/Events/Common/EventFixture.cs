using System.Security.Cryptography;
using Catalog.Infrastructure.Database;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Catalog.Tests.IntegrationTests.Events.Common;

public sealed class EventFixture : IAsyncDisposable
{
    private readonly MsSqlTestcontainer dbContainer;
    public DbContextOptions<CatalogContext> Options;

    public EventFixture()
    {
        var dbServerConfig = new MsSqlTestcontainerConfiguration();
        dbServerConfig.Database = "EventTestDb";
        dbServerConfig.Password = "EventTestDb$$$";

        dbContainer = new TestcontainersBuilder<MsSqlTestcontainer>()
            .WithDatabase(dbServerConfig)
            .Build();

        dbContainer.StartAsync().Wait();
        
        Options = new DbContextOptionsBuilder<CatalogContext>()
            .UseSqlServer(dbContainer.ConnectionString + "Trust Server Certificate=True;")
            .Options;

        using var ctx = new CatalogContext(Options);

        ctx.Database.EnsureCreatedAsync().Wait();
    }

    public async ValueTask DisposeAsync() => await dbContainer.StopAsync();
}

[CollectionDefinition("Event Fixture")]
public class DatabaseCollection : ICollectionFixture<EventFixture>
{
}