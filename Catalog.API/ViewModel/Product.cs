﻿using System.ComponentModel;
using Catalog.Domain.Entities;

namespace Catalog.API.ViewModel;

public record Product
{
    [DefaultValue("00000000-0000-0000-0000-000000000000")]
    public Guid ProductId { get; init; }

    [DefaultValue("Unnamed Product")]
    public string Name { get; init; }

    [DefaultValue(0)]
    public int? Quantity { get; init; }

    [DefaultValue(0.0f)]
    public decimal Price { get; init; }

    [DefaultValue("https://blobstorage.com/default-image.jpg")]
    public string? Image { get; init; }

    [DefaultValue("Not specified")]
    public string? Description { get; init; }

    public Category Category { get; init; }

    public Brand Brand { get; init; }
}

public static class ProductExtensions
{
    public static Product ToProduct(this ProductEntity productEntity)
    {
        return new Product
        {
            ProductId = productEntity.ProductId,
            Name = productEntity.Name,
            Quantity = productEntity.Quantity,
            Price = productEntity.Price,
            Image = productEntity.Image,
            Description = productEntity.Description,
            Category = new Category
            {
                CategoryId = productEntity.CategoryId,
                Name = productEntity.CategoryEntity.Name
            },
            Brand = new Brand
            {
                BrandId = productEntity.BrandId,
                Name = productEntity.BrandEntity.Name
            }
        };
    }
}