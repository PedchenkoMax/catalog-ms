using Catalog.API.Events.ProductImage;
using Catalog.Tests.Events.Common;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Catalog.Tests.Events;

[Collection("Database Fixture")]
public sealed class ProductImageEventsTest :
    BaseEventTest<ProductImageCreatedEventConsumer, ProductImageUpdatedEventConsumer, ProductImageDeletedEventConsumer>,
    IClassFixture<DatabaseFixture>
{
    private readonly ProductEntity seedProduct;

    public ProductImageEventsTest(ITestOutputHelper output) : base(output)
    {
        var seedBrand = new BrandEntity(Guid.NewGuid(), "Name", "Image");
        var seedCategory = new CategoryEntity(Guid.NewGuid(), "Name", "Image");
        seedProduct = new ProductEntity(Guid.NewGuid(), "", "", null, 0, 0, 0, false, seedCategory.Id, null, seedBrand.Id, null);

        AddEntity(seedBrand);
        AddEntity(seedCategory);
        AddEntity(seedProduct);
    }

    [Fact]
    public async Task CreatedEvent_DoesntExist_CreatesProductImage()
    {
        // Arrange


        // Act
        var productImageCreatedEvent = new ProductImageCreatedEvent(Guid.NewGuid(), seedProduct.Id, "url", true);
        await Publish(productImageCreatedEvent);


        // Assert
        CreatedLogger.Entries.Should().NotContain(x => x.LogLevel == LogLevel.Critical);

        var productImage = await FirstOrDefault<ProductImageEntity>(productImageCreatedEvent.Id);

        productImage.Should().NotBeNull();
        productImage!.Id.Should().Be(productImageCreatedEvent.Id);
        productImage.ImageUrl.Should().Be(productImageCreatedEvent.ImageUrl);
        productImage.IsMain.Should().Be(productImageCreatedEvent.IsMain);
    }

    [Fact]
    public async Task CreatedEvent_Exists_LogCritical()
    {
        // Arrange
        var alreadyExistEntity = new ProductImageEntity(Guid.NewGuid(), "url", true, seedProduct.Id, null);
        await AddEntity(alreadyExistEntity);


        // Act
        var productImageCreatedEvent = new ProductImageCreatedEvent(alreadyExistEntity.Id, seedProduct.Id, "new url", false);
        await Publish(productImageCreatedEvent);


        // Assert
        CreatedLogger.Entries.Should().Contain(x => x.LogLevel == LogLevel.Critical);

        var productImage = await FirstOrDefault<ProductImageEntity>(productImageCreatedEvent.Id);

        productImage.Should().NotBeNull();
        productImage!.Id.Should().Be(alreadyExistEntity.Id);
        productImage.ImageUrl.Should().Be(alreadyExistEntity.ImageUrl);
        productImage.IsMain.Should().Be(alreadyExistEntity.IsMain);
    }

    [Fact]
    public async Task UpdatedEvent_UpdatesProductImage()
    {
        // Arrange
        var initialEntity = new ProductImageEntity(Guid.NewGuid(), "url", true, seedProduct.Id, null);
        await AddEntity(initialEntity);


        // Act
        var productImageUpdatedEvent = new ProductImageUpdatedEvent(initialEntity.Id, initialEntity.ProductId, initialEntity.ImageUrl, false);
        await Publish(productImageUpdatedEvent);


        // Assert
        UpdatedLogger.Entries.Should().NotContain(x => x.LogLevel == LogLevel.Critical);

        var productImage = await FirstOrDefault<ProductImageEntity>(productImageUpdatedEvent.Id);

        productImage.Should().NotBeNull();
        productImage!.IsMain.Should().Be(productImageUpdatedEvent.IsMain);
    }

    [Fact]
    public async Task DeletedEvent_Exists_DeletesProductImage()
    {
        // Arrange
        var entity = new ProductImageEntity(Guid.NewGuid(), "url", true, seedProduct.Id, null);
        await AddEntity(entity);


        // Act
        var productImageDeletedEvent = new ProductImageDeletedEvent(entity.Id);
        await Publish(productImageDeletedEvent);


        // Assert
        DeletedLogger.Entries.Should().NotContain(x => x.LogLevel == LogLevel.Critical);

        var productImage = await FirstOrDefault<ProductImageEntity>(productImageDeletedEvent.Id);

        productImage.Should().BeNull();
    }

    [Fact]
    public async Task DeletedEvent_DoesntExist_LogCritical()
    {
        // Arrange


        // Act
        var productImageDeletedEvent = new ProductImageDeletedEvent(Guid.Parse("00000000-0000-0000-0000-000000000000"));
        await Publish(productImageDeletedEvent);


        // Assert
        DeletedLogger.Entries.Should().Contain(x => x.LogLevel == LogLevel.Critical);

        var productImage = await FirstOrDefault<ProductImageEntity>(productImageDeletedEvent.Id);

        productImage.Should().BeNull();
    }
}