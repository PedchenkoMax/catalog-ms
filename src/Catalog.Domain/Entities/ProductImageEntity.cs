using Catalog.Domain.Abstractions;

namespace Catalog.Domain.Entities;

public class ProductImageEntity : Entity
{
    public ProductImageEntity(
        Guid id,
        string imageUrl,
        bool isMain,
        Guid productId,
        ProductEntity? productEntity) : base(id)
    {
        ImageUrl = imageUrl;
        IsMain = isMain;
        ProductId = productId;
        ProductEntity = productEntity;
    }

    public ProductImageEntity()
    {
    }

    public string ImageUrl { get; set; }
    public bool IsMain { get; set; }

    public Guid ProductId { get; set; }
    public ProductEntity? ProductEntity { get; set; }
}