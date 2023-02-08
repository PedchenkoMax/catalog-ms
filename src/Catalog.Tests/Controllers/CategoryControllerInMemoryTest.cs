namespace Catalog.Tests.Aplication;

public class CategoryControllerInMemoryTest
{
    private readonly DbContextOptions<CatalogContext> _contextOptions;    

    public CategoryControllerInMemoryTest()
    {
        _contextOptions = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase(databaseName: "in-memory-brands-database")
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

    [Fact]
    public async Task GetCategoriesAsync_ShouldReturnAllBrands_WhenSuccess()
    {
        using var categoryContext = new CatalogContext(_contextOptions);

        var categoryController = new BrandsController(categoryContext);
        var actionResult = await categoryController.GetBrandsAsync();

        var categories = Assert.IsAssignableFrom<IEnumerable<Category>>(actionResult.Value);
        Assert.Equal(3, categories.Count());
        Assert.Collection(
            categories,
            b => Assert.Equal("Phone", b.Name),
            b => Assert.Equal("TV", b.Name),
            b => Assert.Equal("Notebook", b.Name));
    }    
}


