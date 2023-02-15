using Catalog.Domain.Abstractions;

namespace Catalog.Domain.Entities;

public class BrandEntity : Entity
{
    public BrandEntity(Guid id, string name, string image) : base(id)
    {
        Name = name;
        Image = image;
    }

    public BrandEntity()
    {
    }

    public string Name { get; set; }
    public string Image { get; set; }

    public IList<ProductEntity>? Products { get; set; }
}