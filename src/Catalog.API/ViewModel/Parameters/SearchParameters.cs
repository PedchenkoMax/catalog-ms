namespace Catalog.API.ViewModel.Parameters;

public record SearchParameters
{
    public string Query { get; init; } = string.Empty;
}