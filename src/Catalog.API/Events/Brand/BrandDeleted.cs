using Catalog.API.Events.Abstractions;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Database;

namespace Catalog.API.Events.Brand;

public record BrandDeletedEvent(Guid Id) : IEvent<BrandEntity>
{
    public BrandEntity ToEntity() => new() { Id = Id };
}

public class BrandDeletedEventConsumer : BaseDeletedEventConsumer<BrandDeletedEvent, BrandEntity>
{
    public BrandDeletedEventConsumer(ILogger<BrandDeletedEventConsumer> logger, CatalogContext ctx) :
        base(logger, ctx) { }
}