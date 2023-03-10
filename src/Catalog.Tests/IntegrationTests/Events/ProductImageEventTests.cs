using Catalog.API.Events.ProductImage;
using Catalog.Domain.Entities;
using Catalog.Tests.IntegrationTests.Events.Common;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace Catalog.Tests.IntegrationTests.Events;

[Collection("Event Fixture")]
public sealed class ProductImageEventsTest :
    BaseEventTest<ProductImageCreatedEventConsumer, ProductImageUpdatedEventConsumer, ProductImageDeletedEventConsumer>,
    IClassFixture<EventFixture>
{
    private readonly Guid productId = Guid.NewGuid();

    public ProductImageEventsTest(EventFixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        var seedProduct = new ProductEntity
        {
            Id = productId, Name = "", Description = "", FullPrice = 0, Discount = 0, Quantity = 0, IsActive = false,
            Category = new() { Id = Guid.NewGuid(), Name = "", Image = "" }, Brand = new() { Id = Guid.NewGuid(), Name = "", Image = "" }
        };

        AddEntity(seedProduct);
    }

    [Fact]
    public async Task CreatedEvent_DoesntExist_CreatesProductImage()
    {
        // Act
        var productImageCreatedEvent = new ProductImageCreatedEvent(Guid.NewGuid(), productId, "url", true);
        await Publish(productImageCreatedEvent);


        // Assert
        CreatedLogger.Entries.Should().NotContain(x => x.LogLevel == LogLevel.Critical);

        var productImage = await FirstOrDefault<ProductImageEntity>(productImageCreatedEvent.Id);

        productImage.Should().NotBeNull();
        productImage.ImageUrl.Should().Be(productImageCreatedEvent.ImageUrl);
        productImage.IsMain.Should().Be(productImageCreatedEvent.IsMain);
    }

    [Fact]
    public async Task CreatedEvent_Exists_LogCritical()
    {
        // Arrange
        var alreadyExistEntity = new ProductImageEntity(Guid.NewGuid(), "url", true, productId);
        await AddEntity(alreadyExistEntity);


        // Act
        var productImageCreatedEvent = new ProductImageCreatedEvent(alreadyExistEntity.Id, productId, "new url", false);
        await Publish(productImageCreatedEvent);


        // Assert
        CreatedLogger.Entries.Should().Contain(x => x.LogLevel == LogLevel.Critical);

        var productImage = await FirstOrDefault<ProductImageEntity>(productImageCreatedEvent.Id);

        productImage.Should().NotBeNull();
        productImage.ImageUrl.Should().Be(alreadyExistEntity.ImageUrl);
        productImage.IsMain.Should().Be(alreadyExistEntity.IsMain);
    }

    [Fact]
    public async Task UpdatedEvent_UpdatesProductImage()
    {
        // Arrange
        var initialEntity = new ProductImageEntity(Guid.NewGuid(), "url", true, productId);
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
        var entity = new ProductImageEntity(Guid.NewGuid(), "url", true, productId);
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
        // Act
        var productImageDeletedEvent = new ProductImageDeletedEvent(Guid.Empty);
        await Publish(productImageDeletedEvent);


        // Assert
        DeletedLogger.Entries.Should().Contain(x => x.LogLevel == LogLevel.Critical);

        var productImage = await FirstOrDefault<ProductImageEntity>(productImageDeletedEvent.Id);

        productImage.Should().BeNull();
    }
}