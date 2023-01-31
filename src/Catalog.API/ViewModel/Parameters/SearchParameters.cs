using System.ComponentModel.DataAnnotations;
using Catalog.Domain.Entities;

namespace Catalog.API.ViewModel.Parameters;

public record SearchParameters(
    [MinLength(1)] string? Query);

public static partial class QueryableParametersExtensions
{
    public static IQueryable<ProductEntity> ApplySearch(
        this IQueryable<ProductEntity> products,
        SearchParameters search)
    {
        if (search.Query != null)
            products = products.Where(p => p.Name.Contains(search.Query, StringComparison.OrdinalIgnoreCase));

        return products;
    }
}