using Catalog.API.Controllers.v1;
using Catalog.API.DTO.Filters;
using Catalog.Infrastructure.Database;
using Catalog.Tests.Seed;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Xunit;

namespace Catalog.Tests.UnitTests.Controllers;

public class ProductsControllerTest
{
    private readonly DbContextOptions<CatalogContext> contextOptions;

    public ProductsControllerTest()
    {
        contextOptions = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase("ProductsControllerTest")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        using var context = new CatalogContext(contextOptions);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        FakeData.Seed(context);
    }

    [Fact]
    public async Task ProductByIdAsync_WithNotExistProductId_ReturnNotFound()
    {
        await using var productContext = new CatalogContext(contextOptions);
        var productController = new ProductsController(productContext);
        Guid notExistProductId = Guid.NewGuid();

        var result = await productController.ProductByIdAsync(notExistProductId);

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