using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Tests.Controllers.InMemoryTests;

public class ProductsControllerInMemoryTest
{
    private readonly DbContextOptions<CatalogContext> _contextOptions;
    private readonly ProductsController _controller;

    public ProductsControllerInMemoryTest()
    {
        _contextOptions = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase("ProductsControllerTest")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        using var context = new CatalogContext(_contextOptions);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        //context.Products.AddRange(FakeData.GetFakeProductsList());

        context.SaveChanges();
    }

    [Fact]
    public async Task ProductByIdAsync_WhenProductNotFound_ShouldReturnNotFound()
    {
        using var productContext = new CatalogContext(_contextOptions);
        var productController = new ProductsController(productContext);

        var result = await productController.ProductByIdAsync(Guid.NewGuid());

        var notFoundResult = Assert.IsType<NotFoundResult>(result);
        Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
    }

    [Fact]
    public async Task ProductByIdAsync_WhenGiudIdEmpty_ShouldReturnBadRequest()
    {
        using var productContext = new CatalogContext(_contextOptions);
        var productController = new ProductsController(productContext);

        var result = await productController.ProductByIdAsync(Guid.Empty);
        
        var badRequestResult = Assert.IsType<BadRequestResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
    }

    [Fact]
    public async Task ProductByIdAsync_WhenProductFound_ShouldReturnProduct()
    {
        using var productContext = new CatalogContext(_contextOptions);
        var productController = new ProductsController(productContext);

        var result = await productController.ProductByIdAsync(SeedDataConstants.Phone1);        

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);        
    }

    [Fact]
    public async Task ProductsByParametersAsync_WhenNoProductsFound_ShouldReturnNotFound()
    {
        using var productContext = new CatalogContext(_contextOptions);
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
        using var productContext = new CatalogContext(_contextOptions);
        var productController = new ProductsController(productContext);

        var result = await productController.ProductsByParametersAsync(
            new ProductFilter(default, default, default, default),
            new SearchFilter(default),
            new OrderFilter(default, default),
            new PaginationFilter(default, default));

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }
}




