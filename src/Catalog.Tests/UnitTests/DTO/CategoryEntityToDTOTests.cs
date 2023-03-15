using Catalog.API.DTO;
using Xunit;

namespace Catalog.Tests.UnitTests.ToDTO;

public class CategoryEntityToDTOTests 
{
    [Fact]
    public void CategoryEntityToDTOFirst_ReturnsCorrectDTOObject() 
    {
        var categoryEntity = FakeData.GetFakeCategoryList().First();

        var categoryDto = categoryEntity.ToDTO();

        Assert.Equal(categoryEntity.Id, categoryDto.CategoryId);
        Assert.Equal(categoryEntity.Name, categoryDto.Name);
        Assert.Equal(categoryEntity.Image, categoryDto.Image);
    }

    [Fact]
    public void CategoryEntityToDTOLast_ReturnsCorrectDTOObject() 
    {
        var categoryEntity = FakeData.GetFakeCategoryList().Last();

        var categoryDto = categoryEntity.ToDTO();

        Assert.Equal(categoryEntity.Id, categoryDto.CategoryId);
        Assert.Equal(categoryEntity.Name, categoryDto.Name);
        Assert.Equal(categoryEntity.Image, categoryDto.Image);
    }
}