﻿using System.ComponentModel;
using Catalog.Domain.Entities;

namespace Catalog.API.ViewModel;

public record Brand
{
    [DefaultValue("00000000-0000-0000-0000-000000000000")]
    public Guid BrandId { get; init; }

    [DefaultValue("Unnamed Brand")]
    public string Name { get; init; }

    [DefaultValue("https://blobstorage.com/default-image.jpg")]
    public string ImageUrl { get; init; }
}

public static class BrandExtensions
{
    public static Brand ToBrand(this BrandEntity brandEntity)
    {
        return new Brand
        {
            BrandId = brandEntity.BrandId,
            Name = brandEntity.Name,
            ImageUrl = brandEntity.ImageUrl
        };
    }
}