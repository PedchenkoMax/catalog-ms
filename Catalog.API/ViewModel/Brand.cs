namespace Catalog.API.ViewModel;

public record Brand
{
    public Guid BrandId { get; init; }
    public string Name { get; init; }
}