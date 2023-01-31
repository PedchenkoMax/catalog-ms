using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

#pragma warning disable CS8524

namespace Catalog.API.ViewModel.Filters;

public record OrderFilter(
    [EnumDataType(typeof(OrderByEnum))] OrderByEnum? OrderBy,
    bool? Desc);

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OrderByEnum
{
    FullPrice,
    Discount,
    Quantity
}