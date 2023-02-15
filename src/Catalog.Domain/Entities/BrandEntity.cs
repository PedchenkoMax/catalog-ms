using Catalog.Domain.Abstractions;

namespace Catalog.Domain.Entities;

public class BrandEntity : Entity
{
    public BrandEntity()
    {
    }

    public string Name { get; set; }
    public string Image { get; set; }

    public IList<ProductEntity>? Products { get; set; }
}