﻿namespace Catalog.Domain.Entities;

public class BrandEntity
{
    public Guid BrandId { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }

    public IList<ProductEntity>? Products { get; set; }
}