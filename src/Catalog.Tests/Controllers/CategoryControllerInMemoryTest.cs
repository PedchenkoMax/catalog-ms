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


