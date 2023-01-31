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
    Sale,
    Quantity
}

public static partial class QueryableParametersExtensions
{
    public static IQueryable<ProductEntity> ApplyOrder(
        this IQueryable<ProductEntity> products,
        OrderingParameters ordering)
    {
        var orderBy = ordering.OrderBy ?? OrderByEnum.Sale;
        var desc = ordering.Desc ?? true;

        return orderBy switch
        {
            OrderByEnum.Sale => desc
                ? products.OrderByDescending(p => p.Sale)
                : products.OrderBy(p => p.Sale),
            
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