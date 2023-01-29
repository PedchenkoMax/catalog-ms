using System.ComponentModel;
using Catalog.Domain.Entities;

namespace Catalog.API.ViewModel;

public record Product
{
    [DefaultValue("00000000-0000-0000-0000-000000000000")]
    public Guid ProductId { get; init; }

    [DefaultValue("Unnamed Product")]
    public string Name { get; init; }

    [DefaultValue("Not specified")]
    public string Description { get; init; }

    public IList<ProductImage> Images { get; init; }

    [DefaultValue(0.0f)]
    public decimal FullPrice { get; init; }

    [DefaultValue(0.0f)]
    public decimal Sale { get; init; }

    [DefaultValue(0)]
    public int Quantity { get; init; }

    [DefaultValue(true)]
    public bool IsActive { get; init; }

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
            Description = productEntity.Description,
            Images = productEntity.Images
                .Select(e => new ProductImage
                {
                    ImageUrl = e.ImageUrl,
                    IsMain = e.IsMain
                })
                .ToList(),
            FullPrice = productEntity.FullPrice,
            Sale = productEntity.Sale,
            Quantity = productEntity.Quantity,
            IsActive = productEntity.IsActive,
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