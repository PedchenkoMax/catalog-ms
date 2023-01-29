namespace Catalog.Domain.Entities;

public class ProductEntity
{
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    public int? Quantity { get; set; }
    public bool IsActive { get; set; }
    public decimal Price { get; set; }
    public decimal Sale { get; set; }
    public IList<ProductImageEntity> Images { get; set; }
    public string? Description { get; set; }

    public Guid CategoryId { get; set; }
    public CategoryEntity CategoryEntity { get; set; }

    public Guid BrandId { get; set; }
    public BrandEntity BrandEntity { get; set; }
}