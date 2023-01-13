using Catalog.Domain.Entities;

namespace Catalog.API.ViewModel;

public record Product
{
    public Guid ProductId { get; init; }
    public string Name { get; init; }
    public int? Quantity { get; init; }
    public decimal Price { get; init; }
    public string? Image { get; init; }
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