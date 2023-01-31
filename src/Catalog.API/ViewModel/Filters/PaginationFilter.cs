using System.ComponentModel.DataAnnotations;

namespace Catalog.API.ViewModel.Filters;

public record PaginationFilter(
    [Range(0, int.MaxValue)] int? PageIndex,
    [Range(1, 100)] int? PageSize);