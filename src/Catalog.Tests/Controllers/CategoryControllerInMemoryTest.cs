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

        context.Categories.AddRange(GetFakeCategoryList());

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

        var brands = Assert.IsAssignableFrom<IEnumerable<Category>>(actionResult.Value);
        Assert.Equal(3, brands.Count());
        Assert.Collection(
            brands,
            b => Assert.Equal("Apple", b.Name),
            b => Assert.Equal("Dell", b.Name),
            b => Assert.Equal("Lenovo", b.Name));
    }

    private List<CategoryEntity> GetFakeCategoryList()
    {
        return new List<CategoryEntity>()
        {
            new()
            {
                CategoryId = new Guid("004f54c2-d7dd-4d56-b3ab-b022816471f1"),
                Name = "Phone",
                Image = "https://blob.com/CategoryPhone.png"
            },
            new()
            {
                CategoryId = new Guid("004f54c2-d7dd-4d56-b3ab-b022816471f2"),
                Name = "TV",
                Image = "https://blob.com/CategoryTv.png"
            },
            new()
            {
                CategoryId = new Guid("004f54c2-d7dd-4d56-b3ab-b022816471f3"),
                Name = "Notebook",
                Image = "https://blob.com/CategoryNotebook.png"
            }         
        };        
    }
}


