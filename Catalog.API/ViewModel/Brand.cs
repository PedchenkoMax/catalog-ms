using Catalog.Domain.Entities;

namespace Catalog.API.ViewModel;

public record Brand
{
    public Guid BrandId { get; init; }
    public string Name { get; init; }
}

public static class BrandExtensions
{
    public static Brand ToBrand(this BrandEntity brandEntity)
    {
        return new Brand
        {
            BrandId = brandEntity.BrandId,
            Name = brandEntity.Name,
        };
    }
}