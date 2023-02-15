namespace Catalog.Tests.Events.Common;

public sealed class DatabaseFixture : IDisposable
{
    public const string ConnectionString = "Server=localhost;Database=EventTestDb;User=sa;TrustServerCertificate=true;Trusted_Connection=true;PersistSecurityInfo=true;";

    private readonly DbContextOptions<CatalogContext> options =
        new DbContextOptionsBuilder<CatalogContext>()
            .UseSqlServer(ConnectionString)
            .Options;

    public DatabaseFixture()
    {
        using var ctx = new CatalogContext(options);

        ctx.Database.EnsureDeleted();
        ctx.Database.EnsureCreated();
    }

    public void Dispose()
    {
        using var ctx = new CatalogContext(options);

        ctx.Database.EnsureDeleted();
    }
}

[CollectionDefinition("Database Fixture")]
public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
{
}