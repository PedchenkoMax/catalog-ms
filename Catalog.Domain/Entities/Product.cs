namespace Catalog.Domain.Entities;

public class Product
{
    public Guid ProductId { get; set; }
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Image { get; set; }
    public int StockQuantity { get; set; }
    public bool IsActive { get; set; }
    public Category Category { get; set; }
}