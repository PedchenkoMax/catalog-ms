namespace Catalog.Domain.Entities;

public class Category
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Image { get; private set; }
    public ICollection<Product> Products { get; set; }
}