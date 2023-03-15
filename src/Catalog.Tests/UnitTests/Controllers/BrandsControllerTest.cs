using Catalog.API.Controllers.v1;
using Catalog.Infrastructure.Database;
using Catalog.Tests.Seed;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Xunit;

namespace Catalog.Tests.UnitTests.Controllers;

public class BrandsControllerTest
{
    private readonly DbContextOptions<CatalogContext> contextOptions;

    public BrandsControllerTest()
    {
        contextOptions = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase("BrandsControllerTest")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        using var context = new CatalogContext(contextOptions);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        FakeData.Seed(context);       
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