using Catalog.API.Validation.Attributes;
using Catalog.Domain.Entities;

namespace Catalog.API.ViewModel.Parameters;

public record FilteringParameters(
    [GuidId] Guid? CategoryId,
    [GuidIdCollection] List<Guid>? BrandIds,
    [PriceRange] decimal? MinPrice,
    decimal? MaxPrice);

public static partial class QueryableParametersExtensions
{
    public static IQueryable<ProductEntity> ApplyFilter(
        this IQueryable<ProductEntity> products,
        FilteringParameters filter)
    {
        if (filter.CategoryId != null)
            products = products.Where(p => p.CategoryId == filter.CategoryId);

        if (filter.BrandIds != null && filter.BrandIds.Any())
            products = products.Where(p => filter.BrandIds.Contains(p.BrandId));

        products = products.Where(p =>
            (filter.MinPrice == null || p.FullPrice >= filter.MinPrice) &&
            (filter.MaxPrice == null || p.FullPrice <= filter.MaxPrice));

        return products;
    }
}