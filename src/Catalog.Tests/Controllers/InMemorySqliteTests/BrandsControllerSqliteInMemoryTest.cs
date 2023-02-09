namespace Catalog.Tests.Controllers.InMemorySqliteTests;

public class BrandsControllerSqliteInMemoryTest : IDisposable
{
    private readonly DbConnection connection;
    private readonly DbContextOptions<CatalogContext> contextOptions;

    public BrandsControllerSqliteInMemoryTest()
    {
        connection = new SqliteConnection("Filename=:memory:");
        connection.Open();

        contextOptions = new DbContextOptionsBuilder<CatalogContext>()
            .UseSqlite(connection)
            .Options;

        using var context = new CatalogContext(contextOptions);

        if (context.Database.EnsureCreated())
        {
            using var viewCommand = context.Database.GetDbConnection().CreateCommand();
            viewCommand.CommandText = @"
            CREATE VIEW AllResources AS
            SELECT BrandId,Name,Image
            FROM Brand;";
            viewCommand.ExecuteNonQuery();
        }

        //context.Brands.AddRange(FakeData.GetFakeBrandsList());

        context.SaveChanges();
    }

    CatalogContext CreateContext() => new CatalogContext(contextOptions);

    public void Dispose() => connection.Dispose();

    [Fact]
    public async Task GetBrandsAsync_Returns200Ok_WhenRequestIsSuccess()
    {
        using var brandContext = CreateContext();

        var brandController = new BrandsController(brandContext);
        var actionResult = await brandController.GetBrandsAsync();
        var okResult = actionResult?.Result as OkObjectResult;

        Assert.Equal(StatusCodes.Status200OK, okResult?.StatusCode);
    }

    [Fact]
    public async Task GetBrandsAsync_ReturnActionResultOfIEnumerableOfBrand_WhenSuccess()
    {
        using var brandContext = CreateContext();

        var brandController = new BrandsController(brandContext);
        var actionResult = await brandController.GetBrandsAsync();

        Assert.IsType<ActionResult<IEnumerable<Brand>>>(actionResult);
    }
}