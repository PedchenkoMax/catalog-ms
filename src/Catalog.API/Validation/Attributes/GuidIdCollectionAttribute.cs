using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Validation.Attributes;

/// <summary>
///     Returns `true` if the input value is either `null` or a collection of valid `Guid` values that are not equal to `Guid.Empty`.
///     Returns `false` if the input value is not `null` and is not a collection of valid `Guid` values.
/// </summary>
[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property)]
public class GuidIdCollectionAttribute : ValidationAttribute
{
    public GuidIdCollectionAttribute() : base("The value is not a valid guid id's collection.")
    {
    }

    public override bool IsValid(object? value)
    {
        if (value == null)
            return true;

        if (value is not ICollection<Guid> guidList)
            return false;

        return guidList.All(guid => guid != Guid.Empty);
    }
}