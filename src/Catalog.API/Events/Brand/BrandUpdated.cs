using Catalog.API.Events.Abstractions;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Database;

namespace Catalog.API.Events.Brand;

public record BrandUpdatedEvent(Guid Id, string Name, string Image) : IEvent<BrandEntity>
{
    public BrandEntity ToEntity() => new() { Id = Id, Name = Name, Image = Image };
}

public class BrandBaseUpdatedEventConsumer : BaseUpdatedEventConsumer<BrandUpdatedEvent, BrandEntity>
{
    public BrandBaseUpdatedEventConsumer(ILogger<BaseUpdatedEventConsumer<BrandUpdatedEvent, BrandEntity>> logger, CatalogContext ctx) :
        base(logger, ctx) { }
}