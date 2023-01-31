using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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