namespace Catalog.Domain.Entities;

public class Product
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string? Images { get; set; }
    public string Description { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; }
    
    // public Guid ProviderId  { get; set; }
    // public Provider Provider { get; set; }
}