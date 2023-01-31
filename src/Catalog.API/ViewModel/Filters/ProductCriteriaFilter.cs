using Catalog.API.Validation.Attributes;
using Catalog.Domain.Entities;

namespace Catalog.API.ViewModel.Filters;

public record ProductCriteriaFilter(
    [GuidId] Guid? CategoryId,
    [GuidIdCollection] List<Guid>? BrandIds,
    [PriceRange] decimal? MinPrice,
    decimal? MaxPrice);

public static partial class QueryableFiltersExtensions
{
    public static IQueryable<ProductEntity> ApplyProductCriteria(
        this IQueryable<ProductEntity> products,
        ProductCriteriaFilter criteriaFilter)
    {
        if (criteriaFilter.CategoryId != null)
            products = products.Where(p => p.CategoryId == criteriaFilter.CategoryId);

        if (criteriaFilter.BrandIds != null && criteriaFilter.BrandIds.Any())
            products = products.Where(p => criteriaFilter.BrandIds.Contains(p.BrandId));

        products = products.Where(p => 
            (criteriaFilter.MinPrice == null || p.FullPrice >= criteriaFilter.MinPrice) &&
            (criteriaFilter.MaxPrice == null || p.FullPrice <= criteriaFilter.MaxPrice));

        return products;
    }
}