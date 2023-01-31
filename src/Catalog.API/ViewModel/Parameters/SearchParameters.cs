using System.ComponentModel.DataAnnotations;

namespace Catalog.API.ViewModel.Parameters;

public record SearchParameters(
    [MinLength(1)] string? Query);