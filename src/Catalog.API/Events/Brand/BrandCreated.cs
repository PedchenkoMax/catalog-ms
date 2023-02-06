using Catalog.API.Events.Abstractions;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Database;

namespace Catalog.API.Events.Brand;

public record BrandCreatedEvent(Guid Id, string Name, string Image) : IEvent<BrandEntity>
{
    public BrandEntity ToEntity() => new() { Id = Id, Name = Name, Image = Image };
}

public class BrandCreatedEventConsumer : BaseCreatedEventConsumer<BrandCreatedEvent, BrandEntity>
{
    public BrandCreatedEventConsumer(ILogger<BaseCreatedEventConsumer<BrandCreatedEvent, BrandEntity>> logger, CatalogContext ctx) :
        base(logger, ctx) { }
}