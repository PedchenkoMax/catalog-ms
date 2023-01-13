using Catalog.Domain.Entities;

namespace Catalog.API.ViewModel;

public record Category
{
    public Guid CategoryId { get; init; }
    public string Name { get; init; }
}

public static class CategoryExtensions
{
    public static Category ToCategory(this CategoryEntity categoryEntity)
    {
        return new Category
        {
            CategoryId = categoryEntity.CategoryId,
            Name = categoryEntity.Name,
        };
    }
}