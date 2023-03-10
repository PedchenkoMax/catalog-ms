using Catalog.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Catalog.Tests.IntegrationTests.Events.Common;

public sealed class DatabaseFixture : IDisposable
{
    public readonly DbContextOptions<CatalogContext> Options;

    public DatabaseFixture()
    {
        var connectionString = $"Server=localhost;Database=EventTestDb-{Guid.NewGuid()};User=sa;TrustServerCertificate=true;Trusted_Connection=true;PersistSecurityInfo=true;";

        Options = new DbContextOptionsBuilder<CatalogContext>()
            .UseSqlServer(connectionString)
            .Options;

        using var ctx = new CatalogContext(Options);

        ctx.Database.EnsureDeleted();
        ctx.Database.EnsureCreated();
    }

    public void Dispose()
    {
        using var ctx = new CatalogContext(Options);

        ctx.Database.EnsureDeleted();
    }
}

[CollectionDefinition("Database Fixture")]
public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
{
}