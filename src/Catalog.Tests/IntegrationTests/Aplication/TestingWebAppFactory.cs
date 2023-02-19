namespace Catalog.Tests.IntegrationTests.API;

public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
{
    private const string ConnectionString = "Server=localhost;Database=IntegrationTestDb;User=sa;TrustServerCertificate=true;Trusted_Connection=true;PersistSecurityInfo=true;";
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
            
            dbContext.Database.EnsureCreated();

            dbContext.SaveChanges();
        });
    }
}