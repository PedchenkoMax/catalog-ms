namespace Catalog.API.ViewModel;

public record Category
{
    public Guid CategoryId { get; init; }
    public string Name { get; init; }
}