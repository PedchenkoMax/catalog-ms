using Catalog.API.Events.Abstractions;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Database;

namespace Catalog.API.Events.Brand;

public record BrandUpdatedEvent(Guid Id, string Name, string Image) : IEvent<BrandEntity>
{
    public BrandEntity ToEntity() => new() { Id = Id, Name = Name, Image = Image };
}

public class BrandUpdatedEventConsumer : BaseUpdatedEventConsumer<BrandUpdatedEvent, BrandEntity>
{
    public BrandUpdatedEventConsumer(ILogger<BrandUpdatedEventConsumer> logger, CatalogContext ctx) :
        base(logger, ctx) { }
}