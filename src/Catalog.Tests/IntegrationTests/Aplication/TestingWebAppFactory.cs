namespace Catalog.Tests.IntegrationTests.Aplication;

public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
{
    private string ConnectionString = $"Server=localhost;Database=TestDb-{Guid.NewGuid()};User=sa;TrustServerCertificate=true;Trusted_Connection=true;PersistSecurityInfo=true;";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<CatalogContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            var options = new DbContextOptionsBuilder<CatalogContext>()
                                .UseSqlServer(ConnectionString)
                                .Options;

            services.AddSingleton(options);
            services.AddSingleton<CatalogContext>();

            var dbContext = new CatalogContext(options);            

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
          
            dbContext.Brands.AddRange(FakeData.GetFakeBrandsList());
            dbContext.Categories.AddRange(FakeData.GetFakeCategoryList());
            dbContext.Products.AddRange(FakeData.GetFakeProductsList());

            dbContext.SaveChanges();
        });
    }
}