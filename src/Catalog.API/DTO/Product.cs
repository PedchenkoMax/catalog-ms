using System.ComponentModel;
using Catalog.Domain.Entities;

namespace Catalog.API.DTO;

public record Product
{
    [DefaultValue("00000000-0000-0000-0000-000000000000")]
    public Guid ProductId { get; init; }

    [DefaultValue("Unnamed Product")]
    public string Name { get; init; }

    [DefaultValue("Not specified")]
    public string Description { get; init; }

    public IList<ProductImage>? Images { get; init; }

    [DefaultValue(0.0f)]
    public decimal FullPrice { get; init; }

    [DefaultValue(0.0f)]
    public decimal Discount { get; init; }

    [DefaultValue(0)]
    public int Quantity { get; init; }

    [DefaultValue(true)]
    public bool IsActive { get; init; }

    public Category? Category { get; init; }

    public Brand? Brand { get; init; }
}

public static class ProductExtensions
{
    public static Product ToDTO(this ProductEntity entity)
    {
        return new Product
        {
            ProductId = entity.ProductId,
            Name = entity.Name,
            Description = entity.Description,
            Images = entity.Images?.Select(e => e.ToDTO()).ToList(),
            FullPrice = entity.FullPrice,
            Discount = entity.Discount,
            Quantity = entity.Quantity,
            IsActive = entity.IsActive,
            Category = entity.Category?.ToDTO(),
            Brand = entity.Brand?.ToDTO()
        };
    }
}