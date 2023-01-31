using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Catalog.Domain.Entities;

#pragma warning disable CS8524

namespace Catalog.API.ViewModel.Parameters;

public record OrderingParameters(
    [EnumDataType(typeof(OrderByEnum))] OrderByEnum? OrderBy,
    bool? Desc);

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OrderByEnum
{
    FullPrice,
    Discount,
    Quantity
}

public static partial class QueryableParametersExtensions
{
    public static IQueryable<ProductEntity> ApplyOrder(
        this IQueryable<ProductEntity> products,
        OrderingParameters ordering)
    {
        var orderBy = ordering.OrderBy ?? OrderByEnum.Discount;
        var desc = ordering.Desc ?? true;

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