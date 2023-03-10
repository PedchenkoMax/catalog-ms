using Catalog.Infrastructure.Database;
using Catalog.Tests.UnitTests;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Catalog.Tests.IntegrationTests.Aplication;

public sealed class ApplicationFixture : IDisposable
{
    private readonly DbContextOptions<CatalogContext> options;
    public readonly TestingWebAppFactory Factory;

    public ApplicationFixture()
    {
        var connectionString = $"Server=localhost;Database=ApplicationDb-{Guid.NewGuid()};User=sa;TrustServerCertificate=true;Trusted_Connection=true;PersistSecurityInfo=true;";

        options = new DbContextOptionsBuilder<CatalogContext>().UseSqlServer(connectionString).Options;

        using var ctx = new CatalogContext(options);

        ctx.Database.EnsureDeleted();
        ctx.Database.EnsureCreated();

        ctx.Brands.AddRange(FakeData.GetFakeBrandsList());
        ctx.Categories.AddRange(FakeData.GetFakeCategoryList());
        ctx.Products.AddRange(FakeData.GetFakeProductsList());

        ctx.SaveChanges();

        Factory = new TestingWebAppFactory(options);
    }

    public void Dispose()
    {
        using var ctx = new CatalogContext(options);

        ctx.Database.EnsureDeleted();
    }
}

public class TestingWebAppFactory : WebApplicationFactory<Program>
{
    private readonly DbContextOptions<CatalogContext> options;

    public TestingWebAppFactory(DbContextOptions<CatalogContext> options)
    {
        this.options = options;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder) =>
        builder.ConfigureServices(services => services.AddSingleton<CatalogContext>(_ => new CatalogContext(options)));
}