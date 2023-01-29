namespace Catalog.Domain.Entities;

public class CategoryEntity
{
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
}