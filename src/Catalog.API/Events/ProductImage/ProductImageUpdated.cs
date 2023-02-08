using Catalog.API.Events.Abstractions;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Database;

namespace Catalog.API.Events.ProductImage;

public record ProductImageUpdatedEvent(Guid Id, bool IsMain) : IEvent<ProductImageEntity>
{
    public ProductImageEntity ToEntity() => new() { Id = Id, IsMain = IsMain };
}

public class ProductImageBaseUpdatedEventConsumer : BaseUpdatedEventConsumer<ProductImageUpdatedEvent, ProductImageEntity>
{
    public ProductImageBaseUpdatedEventConsumer(ILogger<BaseUpdatedEventConsumer<ProductImageUpdatedEvent, ProductImageEntity>> logger, CatalogContext ctx) :
        base(logger, ctx) { }
}