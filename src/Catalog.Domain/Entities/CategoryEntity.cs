using Catalog.Domain.Abstractions;

namespace Catalog.Domain.Entities;

public class CategoryEntity : Entity
{
    public CategoryEntity()
    {
    }

    public string Name { get; set; }
    public string Image { get; set; }

    public IList<ProductEntity>? Products { get; set; }
}