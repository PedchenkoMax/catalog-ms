namespace Catalog.Tests.UnitTests.QueryableExtensions;

public class ApplySearchTests
{
    [Fact]
    public void ApplySearch_WithValidSearchFilter_ShouldFilterProducts()
    {
        var products = FakeData.GetFakeProductsList().AsQueryable();
        var query = "iphone";
        var search = new SearchFilter(query);

        var filteredProducts = products.ApplySearch(search);

        Assert.NotNull(filteredProducts);
        Assert.Equal(2, filteredProducts.Count());
        Assert.True(filteredProducts.All(p => p.Name.ToLower().Contains("iphone")));
    }

    [Fact]
    public void ApplySearch_WithNullSearchFilter_ShouldReturnAllProducts()
    {
        var products = FakeData.GetFakeProductsList().AsQueryable();
        var search = new SearchFilter(null);

        var filteredProducts = products.ApplySearch(search);

        Assert.NotNull(filteredProducts);
        Assert.Equal(28, filteredProducts.Count());
    }

    [Fact]
    public void ApplySearch_WithEmptySearchFilter_ShouldReturnAllProducts()
    {
        var products = FakeData.GetFakeProductsList().AsQueryable();
        var search = new SearchFilter(string.Empty);

        var filteredProducts = products.ApplySearch(search);

        Assert.NotNull(filteredProducts);
        Assert.Equal(28, filteredProducts.Count());
    }
}