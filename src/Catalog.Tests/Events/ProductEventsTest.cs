using Catalog.API.Events.Product;
using Catalog.Tests.Events.Common;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Catalog.Tests.Events;

[Collection("Database Fixture")]
public sealed class ProductEventsTest :
    BaseEventTest<ProductCreatedEventConsumer, ProductUpdatedEventConsumer, ProductDeletedEventConsumer>,
    IClassFixture<DatabaseFixture>
{
    private readonly BrandEntity seedBrand;
    private readonly BrandEntity seedBrand2;
    private readonly CategoryEntity seedCategory;
    private readonly CategoryEntity seedCategory2;

    public ProductEventsTest(ITestOutputHelper output) : base(output)
    {
        seedBrand = new BrandEntity(Guid.NewGuid(), "Name", "");
        seedBrand2 = new BrandEntity(Guid.NewGuid(), "Name", "");
        seedCategory = new CategoryEntity(Guid.NewGuid(), "Name", "");
        seedCategory2 = new CategoryEntity(Guid.NewGuid(), "Name", "");

        AddEntity(seedBrand);
        AddEntity(seedBrand2);
        AddEntity(seedCategory);
        AddEntity(seedCategory2);
    }

    [Fact]
    public async Task CreatedEvent_DoesntExist_CreatesProduct()
    {
        // Arrange


        // Act
        var productCreatedEvent = new ProductCreatedEvent(Id: Guid.NewGuid(), Name: "", Description: "", FullPrice: 0, Discount: 0, Quantity: 0,
            IsActive: false, CategoryId: seedCategory.Id, BrandId: seedBrand.Id);
        await Publish(productCreatedEvent);


        // Assert
        CreatedLogger.Entries.Should().NotContain(x => x.LogLevel == LogLevel.Critical);

        var product = await FirstOrDefault<ProductEntity>(productCreatedEvent.Id);

        product.Should().NotBeNull();
        product!.Id.Should().Be(productCreatedEvent.Id);
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
        var alreadyExistEntity = new ProductEntity
        {
            Id = Guid.NewGuid(),
            Name = "",
            Description = "",
            FullPrice = 0,
            Discount = 0,
            Quantity = 0,
            IsActive = false,
            CategoryId = seedCategory.Id,
            BrandId = seedBrand.Id,
        };
        await AddEntity(alreadyExistEntity);


        // Act
        var productCreatedEvent = new ProductCreatedEvent(Id: alreadyExistEntity.Id, Name: "new", Description: "new", FullPrice: 1, Discount: 1,
            Quantity: 1, IsActive: true, CategoryId: seedCategory2.Id, BrandId: seedBrand2.Id);
        await Publish(productCreatedEvent);


        // Assert
        CreatedLogger.Entries.Should().Contain(x => x.LogLevel == LogLevel.Critical);

        var product = await FirstOrDefault<ProductEntity>(productCreatedEvent.Id);

        product.Should().NotBeNull();
        product!.Id.Should().Be(alreadyExistEntity.Id);
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
        var initialEntity = new ProductEntity
        {
            Id = Guid.NewGuid(),
            Name = "",
            Description = "",
            FullPrice = 0,
            Discount = 0,
            Quantity = 0,
            IsActive = false,
            CategoryId = seedCategory.Id,
            BrandId = seedBrand.Id,
        };
        await AddEntity(initialEntity);


        // Act
        var productUpdatedEvent = new ProductUpdatedEvent(Id: initialEntity.Id, Name: "new", Description: "new", FullPrice: 1, Discount: 1,
            Quantity: 1, IsActive: true, CategoryId: seedCategory2.Id, BrandId: seedBrand2.Id);
        await Publish(productUpdatedEvent);


        // Assert
        UpdatedLogger.Entries.Should().NotContain(x => x.LogLevel == LogLevel.Critical);

        var product = await FirstOrDefault<ProductEntity>(productUpdatedEvent.Id);

        product.Should().NotBeNull();
        product!.Id.Should().Be(productUpdatedEvent.Id);
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
        var entity = new ProductEntity
        {
            Id = Guid.NewGuid(),
            Name = "",
            Description = "",
            FullPrice = 0,
            Discount = 0,
            Quantity = 0,
            IsActive = false,
            CategoryId = seedCategory.Id,
            BrandId = seedBrand.Id,
        };
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
        // Arrange


        // Act
        var productDeletedEvent = new ProductDeletedEvent(Guid.Parse("00000000-0000-0000-0000-000000000000"));
        await Publish(productDeletedEvent);


        // Assert
        DeletedLogger.Entries.Should().Contain(x => x.LogLevel == LogLevel.Critical);

        var product = await FirstOrDefault<ProductEntity>(productDeletedEvent.Id);

        product.Should().BeNull();
    }
}