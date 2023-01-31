using System.ComponentModel;
using Catalog.Domain.Entities;

namespace Catalog.API.ViewModel;

public record Category
{
    [DefaultValue("00000000-0000-0000-0000-000000000000")]
    public Guid CategoryId { get; init; }

    [DefaultValue("Unnamed Category")]
    public string Name { get; init; }

    [DefaultValue("https://blobstorage.com/default-image.jpg")]
    public string Image { get; init; }
}

public static class CategoryExtensions
{
    public static Category ToCategory(this CategoryEntity categoryEntity)
    {
        return new Category
        {
            CategoryId = categoryEntity.CategoryId,
            Name = categoryEntity.Name,
            Image = categoryEntity.Image
        };
    }
}