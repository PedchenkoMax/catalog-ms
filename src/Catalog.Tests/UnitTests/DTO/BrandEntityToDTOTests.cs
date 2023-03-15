using Catalog.API.DTO;
using Catalog.Tests.Seed;
using Xunit;

namespace Catalog.Tests.UnitTests.ToDTO;

public class BrandEntityToDTOTests 
{
    [Fact]
    public void BrandEntityToDTOFirst_ReturnsCorrectDTOObject() 
    {
        var brandEntity = FakeData.GetFakeBrandsList().First();

        var brandDto = brandEntity.ToDTO();

        Assert.Equal(brandEntity.Id, brandDto.BrandId);
        Assert.Equal(brandEntity.Name, brandDto.Name);
        Assert.Equal(brandEntity.Image, brandDto.Image);
    }

    [Fact]
    public void BrandEntityToDTOLast_ReturnsCorrectDTOObject() 
    {
        var brandEntity = FakeData.GetFakeBrandsList().Last();

        var brandDto = brandEntity.ToDTO();

        Assert.Equal(brandEntity.Id, brandDto.BrandId);
        Assert.Equal(brandEntity.Name, brandDto.Name);
        Assert.Equal(brandEntity.Image, brandDto.Image);
    }
}