namespace Catalog.API.ViewModel;

public record PaginatedProductsViewModel<TEntity>(int PageIndex, int PageSize, long Count, IEnumerable<TEntity> Data);