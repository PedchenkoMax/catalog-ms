using Catalog.API.ViewModel.Filters;
using Catalog.Domain.Entities;

namespace Catalog.API.QueryableExtensions;

public static class QueryableGenericFilterExtensions
{
    public static IQueryable<ProductEntity> ApplyPagination(
        this IQueryable<ProductEntity> products,
        PaginationFilter pagination)
    {
        var pageIndex = pagination.PageIndex ?? 0;
        var pagePage = pagination.PageSize ?? 10;

        products = products
            .Skip(pageIndex * pagePage)
            .Take(pagePage);

        return products;
    }
}