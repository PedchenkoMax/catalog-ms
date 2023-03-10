using Catalog.API.DTO.Filters;
using Catalog.API.QueryableExtensions;
using Catalog.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Catalog.Tests.UnitTests.QueryableExtensions;

public class ApplySearchTests
{
    private readonly  CatalogContext context;
    
    public ApplySearchTests() 
    {
        var options = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase(databaseName: $"ApplySearchTestDatabase-{Guid.NewGuid()}")
        .Options;

        context = new CatalogContext(options);
        FakeData.Seed(context);
    }

    [Fact]
    public void ApplySearch_WithValidSearchFilter_ShouldReturnFilterProductsByName() 
    {
        var search = new SearchFilter("iphone");

        var filteredProducts = context.Products.ApplySearch(search);

        Assert.NotNull(filteredProducts);
        Assert.Equal(2, filteredProducts.Count());
        Assert.True(filteredProducts.All(p => p.Name.ToLower().Contains("iphone")));
    }

    [Fact]
    public void ApplySearch_WithNullSearchFilter_ShouldReturnAllProducts() 
    {
        var search = new SearchFilter(null);

        var filteredProducts = context.Products.ApplySearch(search);

        Assert.NotNull(filteredProducts);
        Assert.Equal(FakeData.GetFakeProductsList().Count, filteredProducts.Count());
    }

    [Fact]
    public void ApplySearch_WithEmptySearchFilter_ShouldReturnAllProducts() 
    {
        var search = new SearchFilter(string.Empty);

        var filteredProducts = context.Products.ApplySearch(search);

        Assert.NotNull(filteredProducts);
        Assert.Equal(FakeData.GetFakeProductsList().Count, filteredProducts.Count());
    }
}