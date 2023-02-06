using System.ComponentModel;
using Catalog.Domain.Entities;

namespace Catalog.API.DTO;

public record Product
{
    [DefaultValue("1e338b12-8aa6-438f-8832-8c7429805d59")]
    public Guid ProductId { get; init; }

    [DefaultValue("Samsung Galaxy Z Flip")]
    public string Name { get; init; }

    [DefaultValue("The Samsung Galaxy Z Flip features a 6.7-inch foldable AMOLED display, " +
                  "Snapdragon 855+ processor, and a dual camera system.")]
    public string Description { get; init; }

    public IList<ProductImage>? Images { get; init; }

    [DefaultValue(999.99)]
    public decimal FullPrice { get; init; }

    [DefaultValue(100.0)]
    public decimal Discount { get; init; }

    [DefaultValue(12)]
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
            ProductId = entity.Id,
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