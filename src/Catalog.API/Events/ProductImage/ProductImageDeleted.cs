using Catalog.API.Events.Abstractions;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Database;

namespace Catalog.API.Events.ProductImage;

public record ProductImageDeletedEvent(Guid Id) : IEvent<ProductImageEntity>
{
    public ProductImageEntity ToEntity() => new() { Id = Id };
}

public class ProductImageDeletedEventConsumer : BaseDeletedEventConsumer<ProductImageDeletedEvent, ProductImageEntity>
{
    public ProductImageDeletedEventConsumer(ILogger<ProductImageDeletedEventConsumer> logger, CatalogContext ctx) :
        base(logger, ctx) { }
}