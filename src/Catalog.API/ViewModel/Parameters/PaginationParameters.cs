using System.ComponentModel.DataAnnotations;

namespace Catalog.API.ViewModel.Parameters;

public record PaginationParameters(
    [Range(0, int.MaxValue)] int? PageIndex,
    [Range(1, 100)] int? PageSize);