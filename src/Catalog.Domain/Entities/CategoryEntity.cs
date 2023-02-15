using Catalog.Domain.Abstractions;

namespace Catalog.Domain.Entities;

public class CategoryEntity : Entity
{
    public CategoryEntity(Guid id, string name, string image, IList<ProductEntity>? products) : base(id)
    {
        Name = name;
        Image = image;
        Products = products;
    }

    public CategoryEntity()
    {
    }

    public string Name { get; set; }
    public string Image { get; set; }

    public IList<ProductEntity>? Products { get; set; }
}