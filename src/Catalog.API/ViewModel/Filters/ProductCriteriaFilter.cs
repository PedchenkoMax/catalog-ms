﻿using Catalog.API.Validation.Attributes;

namespace Catalog.API.ViewModel.Filters;

public record ProductCriteriaFilter(
    [GuidId] Guid? CategoryId,
    [GuidIdCollection] List<Guid>? BrandIds,
    [PriceRange] decimal? MinPrice,
    decimal? MaxPrice);