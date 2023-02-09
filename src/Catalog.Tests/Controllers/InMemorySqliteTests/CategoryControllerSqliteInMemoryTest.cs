namespace Catalog.Tests.Controllers.InMemorySqliteTests;

public class CategoryControllerSqliteInMemoryTest : IDisposable
{
    private readonly DbConnection connection;
    private readonly DbContextOptions<CatalogContext> contextOptions;

    public CategoryControllerSqliteInMemoryTest()
    {
        connection = new SqliteConnection("Filename=:memory:");
        connection.Open();

        contextOptions = new DbContextOptionsBuilder<CatalogContext>()
            .UseSqlite(connection)
            .Options;

        using var context = new CatalogContext(contextOptions);

        if (context.Database.EnsureCreated())
        {
            using var viewCommand = context.Database.GetDbConnection().CreateCommand();
            viewCommand.CommandText = @"
            CREATE VIEW AllResources AS
            SELECT CategoriesId,Name,Image
            FROM Category;";
            viewCommand.ExecuteNonQuery();
        }

        //context.Categories.AddRangeAsync(FakeData.GetFakeCategoryList());

        context.SaveChanges();
    }

    CatalogContext CreateContext() => new CatalogContext(contextOptions);

    public void Dispose() => connection.Dispose();

    [Fact]
    public async Task GetCategoriesAsync_Returns200Ok_WhenRequestIsSuccess()
    {
        await using var categoryContext = CreateContext();

        var categoryController = new CategoriesController(categoryContext);
        var actionResult = await categoryController.GetCategoriesAsync();
        var okResult = actionResult?.Result as OkObjectResult;

        Assert.Equal(StatusCodes.Status200OK, okResult?.StatusCode);
    }

    [Fact]
    public async Task GetCategoriesAsync_ReturnActionResultOfIEnumerableOfBrand_WhenSuccess()
    {
        await using var categoryContext = CreateContext();

        var categoryController = new CategoriesController(categoryContext);
        var actionResult = await categoryController.GetCategoriesAsync();

        Assert.IsType<ActionResult<IEnumerable<Category>>>(actionResult);
    }
}