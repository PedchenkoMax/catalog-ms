using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Validation.Attributes;

/// <summary>
/// Returns `true` if the input value is either `null` or a valid `Guid` that is not equal to `Guid.Empty`.
/// Returns `false` if the input value is not `null` and is not a valid `Guid`.
/// </summary>
[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property)]
public class GuidIdAttribute : ValidationAttribute
{
    public GuidIdAttribute() : base("The value is not a valid guid id.")
    {
    }

    public override bool IsValid(object? value)
    {
        if (value == null)
            return true;

        if (value is not Guid guidValue)
            return false;

        return guidValue != Guid.Empty;
    }
}