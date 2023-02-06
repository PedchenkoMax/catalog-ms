using Catalog.API.Events.Abstractions;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Database;

namespace Catalog.API.Events.ProductImage;

public record ProductImageCreatedEvent(Guid Id, Guid ProductId, string ImageUrl, bool IsMain) : IEvent<ProductImageEntity>
{
    public ProductImageEntity ToEntity() => new() { Id = Id, ProductId = ProductId, ImageUrl = ImageUrl, IsMain = IsMain };
}

public class ProductImageCreatedEventConsumer : BaseCreatedEventConsumer<ProductImageCreatedEvent, ProductImageEntity>
{
    public ProductImageCreatedEventConsumer(ILogger<BaseCreatedEventConsumer<ProductImageCreatedEvent, ProductImageEntity>> logger, CatalogContext ctx) :
        base(logger, ctx) { }
}