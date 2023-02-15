using Catalog.API.Events.Abstractions;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Database;

namespace Catalog.API.Events.Category;

public record CategoryUpdatedEvent(Guid Id, string Name, string Image) : IEvent<CategoryEntity>
{
    public CategoryEntity ToEntity() => new() { Id = Id, Name = Name, Image = Image };
}

public class CategoryUpdatedEventConsumer : BaseUpdatedEventConsumer<CategoryUpdatedEvent, CategoryEntity>
{
    public CategoryUpdatedEventConsumer(ILogger<CategoryUpdatedEventConsumer> logger, CatalogContext ctx) :
        base(logger, ctx) { }
}