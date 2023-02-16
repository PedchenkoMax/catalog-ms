using Catalog.API.Events.Product;
using Catalog.Tests.IntegrationTests.Events.Common;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Catalog.Tests.IntegrationTests.Events;

[Collection("Database Fixture")]
public sealed class ProductEventsTest :
    BaseEventTest<ProductCreatedEventConsumer, ProductUpdatedEventConsumer, ProductDeletedEventConsumer>,
    IClassFixture<DatabaseFixture>
{
    public ProductEventsTest(DatabaseFixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
    }

    [Fact]
    public async Task CreatedEvent_DoesntExist_CreatesProduct()
    {
        // Act
        var productCreatedEvent = new ProductCreatedEvent(Guid.NewGuid(), "", "", 0, 0, 0, false, Fixture.SeedCategory1.Id, Fixture.SeedBrand1.Id);
        await Publish(productCreatedEvent);


        // Assert
        CreatedLogger.Entries.Should().NotContain(x => x.LogLevel == LogLevel.Critical);

        var product = await FirstOrDefault<ProductEntity>(productCreatedEvent.Id);

        product.Should().NotBeNull();
        product.Name.Should().Be(productCreatedEvent.Name);
        product.Description.Should().Be(productCreatedEvent.Description);
        product.FullPrice.Should().Be(productCreatedEvent.FullPrice);
        product.Discount.Should().Be(productCreatedEvent.Discount);
        product.Quantity.Should().Be(productCreatedEvent.Quantity);
        product.IsActive.Should().Be(productCreatedEvent.IsActive);
        product.CategoryId.Should().Be(productCreatedEvent.CategoryId);
        product.BrandId.Should().Be(productCreatedEvent.BrandId);
    }


    [Fact]
    public async Task CreatedEvent_Exists_LogCritical()
    {
        // Arrange
        var alreadyExistEntity = new ProductEntity(Guid.NewGuid(), "", "", 0, 0, 0, false, Fixture.SeedCategory1.Id, Fixture.SeedBrand1.Id);
        await AddEntity(alreadyExistEntity);


        // Act
        var productCreatedEvent = new ProductCreatedEvent(alreadyExistEntity.Id, "new", "new", 1, 1, 1, true, Fixture.SeedCategory2.Id, Fixture.SeedBrand2.Id);
        await Publish(productCreatedEvent);


        // Assert
        CreatedLogger.Entries.Should().Contain(x => x.LogLevel == LogLevel.Critical);

        var product = await FirstOrDefault<ProductEntity>(productCreatedEvent.Id);

        product.Should().NotBeNull();
        product.Name.Should().Be(alreadyExistEntity.Name);
        product.Description.Should().Be(alreadyExistEntity.Description);
        product.FullPrice.Should().Be(alreadyExistEntity.FullPrice);
        product.Discount.Should().Be(alreadyExistEntity.Discount);
        product.Quantity.Should().Be(alreadyExistEntity.Quantity);
        product.IsActive.Should().Be(alreadyExistEntity.IsActive);
        product.CategoryId.Should().Be(alreadyExistEntity.CategoryId);
        product.BrandId.Should().Be(alreadyExistEntity.BrandId);
    }

    [Fact]
    public async Task UpdatedEvent_UpdatesProduct()
    {
        // Arrange
        var initialEntity = new ProductEntity(Guid.NewGuid(), "", "", 0, 0, 0, false, Fixture.SeedCategory1.Id, Fixture.SeedBrand1.Id);
        await AddEntity(initialEntity);


        // Act
        var productUpdatedEvent = new ProductUpdatedEvent(initialEntity.Id, "new", "new", 1, 1, 1, true, Fixture.SeedCategory2.Id, Fixture.SeedBrand2.Id);
        await Publish(productUpdatedEvent);


        // Assert
        UpdatedLogger.Entries.Should().NotContain(x => x.LogLevel == LogLevel.Critical);

        var product = await FirstOrDefault<ProductEntity>(productUpdatedEvent.Id);

        product.Should().NotBeNull();
        product.Name.Should().Be(productUpdatedEvent.Name);
        product.Description.Should().Be(productUpdatedEvent.Description);
        product.FullPrice.Should().Be(productUpdatedEvent.FullPrice);
        product.Discount.Should().Be(productUpdatedEvent.Discount);
        product.Quantity.Should().Be(productUpdatedEvent.Quantity);
        product.IsActive.Should().Be(productUpdatedEvent.IsActive);
        product.CategoryId.Should().Be(productUpdatedEvent.CategoryId);
        product.BrandId.Should().Be(productUpdatedEvent.BrandId);
    }

    [Fact]
    public async Task DeletedEvent_Exists_DeletesProduct()
    {
        // Arrange
        var entity = new ProductEntity(Guid.NewGuid(), "", "", 0, 0, 0, false, Fixture.SeedCategory1.Id, Fixture.SeedBrand1.Id);
        await AddEntity(entity);


        // Act
        var productDeletedEvent = new ProductDeletedEvent(entity.Id);
        await Publish(productDeletedEvent);


        // Assert
        DeletedLogger.Entries.Should().NotContain(x => x.LogLevel == LogLevel.Critical);

        var product = await FirstOrDefault<ProductEntity>(productDeletedEvent.Id);

        product.Should().BeNull();
    }

    [Fact]
    public async Task DeletedEvent_DoesntExist_LogCritical()
    {
        // Act
        var productDeletedEvent = new ProductDeletedEvent(Guid.Empty);
        await Publish(productDeletedEvent);


        // Assert
        DeletedLogger.Entries.Should().Contain(x => x.LogLevel == LogLevel.Critical);

        var product = await FirstOrDefault<ProductEntity>(productDeletedEvent.Id);

        product.Should().BeNull();
    }
}