using Catalog.API.Events.Abstractions;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Database;

namespace Catalog.API.Events.Category;

public record CategoryCreatedEvent(Guid Id, string Name, string Image) : IEvent<CategoryEntity>
{
    public CategoryEntity ToEntity() => new() { Id = Id, Name = Name, Image = Image };
}

public class CategoryCreatedEventConsumer : BaseCreatedEventConsumer<CategoryCreatedEvent, CategoryEntity>
{
    public CategoryCreatedEventConsumer(ILogger<BaseCreatedEventConsumer<CategoryCreatedEvent, CategoryEntity>> logger, CatalogContext ctx) :
        base(logger, ctx) { }
}