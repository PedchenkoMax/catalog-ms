namespace Catalog.Tests.UnitTests.Validation;

public class GuidIdCollectionAttributeTests
{
    [Fact]
    public void IsValid_ValueIsNull_ReturnsTrue()
    {
        var attribute = new GuidIdCollectionAttribute();

        var result = attribute.IsValid(null);

        Assert.True(result);
    }

    [Fact]
    public void IsValid_ValueIsNotGuidList_ReturnsFalse()
    {
        var attribute = new GuidIdCollectionAttribute();

        var result = attribute.IsValid(new object());

        Assert.False(result);
    }

    [Fact]
    public void IsValid_ValueIsGuidList_AllGuidsAreValid_ReturnsTrue()
    {
        var attribute = new GuidIdCollectionAttribute();
        var guidList = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

        var result = attribute.IsValid(guidList);

        Assert.True(result);
    }

    [Fact]
    public void IsValid_ValueIsGuidList_ContainsInvalidGuid_ReturnsFalse()
    {
        var attribute = new GuidIdCollectionAttribute();
        var guidList = new List<Guid> { Guid.NewGuid(), Guid.Empty };

        var result = attribute.IsValid(guidList);

        Assert.False(result);
    }
}