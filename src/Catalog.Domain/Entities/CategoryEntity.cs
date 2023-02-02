namespace Catalog.Domain.Entities;

public class CategoryEntity
{
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }

    public IList<ProductEntity>? Products { get; set; }
}