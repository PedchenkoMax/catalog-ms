namespace Catalog.API.ViewModel.Filters;

public record SearchFilter
{
    public string Query { get; init; } = string.Empty;
}