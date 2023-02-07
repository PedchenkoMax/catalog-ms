namespace Catalog.Tests.FilterExtensions;
public class ApplyPaginationTests
{
    [Fact]
    public void ApplyPagination_ShouldReturnDefaultPage_WhenPaginationIsNotSet()
    {
        var items = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12}.AsQueryable();

        var result = items.ApplyPagination(new PaginationFilter(null,null));

        Assert.Equal(10, result.Count());
        Assert.Equal(1, result.First());
        Assert.Equal(10, result.Last());
    }        
   
}

