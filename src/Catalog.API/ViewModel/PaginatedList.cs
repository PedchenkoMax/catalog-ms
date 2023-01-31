namespace Catalog.API.ViewModel;

public record PaginatedList<TEntity>(
    int PageIndex,
    int PageSize,
    int TotalCount,
    IEnumerable<TEntity> Items);