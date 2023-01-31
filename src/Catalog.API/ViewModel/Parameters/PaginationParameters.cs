using System.ComponentModel.DataAnnotations;
using Catalog.Domain.Entities;

namespace Catalog.API.ViewModel.Parameters;

public record PaginationParameters(
    [Range(0, int.MaxValue)] int? PageIndex,
    [Range(1, 100)] int? PageSize);

public static partial class QueryableParametersExtensions
{
    public static IQueryable<ProductEntity> ApplyPagination(
        this IQueryable<ProductEntity> products,
        PaginationParameters pagination)
    {
        var pageIndex = pagination.PageIndex ?? 0;
        var pagePage = pagination.PageSize ?? 10;

        products = products
            .Skip(pageIndex * pagePage)
            .Take(pagePage);

        return products;
    }
}