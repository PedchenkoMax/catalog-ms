namespace Catalog.API.ViewModel;

public record Product
{
    public Guid ProductId { get; init; }
    public string Name { get; init; }
    public int? Quantity { get; init; }
    public decimal Price { get; init; }
    public string? Image { get; init; }
    public string? Description { get; init; }
    public Category Category { get; init; }
    public Brand Brand { get; init; }
}