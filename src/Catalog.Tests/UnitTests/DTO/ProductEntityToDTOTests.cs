using Catalog.API.DTO;
using Catalog.Tests.Seed;
using Xunit;

namespace Catalog.Tests.UnitTests.ToDTO;

public class ProductEntityToDTOTests 
{
    [Fact]
    public void ProductEntityToDTOFirst_ReturnsCorrectDTOObject() 
    {
        var productEntity = FakeData.GetFakeProductsList().First();

        var productEntityDto = productEntity.ToDTO();

        Assert.Equal(productEntity.Id, productEntityDto.ProductId);
        Assert.Equal(productEntity.Name, productEntityDto.Name);                
        Assert.Equal(productEntity.FullPrice, productEntityDto.FullPrice);
        Assert.Equal(productEntity.Discount, productEntityDto.Discount);
        Assert.Equal(productEntity.Quantity, productEntityDto.Quantity);
        Assert.Equal(productEntity.Description, productEntityDto.Description);
    }
}