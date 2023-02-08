namespace Catalog.Tests.QueryableExtensions;

public class ApplyFilterTests
{
    [Fact]
    public void ApplyFilter_ShouldFilterByCategoryIds()
    {
        var NotebookCategoryId = SeedDataConstants.CategoryNotebook;
        var products = FakeData.GetFakeProductsList().AsQueryable();
        var criteriaFilter = new ProductFilter(NotebookCategoryId, default, default, default);
        
        var result = products.ApplyFilter(criteriaFilter);
       
        Assert.Equal(6, result.Count());       
        Assert.True(result.All(p => p.CategoryId == NotebookCategoryId));       
    }   

}
