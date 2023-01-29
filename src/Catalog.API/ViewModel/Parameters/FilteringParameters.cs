namespace Catalog.API.ViewModel.Parameters;

public record FilteringParameters
{
    public FilteringParameters(Guid? categoryId = null, List<Guid>? brandIds = null, decimal? minPrice = null, decimal? maxPrice = null)
    {
        CategoryId = categoryId;
        BrandIds = brandIds;
        MinPrice = minPrice;
        MaxPrice = maxPrice;

        if (minPrice > maxPrice)
            throw new ArgumentException("Invalid price range: min price cannot be greater than max price.");
    }

    public Guid? CategoryId { get; init; }
    public List<Guid>? BrandIds { get; init; }
    public decimal? MinPrice { get; init; }
    public decimal? MaxPrice { get; init; }
}