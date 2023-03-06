namespace Catalog.Tests.UnitTests.Controllers.InMemoryTests;

public class BrandsControllerInMemoryTest
{
    private readonly DbContextOptions<CatalogContext> contextOptions;

    public BrandsControllerInMemoryTest()
    {
        contextOptions = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase("BrandsControllerTest")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        using var context = new CatalogContext(contextOptions);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        context.Brands.AddRange(FakeData.GetFakeBrandsList());

        context.SaveChanges();
    }

    [Fact]
    public async Task GetBrandsAsync_Returns200Ok_WhenRequestIsSuccess()
    {
        await using var brandContext = new CatalogContext(contextOptions);

        var brandController = new BrandsController(brandContext);
        var actionResult = await brandController.GetBrandsAsync();
        var okResult = actionResult?.Result as OkObjectResult;

        Assert.Equal(StatusCodes.Status200OK, okResult?.StatusCode);
    }

    [Fact]
    public async Task GetBrandsAsync_ReturnActionResultOfIEnumerableOfBrand_WhenSuccess()
    {
        await using var brandContext = new CatalogContext(contextOptions);

        var brandController = new BrandsController(brandContext);
        var actionResult = await brandController.GetBrandsAsync();

        Assert.IsType<ActionResult<IEnumerable<Brand>>>(actionResult);
    }
}