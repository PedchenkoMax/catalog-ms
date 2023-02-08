namespace Catalog.Tests.QueryableExtensions;

public class ApplyFilterTests
{
    [Fact]
    public void ApplyFilter_ShouldFilterByCategoryId()
    {
        var categoryId = SeedDataConstants.CategoryNotebook;
        var products = FakeData.GetFakeProductsList().AsQueryable();
        var criteriaFilter = new ProductFilter(categoryId, default, default, default);
        
        var result = products.ApplyFilter(criteriaFilter);
       
        Assert.Equal(6, result.Count());       
        Assert.True(result.All(p => p.CategoryId == categoryId));       
    }

    [Fact]
    public void ApplyFilter_ShouldFilterByBrandId()
    {        
        var brandId = new List<Guid>() { SeedDataConstants.BrandApple };
        var products = FakeData.GetFakeProductsList().AsQueryable();
        var criteriaFilter = new ProductFilter(default, brandId.ToList(), default, default);

        var result = products.ApplyFilter(criteriaFilter);

        Assert.Equal(6, result.Count());
        Assert.True(result.All(p => p.BrandId == brandId.First()));
    }
}
