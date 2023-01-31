using System.ComponentModel.DataAnnotations;

namespace Catalog.API.DTO.Filters;

public record SearchFilter(
    [MinLength(1)] string? Query);