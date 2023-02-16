namespace Catalog.Tests.Events.Common;

public sealed class DatabaseFixture : IDisposable
{
    public const string ConnectionString = "Server=localhost;Database=EventTestDb;User=sa;TrustServerCertificate=true;Trusted_Connection=true;PersistSecurityInfo=true;";

    private readonly DbContextOptions<CatalogContext> options =
        new DbContextOptionsBuilder<CatalogContext>()
            .UseSqlServer(ConnectionString)
            .Options;

    public BrandEntity SeedBrand1 { get; private set; }
    public BrandEntity SeedBrand2 { get; private set; }
    public CategoryEntity SeedCategory1 { get; private set; }
    public CategoryEntity SeedCategory2 { get; private set; }
    public ProductEntity SeedProduct1 { get; private set; }
    public ProductEntity Product2 { get; private set; }

    public DatabaseFixture()
    {
        using var ctx = new CatalogContext(options);

        ctx.Database.EnsureDeleted();
        ctx.Database.EnsureCreated();

        Seed(ctx);
    }

    public void Dispose()
    {
        using var ctx = new CatalogContext(options);

        ctx.Database.EnsureDeleted();
    }

    private void Seed(CatalogContext ctx)
    {
        SeedBrand1 = new BrandEntity { Id = Guid.NewGuid(), Name = "Brand1", Image = "BrandUrl1" };
        SeedBrand2 = new BrandEntity { Id = Guid.NewGuid(), Name = "Brand2", Image = "BrandUrl2" };

        SeedCategory1 = new CategoryEntity { Id = Guid.NewGuid(), Name = "Category1", Image = "CategoryUrl1" };
        SeedCategory2 = new CategoryEntity { Id = Guid.NewGuid(), Name = "Category2", Image = "CategoryUrl2" };

        SeedProduct1 = new ProductEntity
        {
            Id = Guid.NewGuid(), CategoryId = SeedCategory1.Id, BrandId = SeedBrand1.Id,
            Name = "ProductName1", Description = "ProductDescription1", FullPrice = 0, Discount = 0, Quantity = 0, IsActive = false
        };
        Product2 = new ProductEntity
        {
            Id = Guid.NewGuid(), CategoryId = SeedCategory2.Id, BrandId = SeedBrand2.Id,
            Name = "ProductName2", Description = "ProductDescription2", FullPrice = 0, Discount = 0, Quantity = 0, IsActive = false
        };

        ctx.AddRange(SeedBrand1, SeedBrand2, SeedCategory1, SeedCategory2, SeedProduct1, Product2);
        ctx.SaveChanges();
    }
}

[CollectionDefinition("Database Fixture")]
public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
{
}