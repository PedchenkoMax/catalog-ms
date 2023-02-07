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
    
}


