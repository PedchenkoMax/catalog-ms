namespace Catalog.Tests.Aplication;

public class CategoryControllerSqliteInMemoryTest : IDisposable
{
    private readonly DbConnection _connection;
    private readonly DbContextOptions<CatalogContext> _contextOptions;

    public CategoryControllerSqliteInMemoryTest()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        _contextOptions = new DbContextOptionsBuilder<CatalogContext>()
            .UseSqlite(_connection)
            .Options;

        using var context = new CatalogContext(_contextOptions);

        if (context.Database.EnsureCreated())
        {
            using var viewCommand = context.Database.GetDbConnection().CreateCommand();
            viewCommand.CommandText = @"
            CREATE VIEW AllResources AS
            SELECT CategoriesId,Name,Image
            FROM Category;";
            viewCommand.ExecuteNonQuery();
        }

        context.Categories.AddRange(GetFakeCategoryList());

        context.SaveChanges();
    }

    CatalogContext CreateContext() => new CatalogContext(_contextOptions);

    public void Dispose() => _connection.Dispose();


    [Fact]
    public async Task GetCategoriesAsync_Returns200Ok_WhenRequestIsSuccess()
    {
        using var categoryContext = CreateContext();

        var categoryController = new CategoriesController(categoryContext);
        var actionResult = await categoryController.GetCategoriesAsync();
        var okResult = actionResult?.Result as OkObjectResult;

        Assert.Equal(StatusCodes.Status200OK, okResult?.StatusCode);
    }

    [Fact]
    public async Task GetCategoriesAsync_ReturnActionResultOfIEnumerableOfBrand_WhenSuccess()
    {
        using var categoryContext = CreateContext();

        var categoryController = new CategoriesController(categoryContext);
        var actionResult = await categoryController.GetCategoriesAsync();

        Assert.IsType<ActionResult<IEnumerable<Category>>>(actionResult);
    }

    [Fact]
    public async Task GetCategoriesAsync_ShouldReturnAllBrands_WhenSuccess()
    {
        using var categoryContext = CreateContext();

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
                CategoryId = new Guid("004f54c2-d7dd-4d56-b3ab-b022816477f1"),
                Name = "Phone",
                Image = "https://blob.com/CategoryPhone.png"
            },
            new()
            {
                CategoryId = new Guid("004f54c2-d7dd-4d56-b3ab-b022816477f2"),
                Name = "TV",
                Image = "https://blob.com/CategoryTv.png"
            },
            new()
            {
                CategoryId = new Guid("004f54c2-d7dd-4d56-b3ab-b022816477f3"),
                Name = "Notebook",
                Image = "https://blob.com/CategoryNotebook.png"
            }         
        };        
    }
}



