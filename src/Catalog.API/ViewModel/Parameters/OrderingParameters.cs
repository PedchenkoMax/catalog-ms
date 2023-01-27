namespace Catalog.API.ViewModel.Parameters;

public record OrderingParameters
{
    public string OrderBy { get; init; } = "ProductId";
    public bool Desc { get; init; } = false;
}