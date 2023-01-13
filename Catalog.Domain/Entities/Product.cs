namespace Catalog.Domain.Entities;

public class Product
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string? Image { get; set; }
    public string Description { get; set; }

    public Guid CategoryId { get; set; }
    public Category Category { get; set; }

    public Guid BrandId { get; set; }
    public Brand Brand { get; set; }
}