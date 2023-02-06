using Catalog.API.Events.Abstractions;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Database;

namespace Catalog.API.Events.Product;

public record ProductCreatedEventImage(Guid Id, string ImageUrl, bool IsMain);

public record ProductCreatedEvent(
    Guid Id,
    string Name,
    string Description,
    List<ProductCreatedEventImage> Images,
    decimal FullPrice,
    decimal Discount,
    int Quantity,
    bool IsActive,
    Guid CategoryId,
    Guid BrandId) : IEvent<ProductEntity>
{
    public ProductEntity ToEntity()
    {
        return new ProductEntity
        {
            Id = Id,
            Name = Name,
            Description = Description,
            Images = Images.Select(i => new ProductImageEntity { Id = i.Id, ImageUrl = i.ImageUrl, IsMain = i.IsMain }).ToList(),
            FullPrice = FullPrice,
            Discount = Discount,
            Quantity = Quantity,
            IsActive = IsActive,
            CategoryId = CategoryId,
            BrandId = BrandId
        };
    }
}

public class ProductCreatedEventConsumer : BaseCreatedEventConsumer<ProductCreatedEvent, ProductEntity>
{
    public ProductCreatedEventConsumer(ILogger<BaseCreatedEventConsumer<ProductCreatedEvent, ProductEntity>> logger, CatalogContext ctx) :
        base(logger, ctx)
    {
    }
}