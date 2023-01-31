using System.ComponentModel.DataAnnotations;

namespace Catalog.API.ViewModel.Filters;

public record SearchFilter(
    [MinLength(1)] string? Query);