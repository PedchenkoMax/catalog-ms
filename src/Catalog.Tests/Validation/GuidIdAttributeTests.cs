namespace Catalog.Tests.ValidationAttribute;

public class GuidIdAttributeTests
{
    [Fact]
    public void IsValid_WithNullValue_ReturnsTrue()
    {
        var attribute = new GuidIdAttribute();

        var result = attribute.IsValid(null);

        Assert.True(result);
    }

    [Fact]
    public void IsValid_WithInvalidType_ReturnsFalse()
    {
        var attribute = new GuidIdAttribute();

        var result = attribute.IsValid("not a guid");

        Assert.False(result);
    }

    [Fact]
    public void IsValid_WithEmptyGuid_ReturnsFalse()
    {
        var attribute = new GuidIdAttribute();

        var result = attribute.IsValid(Guid.Empty);

        Assert.False(result);
    }

    [Fact]
    public void IsValid_WithValidGuid_ReturnsTrue()
    {
        var attribute = new GuidIdAttribute();
        var validGuid = Guid.NewGuid();

        var result = attribute.IsValid(validGuid);

        Assert.True(result);
    }
}