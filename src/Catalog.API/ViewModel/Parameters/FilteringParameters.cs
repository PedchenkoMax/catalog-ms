using Catalog.API.Validation.Attributes;

namespace Catalog.API.ViewModel.Parameters;

public record FilteringParameters(
    [GuidId] Guid? CategoryId,
    [GuidIdCollection] List<Guid>? BrandIds,
    [PriceRange] decimal? MinPrice,
    decimal? MaxPrice);