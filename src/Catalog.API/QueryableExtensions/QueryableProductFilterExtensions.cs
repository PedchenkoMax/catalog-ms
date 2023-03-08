using Catalog.API.DTO.Filters;
using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.QueryableExtensions;

public static class QueryableProductFilterExtensions
{
    public static IQueryable<ProductEntity> ApplyFilter(
        this IQueryable<ProductEntity> products,
        ProductFilter criteriaFilter)
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
            products = products.Where(p => EF.Functions.Like(p.Name, $"%{search.Query}%"));

        return products;
    }

    public static IQueryable<ProductEntity> ApplyOrder(
        this IQueryable<ProductEntity> products,
        OrderFilter ordering,
        OrderByEnum defaultOrderBy = OrderByEnum.Discount,
        bool defaultIsDesc = true)
    {
        var orderBy = ordering.OrderBy ?? defaultOrderBy;
        var isDesc = ordering.IsDesc ?? defaultIsDesc;

        return orderBy switch
        {
            OrderByEnum.Discount => isDesc
                ? products.OrderByDescending(p => p.Discount)
                : products.OrderBy(p => p.Discount),

            OrderByEnum.FullPrice => isDesc
                ? products.OrderByDescending(p => p.FullPrice)
                : products.OrderBy(p => p.FullPrice),

            OrderByEnum.Quantity => isDesc
                ? products.OrderByDescending(p => p.Quantity)
                : products.OrderBy(p => p.Quantity),
            _ => products
        };
    }
}