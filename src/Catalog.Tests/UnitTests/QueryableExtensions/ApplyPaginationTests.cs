using Catalog.API.DTO.Filters;
using Catalog.API.QueryableExtensions;
using Xunit;

namespace Catalog.Tests.UnitTests.QueryableExtensions;

public class ApplyPaginationTests
{
    [Fact]
    public void ApplyPagination_ShouldReturnDefaultPage_WhenPaginationIsNotSet()
    {
        var items = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 }.AsQueryable();

        var result = items.ApplyPagination(new PaginationFilter(null, null));

        Assert.Equal(10, result.Count());
        Assert.Equal(1, result.First());
        Assert.Equal(10, result.Last());
    }

    [Fact]
    public void ApplyPagination_ShouldReturnCorrectPage_WhenPaginationIsSet()
    {
        var items = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 }.AsQueryable();

        var result = items.ApplyPagination(new PaginationFilter(1, 3));

        Assert.Equal(3, result.Count());
        Assert.Equal(4, result.First());
        Assert.Equal(6, result.Last());
    }
}