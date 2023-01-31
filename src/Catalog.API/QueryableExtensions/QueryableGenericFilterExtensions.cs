using Catalog.API.ViewModel.Filters;

namespace Catalog.API.QueryableExtensions;

public static class QueryableGenericFilterExtensions
{
    public static IQueryable<T> ApplyPagination<T>(
        this IQueryable<T> items,
        PaginationFilter pagination)
    {
        var pageIndex = pagination.PageIndex ?? 0;
        var pagePage = pagination.PageSize ?? 10;

        items = items
            .Skip(pageIndex * pagePage)
            .Take(pagePage);

        return items;
    }
}