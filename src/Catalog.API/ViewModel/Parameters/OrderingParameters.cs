namespace Catalog.API.ViewModel.Parameters;

public record OrderingParameters
{
    public string? OrderBy { get; init; }
    public bool? Desc { get; init; }
}