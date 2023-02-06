using Catalog.API.Events.Abstractions;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Database;

namespace Catalog.API.Events.Category;

public record CategoryUpdatedEvent(Guid Id, string Name, string Image) : IEvent<CategoryEntity>
{
    public CategoryEntity ToEntity() => new() { Id = Id, Name = Name, Image = Image };
}

public class CategoryBaseUpdatedEventConsumer : BaseUpdatedEventConsumer<CategoryUpdatedEvent, CategoryEntity>
{
    public CategoryBaseUpdatedEventConsumer(ILogger<BaseUpdatedEventConsumer<CategoryUpdatedEvent, CategoryEntity>> logger, CatalogContext ctx) :
        base(logger, ctx) { }
}