using Catalog.Domain.Entities;
using Catalog.API.Controllers;
using Catalog.API.DTO;
using Catalog.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using Xunit;
using System.Reflection.Metadata;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.AspNetCore.Http;

namespace Catalog.Tests.Aplication;

public class BrandsControllerSqliteInMemoryTest : IDisposable
{
    private readonly DbConnection _connection;
    private readonly DbContextOptions<CatalogContext> _contextOptions;

    #region ConstructorAndDispose
    public BrandsControllerSqliteInMemoryTest()
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
            SELECT BrandId,Name,Image
            FROM Brand;";
            viewCommand.ExecuteNonQuery();
        }

        context.AddRange(GetFakeBrandsList());
        
        context.SaveChanges();
    }

    CatalogContext CreateContext() => new CatalogContext(_contextOptions);

    public void Dispose() => _connection.Dispose();
    #endregion


    [Fact]
    public async Task GetBrandsAsync_Returns200Ok_WhenRequestIsSuccess()
    {     
        using var brandContext = CreateContext();

        var brandController = new BrandsController(brandContext);
        var actionResult = await brandController.GetBrandsAsync();
        var okResult = actionResult.Result as OkObjectResult;        

        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }


    [Fact]
    public async Task GetBrandsAsync_ReturnActionResultOfIEnumerableOfBrand_WhenSuccess()
    {
        using var brandContext = CreateContext();

        var brandController = new BrandsController(brandContext);
        var actionResult = await brandController.GetBrandsAsync();

        Assert.IsType<ActionResult<IEnumerable<Brand>>>(actionResult);             
    }

    [Fact]
    public async Task GetBrandsAsync_ShouldReturnAllBrands_WhenSuccess()
    {
        using var brandContext = CreateContext();

        var brandController = new BrandsController(brandContext);
        var actionResult = await brandController.GetBrandsAsync();        

        var brands = Assert.IsAssignableFrom<List<Brand>>(actionResult.Value);
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
                BrandId = new Guid("002f51c2-d7dd-4d55-b3ab-b022816471f1"),
                Name = "Apple",
                Image = "https://blob.com/BrandApple.png"
            },
            new()
            {
                BrandId = new Guid("002f51c2-d7dd-4d55-b3ab-b022816471f2"),
                Name = "Dell",
                Image = "https://blob.com/BrandDell.png"
            },
            new()
            {
                BrandId = new Guid("002f51c2-d7dd-4d55-b3ab-b022816471f3"),
                Name = "Lenovo",
                Image = "https://blob.com/BrandLenovo.png"
            }
        };        
    }


}


