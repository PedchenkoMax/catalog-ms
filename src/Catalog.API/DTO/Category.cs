using System.ComponentModel;
using Catalog.Domain.Entities;

namespace Catalog.API.DTO;

public record Category
{
    [DefaultValue("5e7274ad-3132-4ad7-be36-38778a8f7b1c")]
    public Guid CategoryId { get; init; }

    [DefaultValue("Phone")]
    public string Name { get; init; }

    [DefaultValue("https://cdn-icons-png.flaticon.com/512/65/65680.png")]
    public string Image { get; init; }
    
    public IList<Product>? Products { get; set; }
}

public static class CategoryExtensions
{
    public static Category ToDTO(this CategoryEntity entity)
    {
        return new Category
        {
            CategoryId = entity.Id,
            Name = entity.Name,
            Image = entity.Image
        };
    }
}