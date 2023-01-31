using Catalog.API.ViewModel.Filters;
using Catalog.Domain.Entities;

namespace Catalog.API.QueryableExtensions;

public static class QueryableProductFilterExtensions
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

    public static IQueryable<ProductEntity> ApplySearch(
        this IQueryable<ProductEntity> products,
        SearchFilter search)
    {
        if (search.Query != null)
            products = products.Where(p => p.Name.Contains(search.Query, StringComparison.OrdinalIgnoreCase));

        return products;
    }

    public static IQueryable<ProductEntity> ApplyOrder(
        this IQueryable<ProductEntity> products,
        OrderFilter ordering,
        OrderByEnum defaultOrderBy = OrderByEnum.Discount,
        bool defaultDesc = true)
    {
        var orderBy = ordering.OrderBy ?? defaultOrderBy;
        var desc = ordering.Desc ?? defaultDesc;

        return orderBy switch
        {
            OrderByEnum.Discount => desc
                ? products.OrderByDescending(p => p.Discount)
                : products.OrderBy(p => p.Discount),

            OrderByEnum.FullPrice => desc
                ? products.OrderByDescending(p => p.FullPrice)
                : products.OrderBy(p => p.FullPrice),

            OrderByEnum.Quantity => desc
                ? products.OrderByDescending(p => p.Quantity)
                : products.OrderBy(p => p.Quantity),
            _ => products
        };
    }
}