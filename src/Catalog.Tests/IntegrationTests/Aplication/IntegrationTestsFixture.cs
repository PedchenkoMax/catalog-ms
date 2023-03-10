namespace Catalog.Tests.IntegrationTests.Aplication;
public class IntegrationTestsFixture : IAsyncLifetime 
{
    public HttpClient? ApiClient;
    public readonly MsSqlTestcontainer DbContainer;
    private readonly WebApplicationFactory<Program> appFactory;

    public IntegrationTestsFixture() 
    {
        var databaseServerContainerConfig = new MsSqlTestcontainerConfiguration();
        databaseServerContainerConfig.Database = "CatalogDB";
        databaseServerContainerConfig.Password = Convert.ToBase64String(RandomNumberGenerator.GetBytes(12));
        databaseServerContainerConfig.Environments.Add("MSSQL_PID", "Express");

        DbContainer = new TestcontainersBuilder<MsSqlTestcontainer>()
            .WithDatabase(databaseServerContainerConfig)
            .Build();

        appFactory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => {
                builder.UseEnvironment("Development");

                builder.ConfigureServices((IServiceCollection services) => {
                    services.RemoveAll<DbContextOptions<CatalogContext>>();
                    services.RemoveAll<DbConnection>();

                    var options = new DbContextOptionsBuilder<CatalogContext>()
                                   .UseSqlServer(DbContainer.ConnectionString + "Trust Server Certificate=True;")
                                   .Options;

                    services.AddSingleton(options);
                    services.AddSingleton<CatalogContext>();

                    var dbContext = new CatalogContext(options);

                    dbContext.Database.EnsureCreated();

                    FakeData.Seed(dbContext);

                });
            });
    }

    public async Task InitializeAsync() 
    {
        await DbContainer.StartAsync();
        ApiClient = appFactory.CreateClient();
    }

    public async Task DisposeAsync() 
    {
        await DbContainer.StopAsync();
    }
}