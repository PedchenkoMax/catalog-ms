namespace Catalog.Tests.Controllers.InMemorySqliteTests;

public class ProductsControllerSqliteInMemoryTest : IDisposable
{
    private readonly DbConnection connection;
    private readonly DbContextOptions<CatalogContext> contextOptions;

    public ProductsControllerSqliteInMemoryTest()
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
            SELECT ProductId, Name, Description, FullPrice, 
            Discount, Quantity, BrandId, CategoryId 
            FROM Product;";
            viewCommand.ExecuteNonQuery();
        }

        //context.Products.AddRangeAsync(FakeData.GetFakeProductsList());

        context.SaveChanges();
    }

    CatalogContext CreateContext() => new CatalogContext(contextOptions);

    public void Dispose() => connection.Dispose();

    [Fact]
    public async Task ProductByIdAsync_WhenProductNotFound_ShouldReturn404NotFound()
    {
        await using var productContext = CreateContext();
        var productController = new ProductsController(productContext);

        var result = await productController.ProductByIdAsync(Guid.NewGuid());

        var notFoundResult = Assert.IsType<NotFoundResult>(result);
        Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
    }

    [Fact]
    public async Task ProductByIdAsync_WhenGiudIdEmpty_ShouldReturn400BadRequest()
    {
        await using var productContext = CreateContext();
        var productController = new ProductsController(productContext);

        var result = await productController.ProductByIdAsync(Guid.Empty);

        var badRequestResult = Assert.IsType<BadRequestResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
    }

    [Fact]
    public async Task ProductByIdAsync_WhenProductFound_ShouldReturn200OK()
    {
        await using var productContext = CreateContext();
        var productController = new ProductsController(productContext);

        var result = await productController.ProductByIdAsync(SeedDataConstants.Phone1);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }
}