namespace Catalog.API.ViewModel.Parameters;

public record PaginationParameters
{
    public int PageIndex { get; init; }
    public int PageSize { get; init; }

    public PaginationParameters(int pageIndex = 0, int pageSize = 10)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;

        if (pageIndex < 0)
            throw new ArgumentException("pageIndex must be greater than or equal to 0");
        if (pageSize <= 0)
            throw new ArgumentException("pageSize must be greater than 0");
        if (pageSize >= 100)
            throw new ArgumentException("pageSize cant be greater than 100");
    }
}