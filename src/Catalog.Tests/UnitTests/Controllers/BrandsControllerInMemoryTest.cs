namespace Catalog.Tests.UnitTests.Controllers;

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
    public async Task GetBrandsAsync_WithData_Returns200Ok()
    {
        await using var brandContext = new CatalogContext(contextOptions);

        var brandController = new BrandsController(brandContext);
        var actionResult = await brandController.GetBrandsAsync();
        var okResult = actionResult?.Result as OkObjectResult;

        Assert.Equal(StatusCodes.Status200OK, okResult?.StatusCode);
    }

    [Fact]
    public async Task GetBrandByIdAsync_WithExistBrandId_Returns200Ok()
    {
        await using var brandContext = new CatalogContext(contextOptions);
        Guid existBrandId = FakeData.BrandApple;

        var brandController = new BrandsController(brandContext);
        var actionResult = await brandController.GetBrandByIdAsync(existBrandId);

        var okResult = Assert.IsType<OkObjectResult>(actionResult);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [Fact]
    public async Task GetBrandByIdAsync_WithNotExistBrandId_ReturnNotFound()
    {
        await using var brandContext = new CatalogContext(contextOptions);
        Guid notExistBrandId = Guid.NewGuid();

        var brandController = new BrandsController(brandContext);
        var actionResult = await brandController.GetBrandByIdAsync(notExistBrandId);

        var result = Assert.IsType<NotFoundResult>(actionResult);
        Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
    }

    [Fact]
    public async Task GetBrandByIdAsync_WithEmptyBrandId_ReturnBadRequest()
    {
        await using var brandContext = new CatalogContext(contextOptions);
        Guid emptyBrandId = Guid.Empty;

        var brandController = new BrandsController(brandContext);
        var actionResult = await brandController.GetBrandByIdAsync(emptyBrandId);

        var result = Assert.IsType<BadRequestResult>(actionResult);
        Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
    }
}