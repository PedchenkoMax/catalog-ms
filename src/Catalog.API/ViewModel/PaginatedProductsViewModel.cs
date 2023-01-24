namespace Catalog.API.ViewModel;

public record PaginatedProductsViewModel<TEntity>(int PageIndex, int PageSize, int totalProducts, IEnumerable<TEntity> productsOnPage);