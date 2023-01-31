using System.ComponentModel.DataAnnotations;
using Catalog.Domain.Entities;

namespace Catalog.API.ViewModel.Filters;

public record SearchFilter(
    [MinLength(1)] string? Query);

public static partial class QueryableFiltersExtensions
{
    public static IQueryable<ProductEntity> ApplySearch(
        this IQueryable<ProductEntity> products,
        SearchFilter search)
    {
        if (search.Query != null)
            products = products.Where(p => p.Name.Contains(search.Query, StringComparison.OrdinalIgnoreCase));

        return products;
    }
}