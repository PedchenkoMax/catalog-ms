using Catalog.API.Validation.Attributes;

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
    
}


