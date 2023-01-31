namespace Catalog.API.DTO;

public record PaginatedList<TEntity>(
    int PageIndex,
    int PageSize,
    int TotalCount,
    IEnumerable<TEntity> Items);