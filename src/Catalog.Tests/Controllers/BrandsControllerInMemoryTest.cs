namespace Catalog.Tests.Aplication;

public class BrandsControllerInMemoryTest
{
    private readonly DbContextOptions<CatalogContext> _contextOptions;    

    public BrandsControllerInMemoryTest()
    {
        _contextOptions = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase(databaseName: "in-memory-brands-database")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;        

        using var context = new CatalogContext(_contextOptions);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        //context.Brands.AddRange(GetFakeBrandsList());

        context.SaveChanges();
    }

    [Fact]
    public async Task GetBrandsAsync_Returns200Ok_WhenRequestIsSuccess()
    {
        using var brandContext = new CatalogContext(_contextOptions);

        var brandController = new BrandsController(brandContext);
        var actionResult = await brandController.GetBrandsAsync();
        var okResult = actionResult.Result as OkObjectResult;

        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [Fact]
    public async Task GetBrandsAsync_ReturnActionResultOfIEnumerableOfBrand_WhenSuccess()
    {
        using var brandContext = new CatalogContext(_contextOptions);

        var brandController = new BrandsController(brandContext);
        var actionResult = await brandController.GetBrandsAsync();

        Assert.IsType<ActionResult<IEnumerable<Brand>>>(actionResult);
    }

    [Fact]
    public async Task GetBrandsAsync_ShouldReturnAllBrands_WhenSuccess()
    {
        using var brandContext = new CatalogContext(_contextOptions);

        var brandController = new BrandsController(brandContext);
        var actionResult = await brandController.GetBrandsAsync();

        var brands = Assert.IsAssignableFrom<IEnumerable<Brand>>(actionResult.Value);
        Assert.Equal(3, brands.Count());
        Assert.Collection(
            brands,
            b => Assert.Equal("Apple", b.Name),
            b => Assert.Equal("Dell", b.Name),
            b => Assert.Equal("Lenovo", b.Name));
    }

    private List<BrandEntity> GetFakeBrandsList()
    {
        return new List<BrandEntity>()
        {
            new()
            {
                BrandId = new Guid("002f54c2-d7dd-4d55-b3ab-b022816471f1"),
                Name = "Apple",
                Image = "https://blob.com/BrandApple.png"
            },
            new()
            {
                BrandId = new Guid("002f54c2-d7dd-4d55-b3ab-b022816471f2"),
                Name = "Dell",
                Image = "https://blob.com/BrandDell.png"
            },
            new()
            {
                BrandId = new Guid("002f54c2-d7dd-4d55-b3ab-b022816471f3"),
                Name = "Lenovo",
                Image = "https://blob.com/BrandLenovo.png"
            }
        };        
    }
}


