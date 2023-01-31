using Catalog.API.DTO.Filters;

namespace Catalog.API.QueryableExtensions;

public static class QueryableGenericFilterExtensions
{
    public static IQueryable<T> ApplyPagination<T>(
        this IQueryable<T> items,
        PaginationFilter pagination,
        int defaultPageIndex = 0,
        int defaultPageSize = 10)
    {
        var pageIndex = pagination.PageIndex ?? defaultPageIndex;
        var pagePage = pagination.PageSize ?? defaultPageSize;

        items = items
            .Skip(pageIndex * pagePage)
            .Take(pagePage);

        return items;
    }
}