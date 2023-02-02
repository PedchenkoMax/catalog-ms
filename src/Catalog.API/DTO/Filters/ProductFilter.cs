using Catalog.API.Validation.Attributes;

namespace Catalog.API.DTO.Filters;

public record ProductFilter(
    [GuidId] Guid? CategoryId,
    [GuidIdCollection] List<Guid>? BrandIds,
    [PriceRange] decimal? MinPrice,
    decimal? MaxPrice);