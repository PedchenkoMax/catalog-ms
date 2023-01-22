namespace Catalog.API.ViewModel.Filters;

public record OrderingFilter
{
    public string OrderBy { get; init; } = "ProductId";
    public bool Desc { get; init; } = false;
}