using Catalog.API.Events.Abstractions;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Database;

namespace Catalog.API.Events.Product;

public record ProductDeletedEvent(Guid Id) : IEvent<ProductEntity>
{
    public ProductEntity ToEntity() => new() { Id = Id };
}

public class ProductDeletedEventConsumer : BaseDeletedEventConsumer<ProductDeletedEvent, ProductEntity>
{
    public ProductDeletedEventConsumer(ILogger<ProductDeletedEventConsumer> logger, CatalogContext ctx) :
        base(logger, ctx) { }
}