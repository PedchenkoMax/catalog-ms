using Catalog.Domain.Entities;
using Catalog.API.Controllers;
using Catalog.API.DTO;
using Catalog.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using Xunit;


namespace Catalog.Tests.Aplication;

public class BrandsControllerTest
{
    private readonly DbContextOptions<CatalogContext> _contextOptions;

    public BrandsControllerTest()
    {
        _contextOptions = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase(databaseName: "InMempryBrandsControllerTest")
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

    [Fact]
    public async Task GetBrandsAsync_Success()
    {
        using var context = new CatalogContext(_contextOptions);

        var brandController = new BrandsController(context);
        var actionResult = await brandController.GetBrandsAsync();

        Assert.IsType<ActionResult<IEnumerable<Brand>>>(actionResult);

        //var brands = Assert.IsAssignableFrom<IEnumerable<Brand>>(actionResult.Value);

        //Assert.Equal(3, brands.Count());

        //Assert.Collection(
        //    brands,
        //    b => Assert.Equal("Apple", b.Name),
        //    b => Assert.Equal("Dell", b.Name),
        //    b => Assert.Equal("Lenovo", b.Name));
    }


}


