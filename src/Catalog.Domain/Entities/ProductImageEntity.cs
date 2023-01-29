namespace Catalog.Domain.Entities;

public class ProductImageEntity
{
    public Guid ProductImageId { get; set; }
    public string ImageUrl { get; set; }
    public bool IsMain { get; set; }

    public Guid ProductId { get; set; }
    public ProductEntity ProductEntity { get; set; }
}