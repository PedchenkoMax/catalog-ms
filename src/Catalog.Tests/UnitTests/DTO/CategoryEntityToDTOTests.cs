using Catalog.API.DTO;
using Xunit;

namespace Catalog.Tests.UnitTests.ToDTO;

public class CategoryEntityToDTOTests 
{
    [Fact]
    public void CategoryEntityToDTOFirst_ReturnsCorrectDTOObject() 
    {
        var categoryEntity = FakeData.GetFakeBrandsList().First();

        var brandDto = categoryEntity.ToDTO();

        Assert.Equal(categoryEntity.Id, brandDto.BrandId);
        Assert.Equal(categoryEntity.Name, brandDto.Name);
        Assert.Equal(categoryEntity.Image, brandDto.Image);
    }

    [Fact]
    public void CategoryEntityToDTOLast_ReturnsCorrectDTOObject() 
    {
        var categoryEntity = FakeData.GetFakeBrandsList().Last();

        var brandDto = categoryEntity.ToDTO();

        Assert.Equal(categoryEntity.Id, brandDto.BrandId);
        Assert.Equal(categoryEntity.Name, brandDto.Name);
        Assert.Equal(categoryEntity.Image, brandDto.Image);
    }
}