namespace Catalog.Tests.FilterExtensions;
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
}

