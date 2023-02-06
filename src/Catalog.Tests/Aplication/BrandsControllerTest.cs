using Catalog.Domain.Entities;
using Catalog.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Xunit;

namespace Catalog.Tests.Aplication;

public class BrandsControllerTest
{
    private readonly DbContextOptions<CatalogContext> _contextOptions;

    public BrandsControllerTest()
    {
        _contextOptions = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase(databaseName: "InMempryBrandControllerTest")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        using var context = new CatalogContext(_contextOptions);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        context.Brands.AddRange(
            new BrandEntity { BrandId = new Guid("002f51c2-d7dd-4d55-b3ab-b6f0f0d11111"), Name = "Apple", Image = "https://blob.com/BrandApple.png" },
            new BrandEntity { BrandId = new Guid("002f51c2-d7dd-4d55-b3ab-b6f0f0d22222"), Name = "Dell", Image = "https://blob.com/BrandDell.png" },
            new BrandEntity { BrandId = new Guid("002f51c2-d7dd-4d55-b3ab-b6f0f0d33333"), Name = "Lenovo", Image = "https://blob.com/BrandLenovo.png" });

        context.SaveChanges();
    }

}


