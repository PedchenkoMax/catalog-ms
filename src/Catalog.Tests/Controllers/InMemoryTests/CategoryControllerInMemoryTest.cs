namespace Catalog.Tests.Controllers.InMemoryTests;

public class CategoryControllerInMemoryTest
{
    private readonly DbContextOptions<CatalogContext> _contextOptions;

    public CategoryControllerInMemoryTest()
    {
        _contextOptions = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase("CategoryControllerTest")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        using var context = new CatalogContext(_contextOptions);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        //context.Categories.AddRange(FakeData.GetFakeCategoryList());

        context.SaveChanges();
    }

    [Fact]
    public async Task GetCategoriesAsync_Returns200Ok_WhenRequestIsSuccess()
    {
        using var categoryContext = new CatalogContext(_contextOptions);

        var categoryController = new CategoriesController(categoryContext);
        var actionResult = await categoryController.GetCategoriesAsync();
        var okResult = actionResult?.Result as OkObjectResult;

        Assert.Equal(StatusCodes.Status200OK, okResult?.StatusCode);
    }

    [Fact]
    public async Task GetCategoriesAsync_ReturnActionResultOfIEnumerableOfBrand_WhenSuccess()
    {
        using var categoryContext = new CatalogContext(_contextOptions);

        var categoryController = new CategoriesController(categoryContext);
        var actionResult = await categoryController.GetCategoriesAsync();

        Assert.IsType<ActionResult<IEnumerable<Category>>>(actionResult);
    }
}