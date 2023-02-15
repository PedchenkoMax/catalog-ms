using Catalog.Domain.Abstractions;

namespace Catalog.Domain.Entities;

public class ProductEntity : Entity
{
    public ProductEntity()
    {
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public IList<ProductImageEntity>? Images { get; set; }
    public decimal FullPrice { get; set; }
    public decimal Discount { get; set; }
    public int Quantity { get; set; }
    public bool IsActive { get; set; }

    public Guid CategoryId { get; set; }
    public CategoryEntity? Category { get; set; }

    public Guid BrandId { get; set; }
    public BrandEntity? Brand { get; set; }
}