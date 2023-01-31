using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Validation.Attributes;

/// <summary>
///     Validates that 'minPrice' and 'maxPrice' is greater than zero,
///     and 'maxPrice' is also greater than 'minPrice'.
/// </summary>
public class PriceRangeAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        var minPrice = (decimal?)value;

        if (minPrice < decimal.Zero)
            return new ValidationResult("MinPrice must not be less than zero.");

        var maxPriceProperty = validationContext.ObjectType.GetProperty("MaxPrice");
        if (maxPriceProperty == null)
            return ValidationResult.Success!;

        var maxPrice = (decimal?)maxPriceProperty.GetValue(validationContext.ObjectInstance);

        if (maxPrice < decimal.Zero)
            return new ValidationResult("MaxPrice must not be less than zero.");

        if (minPrice >= maxPrice)
            return new ValidationResult("MinPrice must be lower than MaxPrice.");

        return ValidationResult.Success!;
    }
}