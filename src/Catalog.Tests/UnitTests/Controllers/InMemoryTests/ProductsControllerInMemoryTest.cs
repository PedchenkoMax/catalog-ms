namespace Catalog.Tests.UnitTests.Controllers.InMemoryTests;

public class ProductsControllerInMemoryTest
{
    private readonly DbContextOptions<CatalogContext> contextOptions;
    private readonly ProductsController controller;

    public ProductsControllerInMemoryTest()
    {
        contextOptions = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase("ProductsControllerTest")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        using var context = new CatalogContext(contextOptions);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        context.Brands.AddRange(FakeData.GetFakeBrandsList());
        context.Categories.AddRange(FakeData.GetFakeCategoryList());
        context.Products.AddRange(FakeData.GetFakeProductsList());

        context.SaveChanges();
    }

    [Fact]
    public async Task ProductByIdAsync_WhenProductNotFound_ShouldReturnNotFound()
    {
        await using var productContext = new CatalogContext(contextOptions);
        var productController = new ProductsController(productContext);

        var result = await productController.ProductByIdAsync(Guid.NewGuid());

        var notFoundResult = Assert.IsType<NotFoundResult>(result);
        Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
    }

    [Fact]
    public async Task ProductByIdAsync_WhenGiudIdEmpty_ShouldReturnBadRequest()
    {
        await using var productContext = new CatalogContext(contextOptions);
        var productController = new ProductsController(productContext);

        var result = await productController.ProductByIdAsync(Guid.Empty);

        var badRequestResult = Assert.IsType<BadRequestResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
    }

    [Fact]
    public async Task ProductByIdAsync_WhenProductFound_ShouldReturnProduct()
    {
        await using var productContext = new CatalogContext(contextOptions);
        var productController = new ProductsController(productContext);

        var result = await productController.ProductByIdAsync(FakeData.Phone1);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [Fact]
    public async Task ProductsByParametersAsync_WhenNoProductsFound_ShouldReturnNotFound()
    {
        await using var productContext = new CatalogContext(contextOptions);
        var productController = new ProductsController(productContext);

        var result = await productController.ProductsByParametersAsync(
            new ProductFilter(Guid.NewGuid(), default, default, default),
            new SearchFilter(default),
            new OrderFilter(default, default),
            new PaginationFilter(default, default));

        var notFoundResult = Assert.IsType<NotFoundResult>(result);
        Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
    }

    [Fact]
    public async Task ProductsByParametersAsync_WhenProductsFound_ShouldReturn200Ok()
    {
        await using var productContext = new CatalogContext(contextOptions);
        var productController = new ProductsController(productContext);

        var result = await productController.ProductsByParametersAsync(
            new ProductFilter(default, default, default, default),
            new SearchFilter(default),
            new OrderFilter(default, default),
            new PaginationFilter(0, 5));

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }
}