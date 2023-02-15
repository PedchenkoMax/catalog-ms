using Catalog.API.Events.Abstractions;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Database;

namespace Catalog.API.Events.Category;

public record CategoryDeletedEvent(Guid Id) : IEvent<CategoryEntity>
{
    public CategoryEntity ToEntity() => new() { Id = Id };
}

public class CategoryDeletedEventConsumer : BaseDeletedEventConsumer<CategoryDeletedEvent, CategoryEntity>
{
    public CategoryDeletedEventConsumer(ILogger<CategoryDeletedEventConsumer> logger, CatalogContext ctx) :
        base(logger, ctx) { }
}