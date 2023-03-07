namespace Catalog.Tests.UnitTests.Controllers;

public class CategoryControllerInMemoryTest
{
    private readonly DbContextOptions<CatalogContext> contextOptions;

    public CategoryControllerInMemoryTest()
    {
        contextOptions = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase("CategoryControllerTest")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        using var context = new CatalogContext(contextOptions);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        context.Categories.AddRange(FakeData.GetFakeCategoryList());

        context.SaveChanges();
    }

    [Fact]
    public async Task GetCategoriesAsync_WithData_Returns200Ok()
    {
        await using var categoryContext = new CatalogContext(contextOptions);

        var categoryController = new CategoriesController(categoryContext);
        var actionResult = await categoryController.GetCategoriesAsync();
        var okResult = actionResult?.Result as OkObjectResult;

        Assert.Equal(StatusCodes.Status200OK, okResult?.StatusCode);
    }

    [Fact]
    public async Task GetCategoryByIdAsync_WithExistCategoryId_Returns200Ok()
    {
        await using var categoryContext = new CatalogContext(contextOptions);
        Guid existCategoryId = FakeData.CategoryNotebook;

        var categoryController = new CategoriesController(categoryContext);
        var actionResult = await categoryController.GetCategoryByIdAsync(existCategoryId);

        var okResult = Assert.IsType<OkObjectResult>(actionResult);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [Fact]
    public async Task GetCategoryByIdAsync_WithNotExistCategoryId_ReturnNotFound()
    {
        await using var categoryContext = new CatalogContext(contextOptions);
        Guid notExistCategoryId = Guid.NewGuid();

        var categoryController = new CategoriesController(categoryContext);
        var actionResult = await categoryController.GetCategoryByIdAsync(notExistCategoryId);

        var result = Assert.IsType<NotFoundResult>(actionResult);
        Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
    }

    [Fact]
    public async Task GetCategoryByIdAsync_WithEmptyCategoryId_ReturnBadRequest()
    {
        await using var categoryContext = new CatalogContext(contextOptions);
        Guid emptyCategoryId = Guid.Empty;

        var categoryController = new CategoriesController(categoryContext);
        var actionResult = await categoryController.GetCategoryByIdAsync(emptyCategoryId);

        var result = Assert.IsType<BadRequestResult>(actionResult);
        Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
    }
}