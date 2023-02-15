﻿using Catalog.API.Events.Brand;
using Catalog.Tests.Events.Common;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Catalog.Tests.Events;

[Collection("Database Fixture")]
public sealed class BrandEventsTest :
    BaseEventTest<BrandCreatedEventConsumer, BrandUpdatedEventConsumer, BrandDeletedEventConsumer>,
    IClassFixture<DatabaseFixture>
{
    public BrandEventsTest(DatabaseFixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
    }

    [Fact]
    public async Task CreatedEvent_DoesntExist_CreatesBrand()
    {
        // Arrange


        // Act
        var brandCreatedEvent = new BrandCreatedEvent(Guid.NewGuid(), "Name", "Image");
        await Publish(brandCreatedEvent);


        // Assert
        CreatedLogger.Entries.Should().NotContain(x => x.LogLevel == LogLevel.Critical);

        var brand = await FirstOrDefault<BrandEntity>(brandCreatedEvent.Id);

        brand.Should().NotBeNull();
        brand!.Id.Should().Be(brandCreatedEvent.Id);
        brand.Name.Should().Be(brandCreatedEvent.Name);
        brand.Image.Should().Be(brandCreatedEvent.Image);
    }

    [Fact]
    public async Task CreatedEvent_Exists_LogCritical()
    {
        // Arrange
        var alreadyExistEntity = new BrandEntity(id: Guid.NewGuid(), name: "Name", image: "Image");
        await AddEntity(alreadyExistEntity);


        // Act
        var brandCreatedEvent = new BrandCreatedEvent(alreadyExistEntity.Id, "Name", "Image");
        await Publish(brandCreatedEvent);


        // Assert
        CreatedLogger.Entries.Should().Contain(x => x.LogLevel == LogLevel.Critical);

        var brand = await FirstOrDefault<BrandEntity>(brandCreatedEvent.Id);

        brand.Should().NotBeNull();
        brand!.Id.Should().Be(alreadyExistEntity.Id);
        brand.Name.Should().Be(alreadyExistEntity.Name);
        brand.Image.Should().Be(alreadyExistEntity.Image);
    }

    [Fact]
    public async Task UpdatedEvent_UpdatesBrand()
    {
        // Arrange
        var initialEntity = new BrandEntity(id: Guid.NewGuid(), name: "Initial Name", image: "Initial Image");
        await AddEntity(initialEntity);


        // Act
        var brandUpdatedEvent = new BrandUpdatedEvent(initialEntity.Id, "New Name", "New Image");
        await Publish(brandUpdatedEvent);


        // Assert
        UpdatedLogger.Entries.Should().NotContain(x => x.LogLevel == LogLevel.Critical);

        var brand = await FirstOrDefault<BrandEntity>(brandUpdatedEvent.Id);

        brand.Should().NotBeNull();
        brand!.Id.Should().Be(brandUpdatedEvent.Id);
        brand.Name.Should().Be(brandUpdatedEvent.Name);
        brand.Image.Should().Be(brandUpdatedEvent.Image);
    }

    [Fact]
    public async Task DeletedEvent_Exists_DeletesBrand()
    {
        // Arrange
        var entity = new BrandEntity(id: Guid.NewGuid(), name: "Name", image: "Image");
        await AddEntity(entity);


        // Act
        var brandDeletedEvent = new BrandDeletedEvent(entity.Id);
        await Publish(brandDeletedEvent);


        // Assert
        DeletedLogger.Entries.Should().NotContain(x => x.LogLevel == LogLevel.Critical);

        var brand = await FirstOrDefault<BrandEntity>(brandDeletedEvent.Id);

        brand.Should().BeNull();
    }

    [Fact]
    public async Task DeletedEvent_DoesntExist_LogCritical()
    {
        // Arrange


        // Act
        var brandDeletedEvent = new BrandDeletedEvent(Guid.Parse("00000000-0000-0000-0000-000000000000"));
        await Publish(brandDeletedEvent);


        // Assert
        DeletedLogger.Entries.Should().Contain(x => x.LogLevel == LogLevel.Critical);

        var brand = await FirstOrDefault<BrandEntity>(brandDeletedEvent.Id);

        brand.Should().BeNull();
    }
}